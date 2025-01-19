using UnityEngine;

[CreateAssetMenu(
    fileName = "preExplodeSO",
    menuName = "Scriptable Objects/EnemyStates/PreExplodeSO"
)]
public class PreExplodeSO : StatesSO
{
    public override void OnStateEnter(AEnemy enemy)
    {
        ExplodingEnemy exploding = enemy as ExplodingEnemy;
        enemy.attackCorroutine = enemy.StartCoroutine(enemy.Attack());
        exploding.audioSource.clip = exploding.preAttackSound;
        exploding.audioSource.Play();
        Color color = exploding.attackArea.GetComponent<SpriteRenderer>().color;
        color.a = 0.05f;
        exploding.attackArea.GetComponent<SpriteRenderer>().color = color;
    }

    public override void OnStateExit(AEnemy enemy)
    {
        ExplodingEnemy exploding = enemy as ExplodingEnemy;

        exploding.audioSource.Stop();
        Color color = exploding.attackArea.GetComponent<SpriteRenderer>().color;
        color.a = 0f;
        exploding.attackArea.GetComponent<SpriteRenderer>().color = color;
    }

    public override void OnStateUpdate(AEnemy enemy) { }
}
