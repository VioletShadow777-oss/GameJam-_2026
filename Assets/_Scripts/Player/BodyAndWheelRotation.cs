using UnityEngine;

public class BodyAndWheelRotation : MonoBehaviour
{
    // Components
    public CarController carController;
    [Space]
    public Transform carBody;
    public Transform parentObject;
    public Transform[] frontWheels;

    // Local Variables
    float bodyZrotaion;
    float wheelsYrotaion;
    

    private void Start()
    {
        bodyZrotaion = 0;
        wheelsYrotaion = 0;
    }
    private void LateUpdate()
    {
        
        carBody.rotation = Quaternion.Euler(0, transform.parent.eulerAngles.y, bodyZrotaion);

        for (int i = 0; i < frontWheels.Length; i++)
        {
            frontWheels[i].localRotation = Quaternion.Euler(0, wheelsYrotaion, 0);
        }

        RotationSmooth();
    }

    // Manages the Wheels and body rotation smoothing
    private void RotationSmooth()
    {
        if(carController.steeringInput == 1)
        {
            bodyZrotaion = Mathf.Lerp(bodyZrotaion, -15, 10 * Time.deltaTime);
            wheelsYrotaion = Mathf.MoveTowards(wheelsYrotaion, -30, 100 *  Time.deltaTime);
        }

        else if (carController.steeringInput == -1)
        {
            bodyZrotaion = Mathf.Lerp(bodyZrotaion, 15, 10 * Time.deltaTime);
            wheelsYrotaion = Mathf.MoveTowards(wheelsYrotaion, 30, 100 * Time.deltaTime);
        }

        else if (carController.steeringInput == 0)
        {
            bodyZrotaion = Mathf.Lerp(bodyZrotaion, 0, 10 * Time.deltaTime);
            wheelsYrotaion = Mathf.MoveTowards(wheelsYrotaion, 0, 100 * Time.deltaTime);
        }

    }

}
