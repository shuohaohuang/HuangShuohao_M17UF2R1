using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DeathUI : MonoBehaviour
{
    [SerializeField]
    FloatVariables pcHp;

    private void Start()
    {
        pcHp.OnValuesUpdate += OnPlayerDeath;
    }

    private void OnPlayerDeath()
    {
        if (pcHp.Current <= 0 && pcHp.BeenInit)
        {
            Destroy(PC.instance.gameObject);
            SceneManager.LoadScene("DEATH");
        }
    }
}
