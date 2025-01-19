using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public abstract class AEnemy : MonoBehaviour
{
    public bool active;
    protected bool eating;

    public float range;

    [SerializeField]
    protected float hunger;

    [SerializeField]
    protected float currentHunger;

    [SerializeField]
    protected float anger;

    [SerializeField]
    protected float angerCap;
    protected float stunTime;

    [SerializeField]
    protected float eatSpeed;

    [SerializeField]
    protected float moveSpeed;
    protected float foodValue;

    [SerializeField]
    protected int damage;

    [SerializeField]
    protected int gold;

    public SpriteRenderer spriteRenderer;

    [SerializeField]
    SpriteSO knockedSprite;

    protected Rigidbody2D _rigidbody2D;
    public NavMeshAgent agent;

    public Coroutine stunCoroutine;
    public Coroutine attackCorroutine;
    public Coroutine knockbackCorroutine;
    public Coroutine statusCourroutine;

    public Animator animator;
    public CircleCollider2D hitbox;

    public AudioSource audioSource;
    public AudioClip attackSound;
    public AudioClip eatSound;

    public RoomBehavior propiety;

    [SerializeField]
    protected Gold collectable;

    public PC target;
    public StatesSO CurrentState;

    public SpriteRenderer statusBarValue;
    public SpriteRenderer statusBarBack;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        anger = 0;
        agent.speed = moveSpeed;
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        currentHunger = hunger;

        GoToState<IdleStateSO>();

        Color ValueColor = statusBarValue.color;
        Color BackColor = statusBarBack.color;
        BackColor.a = 0;
        ValueColor.a = 0;

        statusBarValue.color = ValueColor;
        statusBarBack.color = BackColor;
    }

    private void Update()
    {
        CurrentState.OnStateUpdate(this);
    }

    public virtual void GetFeed(float foodValue)
    {
        if (statusCourroutine != null)
        {
            StopCoroutine(statusCourroutine);
        }

        this.foodValue = foodValue;
        currentHunger -= foodValue;

        currentHunger = Mathf.Clamp(currentHunger, 0, hunger);

        float scale = Mathf.Lerp(0.95f, 0f, currentHunger / hunger);

        statusBarValue.transform.localScale = new Vector3(
            scale,
            statusBarValue.transform.localScale.y,
            statusBarValue.transform.localScale.z
        );

        Color ValueColor = statusBarValue.color;
        Color BackColor = statusBarBack.color;
        BackColor.a = 1;
        ValueColor.a = 1;

        statusBarValue.color = ValueColor;
        statusBarBack.color = BackColor;

        StartCoroutine(StatusBarFadeOut());
    }

    public virtual void GetStun(float stunValue, float angervalue)
    {
        stunTime = stunValue * (1 - anger / angerCap);
        anger += angervalue;
        agent.speed = moveSpeed * (1 + anger / angerCap);
    }

    public virtual void GetKnockBack(float knockbackRate, Vector2 initialPoint)
    {
        GoToState<KnockBackSO>();
        Vector2 direction = ((Vector2)transform.position - initialPoint).normalized;
        _rigidbody2D.AddForce(knockbackRate * direction, ForceMode2D.Impulse);

        knockbackCorroutine = StartCoroutine(Knockback());
    }

    protected virtual IEnumerator Knockback()
    {
        yield return new WaitForSeconds(0.4f);
        Vector2 stopRate = _rigidbody2D.linearVelocity / 10;

        for (int i = 0; i < 10; i++)
        {
            _rigidbody2D.linearVelocity -= stopRate;
            yield return new WaitForSeconds(0.2f);
        }
    }

    public void KnockOut()
    {
        active = false;
        animator.enabled = false;
        propiety.CheckEnemies();
        hitbox.enabled = false;

        if (currentHunger <= 0)
        {
            Gold goldGO = Instantiate(collectable, transform.position, quaternion.identity);
            goldGO.value = gold;
        }

        spriteRenderer.sprite = knockedSprite.sprites[
            UnityEngine.Random.Range(0, knockedSprite.sprites.Count)
        ];
        currentHunger = 0;
        _rigidbody2D.bodyType = RigidbodyType2D.Static;
        // enabled = false;
    }

    public abstract IEnumerator Eat();

    public abstract IEnumerator Stuned();

    public abstract IEnumerator Attack();

    public IEnumerator StatusBarFadeOut()
    {
        Color ValueColor = statusBarValue.color;
        Color BackColor = statusBarBack.color;

        for (float t = 0; t < 2; t += Time.deltaTime)
        {
            float normalizedTime = t / 2;
            float alpha = Mathf.Lerp(1, 0, normalizedTime);
            ValueColor.a = alpha;
            BackColor.a = alpha;

            statusBarValue.color = ValueColor;
            statusBarBack.color = BackColor;
            yield return null;
        }

        BackColor.a = 0;
        ValueColor.a = 0;

        statusBarValue.color = ValueColor;
        statusBarBack.color = BackColor;
    }

    public abstract void InitChase(PC target);

    public void GoToState<T>()
        where T : StatesSO
    {
        if (CurrentState.StatesToGo.Find(state => state is T))
        {
            CurrentState.OnStateExit(this);
            CurrentState = CurrentState.StatesToGo.Find(obj => obj is T);
            CurrentState.OnStateEnter(this);
        }
    }
}
