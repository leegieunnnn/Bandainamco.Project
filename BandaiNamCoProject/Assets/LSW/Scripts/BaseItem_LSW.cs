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
    public CharacterMovement2D_LSW character;

    public abstract void ItemActivate();
    public abstract void ItemDeactivate();
    private void Awake()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager_LSW>();
        character = GameObject.Find("Player").GetComponent<CharacterMovement2D_LSW>();
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {  
            itemManager.TriggerCount(itemNum);

            if(character != null)
            {
                character.lastUsedItem.ItemDeactivate();
            }
            ItemActivate();
            
        }
    }
}

