using UnityEngine;
using Lean.Pool;
using UnityEngine.InputSystem;
public class BombSystemManager : MonoBehaviour
{
    // components
    public BombButtonPulse bombButtonPulse; // Manages the Bomb Button Pulse 
    [Space]
    // To manage the animation of bomb direction UI
    public BombDirectionUIAnimation bombDirectionUIAnimation; 
    [Space]
    public int bombAmmo;
    [Space]
    [Header("Bombs")]
    public Transform bombPrefab;
    public Transform pickBombPrefab;

    [Header("Drop Position")]
    public Transform dropPosition;

    [Header("Spawn")]
    public float minXPos;
    public float maxXPos;
    public float minZPos;
    public float maxZPos;

    private void Start()
    {
        bombAmmo = 0;
        PickBombSpawn(); // Spawning the First pickBomb
    }

    // Spawning the pickbomb
    public void PickBombSpawn()
    { 
        float bombXpos = Random.Range(minXPos,maxXPos);
        float bombZpos = Random.Range(minZPos,maxZPos);

        Vector3 spawnPosition = new Vector3(bombXpos, 1.81f, bombZpos);

        // Spawn the pickBomb at the random position
        pickBombPrefab.position = spawnPosition;
        pickBombPrefab.gameObject.SetActive(true);

        // Stops the Bomb Button Animation
        bombButtonPulse.BombAnimationStop();

    }
     
    // Picking Up the pickBomb
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickBomb"))
        {
            bombAmmo++; // Bomb Ammo Increases
            
            other.gameObject.SetActive(false); // Disabling the pickBomb

            // Playes the bomb Button animation
            bombButtonPulse.BombButtonAnimation();

            // Now the player has a bomb 
            // So hide the Bomb direction UI
            bombDirectionUIAnimation.HideBombDirecionUI();
        }
    }

    // Droping the Bomb
    public void DropBomb(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(bombAmmo > 0)
            {
                bombPrefab.position = dropPosition.position;
                bombPrefab.gameObject.SetActive(true); // Spawns the Bomb

               

                bombAmmo--; // Decreasing the ammo

                // Again calling the pickBombSpawn 
                PickBombSpawn();

                // Now the player dont have a bomb 
                // So Show the Bomb direction UI
                bombDirectionUIAnimation.ShowBombDirecionUI();
            }
            
        }
    }




}
