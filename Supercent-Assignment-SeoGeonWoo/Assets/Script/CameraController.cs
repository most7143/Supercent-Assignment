using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 Offset;

    public Player Player;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = Player.transform.position + Offset;
    }
}