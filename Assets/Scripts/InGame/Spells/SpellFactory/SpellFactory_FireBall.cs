using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class SpellFactory_FireBall : ISpellFactory_SpellWithDirection
{
    public ISpellController CreateSpellController(Message_ServerResponse_CreateSpellWithDirection spellResponse, UnitySpellDefinition definition)
    {
        return new SpellController_Fireball(spellResponse, definition);
    }

    public SpellType GetSpellTypeSupported()
    {
        return SpellType.Fireball;
    }
}