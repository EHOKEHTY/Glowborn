using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private GameObject fireCursor;
    [SerializeField] private bool useFireCursor = false;

    void Start()
    {
        ApplyCursorState();
    }

    void Update()
    {
        if (useFireCursor)
        {
            FollowMouse();
        }
    }

    public void SetFireCursor(bool enabled)
    {
        useFireCursor = enabled;
        ApplyCursorState();
    }

    private void ApplyCursorState()
    {
        Cursor.visible = !useFireCursor;
        fireCursor.SetActive(useFireCursor);
    }

    private void FollowMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        fireCursor.transform.position = mousePos;
    }
}
