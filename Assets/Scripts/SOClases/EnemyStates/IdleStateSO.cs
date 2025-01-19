using UnityEngine;

[CreateAssetMenu(fileName = "IdleStateSO", menuName = "Scriptable Objects/EnemyStates/IdleStateSO")]
public class IdleStateSO : StatesSO
{
    public override void OnStateEnter(AEnemy enemy)
    {
        enemy.agent.enabled = false;
        enemy.hitbox.enabled = false;
    }

    public override void OnStateExit(AEnemy enemy)
    {
        enemy.agent.enabled = true;
        enemy.hitbox.enabled = true;
    }

    public override void OnStateUpdate(AEnemy enemy) { }
}
