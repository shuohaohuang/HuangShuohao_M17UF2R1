using System.Collections;
using System.Data.Common;
using UnityEngine;

public class ExplodingEnemy : AEnemy
{
    public AudioClip preAttackSound;
    public GameObject attackArea;

    protected override void Awake()
    {
        base.Awake();
        attackArea.transform.localScale = new(range * 1.5f * 2, range * 1.5f * 2, 1);
    }

    public override IEnumerator Attack()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        audioSource.clip = preAttackSound;
        audioSource.Play();
        for (int i = 0; i < 3; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.4f);
        }
        DetectColliders();
        GoToState<KnockedStateSO>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range * 1.5f);
    }

    public void DetectColliders()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
        foreach (Collider2D item in Physics2D.OverlapCircleAll(transform.position, range * 1.5f))
        {
            if (item.GetComponent<IDamageable>() is IDamageable damageable)
            {
                damageable.TakeDamage(damage);
            }
        }
    }

    public override void GetStun(float stunValue, float angervalue)
    {
        base.GetStun(stunValue, angervalue);

        if (anger > angerCap)
        {
            GoToState<KnockedStateSO>();
        }
        else
        {
            GoToState<IdleStateSO>();
            if (stunCoroutine != null)
                StopCoroutine(stunCoroutine);
            stunCoroutine = StartCoroutine(Stuned());
        }
    }

    public override IEnumerator Eat()
    {
        eating = true;
        audioSource.clip = eatSound;
        audioSource.Play();
        yield return new WaitForSeconds(foodValue / eatSpeed);
        eating = false;
        audioSource.Stop();

        if (currentHunger > 0)
        {
            GoToState<ExplosiveChaseStateSo>();
        }
        else
        {
            GoToState<KnockedStateSO>();
        }
    }

    public override IEnumerator Stuned()
    {
        yield return new WaitForSeconds(stunTime);
        GoToState<ExplosiveChaseStateSo>();
    }

    public override void InitChase(PC target)
    {
        this.target = target;
        if (currentHunger > 0)
        {
            GoToState<ExplosiveChaseStateSo>();
            active = true;
        }
    }

    protected override IEnumerator Knockback()
    {
        yield return base.Knockback();
        GoToState<ExplosiveChaseStateSo>();

        yield return null;
    }

    public override void GetFeed(float foodValue)
    {
        base.GetFeed(foodValue);
        GoToState<EatStateSO>();
    }
}
