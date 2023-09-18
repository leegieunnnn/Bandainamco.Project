using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D_LSW : MonoBehaviour
{
    // State�� enum���� ����
    private enum State
    {
        Idle,
        Move,
        Fly,
    }
    private State state;
    // ���� �̵��ӵ��� ������
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
        // space �ٸ� ������ ���� ������Ʈ���� �ϴ� �ൿ �ǽ�
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

    private void startTriggerEnter(Collider2D other) //�̰� Collider Tag�̿��ؼ� Trigger�� �븰�ǵ� �ȵ� �׷��� �ٽ� �غ��� ����.
    {
        if (state == State.Move && other.CompareTag("StartPoint"))
        {
            state = State.Fly;
        }
            
    }

    


}
