using ClientServerSharedGameObjectMessages;
using System;
using UnityEngine;

[Serializable]
public class UnitySpellData : MonoBehaviour
{
    public UnitySpellDefinition[] spells;
    public UnitySpellDefinition GetSpellDefinition(SpellType type)
    {
        foreach (var item in spells)
        {
            if (item.type == type)
                return item;
        }
        return null;
    }
}