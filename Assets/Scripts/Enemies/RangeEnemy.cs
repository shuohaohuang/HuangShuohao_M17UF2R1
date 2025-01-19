using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RangeEnemy : AEnemy
{
    public Queue<AEnemyProjectile> projectilePool = new Queue<AEnemyProjectile>();

    [SerializeField]
    protected AEnemyProjectile projectile;

    [SerializeField]
    int projectileSpeed;

    [SerializeField]
    float cd;

    public override IEnumerator Attack()
    {
        AEnemyProjectile currentProjectile;
        if (projectilePool.Count > 0)
        {
            currentProjectile = projectilePool.Dequeue();
            currentProjectile.gameObject.SetActive(true);
        }
        else
        {
            currentProjectile = Instantiate(projectile);
        }

        Vector2 direccion = (target.transform.position - transform.position).normalized;
        currentProjectile.InitProjectile(
            projectileSpeed,
            direccion,
            transform.position,
            Quaternion.identity,
            this,
            damage
        );
        audioSource.clip = attackSound;
        audioSource.Play();
        yield return new WaitForSeconds(cd);
        GoToState<RangeChaseStateSO>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
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

    protected override IEnumerator Knockback()
    {
        yield return base.Knockback();
        GoToState<RangeChaseStateSO>();

        yield return null;
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
            GoToState<RangeChaseStateSO>();
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
            GoToState<RangeChaseStateSO>();
            active = true;
        }
    }

    public override void GetFeed(float foodValue)
    {
        base.GetFeed(foodValue);
        GoToState<EatStateSO>();
    }

    public void ReturnToPool(AEnemyProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectilePool.Enqueue(projectile);
    }
}
