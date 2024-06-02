using UnityEngine;

public class CharacterHandler
{
    public void Look(Transform transform, Vector2 direction)
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.y));

        transform.rotation = rot;
    }
}