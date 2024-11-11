
using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // int: amount to add to the money
    public static Action<int> AddMoney;
    public static Action<int, Action> SpendMoney;
        
    // int: new total amount
    public static event Action<int> UpdateMoney;

    [SerializeField] private IntVariableSO _moneySO;

    public int StartingGold;

    private void Start()
    {
        _moneySO.value = StartingGold;
        UpdateMoney?.Invoke(_moneySO.value);
    }

    private void OnEnable()
    {
        AddMoney += OnMoneyAdded;
        SpendMoney += OnMoneySpent;
    }

    private void OnDisable()
    {
        AddMoney -= OnMoneyAdded;
        SpendMoney -= OnMoneySpent;
    }

    private void OnMoneyAdded(int amount)
    {
        if (amount < 0)
        {
            Debug.LogError("Trying to add negative money");
            return;
        }
        
        Debug.Log($"Money added: {amount}");
        _moneySO.value += amount;
        UpdateMoney?.Invoke(_moneySO.value);
    }
    
    private void OnMoneySpent(int value, Action successCallback)
    {
        if (value < 0)
        {
            Debug.LogError("Trying to spend negative money");
            return;
        }

        if (_moneySO.value < value)
        {
            WarningSystem.Instance.AddWarning("Not enough money!");
            return;
        }
        
        Debug.Log($"Money spent: {value}");
        _moneySO.value -= value;
        UpdateMoney?.Invoke(_moneySO.value);
        successCallback?.Invoke();
    }
}
