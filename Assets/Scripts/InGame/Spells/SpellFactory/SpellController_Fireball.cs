using ClientServerSharedGameObjectMessages;
using UnityEngine;

internal class SpellController_Fireball : ISpellController
{
    private GameObject gmj;
    private Message_ClientRequest_CreateSpell spell;
    private Vector3 direction;

    public SpellController_Fireball(GameObject gmj, Message_ClientRequest_CreateSpell spell)
    {
        this.gmj = gmj;
        this.spell = spell;
        direction = new Vector3(spell.xDir, 0f, spell.zDir);
    }

    public void LocalUpdate(float deltaTime)
    {
        gmj.transform.position += direction * deltaTime;
    }

    public void OnlineUpdate(float deltaTime)
    {
        gmj.transform.position += direction * deltaTime;
    }
}