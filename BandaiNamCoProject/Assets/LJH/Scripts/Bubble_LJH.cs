using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble_LJH : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private float mySpeed;
    private Vector3 initPosition;
    private bool isActive;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        mySpeed = Random.Range(minSpeed, maxSpeed);
        initPosition = transform.localPosition;
        isActive = false;
    }

    public void StartBubble()
    {
        gameObject.SetActive(true);
        isActive = true;
    }

    public void FinishBubble()
    {
        transform.localPosition = initPosition;
        gameObject.SetActive(false);
        isActive = false;
    }

    public void ResetBubble()
    {
        transform.localPosition = initPosition;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {

        }
    }


    //�÷��̾� �浹�� Trigger? Collider?

    private void Update()
    {
        if (!isActive) return;

        transform.localPosition -= new Vector3(0f, mySpeed * Time.deltaTime, 0f);
    }
}
