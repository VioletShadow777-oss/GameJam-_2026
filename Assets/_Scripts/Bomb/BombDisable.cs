using UnityEngine;
using Lean.Pool;
using System.Threading;

public class BombDisable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") )
        {
            gameObject.SetActive(false); // Despawning the bomb itself
            // For enemy it will create an explosion
        }
        


    }
}
