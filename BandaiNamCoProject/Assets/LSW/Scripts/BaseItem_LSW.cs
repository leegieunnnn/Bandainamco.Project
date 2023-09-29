using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using KoreanTyper;
using TMPro;

public abstract class BaseItem_LSW : MonoBehaviour
{
    public int itemNum;
    public ItemManager_LSW itemManager;
    public Transform[] spawnPoints;

    public abstract void UseSkill();
    private void Awake()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager_LSW>();   
    }

    
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            itemManager.TriggerCount(itemNum);
            UseSkill();
        }
    }


}