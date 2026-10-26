using UnityEngine;

public class FramerateManager : MonoBehaviour
{
    private void Awake()
    {
#if UNITY_ANDROID   
        Application.targetFrameRate = 120;
#endif
    }
}
