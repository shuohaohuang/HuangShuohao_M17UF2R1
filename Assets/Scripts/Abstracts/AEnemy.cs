using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float hunger;

    [SerializeField]
    protected float anger;

    [SerializeField]
    protected float angerCap;

    [SerializeField]
    protected float stunTime;

    [SerializeField]
    protected float knockOut;

    [SerializeField]
    protected float knockbackDistance;

    [SerializeField]
    protected float eatSpeed;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float money;

    [SerializeField]
    protected string alias;

    [SerializeField]
    Rigidbody2D _rigidbody2D;

    private void Start()
    {
        hunger = 100;
        anger = 0;
    }

    public void SetAngry(float angerValue)
    {
        anger = angerValue;
        Debug.Log(anger);
    }

    public void SetHunger(float satietyValue)
    {
        hunger -= satietyValue;
        Debug.Log(hunger);
    }

    public void GetFeed(float foodValue)
    {
        hunger += -foodValue;
    }

    public void GetStun(float stunValue, float angervalue)
    {
        stunTime += stunValue * (1 - anger / angerCap);
        anger += angervalue;
    }

    public void GetKnockBack(float knockbackRate, Vector2 initialPoint)
    {
        Vector2 direction = ((Vector2)transform.position - initialPoint).normalized;
        _rigidbody2D.AddForce(knockbackDistance * knockbackRate * direction);
        StartCoroutine(Stop());
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.4f);
        Vector2 stopRate = _rigidbody2D.linearVelocity / 10;

        for (int i = 0; i < 10; i++)
        {
            _rigidbody2D.linearVelocity -= stopRate;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
