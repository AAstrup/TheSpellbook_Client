using System;

public class DeviceInputFactory 
{
    /// <summary>
    /// Creates a device input that support interface IDeviceInput
    /// </summary>
    public static IDeviceInput Create(UnityDeviceInputData.InputType inputType)
    {
        switch (inputType)
        {
            case UnityDeviceInputData.InputType.PC:
                return new InputTypePC();
            case UnityDeviceInputData.InputType.Android:
                return new InputTypeAndroid();
            default:
                throw new Exception("Input type not supported, value " + inputType.ToString());
        }
    }
}