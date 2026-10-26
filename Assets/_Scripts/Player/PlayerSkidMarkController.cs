using UnityEngine;

public class PlayerSkidMarkController : MonoBehaviour
{
    // Components
    CarController carController;
    TrailRenderer trailRenderer;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();

        trailRenderer = GetComponent<TrailRenderer>();


    }

    private void Start()
    {
        trailRenderer.emitting = false;
    }

    private void Update()
    {
        // It the car tires are screeching then we'll emitt a trail
        if(carController.IsTireScreeching(out float lateralVelocity))
        {
            trailRenderer.emitting = true;
        }
        else
        {
            trailRenderer.emitting = false;
        }
    }
}
