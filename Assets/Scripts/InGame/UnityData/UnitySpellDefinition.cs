using ClientServerSharedGameObjectMessages;
using System;
using UnityEngine;

[Serializable]
public class UnitySpellDefinition
{
    public SpellType type;
    public string name;
    public GameObject prefabs;
}