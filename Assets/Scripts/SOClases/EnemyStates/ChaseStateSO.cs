using UnityEngine;

[CreateAssetMenu(
    fileName = "ChaseStateSo",
    menuName = "Scriptable Objects/EnemyStates/ChaseStateSo"
)]
public class ChaseStateSO : StatesSO
{
    public override void OnStateEnter(AEnemy enemy)
    {
        enemy.animator.enabled = true;
        enemy.agent.enabled = true;
        enemy.hitbox.enabled = true;
    }

    public override void OnStateExit(AEnemy enemy) { }

    public override void OnStateUpdate(AEnemy enemy)
    {
        enemy.agent.SetDestination(enemy.target.transform.position);
    }
}
