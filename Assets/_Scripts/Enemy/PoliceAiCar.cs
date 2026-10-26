using System.Collections;
using System.Reflection;
using Unity.Burst;
using UnityEngine;


public class PoliceAICar : MonoBehaviour
{
    [SerializeField] private PoliceAiCarScriptableObject policeAiCarScriptable;

    // Components
    Rigidbody carRigidbody;
    Transform target;


    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag(policeAiCarScriptable.targetObject).GetComponent<Transform>();
    }

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ApplyEngineForce();
        Steering();
        
        KillOrthogonalVelocity();
    }


    #region Accleration
    // Moves the car Forward
    private void ApplyEngineForce()
    {
        Vector3 engineFactiorVector = transform.forward * policeAiCarScriptable.accelerationFactor;

       
        // Applying force to the car to move forward
        carRigidbody.AddForce(engineFactiorVector * Time.fixedDeltaTime, ForceMode.VelocityChange);

        // Clamping the Velocity of the car
        carRigidbody.linearVelocity = Vector3.ClampMagnitude(carRigidbody.linearVelocity, 
                                                            policeAiCarScriptable.maxVelocity);
    }
    #endregion

    #region Steering
    // Steers the car
    private void Steering()
    {
        // Calculate the direction vector from this object to the target
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToTarget = Vector3.SignedAngle(transform.forward, directionToTarget, Vector3.up);

        // --- NEW: Apply Steering Threshold ---
        float desiredSteering = 0f;

        // Check if the angle difference is larger than the threshold
        if (Mathf.Abs(angleToTarget) > policeAiCarScriptable.turnThreshold)
        {
            // 1. Calculate Desired Steering (Error Scaling)
            // Use the angle, scaled by sensitivity, as the raw desired steering input
            // This ensures steering input is proportional to the error, up to the max (1 or -1).
            desiredSteering = Mathf.Clamp(angleToTarget * policeAiCarScriptable.turnFactor, -1f, 1f);
        }
        // If the angle is within the threshold, desiredSteering remains 0, 
        // effectively locking the steering wheel straight.

        // 2. Apply Damping (Smooth Transition)
        // Lerp between the current steering and the desired steering (which may be 0)
        policeAiCarScriptable.currentSteeringInput = Mathf.Lerp(policeAiCarScriptable.currentSteeringInput, 
                                          desiredSteering, 
                                          Time.fixedDeltaTime * policeAiCarScriptable.steeringDamping);

        

        // 3. Update the rotation angle using the DAMPED steering value
        policeAiCarScriptable.rotationAngle += policeAiCarScriptable.currentSteeringInput * policeAiCarScriptable.turnFactor;

        // 4. Apply the rotation to the Rigidbody
        carRigidbody.MoveRotation(Quaternion.Euler(0, policeAiCarScriptable.rotationAngle, 0));

    }



    #endregion



    #region Drift
    // Manages the Drift
    public void KillOrthogonalVelocity()
    {
        Vector3 forwardVelocity = transform.forward * Vector3.Dot(carRigidbody.linearVelocity, transform.forward);
        Vector3 rightVelocity = transform.right * Vector3.Dot(carRigidbody.linearVelocity, transform.right);

        carRigidbody.linearVelocity = forwardVelocity + rightVelocity * policeAiCarScriptable.driftFactor;
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

        if (Mathf.Abs(GetLateralVelocity()) > policeAiCarScriptable.skidMarkEnableVelocity)
        {
            return true;
        }
        return false;
    }
    #endregion

    #region Car Velocity
    public float GetVelocityMagnitude()
    {
        float velocity = carRigidbody.linearVelocity.magnitude;
        return velocity;
    }
    #endregion
}