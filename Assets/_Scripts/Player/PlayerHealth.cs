using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    

    public float health;
    public TextMeshProUGUI healthText;
    public float receiveDamage;


    public event Action Died;
    private bool isDead;
    

    private void Start()
    {
        healthText.text = health.ToString(); // setting the Health with initial values
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Damage();
        }
    }

    // Handles the Damage and string update
    private void Damage()
    {
        // Prevent further damage after death.
        if (isDead)
            return;

        // Prevent health from going below zero.
        health = Mathf.Max(0f, health - receiveDamage);

        healthText.text = health.ToString(); // setting the Health with initial values

        // Detect the transition to dead only once.
        if (health <= 0f)
        {
            isDead = true;
            Died?.Invoke();
        }

    }

    

    
}
