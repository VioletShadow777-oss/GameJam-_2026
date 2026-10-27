
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject[] enemies;

    [Space]
    public Transform enemySpawnPosition;

    [Header("Enemy Intro Images")]
    public Image[] enemyIntroImages;

    [Tooltip("How long the intro image stays visible")]
    public float introDuration = 2f;

    [Header("Spawn Settings")]
    [Tooltip("Seconds between enemy spawns")]
    public float spawnInterval = 30f;

    [Header("Game State")]
    public bool isGameActive;

    [Header("Star Spawning")]
    public GameObject starParent;
    public Transform starPrefab;
    // Local Variables
    private int enemyIndex;
    private float spawnCountdown;
    private float introCountdown;

    private bool isShowingIntro;
    private bool hasStarted;

    private void Start()
    {
        enemyIndex = 0;
        spawnCountdown = spawnInterval;

        // Hide all intro images at the beginning
        foreach (Image image in enemyIntroImages)
        {
            if (image != null)
                image.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!isGameActive)
            return;

        if (enemyIndex >= enemies.Length)
            return;

        // Handle the intro image countdown
        if (isShowingIntro)
        {
            introCountdown -= Time.deltaTime;

            if (introCountdown <= 0f)
            {
                HideIntroImage();

                SpawnEnemy();

                // Begin waiting for the next enemy
                spawnCountdown = spawnInterval;
            }

            return;
        }

        // Countdown until the next enemy's intro
        spawnCountdown -= Time.deltaTime;

        if (spawnCountdown <= 0f)
        {
            ShowIntroImage();
        }
    }

    public void StartSpawning()
    {
        if (hasStarted)
            return;

        hasStarted = true;
        isGameActive = true;

        // Begin with the first enemy's intro
        ShowIntroImage();
    }

    private void ShowIntroImage()
    {
        // Skip the intro if no image is assigned
        if (enemyIntroImages == null ||
            enemyIndex >= enemyIntroImages.Length ||
            enemyIntroImages[enemyIndex] == null)
        {
            SpawnEnemy();
            spawnCountdown = spawnInterval;
            return;
        }

        enemyIntroImages[enemyIndex].gameObject.SetActive(true);

        introCountdown = introDuration;
        isShowingIntro = true;
    }

    private void HideIntroImage()
    {
        if (enemyIndex < enemyIntroImages.Length &&
            enemyIntroImages[enemyIndex] != null)
        {
            enemyIntroImages[enemyIndex].gameObject.SetActive(false);
        }

        isShowingIntro = false;
    }

    private void SpawnEnemy()
    {
        if (enemies[enemyIndex] == null ||
            enemySpawnPosition == null)
        {
            Debug.LogError("Enemy or spawn position is missing!");
            return;
        }

        enemies[enemyIndex].transform.position =
            enemySpawnPosition.position;

        enemies[enemyIndex].SetActive(true);

        Debug.Log("Spawned enemy: " + enemies[enemyIndex].name);

        enemyIndex++;

        SpawnStar();
    }

    private void SpawnStar()
    {
        Instantiate(starPrefab, starParent.transform);
    }
}
