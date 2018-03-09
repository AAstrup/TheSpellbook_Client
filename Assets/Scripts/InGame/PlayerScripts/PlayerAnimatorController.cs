using System;
using UnityEngine;

public class PlayerAnimatorController
{
    string lastAnimationName;
    Animator animator;
    public PlayerAnimatorController(GameObject playerGmj)
    {
        animator = playerGmj.GetComponentInChildren<Animator>();
    }

    internal void Idle()
    {
        StartAnimation("Idle");
    }

    private void StartAnimation(string v)
    {
        if(lastAnimationName != v)
        {
            animator.SetTrigger(v);
            lastAnimationName = v;
        }
    }

    internal void Walk()
    {
        StartAnimation("Walk");
    }

    internal void CastSpell()
    {
        StartAnimation("Attack1");
    }
}