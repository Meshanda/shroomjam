
using System;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    // int: amount to add to the money
    public static Action<int> AddMoney;
        
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
    }

    private void OnDisable()
    {
        AddMoney -= OnMoneyAdded;
    }

    private void OnMoneyAdded(int amount)
    {
        Debug.Log($"Money added: {amount}");
        _moneySO.value += amount;
        UpdateMoney?.Invoke(_moneySO.value);
    }
}
