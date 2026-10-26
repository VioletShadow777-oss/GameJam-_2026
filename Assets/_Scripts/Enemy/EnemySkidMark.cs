using UnityEngine;

public class EnemySkidMark : MonoBehaviour
{
    // Components
    PoliceAICar policeAICar;
    TrailRenderer trailRenderer;

    private void Awake()
    {
        policeAICar = GetComponentInParent<PoliceAICar>();

        trailRenderer = GetComponent<TrailRenderer>();


    }
    

    private void Start()
    {
        trailRenderer.emitting = false;
    }

    private void Update()
    {
        // It the car tires are screeching then we'll emitt a trail
        if (policeAICar.IsTireScreeching(out float lateralVelocity))
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}
