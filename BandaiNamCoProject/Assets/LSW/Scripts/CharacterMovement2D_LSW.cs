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
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            rb.velocity = Vector3.zero;
            Vector2 dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            dir.Normalize();
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

    private void OnTriggerEnter(Collider other) //이게 Collider Tag이용해서 Trigger를 노린건데 안됨 그래서 다시 해보고 있음.
    {

    }

    


}
