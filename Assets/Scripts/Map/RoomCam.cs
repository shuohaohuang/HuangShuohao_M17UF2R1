using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCam : MonoBehaviour
{
    public Vector2Int RoomId;

    public float moveTime = 0.5f;
    public Coroutine camMovement;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!Equals(other.gameObject.GetComponent<PlayerInputs>(), null))
        {
            RoomBehavior roomBehavior = GetComponentInChildren<RoomBehavior>();
            roomBehavior.enabled = true;
            StartCoroutine(MoveCam());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!Equals(other.gameObject.GetComponent<PlayerInputs>(), null))
        {
            RoomBehavior roomBehavior = GetComponentInChildren<RoomBehavior>();
            roomBehavior.enabled = false;
            if (camMovement != null)
            {
                StopCoroutine(camMovement);
            }
        }
    }

    IEnumerator MoveCam()
    {
        Vector3 startPosition = Camera.main.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < moveTime)
        {
            Vector2 interpolatedPosition = Vector2.Lerp(
                new Vector2(startPosition.x, startPosition.y),
                new Vector2(transform.position.x, transform.position.y),
                elapsedTime / moveTime
            );

            Camera.main.transform.position = new Vector3(
                interpolatedPosition.x,
                interpolatedPosition.y,
                -10
            );
            elapsedTime = Mathf.Min(elapsedTime + Time.deltaTime, moveTime);
            yield return null;
        }

        yield return null;
    }
}
