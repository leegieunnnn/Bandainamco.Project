using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem : BaseItem
{
    public string TestItem;
    
    public override void Appear()
    {
        // 세부 등장 알고리즘
        // 위치, 색깔
        Debug.Log(itemName + " has appeared!");
        gameObject.SetActive(true);
    }

    public override void UseItem()
    {
        Debug.Log("기능 사용! " + itemName);
        // 기능
        gameObject.SetActive(false);
        //Destroy(gameObject);
       
    }
}
