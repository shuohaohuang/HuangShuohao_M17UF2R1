using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapes : ARangedItem
{
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
            direction,
            transform.position,
            transform.rotation,
            this,
            satiety
        );
        audioSource.Play();
    }
}
