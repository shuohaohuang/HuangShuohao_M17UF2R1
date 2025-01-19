using UnityEngine;

[CreateAssetMenu(
    fileName = "AttackStateSO",
    menuName = "Scriptable Objects/EnemyStates/AttackStateSO"
)]
public class AttackStateSO : StatesSO
{
    public override void OnStateEnter(AEnemy enemy)
    {
        enemy.attackCorroutine = enemy.StartCoroutine(enemy.Attack());
    }

    public override void OnStateExit(AEnemy enemy)
    {
        enemy.spriteRenderer.enabled = true;
        enemy.agent.enabled = true;
        enemy.StopCoroutine(enemy.attackCorroutine);
    }

    public override void OnStateUpdate(AEnemy enemy) { }
}
