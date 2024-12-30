using UnityEngine;

public abstract class AEnemy : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float hunger;

    [SerializeField]
    protected float anger;

    [SerializeField]
    protected float angerCap;

    [SerializeField]
    protected float knockOut;

    [SerializeField]
    protected float eatSpeed;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected float money;

    [SerializeField]
    protected string alias;

    private void Start()
    {
        hunger = 100;
        anger = 0;
    }

    public void SetAngry(float angerValue)
    {
        anger = angerValue;
        Debug.Log(anger);
    }

    public void SetHunger(float satietyValue)
    {
        hunger -= satietyValue;
        Debug.Log(hunger);
    }
}
