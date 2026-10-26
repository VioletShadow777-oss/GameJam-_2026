using UnityEngine;

public class EnemyWheelParticalHandler : MonoBehaviour
{
    float particalEmissionRate;

    // Components
    PoliceAICar policeAICar;

    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule particleSystemEmissionModule;

    private void Awake()
    {
        policeAICar = GetComponentInParent<PoliceAICar>();

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


        if (policeAICar.IsTireScreeching(out float lateralVelocity))
        {
            particalEmissionRate = 30;
        }

        particleSystemEmissionModule.rateOverTime = particalEmissionRate;
    }
}
