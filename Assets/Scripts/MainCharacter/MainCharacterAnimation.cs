using Unity.Hierarchy;
using UnityEngine;

public class MainCharaterAnimation : MonoBehaviour
{
    Animator _animator;
    Vector2 forward;
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Moving(Vector2 forward)
    {
        this.forward = forward;
        _animator.SetBool("MOVING", true);
        _animator.SetFloat("X_AXIS", this.forward.x);
        _animator.SetFloat("Y_AXIS", this.forward.y);
    }

    public void StopMoving()
    {
        _animator.SetBool("MOVING", false);
        _animator.SetFloat("X_AXIS", forward.x);
        _animator.SetFloat("Y_AXIS", forward.y);
    }

}
