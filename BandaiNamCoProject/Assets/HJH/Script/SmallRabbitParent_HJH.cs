using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRabbitParent_HJH : MonoBehaviour
{
    public GameObject[] smallRabits;
    public Transform[] transforms;
    public CharacterMovement2D_LSW player;
    public float nextRabbit;
    public RabbitItrm_HJH rabbitItem;
    public float itemDuringTime;
    public float duringTime;
    // Start is called before the first frame update

    public void Set()
    {
        for(int i = 0; i < smallRabits.Length; i++)
        {
            smallRabits[i].transform.position = transform.position;
            smallRabits[i].GetComponent<SmallRabbit_HJH>().Set();
        }
        rabbitItem.gameObject.SetActive(false);
    }
    public void SetChild()
    {
        for (int i = 0; i < smallRabits.Length; i++)
        {
            smallRabits[i].transform.position = transform.position;
            smallRabits[i].GetComponent<SmallRabbit_HJH>().Set();
        }
    }
    public void GoGo()
    {
        StopAllCoroutines();
        StartCoroutine(TimeCheck());
        StartCoroutine(MoveStart());
        
    }

    IEnumerator MoveStart()
    {
        SetChild();
        int movePos = 0;
        int[] before = new int[smallRabits.Length];
        for(int i =0; i<before.Length; i++)
        {
            before[i] = -1;
        }
        for(int i =0; i< transforms.Length; i++)
        {
            int ran = UnityEngine.Random.Range(0, smallRabits.Length);
            while (Array.Exists(before, x => x == ran))
            {
                ran = UnityEngine.Random.Range(0, smallRabits.Length);
            }
            before[i] = ran;
            smallRabits[ran].SetActive(true);
            smallRabits[ran].GetComponent<SmallRabbit_HJH>().trans = transforms[movePos];
            smallRabits[ran].GetComponent<SmallRabbit_HJH>().StartCoroutine(smallRabits[ran].GetComponent<SmallRabbit_HJH>().Move());
            movePos++;
            yield return new WaitForSeconds(nextRabbit);
        }
    }

    IEnumerator TimeCheck()
    {
        yield return new WaitForSeconds(duringTime);
        Set();
    }

    public async void ReturnPower()
    {
        await UniTask.Delay((int)(1000 * itemDuringTime));
        player.jumpPower = player.firstJumpPower;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
