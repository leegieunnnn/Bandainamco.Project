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
        Die,
    }
<<<<<<< Updated upstream
    public State state;
    // 각각 이동속도와 점프힘
    public float moveSpeed = 100.0f;
    public float flyForce = 200.0f;
    
=======
    private State state = State.Idle;

    // 각각 이동속도와 점프힘
    [SerializeField]
    private float moveSpeed = 50.0f;
    [SerializeField]
    private float flyForce = 200.0f;

>>>>>>> Stashed changes
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
<<<<<<< Updated upstream
        state = State.Idle;
        Debug.Log(transform.up);
=======


>>>>>>> Stashed changes
    }

    private void FixedUpdate()
    {
        if (state == State.Move)
        {
<<<<<<< Updated upstream
            rb.AddForce(transform.up * moveSpeed);
            gameObject.transform.up = rb.velocity;
=======
            rb.velocity = new Vector2(moveSpeed, 0);
>>>>>>> Stashed changes
        }
    }

    void Update()
    {
<<<<<<< Updated upstream
        if(state == State.Idle) 
        {
            rb.velocity = Vector2.zero;
        }
        // space 바를 누르면 이제 스테이트마다 하는 행동 실시
        if (Input.GetKeyDown(KeyCode.Space))
=======
        switch (state)
>>>>>>> Stashed changes
        {
            case State.Idle:
                UpdateIdle();
                break;
            case State.Move:
                UpdateMove();
                break;
            case State.Fly:
                UpdateFly();
                // 땅에 닿았을 때 죽는 상태로
                if (rb.velocity.magnitude > flyForce)
                {
                    state = State.Die;
                }
                break;
            case State.Die:
                // 다시 처음스폰장소 / 아니면 게임오버창으로
                break;
        }
    }

<<<<<<< Updated upstream
    private void OnTriggerEnter(Collider other) //이게 Collider Tag이용해서 Trigger를 노린건데 안됨 그래서 다시 해보고 있음.
=======
    private void UpdateIdle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.Move;
        }
    }

    private void UpdateMove()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = State.Fly;
        }
    }

    private void UpdateFly()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * flyForce, ForceMode2D.Impulse);
        }
    }


}


    /*
    private void startTriggerEnter(Collider2D other) //이게 Collider Tag이용해서 Trigger를 노린건데 안됨 그래서 다시 해보고 있음.
>>>>>>> Stashed changes
    {
        if (state == State.Move && other.CompareTag("StartPoint"))
        {
            state = State.Fly;
        }
            
    }
    */
    
