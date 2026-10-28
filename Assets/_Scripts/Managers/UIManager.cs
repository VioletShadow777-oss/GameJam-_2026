using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;


    private void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Pause();
        }
    }


    private void Pause()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }
}
