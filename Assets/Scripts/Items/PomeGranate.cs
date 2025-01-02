using System.Net;
using Unity.Mathematics;
using UnityEngine;

public class PomeGranate : ARangedItem
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Track(Vector2 endPoint)
    {
        objectivePoint = endPoint;
        base.Track(endPoint);
    }

    public override void Use()
    {
        if (!useAvaible)
            return;
        StartCoroutine(Cooldown());
        GameObject newProjectile = Instantiate(projectile, transform.position, quaternion.identity);
        newProjectile.GetComponent<PomeGranateProjectile>().GoTo(objectivePoint);
    }
}
