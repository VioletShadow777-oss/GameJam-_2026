using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;

    public float amplitude;
    public float frequency;
    private void Awake()
    {
        m_MultiChannelPerlin = GetComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    // This Coroutine will be called from "EvemyDieEffect" Script
    public IEnumerator CameraShakeCoroutine()
    {
        // 1. Set initial values
        float elapsed = 0f;

        // Start the shake immediately at the desired intensity
        m_MultiChannelPerlin.AmplitudeGain = amplitude;
        m_MultiChannelPerlin.FrequencyGain = frequency;

        // 2. Loop to decrease values over time (0.5 seconds)
        while (elapsed < 0.5f)
        {
            // Calculate a ratio (0 to 1) of how much time has passed
            elapsed += Time.deltaTime;
            float percentageComplete = elapsed / 1.5f;

            // Calculate the current shake value using Lerp or a simple interpolation.
            // Lerp (Linear Interpolation) moves from start to end based on the third parameter (0 to 1).
            float currentAmplitude = Mathf.Lerp(2, 0f, percentageComplete);
            float currentFrequency = Mathf.Lerp(2, 0f, percentageComplete);

            // Apply the new, fading shake values
            m_MultiChannelPerlin.AmplitudeGain = currentAmplitude;
            m_MultiChannelPerlin.FrequencyGain = currentFrequency;

            // Wait until the next frame
            yield return null;
        }
        // 3. Ensure values are precisely 0 at the end
        m_MultiChannelPerlin.AmplitudeGain = 0;
        m_MultiChannelPerlin.FrequencyGain = 0;
    }
}
