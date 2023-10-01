using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D_LSW : MonoBehaviour
{
    //점프힘
    public float jumpPower = 100.0f;
    //점프 쿨타임
    public float coolTime = 1f;
    //점프가 가능한지에 대한 불값
    bool jumpReady = true;
    bool jump = false;
    private Rigidbody2D rb;
    // 마지막 아이템 확인용
    public BaseItem_LSW lastUsedItem;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    }
    IEnumerator JumpCoolTime()
    {
        yield return new WaitForSeconds(coolTime);
        jumpReady = true;
    }

    

    


}
