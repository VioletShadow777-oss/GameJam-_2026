using UnityEngine;

public class PlayerDamageEffect : MonoBehaviour
{
    // Components
    private Rigidbody carRigidbody;
    private PlayerHealth playerHealth;
    public FlashOnHit flashOnHit;

    public float damageImpulseForce;
    public AudioSource carHitAudioSource;
    public GameObject lowHealthSmoke;
    
    private void Awake()
    {
        carRigidbody = GetComponent<Rigidbody>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        lowHealthSmoke.SetActive(false);
        
    }

    private void Update()
    {
        LowHealthSmoke();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            HitPush();
            HitAudio();
            StartCoroutine(flashOnHit.FlashCoroutine());
        }
    }


    // pushes the car forward with an Impulse
    private void HitPush()
    {
        carRigidbody.AddForce(transform.forward * damageImpulseForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }

    // Plays the Hit audio
    private void HitAudio()
    {
        carHitAudioSource.PlayOneShot(carHitAudioSource.clip);
    }

    // Player low health Smoke effect controller
    private void LowHealthSmoke()
    {
        if(playerHealth.health <= 1)
        {
            lowHealthSmoke.SetActive(true);
        }

        else if(playerHealth.health >= 2)
        {
            lowHealthSmoke.SetActive(false);
            
        }
    }
}
