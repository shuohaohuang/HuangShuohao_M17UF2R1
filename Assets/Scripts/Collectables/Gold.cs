using System.Collections;
using UnityEngine;

public class Gold : ACollectables
{
    public int value;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PC>() is PC player)
        {
            player.TakeGold(value);
            audioSource.Play();
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(Desactive());
        }
    }

    IEnumerator Desactive()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
