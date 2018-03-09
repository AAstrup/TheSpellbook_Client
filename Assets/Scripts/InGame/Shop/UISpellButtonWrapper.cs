using ClientServerSharedGameObjectMessages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpellButtonWrapper : MonoBehaviour {
    public static UISpellButtonWrapper instance;
    public UISpellButtonDefinition[] buttons;
    public SpellMessageFactory spellMessageFactory;

    internal void StartShop()
    {
        instance = this;
        spellMessageFactory = new SpellMessageFactory();
        Setup();
    }

    private void Setup()
    {
        List<UnitySpellDefinition> list = new List<UnitySpellDefinition>();
        foreach (var item in InGameWrapper.instance.spellsWrapper.spellData.spellDefinitions)
        {
            if (item.rank >= 0)
                list.Add(item);
        }
        SetupButtonsForSpells(list);
    }

    public void NewSpell(UnitySpellDefinition newSpell)
    {
        Setup();
    }

    public void SetupButtonsForSpells(List<UnitySpellDefinition> definitions)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i < definitions.Count)
            {
                buttons[i].gameObject.SetActive(true);
                buttons[i].spellImage.sprite = definitions[i].UISprite;
                buttons[i].spellDefinition = definitions[i];
            }
            else
                buttons[i].gameObject.SetActive(false);
        }
    }

    public void Reset()
    {
        foreach (var item in buttons)
        {
            item.Reset();
        }
    }

    void Update()
    {
        foreach (var item in buttons)
        {
            if (!CanCastSpell())
                break;

            if (!item.gameObject.activeSelf)
            {
                return;
            }
            if (Input.GetKeyUp(item.keyCode) && !item.IsOnCooldown())
            {
                item.SetOnCooldown();
                SendMessage(item, InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer().GetGmj().transform.position);
            }
            item.Update();
        }
    }

    /// <summary>
    /// Spell button pressed in UI
    /// Either cast the spell if no aim is requires
    /// Or show the aim for the player to press and fire spell
    /// </summary>
    /// <param name="spellDefinition"></param>
    public void UIMethod_CastSpell(UISpellButtonDefinition spellDefinition) {
        if (!CanCastSpell() || spellDefinition.IsOnCooldown())
            return;

        if (spellDefinition.spellDefinition.aimType != AimType.None)
        {
            InGameWrapper.instance.aimWrapper.StartAimSpell(spellDefinition);
        }
        else
        {
            spellDefinition.SetOnCooldown();
            SendMessage(spellDefinition, InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer().GetGmj().transform.position);
        }
    }

    /// <summary>
    /// Returns whether or not the local player can cast a new spell
    /// </summary>
    /// <returns></returns>
    private bool CanCastSpell()
    {
        var localPlayer = InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer();
        if (localPlayer == null)
            return false;
        return !InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer().spellCaster.IsCasting();
    }

    public void SendMessage(UISpellButtonDefinition item,Vector3 spawnPos)
    {
        var msg = spellMessageFactory.BuildMessage(item.spellDefinition, spawnPos);
        Match_DotNetAdapter.instance.Send(msg);
    }
}
