using System;
using UnityEngine;

/// <summary>
/// Responsible for showing aim for the local player and
/// firing the spell in that direction or at that position
/// </summary>
public class AimWrapper
{
    private UnityAimData aimData;
    UISpellButtonDefinition currentSpellDefinition; //Null if none
    IDeviceInput deviceInput;
    GameObject aimGmj;

    public AimWrapper(UnityAimData aimData, IDeviceInput deviceInput)
    {
        this.deviceInput = deviceInput;
        this.aimData = aimData;
        aimData.aimGmjDirection.SetActive(false);
        aimData.aimGmjPosition.SetActive(false);
    }

    /// <summary>
    /// Shows the aim for the selected spell
    /// </summary>
    /// <param name="spellDefinition"></param>
    internal void StartAimSpell(UISpellButtonDefinition spellDefinition)
    {
        currentSpellDefinition = spellDefinition;
        if (spellDefinition.spellDefinition.aimType == AimType.Direction)
            aimGmj = aimData.aimGmjDirection;
        else if (spellDefinition.spellDefinition.aimType == AimType.Position)
            aimGmj = aimData.aimGmjPosition;
        else
            throw new Exception("AimType not supported, value " + spellDefinition.spellDefinition.aimType);

        aimGmj.SetActive(true);
    }

    /// <summary>
    /// Checks for the trigger to being fired and fires the spell 
    /// otherwise we show the aim
    /// </summary>
    /// <param name="deltaTime"></param>
    internal void Update(float deltaTime)
    {
        if (currentSpellDefinition == null)
            return;

        if (deviceInput.MouseButtonHeldDown())
        {
            currentSpellDefinition.SetOnCooldown();
            UISpellButtonWrapper.instance.SendMessage(currentSpellDefinition, InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer().GetGmj().transform.position);
            currentSpellDefinition = null;
            aimGmj.SetActive(false);
        }
        else
        {
            if (EventSystemReference.instance.EventSystem.IsPointerOverGameObject())
                return;
            if (deviceInput.PositionUpdated()) {
                RaycastHit hit = new RaycastHit();
                Ray mouseRay = InGameWrapper.instance.camera.ScreenPointToRay(deviceInput.GetMousePosition());
                if (Physics.Raycast(mouseRay, out hit, 100f, aimData.groundMask))
                {
                    aimGmj.transform.position = hit.point;
                    var player = InGameWrapper.instance.playersWrapper.GetOnlyLocalPlayer().GetGmj().transform;
                    aimGmj.transform.LookAt(player.transform);
                    aimGmj.transform.rotation = Quaternion.Euler(0, aimGmj.transform.eulerAngles.y, 0);
                }
            }
        }
    }

    /// <summary>
    /// Returns of whether or not the player is aiming a spell
    /// </summary>
    /// <returns>If a spell is being aimed</returns>
    internal bool IsAiming()
    {
        return currentSpellDefinition != null;
    }
}