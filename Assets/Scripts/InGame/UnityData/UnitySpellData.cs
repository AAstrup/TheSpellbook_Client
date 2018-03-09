using ClientServerSharedGameObjectMessages;
using System;
using UnityEngine;

[Serializable]
public class UnitySpellData : MonoBehaviour
{
    public UnitySpellDefinition[] spellDefinitions;
    public UnitySpellDefinition GetSpellDefinition(SpellType type)
    {
        foreach (var item in spellDefinitions)
        {
            if (item.type == type)
                return item;
        }
        return null;
    }
}