using System.Collections;
using UnityEngine;

public class PC : MonoBehaviour, IDamageable
{
    public static PC instance { get; set; }

    [SerializeField]
    FloatVariables hp;

    [SerializeField]
    FloatVariables gold;

    [SerializeField]
    int hpMax;

    [SerializeField]
    int goldMax;

    float hitcd = 1f;
    bool hitable = true;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(instance.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gold.Max = goldMax;
        gold.Current = 1000;
        hp.Max = hpMax;
        hp.Current = hp.Max;
    }

    public bool TakeDamage(int damageValue)
    {
        if (hitable)
        {
            hp.Current -= damageValue;
            StartCoroutine(Hit());
            return true;
        }
        return false;
    }

    public void TakeGold(int value)
    {
        gold.Current += value;
    }

    IEnumerator Hit()
    {
        hitable = false;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        float cycleDuration = 0.3f;
        int iterations = Mathf.FloorToInt(hitcd / cycleDuration);

        for (int i = 0; i < iterations; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(hitcd - (iterations * cycleDuration));
        hitable = true;
    }

    private void OnDestroy()
    {
        hp.BeenInit = false;
    }
}
