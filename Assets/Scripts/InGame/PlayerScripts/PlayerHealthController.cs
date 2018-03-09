using System;
using UnityEngine;
/// <summary>
/// Responsible for containing all logic for the health of the player
/// </summary>
public class PlayerHealthController
{
    float health;
    static float maxHealth = 100;
    private PlayerController playerController;
    private UnityPlayerData generalPlayerData;

    public PlayerHealthController(PlayerController playerController, UnityPlayerData generalPlayerData)
    {
        health = maxHealth;
        this.playerController = playerController;
        this.generalPlayerData = generalPlayerData;
        UpdateVisuals();
    }

    internal void CheckInsideMap()
    {
        bool insideMap = InGameWrapper.instance.mapWrapper.IsPlayerSafe(playerController.GetGmj().transform.position);
        if (!insideMap)
        {
            InGameWrapper.instance.mapWrapper.DamageTick(this);
        }
    }

    internal void Damage(float mapDamage, int? KillerPlayerGUID)
    {
        health -= mapDamage;
        UpdateVisuals();
        if (health <= 0 && InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer() == playerController)
        {
            LocalDie(KillerPlayerGUID);
        }
    }

    /// <summary>
    /// Only update visuals when it is representing the local player
    /// </summary>
    private void UpdateVisuals()
    {
        if (playerController != InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer() && InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer() != null)
            return;
        generalPlayerData.playerHealthImage.fillAmount = Mathf.Max(0, health / maxHealth);
        generalPlayerData.playerHealthText.text = health + "/" + maxHealth;
    }

    public void LocalDie(int? KillerPlayerGUID = null)
    {
        var msg = new Message_ClientCommand_GameObjectDied()
        {
            DyingPlayerGUID = playerController.PlayerControllerGUID
        };
        if (KillerPlayerGUID.HasValue)
        {
            msg.KillerPlayerGUID = KillerPlayerGUID.Value;
        }
        Match_DotNetAdapter.instance.Send(msg);
        Match_DotNetAdapter.instance.clientEndPoint.HandleLocalMessage(msg);
    }

    internal void Reset()
    {
        health = maxHealth;
        UpdateVisuals();
    }
}