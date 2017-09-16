using System;
using UnityEngine.UI;

internal class Client_MessageHandler_ReadyCheck : IUpdatable
{
    private Client client;
    private UpdateController updateController;
    private float duration;
    private Slider slider;

    public Client_MessageHandler_ReadyCheck(UpdateController updateController,Client client)
    {
        this.client = client;
        this.updateController = updateController;
    }

    internal void Handle(Message_ServerRequest_ReadyCheck data)
    {
        MM_GUIHandler.queueReadyMsg = data;
        MM_GUIHandler.Instance.SetUIState_QueueRead();
        updateController.Add(this);
        duration = data.duration;
        slider = AppConfig.GetReadyCheckSlider();
        slider.maxValue = duration;
    }

    public void Update(float deltaTime)
    {
        duration -= deltaTime;
        slider.value = duration;
    }

    public bool HasExpired()
    {
        return duration < 0;
    }

    public void End()
    {
        client.Dispose();
        MM_GUIHandler.Instance.SetUIState_MM();
    }
}