using UnityEngine;

internal class LightChangerComponent : MonoBehaviour
{
    public AnimationCurve AnimationCurve;
    public Light Light;
    public float timeSpeed;

    private void Update()
    {
        Light.intensity = AnimationCurve.Evaluate((Time.time * timeSpeed) % 1);
    }
}