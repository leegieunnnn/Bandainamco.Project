using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement2D_LSW : MonoBehaviour
{
    //������
    public float jumpPower = 100.0f;
    //���� ������
    public Image jumpIcon;
    public TMP_Text jumpCoolText;
    //���� ��Ÿ��
    public float coolTime = 1f;
    float firstCoolTime = 0;
    //������ ���������� ���� �Ұ�
    bool jumpReady = true;
    bool jump = false;
    private Rigidbody2D rb;
    public ItemManager_LSW itemManager;
    // ������ ������ Ȯ�ο�
    public int? lastUsedItem;

    #region ���ɿ�
    public Vector2 minBoundary;
    public Vector2 maxBoundary;
    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        minBoundary = new Vector2(-(itemManager.bgSize.x / 2) , -(itemManager.bgSize.y / 2));
        maxBoundary = new Vector2((itemManager.bgSize.x / 2), (itemManager.bgSize.y / 2));
        lastUsedItem = null;
        firstCoolTime = coolTime;
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            Vector2 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            dir.Normalize();
            if(dir.y > 0)
            {
                rb.velocity = Vector3.zero;
            }
            if(dir!= Vector2.zero)
            {
                rb.AddForce(dir * jumpPower,ForceMode2D.Force);
            }
            jumpIcon.fillAmount = 0;
            jumpCoolText.gameObject.SetActive(true);
            jump = false;
        }
    }

    void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        if (Input.GetMouseButtonDown(0) && jumpReady)
        {
            jump = true;
            jumpReady = false;
            StartCoroutine(JumpCoolTime());
        }
        
        if (lastUsedItem.HasValue && lastUsedItem.Value == 1)
        {
            Lotus();
        }
        if (lastUsedItem.HasValue && lastUsedItem.Value != 0)
        {
            coolTime = firstCoolTime;
        }
    }

    void Lotus()//���� ���
    {
        // Get the player's position
        if (transform.position.x < minBoundary.x || transform.position.x > maxBoundary.x || transform.position.y < minBoundary.y || transform.position.y > maxBoundary.y)
        {
            transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;
            //���� �ִϸ��̼� �߰��ؾߵ�.
        }
    }
    IEnumerator JumpCoolTime()
    {
        float currentTime = 0;
        while (true)
        {
            yield return null;
            currentTime += Time.deltaTime;
            jumpIcon.fillAmount = currentTime/coolTime;
            jumpCoolText.text = string.Format("{0:N1}",coolTime - currentTime);
            if(currentTime > coolTime)
            {
                break;
            }

        }
        jumpCoolText.gameObject.SetActive(false);
        jumpReady = true;
    }

    

    


}
