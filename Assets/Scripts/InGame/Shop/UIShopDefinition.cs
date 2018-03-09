using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShopDefinition : MonoBehaviour {

    public Image image;
    public Text titleText;
    public Text descriptionText;
    public Text rankText;
    public Button button;
    public Text buttonText;
    private ShopDefinition cachedShopDefinition;

    public void UpdateUI(ShopDefinition shopDefinition)
    {
        this.cachedShopDefinition = shopDefinition;
        var spellInfo = InGameWrapper.instance.spellsWrapper.spellData.GetSpellDefinition(shopDefinition.type);
        UpdateGeneralFields(shopDefinition, spellInfo);

        if (spellInfo.rank == UnitySpellDefinition.Unranked)
        {
            ShowAsNewItem(shopDefinition,spellInfo);
        }
        else if(spellInfo.rank != spellInfo.GetMaxRank())
        {
            ShowAsUpdateItem(shopDefinition, spellInfo);
        }
        else
        {
            ShowAsFullyUpgradedItem(shopDefinition, spellInfo);
        }
    }

    private void UpdateGeneralFields(ShopDefinition shopDefinition, UnitySpellDefinition spellInfo)
    {
        titleText.text = spellInfo.name;
        image.sprite = spellInfo.UISprite;

        if (spellInfo.rank + 1 < spellInfo.upgrades.Count) {
            buttonText.text = spellInfo.upgrades[spellInfo.rank + 1].costToUpgrade.ToString();
        }
        else
        {
            buttonText.text = "";
        }
    }

    private bool CanAfford(float cost)
    {
        Debug.Log("Cost no implemented!");
        return true;
    }

    private void ShowAsFullyUpgradedItem(ShopDefinition shopDefinition, UnitySpellDefinition spellInfo)
    {
        button.interactable = false;
        rankText.text = "MAX RANK";
        descriptionText.text = "Fully upgraded";
    }

    bool FloatEqual(float a, float b) { return Mathf.Abs(a - b) < 0.001f; }

    private void ShowAsUpdateItem(ShopDefinition shopDefinition, UnitySpellDefinition spellInfo)
    {
        bool affordable = CanAfford(spellInfo.upgrades[spellInfo.rank + 1].costToUpgrade);
        button.interactable = affordable;
        button.onClick.RemoveAllListeners();
        if (affordable)
            SetupButtonBehavior(button,shopDefinition,spellInfo);

        rankText.text = "RANK " + (spellInfo.rank + 1);

        descriptionText.text = "";
        var thisRank = spellInfo.upgrades[spellInfo.rank];
        var upgradedRank = spellInfo.upgrades[spellInfo.rank+1];
        if (!FloatEqual(thisRank.damage, upgradedRank.damage))
        {
            descriptionText.text += "Damage increased from " + thisRank.damage + " to " + GetAsBlueText(upgradedRank.damage) + System.Environment.NewLine;
        }
        if (!FloatEqual(thisRank.pushBackMultiplier, upgradedRank.pushBackMultiplier))
        {
            descriptionText.text += "Pushback increased from " + thisRank.pushBackMultiplier + " to " + GetAsBlueText(upgradedRank.pushBackMultiplier) + System.Environment.NewLine;
        }
        if (!FloatEqual(thisRank.Cooldown, upgradedRank.Cooldown))
        {
            descriptionText.text += "Cooldown decreased from " + thisRank.Cooldown + " to " + GetAsBlueText(upgradedRank.Cooldown) + System.Environment.NewLine;
        }
        if (FloatEqual(thisRank.moveSpeed, upgradedRank.moveSpeed))
        {
            descriptionText.text += "Travel speed increased from " + thisRank.moveSpeed + " to " + GetAsBlueText(upgradedRank.moveSpeed) + System.Environment.NewLine;
        }
        if (FloatEqual(thisRank.CastTime, upgradedRank.CastTime))
        {
            descriptionText.text += "Cast Time decreased from " + thisRank.CastTime + " to " + GetAsBlueText(upgradedRank.CastTime) + System.Environment.NewLine;
        }
    }

    private void ShowAsNewItem(ShopDefinition shopDefinition, UnitySpellDefinition spellInfo)
    {
        bool affordable = CanAfford(spellInfo.upgrades[UnitySpellDefinition.FirstRank].costToUpgrade);
        button.interactable = affordable;
        button.onClick.RemoveAllListeners();
        if (affordable)
            SetupButtonBehavior(button, shopDefinition, spellInfo);

        rankText.text = "UKNOWN SPELL";

        descriptionText.text = "";
        var thisRank = spellInfo.upgrades[UnitySpellDefinition.FirstRank];
        descriptionText.text += "Damage " + GetAsBlueText(thisRank.damage) + GetTapString();
        descriptionText.text += "Cooldown " + GetAsBlueText(thisRank.Cooldown) + System.Environment.NewLine;
        descriptionText.text += "PushBack " + GetAsBlueText(thisRank.pushBackMultiplier) + GetTapString();
        descriptionText.text += "CastTime " + GetAsBlueText(thisRank.CastTime) + System.Environment.NewLine;
        descriptionText.text += "Travel speed " + GetAsBlueText(thisRank.moveSpeed);
    }

    private string GetAsBlueText(float s)
    {
        return "<color=\"#0083DFFF\">"+ s + "</color>";
    }

    private string GetTapString()
    {
        return "     ";
    }

    private void SetupButtonBehavior(Button button, ShopDefinition shopDefinition, UnitySpellDefinition spellInfo)
    {
        button.onClick.AddListener(delegate { UpgradeSpell(spellInfo); });
    }

    public void UpgradeSpell(UnitySpellDefinition spellInfo)
    {
        int cost = spellInfo.upgrades[spellInfo.rank + 1].costToUpgrade;
        if (!InGameWrapper.instance.currencyWrapper.CanAfford(cost))
            return;
        InGameWrapper.instance.currencyWrapper.ChangeCurrency(-cost);
        spellInfo.rank++;
        if (spellInfo.rank == UnitySpellDefinition.FirstRank)
            UISpellButtonWrapper.instance.NewSpell(spellInfo);
        UpdateUI(cachedShopDefinition);
    }
}
