using UnityEngine;
/// <summary>
/// Manages all classes in the scene InGame
/// </summary>
public class InGameWrapper
{
    public static InGameWrapper instance;
    public CarWrapper carController;
    public PlayersWrapper playersWrapper;
    public MapWrapper mapWrapper;
    public SpellWrapper spellsWrapper;
    public Camera camera;

    public InGameWrapper(UnityData unityData)
    {
        instance = this;
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        carController = new CarWrapper(unityData.unityCarData);
        playersWrapper = new PlayersWrapper(unityData.playerData);
        mapWrapper = new MapWrapper(unityData.mapData, playersWrapper.GetOnlyLocalPlayer());
        spellsWrapper = new SpellWrapper(unityData.spellData);
    }

    public void Update(float deltaTime)
    {
        carController.Update(deltaTime);
        playersWrapper.Update(deltaTime);
        mapWrapper.Update(deltaTime);
        spellsWrapper.Update(deltaTime);
    }
}