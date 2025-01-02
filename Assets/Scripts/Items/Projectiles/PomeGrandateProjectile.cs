using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PomeGranateProjectile : Projectile
{
    float speed = 1f;

    float time;

    public void GoTo(Vector2 objectivePoint)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(objectivePoint);

        Vector2 direction = worldPosition - (Vector2)transform.position;

        _rb.linearVelocity = direction.normalized * speed;

        time = Vector2.Distance(worldPosition, transform.position) / speed;
        Debug.Log(time);
        Debug.Log(worldPosition);
        Debug.Log(transform.position);
        StartCoroutine(Explode());
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.angularVelocity = Mathf.Deg2Rad * 50;
    }

    protected override void OnTriggerEnter2D(Collider2D other) { }
}
