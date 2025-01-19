using System.Collections;
using UnityEngine;

public class Fluflu : AItem, ITrackable
{
    [SerializeField]
    ParticleSystem particle;
    public float stunTime;
    public float angryRate;
    public float range;
    public float satiety;
    public float minDistance;

    public float capacity;
    public float currentQuantity;
    public float consumption;
    public float refillPerSecond;

    [SerializeField]
    protected Vector2 direction;
    Coroutine RefillCoroutine;
    Coroutine fuelBarFadeOut;
    Coroutine fuelBarComsum;

    public SpriteRenderer statusBarValue;
    public SpriteRenderer statusBarBack;

    public override void StartUse()
    {
        if (RefillCoroutine != null)
        {
            StopCoroutine(RefillCoroutine);
        }

        particle.Play();
        audioSource.Play();
        useCoroutine = StartCoroutine(Use());
    }

    public override void StopUse()
    {
        if (useCoroutine != null)
        {
            StopCoroutine(useCoroutine);
        }

        RefillCoroutine = StartCoroutine(Refill());
        particle.Stop();
        audioSource.Stop();
    }

    protected override void InitializeStats()
    {
        //Getting Components
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        particle = GetComponentInChildren<ParticleSystem>();
        //SO cast
        FluFluSo Fluflu = itemsSO as FluFluSo;

        //SO values asignations
        alias = Fluflu.alias;
        angryRate = Fluflu.angryRate;
        capacity = Fluflu.capacity;
        cd = Fluflu.cd;
        consumption = Fluflu.consumption;
        currentQuantity = Fluflu.capacity;
        minDistance = Fluflu.minDistance;
        price = Fluflu.price;
        range = Fluflu.range;
        refillPerSecond = Fluflu.consumption;
        satiety = Fluflu.satiety;
        stunTime = Fluflu.stunTime;

        sprite = Fluflu.sprite;
        renderer.sprite = Fluflu.sprite;
        onUseAudio = Fluflu.onUseAudio;

        //PS asignations
        var main = particle.main;
        main.startLifetime = range;
        audioSource.clip = onUseAudio;
        currentCd = -0.1f;
    }

    protected override IEnumerator Use()
    {
        Color ValueColor = statusBarValue.color;
        Color BackColor = statusBarBack.color;
        BackColor.a = 1;
        ValueColor.a = 1;
        statusBarValue.color = ValueColor;
        statusBarBack.color = BackColor;
        while (currentQuantity > 0)
        {
            currentQuantity = Mathf.Max(currentQuantity - Time.deltaTime * consumption, 0);
            float scale = Mathf.Lerp(0f, 0.95f, currentQuantity / capacity);

            statusBarValue.transform.localScale = new Vector3(
                scale,
                statusBarValue.transform.localScale.y,
                statusBarValue.transform.localScale.z
            );

            yield return null;
        }

        BackColor.a = 0;
        ValueColor.a = 0;
        statusBarValue.color = ValueColor;
        statusBarBack.color = BackColor;
        particle.Stop();
        audioSource.Stop();
    }

    IEnumerator Refill()
    {
        Color valueColor = statusBarValue.color;
        Color backColor = statusBarBack.color;
        backColor.a = 1;
        valueColor.a = 1;
        statusBarValue.color = valueColor;
        statusBarBack.color = backColor;

        while (currentQuantity < capacity)
        {
            currentQuantity = Mathf.Min(
                currentQuantity + Time.deltaTime * refillPerSecond,
                capacity
            );

            float scale = Mathf.Lerp(0f, 0.95f, currentQuantity / capacity);

            statusBarValue.transform.localScale = new Vector3(
                scale,
                statusBarValue.transform.localScale.y,
                statusBarValue.transform.localScale.z
            );

            yield return null;
        }

        backColor.a = 0;
        valueColor.a = 0;
        statusBarValue.color = valueColor;
        statusBarBack.color = backColor;
    }

    public void Track(Vector2 endPoint)
    {
        Vector2 thisScreePosition = (Vector2)
            Camera.main.WorldToScreenPoint(transform.parent.position);

        direction = (endPoint - thisScreePosition).normalized;
        transform.localPosition = direction * minDistance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    public override void SwapIn()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        statusBarValue.enabled = true;
        statusBarBack.enabled = true;
    }

    public override void SwapOut()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        statusBarValue.enabled = false;
        statusBarBack.enabled = false;
    }

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
}
