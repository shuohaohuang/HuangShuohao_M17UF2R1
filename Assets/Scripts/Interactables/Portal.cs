using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : AInteratacable
{
    [SerializeField]
    string sceneName;

    public override void Act()
    {
        AItem.pickAvaible.Clear();
        AInteratacable.interatacables.Clear();
        SceneManager.LoadScene(sceneName);
    }
}
