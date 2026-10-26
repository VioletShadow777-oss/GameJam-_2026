using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    

    public float health;
    public TextMeshProUGUI healthText;
    public float receiveDamage;
    


    

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
        if(health > 0)
        {
            // Decreasing the health
            health -= receiveDamage;

            // Updating the Health string
            healthText.text = health.ToString();
        }
        
    }

    

    
}
