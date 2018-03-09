using ClientServerSharedGameObjectMessages;

internal interface ISpellFactory_SpellWithDirection
{
    SpellType GetSpellTypeSupported();
    ISpellController CreateSpellController(Message_ServerResponse_CreateSpellWithDirection spell, UnitySpellDefinition definition);
}