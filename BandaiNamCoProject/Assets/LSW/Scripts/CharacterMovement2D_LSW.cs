using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D_LSW : MonoBehaviour
{
    // State를 enum으로 정리
    private enum State
    {
        Idle,
        Move,
        Fly,
    }
    private State state;
    // 각각 이동속도와 점프힘
    private float moveSpeed = 100.0f;
    private float flyForce = 200.0f;

    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Idle;
        
    }

    private void FixedUpdate()
    {
       if (state == State.Move)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
    }

    void Update()
    {
        // space 바를 누르면 이제 스테이트마다 하는 행동 실시
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (state)
            {
                case State.Idle:
                    state = State.Move;
                    break;
                case State.Move:
                    break;
                case State.Fly:
                    rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
                    break;
            }
        }
    }

    private void startTriggerEnter(Collider2D other) //이게 Collider Tag이용해서 Trigger를 노린건데 안됨 그래서 다시 해보고 있음.
    {
        if (state == State.Move && other.CompareTag("StartPoint"))
        {
            state = State.Fly;
        }
            
    }

    


}
