using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalItem_LSW : BaseItem_LSW
{
    public string TestItem;
    
    public override void Appear()
    {
        // ���� ���� �˰���
        // ��ġ, ����
        Debug.Log(itemName + " has appeared!");
        gameObject.SetActive(true);
    }

    public override void UseItem()
    {
        Debug.Log("��� ���! " + itemName);
        // ���
        gameObject.SetActive(false);
        //Destroy(gameObject);
       
    }
}
