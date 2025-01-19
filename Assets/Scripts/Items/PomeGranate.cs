using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PomeGranate : ARangedItem
{
    public override void Track(Vector2 endPoint)
    {
        objectivePoint = endPoint;
        base.Track(endPoint);
    }

    protected override IEnumerator Use()
    {
        while (isUsing)
        {
            if (currentCd < 0)
            {
                if (cooldownCoroutine != null)
                    StopCoroutine(cooldownCoroutine);
                LaunchProjectile();
                cooldownCoroutine = StartCoroutine(Cooldown());
            }
            yield return null;
        }
    }

    // public override void Use()
    // {
    //     if (!currentCd)
    //         return;
    //     StartCoroutine(Cooldown());
    //     LaunchProjectile();
    // }

    private void LaunchProjectile()
    {
        AProjectile newProjectile;

        if (projectilePool.Count > 0)
        {
            newProjectile = projectilePool.Dequeue();
            newProjectile.gameObject.SetActive(true);
        }
        else
        {
            newProjectile = Instantiate(projectile, transform.position, transform.rotation);
        }

        newProjectile.InitProjectile(
            projectileSpeed,
            objectivePoint,
            transform.position,
            transform.rotation,
            this,
            satiety
        );
        audioSource.Play();
    }
}
