using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mashroom_yd : BaseItem_LJH
{
    //ũ��
    public float scale = 2;
    //����ũ��� ���ƿ��� �ð�
    public float resetTime = 2f;

    [SerializeField] private Vector3 originalScale;
    public bool isScale = false;
    [SerializeField] private int mashroomTrigger = 0;
    [SerializeField] private float mashroomTime = 0f;
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
        StartCoroutine(PlayerScale(collision.transform,scale,resetTime));
        base.OnTriggerEnter2D(collision);
       
    }

    public IEnumerator PlayerScale(Transform targetTr, float scale, float resetTime)
    {
        Vector3 originalScale = targetTr.localScale;
        Vector3 targetScale = new Vector3(originalScale.x * scale, originalScale.y * scale, originalScale.z * scale);
        float currentTime = 0;
        targetTr.localScale = targetScale;
        while (currentTime < mashroomTime)
        {
            targetTr.localScale = Vector3.Lerp(originalScale, targetScale, currentTime / mashroomTime);
            currentTime += Time.deltaTime;
            Debug.Log("Ŀ��");
            yield return null;
        }
        yield return new WaitForSeconds(resetTime);
        Debug.Log("�����ð�");
        currentTime = 0;

        // currentTime = 0;
        while (currentTime < mashroomTime)
        {
            targetTr.localScale = Vector3.Lerp(targetScale, originalScale, currentTime / mashroomTime);
            currentTime += Time.deltaTime;
            yield return null;
            Debug.Log("�۾���");

        }
        targetTr.localScale = originalScale;

        yield return null;
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
}
