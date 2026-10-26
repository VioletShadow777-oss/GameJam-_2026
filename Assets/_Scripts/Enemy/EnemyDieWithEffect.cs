using System.Collections;
using Lean.Pool;
using UnityEngine;


public class EnemyDieWithEffect : MonoBehaviour
{
    [SerializeField] private PoliceDieEffectScriptableObject policeDieEffectScriptable;


    // Components
    private Rigidbody rigidbody;
    private PoliceAICar policeAICar;
    public CameraShake cameraShake;

    [Header("Skid Marks")]
    public GameObject[] skidMarks;
     


    [Header("Explosion Effect")]
    public Transform explosionPrefab;
    public AudioSource explosionAudioSource;
    
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        policeAICar = GetComponent<PoliceAICar>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            // Disabling the SkidMarks
            for(int i = 0;  i < skidMarks.Length; i++)
            {

                skidMarks[i].SetActive(false); 

                
            }

            

            // Playing the camera shake
            StartCoroutine(cameraShake.CameraShakeCoroutine());

            // Playing the explosion Audio
            float pitch = Random.Range(0.8f, 1f);
            explosionAudioSource.pitch = pitch; // Adjust the pitch of the audio to give a varity
            explosionAudioSource.PlayOneShot(explosionAudioSource.clip);

            // Disable the PoliceAiCar Script
            policeAICar.enabled = false;

            // Disable the Rigidbody Constrains
            rigidbody.constraints = rigidbody.constraints & ~RigidbodyConstraints.FreezePositionY;

            // Throwing the car Upward with a force and tourque
            ImpulseAndtorque();

            // Spawns the Explosion effect
            ExpolisionSpawn();

            // reset everything so that the police car can follow the player again
            StartCoroutine(EnemyResetEverything());
        }
    }

    // Upward Impuls and Rotation Torque
    private void ImpulseAndtorque()
    {
        rigidbody.AddForce(Vector3.up * policeDieEffectScriptable.impulsForce * Time.fixedDeltaTime, ForceMode.Impulse);
        rigidbody.AddTorque(Vector3.right * -policeDieEffectScriptable.torqueForce * Time.fixedDeltaTime, ForceMode.Impulse);
    }


    private IEnumerator EnemyResetEverything()
    {
        yield return new WaitForSeconds(3);

        // reseting Enemies Position
        transform.position = new Vector3(0f, 0.8f, 0f);

        // Reseting Enemies Rotation
        transform.rotation = Quaternion.identity;

        // Re-enable (freeze) the Y position constraint
        rigidbody.constraints = rigidbody.constraints | RigidbodyConstraints.FreezePositionY;

        // Stoping all the angular velocity caused by the torque
        rigidbody.angularVelocity = Vector3.zero;

        // Enabling the PoliceAiCar Script
        policeAICar.enabled = true;
        
        // Enabling the Skidmarks
        for(int i = 0; i < skidMarks.Length; i++)
        {

            skidMarks[i].SetActive(true);

            
        }

        
    }

    // Handles spawning of the Explosion Effect
    private void ExpolisionSpawn()
    {
        LeanPool.Spawn(explosionPrefab, transform.position, explosionPrefab.rotation);
    }

    

    





}
