using UnityEngine;

public class PlayerWheelParticalHandler : MonoBehaviour
{
    
    /// <summary>
    /// Particales are not visualizing but component shows they are 
    /// So I am disabling the parctical effect objects and will work on it later
    /// </summary>

    float particalEmissionRate;

    // Components
    CarController carController;

    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();

        particleSystem = GetComponent<ParticleSystem>();

        particleSystemEmissionModule = particleSystem.emission;
    }

    private void Start()
    {
        particleSystemEmissionModule.rateOverTime = 0;
    }

    private void Update()
    {
        // reduces the particales over the time
        particalEmissionRate = Mathf.Lerp(particalEmissionRate, 0, Time.deltaTime * 5);


        if(carController.IsTireScreeching(out float lateralVelocity))
        {
            particalEmissionRate = 50;
        }
        
        particleSystemEmissionModule.rateOverTime = particalEmissionRate;    
    }
}
