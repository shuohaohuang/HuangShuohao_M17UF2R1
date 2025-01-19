using UnityEngine;

[CreateAssetMenu(
    fileName = "RangeChaseStateSO",
    menuName = "Scriptable Objects/EnemyStates/ExplosiveChaseStateSo"
)]
public class ExplosiveChaseStateSo : StatesSO
{
    public override void OnStateEnter(AEnemy enemy)
    {
        enemy.animator.enabled = true;
        enemy.agent.enabled = true;
        enemy.hitbox.enabled = true;
    }

    public override void OnStateExit(AEnemy enemy)
    {
        enemy.agent.enabled = false;
    }

    public override void OnStateUpdate(AEnemy enemy)
    {
        if (
            Vector2.Distance(enemy.transform.position, enemy.target.transform.position)
            > enemy.range
        )
        {
            enemy.agent.SetDestination(enemy.target.transform.position);
        }
        else
        {
            enemy.GoToState<PreExplodeSO>();
        }
    }
}
