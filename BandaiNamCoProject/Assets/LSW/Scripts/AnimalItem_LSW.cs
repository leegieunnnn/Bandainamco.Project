using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AnimalItem_LSW : BaseItem_LSW
{   
    public GameObject[] itemPrefabs;
    public string[] itemNames;


    protected override GameObject GetItemPrefabByName(string itemName)
    {
        // Search for the item name in the itemNames array.
        int index = System.Array.IndexOf(itemNames, itemName);

        if (index != -1 && index < itemPrefabs.Length)
        {
            return itemPrefabs[index]; // Return the corresponding item prefab.
        }
        else
        {
            Debug.LogError("Item prefab not found for item name: " + itemName);
            return null;
        }
    }

   
    public override void SpawnPoint()
    {
        // Random appear , gameobject Type, count, extra
        Debug.Log(itemName + " has appeared!");
        gameObject.SetActive(true);
    }

    public override void UseSkill()
    {
        // Use Skill
        Debug.Log(" Item used! " + itemName);
        // After used
        gameObject.SetActive(false);
        //Destroy(gameObject);
       
    }

   
    public void Start()
    {
        itemName = "TestItem";
        itemAmount = 1;
        SpawnPoint();
    }

    
}
