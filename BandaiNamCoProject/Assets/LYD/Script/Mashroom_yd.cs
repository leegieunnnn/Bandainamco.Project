using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom_yd : MonoBehaviour
{
    //크기
    public float scale = 2;
    //원래크기로 돌아오는 시간
    public float resetTime = 2f;

    [SerializeField] private Vector3 originalScale;
    private bool isScale = false;
    [SerializeField] private int mashroomTrigger = 0; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isScale)
        {
            if (mashroomTrigger == 0)
            {
                mashroomTrigger = 1;
                originalScale = collision.transform.localScale;
                isScale = true;
                StartCoroutine(PlayerScale(collision.transform));
            }
        }
       
    }

    private IEnumerator PlayerScale(Transform targetTr)
    {
        Vector3 targetScale = new Vector3(originalScale.x * scale, originalScale.y * scale, originalScale.z * scale);
        targetTr.localScale = targetScale;

        /* float currentTime = 0f;
         while(currentTime < resetTime)
         {

         }*/

        yield return new WaitForSeconds(resetTime);
        targetTr.localScale = originalScale;
        Debug.Log("됐다");
        isScale = false;

        yield return null;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
