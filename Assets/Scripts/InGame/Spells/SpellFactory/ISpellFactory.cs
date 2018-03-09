using ClientServerSharedGameObjectMessages;
using UnityEngine;

internal interface ISpellFactory_InStaticPosition
{
    SpellType GetSpellTypeSupported();
    ISpellController CreateSpellController(Message_ServerResponse_CreateSpellInStaticPosition spell, UnitySpellDefinition definition);
}