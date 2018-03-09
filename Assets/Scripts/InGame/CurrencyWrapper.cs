using System;
using UnityEngine.UI;

public class CurrencyWrapper
{
    Text currencyText;
    int gold;

    public CurrencyWrapper(UnityCurrencyData currencyData)
    {
        gold = currencyData.startGold;
        currencyText = currencyData.currencyText;
        UpdateGold();
    }

    private void UpdateGold()
    {
        currencyText.text = gold.ToString();
    }

    public void ChangeCurrency(int amount)
    {
        gold += amount;
        UpdateGold();
    }

    public bool CanAfford(int amount)
    {
        return gold >= amount;
    }
}