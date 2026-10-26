using UnityEngine;

public class CarDeadEffect : MonoBehaviour
{
    [Header("Script References")]
    public PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }
    private void OnEnable()
    {
        playerHealth.Died += StopCarOnDead;
    }

    private void StopCarOnDead()
    {
        gameObject.SetActive(false);


    }
}

