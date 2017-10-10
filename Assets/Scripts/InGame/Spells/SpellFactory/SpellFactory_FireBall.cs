using ClientServerSharedGameObjectMessages;
using UnityEngine;

public class SpellFactory_FireBall : ISpellFactory
{
    public ISpellController CreateSpellController(GameObject gmj, Message_ServerResponse_CreateSpell spell)
    {
        return new SpellController_Fireball(gmj, spell);
    }

    public SpellType GetSpellTypeSupported()
    {
        return SpellType.Fireball;
    }
}