using UnityEngine;

public class BombDirectionUIAnimation : MonoBehaviour
{
    // Components
    private Animator bombDirectionUIAnimator;

    private void Awake()
    {
        bombDirectionUIAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Enabling the ui for the first time
        bombDirectionUIAnimator.SetBool("enable_b",true);
        
    }

    // This will make the UI Show
    // This Method will be called from the BombSystemManager Script attached with the Player
    public void ShowBombDirecionUI()
    {
        bombDirectionUIAnimator.SetBool("enable_b",true);
    }

    // This will make the UI Hide
    // This Method will be called from the BombSystemManager Script attached with the Player
    public void HideBombDirecionUI()
    {
        bombDirectionUIAnimator.SetBool("enable_b", false);
    }


}
