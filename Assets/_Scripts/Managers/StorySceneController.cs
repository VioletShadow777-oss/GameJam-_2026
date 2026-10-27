
using UnityEngine;
using UnityEngine.SceneManagement;

public class StorySceneController : MonoBehaviour
{
    // Connect this method to the StoryScene Next button.
    public void SkipButton()
    {
        if (BribeSystem.IsBribed)
        {
            // The player already completed the game.
            SceneManager.LoadScene(2);
        }
        else
        {
            // The player still needs to earn money and bribe the cops.
            SceneManager.LoadScene(1);
        }
    }
}
