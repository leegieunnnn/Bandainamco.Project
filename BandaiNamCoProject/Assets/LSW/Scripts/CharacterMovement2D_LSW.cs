using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement2D_LSW : MonoBehaviour
{
    // State�� enum���� ����
    public enum State
    {
        Idle,
        Move,
        Fly,
    }
    public State state;
    // ���� �̵��ӵ��� ������
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
