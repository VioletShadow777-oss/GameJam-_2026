using System.Collections;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class BombButtonPulse : MonoBehaviour
{
    // Components
    private Animator bombButtonAnimator;

    private void Awake()
    {
        bombButtonAnimator = GetComponent<Animator>();
    }

    // This Method will be called from "BombSystemManager" script attached with the "Player" Object
    public void BombButtonAnimation()
    {

        // Plays the animation
        bombButtonAnimator.SetBool("pulse_b", true);

        
    }

    // Turns the pulse_b bool false after a few seconds to make the animation play again
    // This Method will be called from "BombSystemManager" script attached with the "Player" Object
    public void BombAnimationStop()
    {
        
        bombButtonAnimator.SetBool("pulse_b", false);

    }
}
