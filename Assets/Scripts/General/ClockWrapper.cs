using System;
using UnityEngine;

public class ClockWrapper : IClock
{
    double timeDotTimeOffsetInMiliSeconds;
    bool running;
    private double lastPrint;

    internal void SetClockTime(double gameClockTime)
    {
        timeDotTimeOffsetInMiliSeconds = (Time.realtimeSinceStartup * 1000) - gameClockTime;
        running = true;
    }
    public void Update(float deltaTime)
    {
        if (!running)
            return;
        if (Math.Floor(GetTimeInMiliSeconds() / 1000.0) > lastPrint)
        {
            lastPrint = Math.Floor(GetTimeInMiliSeconds() / 1000.0);
        }
    }

    public double GetTimeInMiliSeconds()
    {
        return (Time.realtimeSinceStartup * 1000) - timeDotTimeOffsetInMiliSeconds;
    }
}