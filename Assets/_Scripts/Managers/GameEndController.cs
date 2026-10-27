
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndController : MonoBehaviour
{
    // Connect this method to the GameEndScene New Game button.
    public void NewGameButton()
    {
        // Clear the previous run's saved data.
        BribeSystem.ResetBribeData();

        // Start a fresh game.
        SceneManager.LoadScene(1);
    }
}
