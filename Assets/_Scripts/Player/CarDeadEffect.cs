using UnityEngine;
using Lean.Pool;

public class CarDeadEffect : MonoBehaviour
{
    [Header("Script References")]
    public PlayerHealth playerHealth;
    public CameraShake cameraShake;

    [Header("Explosion Effect")]
    public Transform explosionPrefab;
    public AudioSource explosionAudioSource;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnEnable()
    {
        playerHealth.Died += OnDeath;
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);

        // Playing the camera shake
        StartCoroutine(cameraShake?.CameraShakeCoroutine());

        // Playing the explosion Audio
        float pitch = Random.Range(0.8f, 1f);
        explosionAudioSource.pitch = pitch; // Adjust the pitch of the audio to give a varity
        explosionAudioSource.PlayOneShot(explosionAudioSource.clip);

        // Spawning Explosion Effect
        ExpolisionSpawn();
    }

    private void ExpolisionSpawn()
    {
        LeanPool.Spawn(explosionPrefab, transform.position, explosionPrefab.rotation);
    }
}

