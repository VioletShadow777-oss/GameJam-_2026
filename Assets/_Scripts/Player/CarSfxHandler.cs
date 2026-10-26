using UnityEngine;

public class CarSfxHandler : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource tireScreechAudioSource;
    public AudioSource engineAudioSource;
    public AudioSource carHitAudioSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;
    // Components 
    CarController carController;

    private void Awake()
    {
        carController = GetComponentInParent<CarController>();
    }

    private void Update()
    {
        UpdateEngineSFX();
        TireScreechingSFX();
    }

    private void UpdateEngineSFX()
    {
        // Getting the Velocity from the CarController
        float velocityMagnitude = carController.GetVelocityMagnitude();

        // Increase the engine volume as the car gose faster
        float desiredEngineVolume = velocityMagnitude * 0.1f;

        // But keep a minimum level so it playes even the car is idle
        desiredEngineVolume = Mathf.Clamp(desiredEngineVolume, 0.2f, 1f);

        engineAudioSource.volume = Mathf.Lerp(engineAudioSource.volume, desiredEngineVolume, Time.deltaTime * 10);

        // to add more variation to the engine sound we also change the pitch
        desiredEnginePitch = velocityMagnitude * 0.05f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineAudioSource.pitch = Mathf.Lerp(engineAudioSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);

    }

    private void TireScreechingSFX()
    {
        if(carController.IsTireScreeching(out float lateralVelocity))
        {
            tireScreechAudioSource.volume = Mathf.Lerp(tireScreechAudioSource.volume, 1f, Time.deltaTime * 10);
            tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
        }
        else
        {
            tireScreechAudioSource.volume = Mathf.Lerp(tireScreechAudioSource.volume, 0f, Time.deltaTime * 10);

        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Storing the velocity (How fast the car is moving)
        float relativeVelocity = collision.relativeVelocity.magnitude;

        // Adjusting the volume According to the cars Velocity ( The faster the car gose the higher the volume)
        float volume = relativeVelocity * 0.1f;
        carHitAudioSource.volume = volume;

        // Randomizing the pitch to give a variation in sound
        carHitAudioSource.pitch = Random.Range(0.95f, 1.05f);

        // Playing the audio if it is not playing currently
        if (!carHitAudioSource.isPlaying)
        {
            // Playing the hit sound with the volume
            carHitAudioSource.PlayOneShot(carHitAudioSource.clip, volume);
        }
        
    }


}
