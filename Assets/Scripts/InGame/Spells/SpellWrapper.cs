using System;
using ClientServerSharedGameObjectMessages;
using UnityEngine;
using System.Collections.Generic;

public class SpellWrapper
{
    public UnitySpellData spellData;
    private SpellFactory spellFactory;
    private Dictionary<int, ISpellController> spellsDictionary;
    public List<ISpellController> localSpells;
    public List<ISpellController> onlineSpells;

    public SpellWrapper(UnitySpellData spellData)
    {
        this.spellData = spellData;
        spellsDictionary = new Dictionary<int, ISpellController>();
        spellFactory = new SpellFactory(spellData);
        localSpells = new List<ISpellController>();
        onlineSpells = new List<ISpellController>();
    }

    public void SpawnSpell(Message_ServerResponse_CreateSpellWithDirection spell, bool isMine)
    {
        var spellController = spellFactory.CreateSpell(spellData.GetSpellDefinition(spell.request.spellType), spell);
        spellsDictionary.Add(spell.spellID, spellController);
        if (isMine)
            localSpells.Add(spellController);
        else
            onlineSpells.Add(spellController);
    }

    public void SpawnSpell(Message_ServerResponse_CreateSpellInStaticPosition spell, bool isMine)
    {
        var spellController = spellFactory.CreateSpell(spellData.GetSpellDefinition(spell.request.spellType), spell);
        spellsDictionary.Add(spell.spellID, spellController);
        if (isMine)
            localSpells.Add(spellController);
        else
            onlineSpells.Add(spellController);
    }

    public void Update(float deltaTime)
    {
        for (int i = 0; i < localSpells.Count; i++)
        {
            localSpells[i].LocalUpdate(deltaTime);
            if (localSpells[i].IsDead())
                DestroySpell(localSpells[i],true);
        }

        for (int i = 0; i < onlineSpells.Count; i++)
        {
            if (onlineSpells[i].IsDead())
            {
                onlineSpells.RemoveAt(i);
                continue;
            }
            onlineSpells[i].OnlineUpdate(deltaTime);
        }
    }

    public void DestroySpell(ISpellController spellController,bool isLocal)
    {
        spellsDictionary.Remove(spellController.GetGuid());
        if (isLocal)
            localSpells.Remove(spellController);
        else
            onlineSpells.Remove(spellController);
    }

    public ISpellController GetSpellController(int GUID)
    {
        return spellsDictionary[GUID];
    }

    internal void TEMPORARYDESTROYALLSPELLS()
    {
        foreach (var item in localSpells)
        {
            item.Destroy();
        }
        localSpells = new List<ISpellController>();
        foreach (var item in onlineSpells)
        {
            item.Destroy();
        }
        onlineSpells = new List<ISpellController>();
    }
}