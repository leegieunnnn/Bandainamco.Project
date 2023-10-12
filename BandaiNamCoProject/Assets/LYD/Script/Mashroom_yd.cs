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
        //if (collision.CompareTag("Player") && !isScale)
        //{
        //    if (mashroomTrigger == 0)
        //    {
        //        mashroomTrigger = 1;
        //        originalScale = collision.transform.localScale;
        //        isScale = true;
        //        tr = collision.transform;
        //       StartCoroutine(PlayerScale(collision.transform));
        //    }
        //}
        itemManager.StartCoroutine(itemManager.PlayerScale(collision.transform,scale,resetTime));
        base.OnTriggerEnter2D(collision);
       
    }
   public void Scale()
    {
        //StartCoroutine(PlayerScale(tr));

    }
    //public IEnumerator PlayerScale(Transform targetTr)
    //{
    //    Vector3 targetScale = new Vector3(originalScale.x * scale, originalScale.y * scale, originalScale.z * scale);
    //    targetTr.localScale = targetScale;
    //    yield return new WaitForSeconds(resetTime);
    //    targetTr.localScale = originalScale;
    //    yield return null;
    //    isScale = false;

    //}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
