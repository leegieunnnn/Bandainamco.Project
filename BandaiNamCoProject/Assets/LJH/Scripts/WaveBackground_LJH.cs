using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBackground_LJH : MonoBehaviour
{
    enum MoveState
    {
        Right, Left
    }

    [SerializeField] private float moveOffset;
    [SerializeField] private float moveSpeed;
    [SerializeField] private MoveState moveState;

    Vector3 originPos;
    float elapsedOffset = 0f;

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        CheckDirection();
        elapsedOffset += moveSpeed * Time.unscaledDeltaTime;

        switch (moveState)
        {
            case MoveState.Left:
                originPos += new Vector3(-elapsedOffset, 0f, 0f);
                break;
            case MoveState.Right:
                originPos += new Vector3(elapsedOffset, 0f, 0f);
                break;
        }
    }

    private void CheckDirection()
    {
        if (elapsedOffset > moveOffset)
        {
            moveState = moveState == MoveState.Right ? MoveState.Left : MoveState.Right;
            elapsedOffset = 0f;
        }
    }
}
