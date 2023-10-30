using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

public class SmallRabbit_HJH : MonoBehaviour
{
    public float duringTime;
    public float jumpPowerPlus;
    public float moveTime;
    public float moveRange;
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.SetSiblingIndex(Random.Range(0,transform.parent.childCount));
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(start)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<CharacterMovement2D_LSW>().Rabbit(jumpPowerPlus,duringTime);
                transform.parent.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    public IEnumerator Move()
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
        start = true;
    }

    
}
