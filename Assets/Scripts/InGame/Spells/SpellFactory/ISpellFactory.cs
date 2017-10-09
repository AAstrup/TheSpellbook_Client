using ClientServerSharedGameObjectMessages;
using UnityEngine;

internal interface ISpellFactory
{
    SpellType GetSpellTypeSupported();
    ISpellController CreateSpellController(GameObject gmj, Message_ClientRequest_CreateSpell spell);
}