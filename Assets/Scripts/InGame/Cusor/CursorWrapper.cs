using UnityEngine;

/// <summary>
/// This wrapper is responsible for showing where the player pressed to move
/// </summary>
public class CursorWrapper
{
    GameObject cursorGameObject;
    float size = 0f;
    private float minimumSize = 0.55f;
    private float maxSize = 1f;
    private Vector3 startSize;

    public CursorWrapper(UnityCursorData cursorData)
    {
        cursorGameObject = GameObject.Instantiate(cursorData.CursorPrefab, Vector3.zero, Quaternion.Euler(90,0,0));
        cursorGameObject.SetActive(false);
        startSize = cursorGameObject.transform.localScale;
    }

    public void SetPosition(Vector3 newPos)
    {
        var pos = new Vector3(newPos.x,UnityStaticValues.CursorStaticYPos,newPos.z);
        cursorGameObject.transform.position = pos;
        size = 1.3f;
        cursorGameObject.SetActive(true);

    }

    public void Update(float deltaTime)
    {
        size -= deltaTime;
        if (size > minimumSize) {
            var actualSize = Mathf.Min(size, maxSize);
            cursorGameObject.transform.localScale = new Vector3(actualSize * startSize.x, actualSize * startSize.y, actualSize * startSize.z);
        }
        else
        {
            if (cursorGameObject.activeSelf)
                cursorGameObject.SetActive(false);
        }
    }
}