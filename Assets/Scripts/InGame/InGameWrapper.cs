﻿/// <summary>
/// Manages all classes in the scene InGame
/// </summary>
public class InGameWrapper
{
    public static InGameWrapper instance;
    public CarWrapper carController;
    public PlayersWrapper playersWrapper;
    public MapWrapper mapWrapper;

    public InGameWrapper(UnityData unityData)
    {
        instance = this;
        carController = new CarWrapper(unityData.unityCarData);
        playersWrapper = new PlayersWrapper(unityData.playerData);
        mapWrapper = new MapWrapper(unityData.mapData, playersWrapper.GetLocalPlayer());
    }

    public void Update(float deltaTime)
    {
        carController.Update(deltaTime);
        playersWrapper.Update(deltaTime);
        mapWrapper.Update(deltaTime);
    }
}