using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;

public class SmallRabbit_HJH : MonoBehaviour
{
    public SmallRabbitParent_HJH rabbit;
    public float jumpPowerPlus;
    public Transform trans;
    float currentTime;
    public float moveTime;
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Set()
    {
        gameObject.SetActive(false);
        start = false;
        currentTime = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(start)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.GetComponent<CharacterMovement2D_LSW>().jumpPower *= jumpPowerPlus;
                rabbit.player = collision.GetComponent<CharacterMovement2D_LSW>();
                rabbit.ReturnPower();
                rabbit.Set();
            }
        }

    }

    public IEnumerator Move()
    {
        while (currentTime < moveTime)
        {
            currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, trans.position, currentTime / moveTime);
            yield return null;
        }
        start = true;
    }

    
}
