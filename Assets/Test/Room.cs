using System.Collections.Generic;
using UnityEngine;

public class RoomCam : MonoBehaviour
{
    Collider2D _collider;

    public Vector2Int RoomId;
    public Vector3 CameraPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Equals(other.gameObject.GetComponent<PlayerInputs>(), null))
        {
            Camera.main.transform.SetParent(this.transform);
            Camera.main.transform.position = CameraPosition;
            RoomBehavior roomBehavior = GetComponentInChildren<RoomBehavior>();
            roomBehavior.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Equals(other.gameObject.GetComponent<PlayerInputs>(), null))
        {
            RoomBehavior roomBehavior = GetComponentInChildren<RoomBehavior>();
            roomBehavior.enabled = false;
        }
    }
}
