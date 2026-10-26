
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Car")]
    public Transform CarPrefab;
    public GameObject startUI;
    public GameObject gameOverUI;

    [Header("Script References")]
    public PlayerHealth playerHealth;
    public PlayerInput playerInput;
    public EnemySpawnManager enemySpawnManager;
    public CarController carController;


    private void Awake()
    {
        Vector3 instantiatePosition = new Vector3(0, 0.773000002f, -43.7000008f);
        //Instantiate(CarPrefab, instantiatePosition, Quaternion.identity);
    }
    private void OnEnable()
    {
        // Subscribe to Player died event
        if (playerHealth != null)
        {
            playerHealth.Died += HandlePlayerDied;
        }
    }

    private void Start()
    {

        GetRefernces();


        startUI.SetActive(true);
        gameOverUI.SetActive(false);
        carController.enabled = false;
        playerInput.enabled = false;
    }

    private void GetRefernces()
    {
        carController = GameObject.FindGameObjectWithTag("Player").GetComponent<CarController>();
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInput>();
    }
    public void StartGame()
    {
        // Starting the enemy Spawning
        StartCoroutine(enemySpawnManager.EnemySpawn_C());

        // Enable Player Input
        playerInput.enabled = true;

        // Enabling the car Movement
        carController.enabled = true;

        // Disabling the Start UI
        startUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void HandlePlayerDied()
    {
        // Stop car movement.
        carController.enabled = false;

        // Player Input Stopping
        playerInput.enabled = false;

        // Stop any ongoing enemy-spawning coroutine.
        StopCoroutine(enemySpawnManager.EnemySpawn_C());

        // Display the game-over UI.
        gameOverUI.SetActive(true);
    }

}
