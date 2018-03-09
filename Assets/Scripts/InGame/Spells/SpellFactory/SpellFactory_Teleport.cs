using ClientServerSharedGameObjectMessages;

internal class SpellFactory_Teleport : ISpellFactory_InStaticPosition
{
    public ISpellController CreateSpellController(Message_ServerResponse_CreateSpellInStaticPosition spell, UnitySpellDefinition definition)
    {
        return new SpellController_Teleport(spell, definition);
    }

    public SpellType GetSpellTypeSupported()
    {
        return SpellType.Teleport;
    }
}