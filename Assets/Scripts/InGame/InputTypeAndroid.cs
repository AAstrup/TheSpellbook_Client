using UnityEngine;

internal class InputTypeAndroid : IDeviceInput
{
    public int GetMousePointerId()
    {
        return Input.GetTouch(0).fingerId;
    }

    public Vector3 GetMousePosition()
    {
        if (Input.touchCount == 0)
            return Vector3.zero;
        else
            return Input.GetTouch(0).position;
    }

    public bool MouseButtonHeldDown()
    {
        return Input.touchCount > 0;
    }

    public bool PositionUpdated()
    {
        return Input.touchCount > 0 && Input.GetTouch(0).phase != TouchPhase.Stationary;
    }
}