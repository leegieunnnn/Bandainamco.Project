using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitItrm_HJH : BaseItem_LJH
{
    public bool end = false;
    public CharacterMovement2D_LSW player;
    public GameObject smallRabbit;
    public float duringTime;
    float currentTime;
    bool not = true;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (not)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (ItemManager_LJH.Instance.CurrItem != null)
                {
                    if (ItemManager_LJH.Instance.CurrItem.myItem.itemType != ItemType.Rabbit)
                {
                    smallRabbit.GetComponent<SmallRabbitParent_HJH>().Set();
                }
                    }
                player = other.GetComponent<CharacterMovement2D_LSW>();
                other.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                ItemManager_LJH.Instance.itemCount += 1;
                ItemManager_LJH.Instance.CurrItem = this;

                if (!myItem.isVisited)
                {
                    WorldManager.Instance.MainState = MainState.Pause;
                    if (myItem.needWholeCam)
                    {
                        CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), true);
                    }
                    else
                    {
                        CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), false);

                    }
                }
                myItem.isVisited = true;
                smallRabbit.GetComponent<SmallRabbitParent_HJH>().rabbitItem = this;
                RabbitAni();
                not = false;
            }
        }

    }
    private void Start()
    {
        smallRabbit = GameObject.Find("SmallRabbit");
        duringTime = smallRabbit.GetComponent<SmallRabbitParent_HJH>().duringTime;
    }


    async void RabbitAni()
    {
        smallRabbit.transform.position = transform.position;
        smallRabbit.GetComponent<SmallRabbitParent_HJH>().GoGo();
        while (true)
        {
            currentTime += Time.deltaTime;
            if (currentTime > duringTime)
            {
                end = true;
            }
            if (end)
            {
                gameObject.SetActive(false);
                break;
            }
            await UniTask.Yield();
        }
    }
}
