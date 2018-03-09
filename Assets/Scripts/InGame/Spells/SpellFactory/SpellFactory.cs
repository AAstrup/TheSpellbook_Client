using System;
using ClientServerSharedGameObjectMessages;
using System.Collections.Generic;
using UnityEngine;

internal class SpellFactory
{
    //private UnitySpellData spellData;
    private Dictionary<SpellType, ISpellFactory_InStaticPosition> staticSpellsFactory;
    private Dictionary<SpellType, ISpellFactory_SpellWithDirection> directionSpellsFactory;

    public SpellFactory(UnitySpellData spellData)
    {
        //Direction
        directionSpellsFactory = new Dictionary<SpellType, ISpellFactory_SpellWithDirection>();
        AddDirectionToSpellsFactory(new SpellFactory_FireBall());
        //Static
        staticSpellsFactory = new Dictionary<SpellType, ISpellFactory_InStaticPosition>();
        AddStaticToSpellsFactory(new SpellFactory_Explode());
        var debug = new SpellFactory_Teleport();
        AddStaticToSpellsFactory(debug);
    }

    private void AddDirectionToSpellsFactory(ISpellFactory_SpellWithDirection factory)
    {
        directionSpellsFactory.Add(factory.GetSpellTypeSupported(), factory);
    }

    private void AddStaticToSpellsFactory(ISpellFactory_InStaticPosition factory)
    {
        staticSpellsFactory.Add(factory.GetSpellTypeSupported(), factory);
    }

    /// <summary>
    /// Creates a spell controller for a given spell using a ISpellFactory
    /// </summary>
    /// <param name="unitySpellDefinition">Information about the spell in general</param>
    /// <param name="spell">Can be either Message_ServerResponse_CreateSpellInStaticPosition or Message_ServerResponse_CreateSpellWithDirection</param>
    /// <returns></returns>
    public ISpellController CreateSpell(UnitySpellDefinition unitySpellDefinition, object spell) 
    {
        if (spell.GetType() == typeof(Message_ServerResponse_CreateSpellInStaticPosition))
            return staticSpellsFactory[((Message_ServerResponse_CreateSpellInStaticPosition)spell).request.spellType].CreateSpellController((Message_ServerResponse_CreateSpellInStaticPosition)spell, unitySpellDefinition);
        else //Message_ServerResponse_CreateSpellInStaticPosition
            return directionSpellsFactory[((Message_ServerResponse_CreateSpellWithDirection)spell).request.spellType].CreateSpellController((Message_ServerResponse_CreateSpellWithDirection)spell, unitySpellDefinition);
    }
}