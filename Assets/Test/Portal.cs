using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Ainteratacable
{
    [SerializeField]
    string sceneName;

    public override void Act()
    {
        AItems.pickAvaible.Clear();
        Ainteratacable.interatacables.Clear();
        SceneManager.LoadScene(sceneName);
    }
}
