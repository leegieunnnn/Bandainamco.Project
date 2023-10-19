using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallCloud_HJH : MonoBehaviour
{
    public float jumpPower;
    public float moveRange;
    public float moveTime;
    public float remainTime;

    public void Start()
    {
        StartCoroutine(RandomMove());
        StartCoroutine(TimeCheck());
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        { 
            if(collision.transform.position.y > transform.position.y)
            {
                Rigidbody2D rigid = collision.transform.parent.gameObject.GetComponent<Rigidbody2D>();
                rigid.velocity = Vector3.zero;
                rigid.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                gameObject.SetActive(false);
            }
        }
    }

    IEnumerator RandomMove()
    {
        Vector2 ran = Random.insideUnitCircle * moveRange;
        float currentTime = 0;
        Vector3 current = transform.position;
        while (currentTime < moveTime)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(current, current + (Vector3)ran, currentTime / moveTime);
            yield return null;
        }
        transform.position = current + (Vector3)ran;
    }
    IEnumerator TimeCheck()
    {
        float currentTime = 0;
        while (currentTime < remainTime)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
