using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatesSO", menuName = "Scriptable Objects/StatesSO")]
public abstract class StatesSO : ScriptableObject
{
    public List<StatesSO> StatesToGo;

    public abstract void OnStateEnter(AEnemy enemy);

    public abstract void OnStateUpdate(AEnemy enemy);

    public abstract void OnStateExit(AEnemy enemy);
}
