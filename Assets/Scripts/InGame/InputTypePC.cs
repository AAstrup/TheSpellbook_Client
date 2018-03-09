using UnityEngine;

internal class InputTypePC : IDeviceInput
{
    /// <summary>
    /// Id specified at https://docs.unity3d.com/ScriptReference/EventSystems.EventSystem.IsPointerOverGameObject.html
    /// </summary>
    /// <returns></returns>
    public int GetMousePointerId()
    {
        return -1;
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

    public bool MouseButtonHeldDown()
    {
        return Input.GetMouseButton(0);
    }

    public bool PositionUpdated()
    {
        return true;
    }
}