using System.Collections.Generic;
using UnityEngine;

public abstract class ARangedItem : AItem, ITrackable
{
    protected float projectileSpeed;
    protected int satiety;
    protected float minDistance;

    [SerializeField]
    protected Vector2 direction;

    [SerializeField]
    protected Vector2 objectivePoint;

    protected AProjectile projectile;

    protected Queue<AProjectile> projectilePool = new Queue<AProjectile>();

    public virtual void Track(Vector2 endPoint)
    {
        Vector2 thisScreePosition = (Vector2)
            Camera.main.WorldToScreenPoint(transform.parent.position);

        direction = (endPoint - thisScreePosition).normalized;
        transform.localPosition = direction * minDistance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void InitializeStats()
    {
        RangedSO aRangedSO = itemsSO as RangedSO;
        alias = aRangedSO.alias;
        price = aRangedSO.price;
        cd = aRangedSO.cd;
        onUseAudio = aRangedSO.onUseAudio;
        projectileSpeed = aRangedSO.projectileSpeed;
        satiety = aRangedSO.satiety;
        minDistance = aRangedSO.minDistance;
        projectile = aRangedSO.projectile;
        sprite = aRangedSO.sprite;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = onUseAudio;
        currentCd = -0.1f;
    }

    public void ReturnToPool(AProjectile projectile)
    {
        projectile.gameObject.SetActive(false);
        projectilePool.Enqueue(projectile);
    }

    public void DestroyPool()
    {
        if (projectilePool.Count > 0)
            projectilePool = new();
    }

    public override void SwapIn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public override void SwapOut()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
