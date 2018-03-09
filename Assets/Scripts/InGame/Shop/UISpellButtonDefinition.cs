using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISpellButtonDefinition : MonoBehaviour {

    public Image spellImage;
    public Text keyboundsText;
    public Button button;
    [HideInInspector]
    public UnitySpellDefinition spellDefinition;
    public KeyCode keyCode;
    public float lastTimePlayed;
    public GameObject cooldownOverlay;
    public Text overlayText;

    private void Awake()
    {
        lastTimePlayed = float.NegativeInfinity;
    }

    public float GetCooldownTime()
    {
        return (Time.time - lastTimePlayed) - spellDefinition.GetCooldown(spellDefinition.rank);
    }

    internal bool IsOnCooldown()
    {
        return (Time.time - lastTimePlayed) < spellDefinition.GetCooldown(spellDefinition.rank);
    }

    public void Update()
    {
        if (spellDefinition.rank == UnitySpellDefinition.Unranked)
            return;
        var val = spellDefinition.GetCooldown(spellDefinition.rank) - (Time.time - lastTimePlayed);
        if (val > 0f)
        {
            if (!cooldownOverlay.activeSelf)
                cooldownOverlay.SetActive(true);
            overlayText.text = val.ToString("0.0");
        }
        else if (cooldownOverlay.activeSelf)
            cooldownOverlay.SetActive(false);
    }

    internal void Reset()
    {
        lastTimePlayed = float.NegativeInfinity;
    }

    internal void SetOnCooldown()
    {
        lastTimePlayed = Time.time;
    }
}
