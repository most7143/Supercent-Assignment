using UnityEngine;

public class Joystick : MonoBehaviour
{
    public RectTransform Rect;
    public RectTransform Stick;

    public bool IsActivate = false;

    [Range(0, 150)]
    public float Range;

    private Vector3 _centerLocalPosition = new Vector3(140f, 35f, 0f);

    private void FixedUpdate()
    {
        if (IsActivate)
        {
            Move();
        }
    }

    public void RestPosition()
    {
        Rect.anchoredPosition3D = Input.mousePosition;
        Stick.anchoredPosition3D = _centerLocalPosition;
    }

    public void Move()
    {
        Stick.anchoredPosition3D = GetStickDir() + _centerLocalPosition;
    }

    public Vector3 GetStickDir()
    {
        Vector3 inputDir = Input.mousePosition - (Vector3)Rect.anchoredPosition;

        return inputDir.magnitude < Range ? inputDir : inputDir.normalized * Range;
    }
}