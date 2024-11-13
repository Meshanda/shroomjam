using TMPro;
using UnityEngine;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] private IntVariableSO _moneySO;
    [SerializeField] private TextMeshProUGUI _moneyText;
    
    private void Start()
    {
        UpdateMoneyDisplay(_moneySO.value);
    }
    
    private void OnEnable()
    {
        MoneyManager.UpdateMoney += UpdateMoneyDisplay;
    }
    
    private void OnDisable()
    {
        MoneyManager.UpdateMoney -= UpdateMoneyDisplay;
    }

    private void UpdateMoneyDisplay(int newValue)
    {
        _moneyText.text = "" + newValue;
    }
}
