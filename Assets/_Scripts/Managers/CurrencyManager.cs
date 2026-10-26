
using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    [Header("Currency Settings")]
    public float currentMoney = 0f;
    public float addAmount = 500f;
    public float spendAmount = 1000f;

    [Header("UI")]
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        currentMoney = CurrencySaveSystem.LoadBalance();

        Debug.Log($"[CurrencyManager] Initialized with: {currentMoney}");
    }

    private void Start()
    {
        UpdateUI();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    public void AddMoney(float amount)
    {
        if (amount <= 0f)
            return;

        currentMoney += amount;

        CurrencySaveSystem.SaveBalance(currentMoney);

        UpdateUI();
    }

    public bool SpendMoney(float cost)
    {
        if (cost <= 0f || currentMoney < cost)
            return false;

        currentMoney -= cost;

        CurrencySaveSystem.SaveBalance(currentMoney);

        UpdateUI();
        return true;
    }

    public void UpdateUI()
    {
        if (moneyText != null)
            moneyText.text = currentMoney.ToString("0.##");
    }
}
