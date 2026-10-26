using TMPro;
using UnityEngine;

public class BombDirectionAndDistance : MonoBehaviour
{
    private Transform target; // the PickBomb

    public TextMeshProUGUI bombDistanceText;
    private int bombdistance;

    // Local Variables
    Vector3 directionToTarget;
    void Update()
    {

        Arrow();


    }

    private void Arrow()
    {
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("PickBomb").GetComponent<Transform>();

        }

        if (target.gameObject.activeSelf)
        {
            // 1. Get the vector direction to the target.
            directionToTarget = target.position - transform.position;

            // 2. Calculate the full desired rotation using LookRotation.
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            // 3. Extract the full Euler angles.
            Vector3 euler = targetRotation.eulerAngles;

            // 4. Create a NEW rotation, keeping only the Y angle (Yaw) and setting X/Z to 0.
            Quaternion yOnlyRotation = Quaternion.Euler(
                transform.rotation.eulerAngles.x, // Keep the existing X rotation (often 0 anyway)
                euler.y,                        // Use the new Y rotation
                transform.rotation.eulerAngles.z  // Keep the existing Z rotation (often 0 anyway)
            );

            // 5. Apply the new rotation.
            transform.rotation = yOnlyRotation;

            // Note: You can use '0f' instead of 'transform.rotation.eulerAngles.x' if you want to explicitly enforce 0 rotation on X/Z.


            DistanceText();
        }
    }

    private void DistanceText()
    {
        // Converting into Int
        bombdistance = Mathf.RoundToInt(directionToTarget.magnitude);

        // Setting that int value into bombdistance text
        bombDistanceText.text = bombdistance.ToString()+"m";
    }
}
