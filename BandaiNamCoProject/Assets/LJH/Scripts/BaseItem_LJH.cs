using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem_LJH : MonoBehaviour
{
    public Item_HJH myItem;

    public void Init(Item_HJH item)
    {
        myItem = item;
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemManager_LJH.Instance.itemCount += 1;
            ItemManager_LJH.Instance.currItem = this;
            WorldManager.Instance.MainState = MainState.Pause;

            if (myItem.needWholeCam)
            {
                CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), true);
            }
            else
            {
                CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), false);
            }

            myItem.isVisited = true;

            gameObject.SetActive(false);
        }
    }
}
