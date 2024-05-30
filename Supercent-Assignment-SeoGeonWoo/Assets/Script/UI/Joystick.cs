using UnityEngine;

public class Joystick : MonoBehaviour
{
    public RectTransform Rect;
    public RectTransform Stick;

    [Range(0, 150)]
    public float Range;

    private Vector3 centerLocalPosition = new Vector3(140f, 35f, 0f);

    private void FixedUpdate()
    {
        Move();
    }

    public void RestPosition()
    {
        Rect.anchoredPosition3D = Input.mousePosition;
        Stick.anchoredPosition3D = centerLocalPosition;
    }

    public void Move()
    {
        Stick.anchoredPosition3D = GetStickDir() + centerLocalPosition;
    }

    public Vector3 GetStickDir()
    {
        Vector3 inputDir = Input.mousePosition - (Vector3)Rect.anchoredPosition;

        return inputDir.magnitude < Range ? inputDir : inputDir.normalized * Range;
    }
}