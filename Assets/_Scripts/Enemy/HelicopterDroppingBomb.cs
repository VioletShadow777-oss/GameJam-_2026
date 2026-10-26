using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class HelicopterDroppingBomb : MonoBehaviour
{
    // Components
    private Transform playerPrefab; // To get the Player Position

    [Header("Helicopter")]
    public float speed; // The speed of local Z axis

    [Space]
    [HideInInspector] public float helicopterXPos;
    [HideInInspector] public float helicopterZPos;

    [Header("Messile")]
    public Transform missile;
    public float missileSpeed;

    [Header("Missile Aim")]
    public RectTransform missileAim;

    private void Awake()
    {
        playerPrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        HelicopterMovement();
        MissileMove();
    }

    private void OnEnable()
    {
        // Initiates the Helicopter Position
        HelicopterSpawnPosition();

        // When Helicopter enables it initiates the missile possition as well
        MissilePosition();

        // Also fixes the Missile Aim under the missile
        MissileAimPosition();

        // Disabling the missile
        missile.gameObject.SetActive(false);

        // Disable the Missile aim
        missileAim.gameObject.SetActive(false);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MissileTrigger"))
        {
            // Enabling the missile
            missile.gameObject.SetActive(true);

            // Enabnle the Missile Aim
            missileAim.gameObject.SetActive(true);

        }
    }
    #region Helicopter
    // Determines the Helicopter Spawn Posiotion relative to Player
    private void HelicopterSpawnPosition()
    {
        // Randomizing the Helicopter X position
        helicopterXPos = Random.Range(playerPrefab.position.x + 30f, playerPrefab.position.x - 30f);

        // Fixing the Helicopter Z Posiotion
        helicopterZPos = playerPrefab.position.z + 30f;

        // Now Finally Fixing the Helicopter Positoin
        transform.position = new Vector3(helicopterXPos, 31.5f, helicopterZPos);
    }
   
    // Moves the Helicopter In the z axis
    private void HelicopterMovement()
    {
        if(gameObject.activeSelf)
        {
            transform.Translate(transform.forward * -speed * Time.deltaTime);
        }
    }
    #endregion

    #region Missile
    // Determines the Missile Spawn Position
    private void MissilePosition()
    {
        // Every missile spawns with same X and Z position of Helicopter
        missile.position = new Vector3(helicopterXPos, 31.5f, helicopterZPos);
    }

    // Move the Missile down
    private void MissileMove()
    {
        if (missile.gameObject.activeSelf)
        {
            missile.Translate(missile.up * -missileSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Missile Aim
    // Determines the aim Position
    private void MissileAimPosition()
    {
        missileAim.position = new Vector3(helicopterXPos, 1f, helicopterZPos);
    }
    #endregion

}
