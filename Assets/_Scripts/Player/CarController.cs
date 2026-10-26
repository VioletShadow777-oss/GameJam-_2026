using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Car Settings")]
    public float driftFactor = 0.95f;
    public float skidMarkEnableVelocity = 4f;
    public float accelerationFactor = 30f;
    public float maxVelocity = 40f;
    public float turnFactor = 3.5f;

   
    
    [HideInInspector] public float steeringInput = 0;

    float rotationAngle = 0;
    float steeringMultiplier;
    // Components
    Rigidbody carRigidbody;

    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
    }

    

    private void FixedUpdate()
    {
        ApplyEngineForce();

        ApplySteering();
        //FixRigidbodyRotation();
        KillOrthogonalVelocity();
    }

    #region Physics Fixes
    /// <summary>
    /// Zeros out unwanted rotation on the X and Z axes after physics calculations 
    /// to prevent jittering due to collisions.
    /// </summary>
    private void FixRigidbodyRotation()
    {
        // 1. Clamp Angular Velocity (Stops the physical spin/roll)
        // Set the rotational velocity on X and Z axes to zero.
        Vector3 angularVelocity = carRigidbody.angularVelocity;
        angularVelocity.x = 0;
        angularVelocity.z = 0;
        carRigidbody.angularVelocity = angularVelocity;

        // 2. Reset the Orientation (Stops pitch/roll due to collision trauma)
        // Get the current Y rotation from the Rigidbody
        float currentYRotation = carRigidbody.rotation.eulerAngles.y;

        // This calculates a Quaternion with the current Y rotation, but zero X and Z rotation.
        Quaternion leveledRotation = Quaternion.Euler(0, currentYRotation, 0);

        // We use MoveRotation to apply this level rotation in the physics step.
        carRigidbody.MoveRotation(leveledRotation);
    }
    #endregion

    #region Input System
    // Handles Input
    public void SteeringInput(InputAction.CallbackContext context)
    {
        steeringInput = context.ReadValue<float>();
    }
    #endregion

    #region Accleration
    // Moves the car Forward
    private void ApplyEngineForce()
    {
        Vector3 engineFactiorVector = transform.forward * accelerationFactor;

        // Applying force to the car to move forward
        carRigidbody.AddForce(engineFactiorVector * Time.fixedDeltaTime, ForceMode.VelocityChange);

        // Clamping the Velocity of the car
        carRigidbody.linearVelocity = Vector3.ClampMagnitude(carRigidbody.linearVelocity, maxVelocity);
    }
    #endregion

    #region Steering
    // Steers the car
    private void ApplySteering()
    {
        if(steeringInput < 0)
        {
            steeringMultiplier = Mathf.MoveTowards(steeringMultiplier, -1, 5 *  Time.fixedDeltaTime);
        }
        else if(steeringInput > 0)
        {
            steeringMultiplier = Mathf.MoveTowards(steeringMultiplier, 1, 5 *  Time.fixedDeltaTime);
        }
        else if(steeringInput == 0)
        {
            steeringMultiplier = Mathf.MoveTowards(steeringMultiplier, 0, 5 *  Time.fixedDeltaTime);
        }

            // Updates the Rotation based on Input
            rotationAngle -= steeringMultiplier * turnFactor * Time.fixedDeltaTime;

        carRigidbody.MoveRotation(Quaternion.Euler(0, rotationAngle, 0));
    }
    #endregion

    #region Drift
    // Manages the Drift
    public void KillOrthogonalVelocity()
    {
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(carRigidbody.linearVelocity, transform.forward);
        Vector3 rightVelocity = transform.right * Vector3.Dot(carRigidbody.linearVelocity, transform.right);

        carRigidbody.linearVelocity = forwardVelocity + rightVelocity * driftFactor;
    }
    #endregion

    #region Skid Mark Emission Determiner
    float GetLateralVelocity()
    {
        // Retuns How fast the car is moving sideways
        return Vector3.Dot(transform.right, carRigidbody.linearVelocity);
    }

    // Gives the lateralVelocity so that "PlayerSkidMark" Script can determine 
    // wheater that script should emmit trails or not
    public bool IsTireScreeching(out float lateralVelocity)
    {
        lateralVelocity = GetLateralVelocity();

        if(Mathf.Abs(GetLateralVelocity()) > skidMarkEnableVelocity)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Car Velocity
    public float GetVelocityMagnitude()
    {
        float velocity = carRigidbody.linearVelocity.magnitude ;
        return velocity;
    }
    #endregion

    
}
