using UnityEngine;

public class MouseLockToggle : MonoBehaviour
{
    public static bool allowMouseControl = true;
    private bool cursorLocked = true;

    void Start()
    {
        SetCursorState(cursorLocked);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            cursorLocked = !cursorLocked;
            allowMouseControl = cursorLocked; // синхронізація
            SetCursorState(cursorLocked);
        }
    }

    public void SetCursorState(bool locked)
    {
        Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !locked;
    }
}
