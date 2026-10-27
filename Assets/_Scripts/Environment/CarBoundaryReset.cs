
using System.Collections;
using UnityEngine;
using TMPro;

public class CarBoundaryReset : MonoBehaviour
{
    [Header("Player")]
    public Transform playerCar;

    [Header("Boundary")]
    public float boundaryLimit = 500f;

    [Header("Reset")]
    public Vector3 resetPosition = new Vector3(
        0f, 0.773000002f, -43.7000008f
    );

    public float resetDelay = 3f;

    [Header("UI")]
    public TextMeshProUGUI countdownText;

    private Coroutine resetCoroutine;

    private void Update()
    {
        if (IsOutsideBoundary())
        {
            if (resetCoroutine == null)
                resetCoroutine = StartCoroutine(ResetCountdown());
        }
        else
        {
            CancelCountdown();
        }
    }

    // Checks whether the car has exceeded any boundary.
    private bool IsOutsideBoundary()
    {
        return playerCar.position.x > boundaryLimit ||
               playerCar.position.x < -boundaryLimit ||
               playerCar.position.z > boundaryLimit ||
               playerCar.position.z < -boundaryLimit;
    }

    // Counts down and resets the car if it stays outside.
    private IEnumerator ResetCountdown()
    {
        float remainingTime = resetDelay;

        while (remainingTime > 0f)
        {
            if (!IsOutsideBoundary())
            {
                resetCoroutine = null;
                ClearCountdownUI();
                yield break;
            }

            UpdateCountdownUI(remainingTime);

            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        if (IsOutsideBoundary())
            ResetCarPosition();

        ClearCountdownUI();
        resetCoroutine = null;
    }

    // Updates the countdown text.
    private void UpdateCountdownUI(float remainingTime)
    {
        if (countdownText != null)
            countdownText.text =
                $"Resetting car position in {Mathf.CeilToInt(remainingTime)}...";
    }

    // Cancels the countdown when the car returns inside.
    private void CancelCountdown()
    {
        if (resetCoroutine != null)
        {
            StopCoroutine(resetCoroutine);
            resetCoroutine = null;
        }

        ClearCountdownUI();
    }

    // Resets only the car's position.
    private void ResetCarPosition()
    {
        playerCar.position = resetPosition;
    }

    // Clears the countdown UI.
    private void ClearCountdownUI()
    {
        if (countdownText != null)
            countdownText.text = "";
    }
}
