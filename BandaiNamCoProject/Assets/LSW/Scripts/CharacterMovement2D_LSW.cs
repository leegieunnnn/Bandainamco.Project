using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement2D_LSW : MonoBehaviour
{
    //점프힘
    public float jumpPower = 100.0f;
    //점프 아이콘
    public Image jumpIcon;
    public TMP_Text jumpCoolText;
    //점프 쿨타임
    public float coolTime = 1f;
    //점프가 가능한지에 대한 불값
    bool jumpReady = true;
    bool jump = false;
    private Rigidbody2D rb;
    public ItemManager_LSW itemManager;
    GameObject player;

    // 마지막 아이템 확인용
    public int? lastUsedItem;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        lastUsedItem = null;
            
        
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
        
        if (lastUsedItem.HasValue && lastUsedItem.Value == 2)
        {
            if (player != null)
            {
                // Get the player's position
                Vector3 playerPosition = player.transform.position;
                Debug.Log(playerPosition);
                Debug.Log(itemManager.bgSize);
                if (playerPosition.x > itemManager.bgSize.x / 2 || playerPosition.x < -itemManager.bgSize.x / 2)
                {
                    if (playerPosition.y > itemManager.bgSize.y / 2 || playerPosition.y < -itemManager.bgSize.y / 2)
                    {
                        player.transform.position = new Vector3(0, 0, 0);
                    }
                }
            }
            Debug.Log("it Worked!");
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
