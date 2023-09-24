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
        Die,
    }
<<<<<<< Updated upstream
    public State state;
    // ���� �̵��ӵ��� ������
    public float moveSpeed = 100.0f;
    public float flyForce = 200.0f;
    
=======
    private State state = State.Idle;

    // ���� �̵��ӵ��� ������
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
        // space �ٸ� ������ ���� ������Ʈ���� �ϴ� �ൿ �ǽ�
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
                // ���� ����� �� �״� ���·�
                if (rb.velocity.magnitude > flyForce)
                {
                    state = State.Die;
                }
                break;
            case State.Die:
                // �ٽ� ó��������� / �ƴϸ� ���ӿ���â����
                break;
        }
    }

<<<<<<< Updated upstream
    private void OnTriggerEnter(Collider other) //�̰� Collider Tag�̿��ؼ� Trigger�� �븰�ǵ� �ȵ� �׷��� �ٽ� �غ��� ����.
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
    private void startTriggerEnter(Collider2D other) //�̰� Collider Tag�̿��ؼ� Trigger�� �븰�ǵ� �ȵ� �׷��� �ٽ� �غ��� ����.
>>>>>>> Stashed changes
    {
        if (state == State.Move && other.CompareTag("StartPoint"))
        {
            state = State.Fly;
        }
            
    }
    */
    
