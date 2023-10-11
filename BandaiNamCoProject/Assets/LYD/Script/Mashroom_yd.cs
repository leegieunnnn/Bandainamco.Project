using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom_yd : BaseItem_LSW
{
    //크기
    public float scale = 2;
    //원래크기로 돌아오는 시간
    public float resetTime = 2f;

    [SerializeField] private Vector3 originalScale;
    public bool isScale = false;
    [SerializeField] private int mashroomTrigger = 0;
    Transform tr;
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isScale)
        {
            if (mashroomTrigger == 0)
            {
                Debug.Log("1");
                mashroomTrigger = 1;
                originalScale = collision.transform.localScale;
                Debug.Log("2");
                isScale = true;
                Debug.Log("4");
                tr = collision.transform;
                Debug.Log("5");
               StartCoroutine(PlayerScale(collision.transform));
            }
        }
        base.OnTriggerEnter2D(collision);
       
    }
   public void Scale()
    {
        StartCoroutine(PlayerScale(tr));

    }
    public IEnumerator PlayerScale(Transform targetTr)
    {
        Vector3 targetScale = new Vector3(originalScale.x * scale, originalScale.y * scale, originalScale.z * scale);
        targetTr.localScale = targetScale;
        Debug.Log("6");
        yield return new WaitForSeconds(resetTime);
        targetTr.localScale = originalScale;
        Debug.Log("됐다");
        yield return null;
        isScale = false;

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
