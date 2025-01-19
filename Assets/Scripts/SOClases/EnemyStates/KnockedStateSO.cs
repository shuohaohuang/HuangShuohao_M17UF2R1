using UnityEngine;

[CreateAssetMenu(
    fileName = "KnockedStateSO",
    menuName = "Scriptable Objects/EnemyStates/KnockedStateSO"
)]
public class KnockedStateSO : StatesSO
{
    public override void OnStateEnter(AEnemy enemy)
    {
        enemy.agent.enabled = false;
        enemy.KnockOut();
    }

    public override void OnStateExit(AEnemy enemy) { }

    public override void OnStateUpdate(AEnemy enemy) { }
}
