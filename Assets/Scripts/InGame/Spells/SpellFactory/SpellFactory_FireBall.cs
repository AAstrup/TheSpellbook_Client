using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class SpellFactory_FireBall : ISpellFactory
{
    public ISpellController CreateSpellController(GameObject gmj, Message_ServerResponse_CreateSpell spellResponse)
    {
        return new SpellController_Fireball(gmj, spellResponse);
    }

    public SpellType GetSpellTypeSupported()
    {
        return SpellType.Fireball;
    }
}