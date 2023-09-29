using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using KoreanTyper;
using TMPro;

public abstract class BaseItem_LSW : MonoBehaviour
{
    protected string itemName;
    protected int itemAmount;


    protected int triggerCount = 0;
    public ItemManager_LSW itemManager;
    public Transform[] spawnPoints;

    public abstract void UseSkill();
    protected abstract GameObject GetItemPrefabByName(string itemName);
    
    public virtual void SpawnPoint()
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
    
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("MakeContact");
        if (other.gameObject.CompareTag("Player"))
        {
            triggerCount++;
            Debug.Log(triggerCount);
            itemManager.CameraZoomOutFuncStart(Camera.main.transform.position);
            UseSkill();
        }
    }


}