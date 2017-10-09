using System;
using ClientServerSharedGameObjectMessages;
using System.Collections.Generic;
using UnityEngine;

internal class SpellFactory
{
    private UnitySpellData spellData;
    private Dictionary<SpellType, ISpellFactory> spellsFactory;

    public SpellFactory(UnitySpellData spellData)
    {
        this.spellData = spellData;
        spellsFactory = new Dictionary<SpellType, ISpellFactory>();
        AddToSpellsFactory(new SpellFactory_FireBall());
    }

    private void AddToSpellsFactory(ISpellFactory factory)
    {
        spellsFactory.Add(factory.GetSpellTypeSupported(), factory);
    }

    public ISpellController CreateSpell(UnitySpellDefinition unitySpellDefinition, Message_ClientRequest_CreateSpell spell)
    {
        GameObject gmj = (GameObject)GameObject.Instantiate(unitySpellDefinition.prefabs, new Vector3(spell.xPos, UnityStaticValues.StaticYPos, spell.zPos), Quaternion.identity);
        var spellController = spellsFactory[spell.spellType].CreateSpellController(gmj, spell);
        return spellController;
    }
}