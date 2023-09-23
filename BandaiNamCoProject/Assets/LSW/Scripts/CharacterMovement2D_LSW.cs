using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D_LSW : MonoBehaviour
{
    // State를 enum으로 정리
    public enum State
    {
        Idle,
        Move,
        Fly,
    }
    public State state;
    // 각각 이동속도와 점프힘
    public float moveSpeed = 100.0f;
    public float flyForce = 200.0f;
    
    private Rigidbody2D rb;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        state = State.Idle;
        Debug.Log(transform.up);
    }

    private void FixedUpdate()
    {
       if (state == State.Move)
        {
            rb.AddForce(transform.up * moveSpeed);
            gameObject.transform.up = rb.velocity;
        }
    }

    void Update()
    {
        if(state == State.Idle) 
        {
            rb.velocity = Vector2.zero;
        }
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
