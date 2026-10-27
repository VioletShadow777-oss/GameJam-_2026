
using UnityEngine;
using UnityEngine.SceneManagement;

public class BribeSystem : MonoBehaviour
{
    private const string BribeKey = "IsBribed";

    [Header("Bribe Settings")]
    public float bribeCost = 5000f;

    // Read the saved bribe status at any time.
    public static bool IsBribed
    {
        get { return PlayerPrefs.GetInt(BribeKey, 0) == 1; }
    }

    // Save the bribe status permanently on this device.
    private static void SetBribed(bool value)
    {
        PlayerPrefs.SetInt(BribeKey, value ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Connect this method to your Bribe button in the GameScene.
    public void BribeCopsFromButton()
    {
        // Do not charge the player twice.
        if (IsBribed)
        {
            Debug.Log("The cops have already been bribed.");
            return;
        }

        // Check that the CurrencyManager exists.
        if (CurrencyManager.Instance == null)
        {
            Debug.LogError("CurrencyManager was not found.");
            return;
        }

        // Attempt to deduct the bribe cost.
        bool paymentSuccessful =
            CurrencyManager.Instance.SpendMoney(bribeCost);

        if (!paymentSuccessful)
        {
            Debug.Log("Not enough money to bribe the cops.");
            return;
        }

        // Payment succeeded. Save the completed bribe.
        SetBribed(true);

        Debug.Log("Bribe successful! You are free.");

        // Immediately finish the game.
        SceneManager.LoadScene(2);
    }

    // Reset the bribe status and saved money for a new game.
    public static void ResetBribeData()
    {
        SetBribed(false);

        // Reset the existing currency save file through its API.
        CurrencySaveSystem.SaveBalance(0f);

        // Update the live manager too, if one exists.
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.currentMoney = 0f;
            CurrencyManager.Instance.UpdateUI();
        }

        Debug.Log("New game: bribe status and money reset.");
    }
}
