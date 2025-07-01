using UnityEngine;

public class CoursoreControl : MonoBehaviour
{
    private bool CustomCursorVisible;
    [SerializeField] GameObject CursorObject;
    public void ToggleCursor()
    {
        CustomCursorVisible = !CustomCursorVisible;
        Cursor.visible = !CustomCursorVisible;
    }

    private void Update()
    {
        if (CustomCursorVisible)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
