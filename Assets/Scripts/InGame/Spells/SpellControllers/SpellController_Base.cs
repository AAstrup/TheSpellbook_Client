using UnityEngine;

public abstract class SpellController_Base
{
    public double TimeStartedCasting;
    private float castTime;

    public SpellController_Base(double TimeStartedCasting,float castTime)
    {
        this.TimeStartedCasting = TimeStartedCasting;
        this.castTime = castTime;
    }

    protected bool FinishedCasting()
    {
        return (InGameWrapper.instance.clockWrapper.GetTimeInMiliSeconds() > (TimeStartedCasting + (castTime * 1000)));
    }
}