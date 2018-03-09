using System;
using ClientServerSharedGameObjectMessages;

public class SpellFactory_Explode : ISpellFactory_InStaticPosition
{
    public ISpellController CreateSpellController(Message_ServerResponse_CreateSpellInStaticPosition spell, UnitySpellDefinition definition)
    {
        return new SpellController_Explode(spell, definition);
    }

    public SpellType GetSpellTypeSupported()
    {
        return SpellType.Explode;
    }
}