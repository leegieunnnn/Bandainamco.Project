using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public abstract class BaseItem_LSW : MonoBehaviour
{
    protected string itemName;
    protected int itemAmount;
    
    protected int triggerCount = 0; 

     public Transform[] spawnPoints;

    public abstract void useSkill();
    protected abstract GameObject GetItemPrefabByName(string itemName);
    
    public virtual void spawnPoint()
    {
        // Get the item prefab using the itemName.
        GameObject itemPrefab = GetItemPrefabByName(itemName);

        if (itemPrefab == null)
        {
            Debug.LogError("Item prefab not found for item name: " + itemName);
            return;
        }

        // Generate a random number of items to spawn within the specified amount.
        for (int i = 0; i < itemAmount; i++)
        {

            // Generate a random index to select a spawn point.
            int randomIndex = Random.Range(0, spawnPoints.Length);

            // Instantiate the item at the selected spawn point's position.
            Instantiate(itemPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
        }
    }
    
    public virtual void OnTriggerEnter2D(Collision2D other)
    {
        Debug.Log("MakeContact");
        if (other.gameObject.CompareTag("Player"))
        {
            triggerCount++;
            Debug.Log(triggerCount);
            // Camera zoom out function
            useSkill();
        }
    }
    
    
}