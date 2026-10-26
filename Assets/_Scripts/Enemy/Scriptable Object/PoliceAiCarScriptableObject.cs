using UnityEngine;

[CreateAssetMenu(fileName = "PoliceAiCarScriptableObject", menuName = "Scriptable Objects/PoliceAiCarScriptableObject")]
public class PoliceAiCarScriptableObject : ScriptableObject
{
    [Tooltip("Target Object that the Police will follow")]
    public string targetObject;
    [Space]
    [Tooltip("How much the car drifts (0 = full grip, 1 = no grip).")]
    public float driftFactor;

    [Tooltip("The side velocity needed to start creating skid marks.")]
    public float skidMarkEnableVelocity = 4f;

    [Tooltip("The force multiplier for forward acceleration.")]
    public float accelerationFactor = 30f;

    [Tooltip("The maximum speed (velocity magnitude) the car can reach.")]
    public float maxVelocity = 40f;

    [Tooltip("The multiplier for how sharply the car can turn when steering.")]
    public float turnFactor = 3.5f;

    [Tooltip("The angle (in degrees) the car must be off target before it starts steering.")]
    public float turnThreshold = 10f;

    [Tooltip("Controls how smoothly the steering reacts. Higher values reduce jittering.")]
    public float steeringDamping = 10f;

    [HideInInspector] public float rotationAngle;
    [HideInInspector] public float currentSteeringInput;
}
