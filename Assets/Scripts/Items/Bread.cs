using System.Collections;
using UnityEngine;

public class Bread : AMeleeItem
{
    protected override IEnumerator Use()
    {
        while (isUsing)
        {
            if (currentCd < 0)
            {
                Debug.Log("hola");
                if (cooldownCoroutine != null)
                    StopCoroutine(cooldownCoroutine);
                animator.SetTrigger("ATTACK");
                DetectColliders();
                cooldownCoroutine = StartCoroutine(Cooldown());
            }
            yield return null;
        }
    }
}
