using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem_LJH : MonoBehaviour
{
    public Item_HJH myItem;
    public GameObject bubble;
    public void Init(Item_HJH item)
    {
        myItem = item;
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemManager_LJH.Instance.itemCount += 1;
            ItemManager_LJH.Instance.CurrItem = this;
            GamePlayManager_HJH.Instance.AddConsumedItem(this);

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
            Animator bubbleAni;
            if (bubble.TryGetComponent<Animator>(out bubbleAni))
            {
                bubbleAni.SetTrigger("Pop");
            }
            ActiveFalse();
        }
    }
    async void ActiveFalse()
    {
        await UniTask.Delay(1000);
        gameObject.SetActive(false);
    }

    public virtual void Reset() { }
}
