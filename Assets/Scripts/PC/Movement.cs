using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    float speed;

    public Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void MoveTo(Vector2 forward)
    {
        _rb.linearVelocity = forward * speed;
    }
}
