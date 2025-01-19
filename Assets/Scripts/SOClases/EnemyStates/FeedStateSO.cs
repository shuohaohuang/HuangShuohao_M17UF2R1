using UnityEngine;

[CreateAssetMenu(fileName = "EatStateSO", menuName = "Scriptable Objects/EnemyStates/EatStateSO")]
public class EatStateSO : StatesSO
{
    public override void OnStateEnter(AEnemy enemy)
    {
        enemy.agent.enabled = false;
        enemy.StartCoroutine(enemy.Eat());
    }

    public override void OnStateExit(AEnemy enemy)
    {
        enemy.agent.enabled = true;
    }

    public override void OnStateUpdate(AEnemy enemy) { }
}
