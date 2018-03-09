using System;
using UnityEngine;
/// <summary>
/// Manages all classes in the scene InGame
/// </summary>
public class InGameWrapper
{
    public static InGameWrapper instance;
    public ResetLogic resetLogic;
    public PlayersWrapper playersWrapper;
    public CurrencyWrapper currencyWrapper;
    public MapWrapper mapWrapper;
    public SpellWrapper spellsWrapper;
    public CursorWrapper cursorWrapper;
    public AimWrapper aimWrapper;
    public Camera camera;
    public ClockWrapper clockWrapper;
    public ILogger logger;
    public bool roundActive;

    public InGameWrapper(UnityData unityData, IDeviceInput deviceInput)
    {
        instance = this;
        logger = new UnityLogger();
        camera = GameObject.Find("Camera").GetComponent<Camera>();
        clockWrapper = new ClockWrapper();
        resetLogic = new ResetLogic();
        playersWrapper = new PlayersWrapper(unityData.playerData, deviceInput);
        mapWrapper = new MapWrapper(unityData.mapData, playersWrapper.GetOnlyLocalPlayer());
        spellsWrapper = new SpellWrapper(unityData.spellData);
        cursorWrapper = new CursorWrapper(unityData.cursorData);
        aimWrapper = new AimWrapper(unityData.aimData, deviceInput);
        currencyWrapper = new CurrencyWrapper(unityData.currencyWrapper);
        EverythingSetupEvent();
    }

    private void EverythingSetupEvent()
    {
        UIShopWrapper.instance.Hide();
    }

    public void Update(float deltaTime)
    {

    }

    internal void FixedUpdate(float deltaTime)
    {
        if (roundActive)
        {
            playersWrapper.Update(deltaTime);
            mapWrapper.Update(deltaTime);
            spellsWrapper.Update(deltaTime);
            cursorWrapper.Update(deltaTime);
            aimWrapper.Update(deltaTime);
        }
        clockWrapper.Update(deltaTime);
        resetLogic.Update(deltaTime);
    }
}