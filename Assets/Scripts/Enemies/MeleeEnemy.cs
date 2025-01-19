using System.Collections;
using UnityEngine;

public class MeleeEnemy : AEnemy
{
    public override void GetFeed(float foodValue)
    {
        base.GetFeed(foodValue);
        GoToState<EatStateSO>();
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
            Debug.Log(stunTime);

            GoToState<IdleStateSO>();
            if (stunCoroutine != null)
                StopCoroutine(stunCoroutine);
            stunCoroutine = StartCoroutine(Stuned());
        }
    }

    protected override IEnumerator Knockback()
    {
        yield return base.Knockback();
        GoToState<ChaseStateSO>();

        yield return null;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IDamageable>() is IDamageable damageable && !eating)
        {
            if (damageable.TakeDamage(damage))
            {
                audioSource.clip = attackSound;
                audioSource.Play();
                GetFeed(10);
            }
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
            GoToState<ChaseStateSO>();
        }
        else
        {
            GoToState<KnockedStateSO>();
        }
    }

    public override IEnumerator Stuned()
    {
        yield return new WaitForSeconds(stunTime);
        GoToState<RangeChaseStateSO>();
    }

    public override void InitChase(PC target)
    {
        this.target = target;
        if (currentHunger > 0)
        {
            GoToState<ChaseStateSO>();
            active = true;
        }
    }

    public override IEnumerator Attack()
    {
        yield return null;
    }
}
