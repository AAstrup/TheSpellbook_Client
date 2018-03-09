using UnityEngine;
/// <summary>
/// Cross platform input control 
/// </summary>
public interface IDeviceInput
{
    /// <summary>
    /// Returns whether or not the mouse was pressed this frame
    /// </summary>
    /// <returns></returns>
    bool MouseButtonHeldDown();
    Vector3 GetMousePosition();
    bool PositionUpdated();
    /// <summary>
    /// Needed to check if the mouse pointer is above UI by the EventSystem.IsPointerOverGameObject function
    /// </summary>
    /// <returns>Returns id of the current mouse</returns>
    int GetMousePointerId();
}