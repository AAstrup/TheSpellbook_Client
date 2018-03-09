using ClientServerSharedGameObjectMessages;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UnitySpellDefinition
{
    public SpellType type;
    public string name;
    public AimType aimType;
    public Sprite UISprite;
    public int rank = Unranked;
    public List<UnitySpellDefinitionUpgrade> upgrades;
    internal static int Unranked = -1;
    internal static int FirstRank = 0;

    public SpellMovementType spellMovementType;
    public GameObject castEffectPrefab;
    public GameObject projectilePrefab;
    public GameObject hitEffectPrefab;

    public float GetMoveSpeed(int rank) { return upgrades[rank].moveSpeed; }
    public float GetHitRange(int rank) { return upgrades[rank].hitRange; }
    public float GetPushBackMultiplier(int rank) { return upgrades[rank].pushBackMultiplier; }
    public float GetDamage(int rank) { return upgrades[rank].damage; }
    public float GetCooldown(int rank) { return upgrades[rank].Cooldown; }
    public float GetCastTimeInSeconds(int rank) { return upgrades[rank].CastTime; }
    public float GetCastTimeInMiliSeconds(int rank) { return upgrades[rank].CastTime * 1000f; }
    public int GetMaxRank() { return upgrades.Count - 1; }
    
    public enum SpellMovementType
    {
        Direction,
        AOE
    }
}