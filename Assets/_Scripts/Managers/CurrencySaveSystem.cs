
using System;
using System.IO;
using UnityEngine;

public class CurrencySaveSystem : MonoBehaviour
{
    public static CurrencySaveSystem Instance;

    private static string SavePath => Path.Combine(
        Application.persistentDataPath,
        "currencySave.json"
    );

    [Serializable]
    private class CurrencyData
    {
        public float currentMoney;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public static void SaveBalance(float amount)
    {
        try
        {
            CurrencyData data = new CurrencyData
            {
                currentMoney = amount
            };

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(SavePath, json);

            Debug.Log($"[CurrencySave] Saved balance: {amount} | File: {SavePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"[CurrencySave] Save failed: {e}");
        }
    }

    public static float LoadBalance()
    {
        try
        {
            if (!File.Exists(SavePath))
            {
                Debug.LogWarning($"[CurrencySave] No save file found: {SavePath}");
                return 0f;
            }

            string json = File.ReadAllText(SavePath);
            CurrencyData data = JsonUtility.FromJson<CurrencyData>(json);

            if (data == null)
            {
                Debug.LogError("[CurrencySave] Invalid save data.");
                return 0f;
            }

            Debug.Log($"[CurrencySave] Loaded balance: {data.currentMoney}");
            return Mathf.Max(0f, data.currentMoney);
        }
        catch (Exception e)
        {
            Debug.LogError($"[CurrencySave] Load failed: {e}");
            return 0f;
        }
    }

    public void DeleteAllProgress()
    {
        try
        {
            if (File.Exists(SavePath))
                File.Delete(SavePath);

            if (CurrencyManager.Instance != null)
            {
                CurrencyManager.Instance.currentMoney = 0f;
                CurrencyManager.Instance.UpdateUI();
            }

            Debug.Log("[CurrencySave] Progress deleted.");
        }
        catch (Exception e)
        {
            Debug.LogError($"[CurrencySave] Delete failed: {e}");
        }
    }
}
