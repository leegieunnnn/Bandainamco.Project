using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using KoreanTyper;

public abstract class BaseItem_LSW : MonoBehaviour
{
    protected string itemName;
    protected int itemAmount;


    protected int triggerCount = 0; 

     public Transform[] spawnPoints;
    #region CameraZoomOut, 나중에 메니저로 옮길것
    public GameObject bg;
    public CamFollowe_HJH camFollow;
    #endregion
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
            CameraZoomOutFuncStart();
            UseSkill();
        }
    }

    #region CameraZoomOut, 나중에 아이템 메니저로 옮기는게 좋을듯
    public void CameraZoomOutFuncStart()
    {
        Time.timeScale = 0f;
        Vector3 bgSize = GetBGSize(bg);
        camFollow.camFollow = false;
        Camera.main.transform.position = Vector3.zero;
        float bigSize = Mathf.Max(bgSize.x, bgSize.y);
        Camera.main.orthographicSize = bigSize / 2;

    }
    public Vector3 GetBGSize(GameObject bG)
    {
        Vector2 bGSpriteSize = bG.GetComponent<SpriteRenderer>().sprite.rect.size;
        Vector2 localbGSize = bGSpriteSize / bG.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        Vector3 worldbGSize = localbGSize;
        worldbGSize.x *= bG.transform.lossyScale.x;
        worldbGSize.y *= bG.transform.lossyScale.y;
        return worldbGSize;
    }
    IEnumerator CameraZoomOut()
    {
        yield return null;
    }
    //IEnumerator TextAni()
    //{
    //    cutSceneTextEnd = false;
    //    float textScale = cutSceneText[cutSceneIdx].fontSize;
    //    cutSceneText[cutSceneIdx].enableAutoSizing = false;
    //    cutSceneText[cutSceneIdx].fontSize = textScale;
    //    string text = cutSceneText[cutSceneIdx].text;
    //    int typeLength = cutSceneText[cutSceneIdx].text.GetTypingLength();
    //    for (int i = 0; i < typeLength + 1; i++)
    //    {
    //        cutSceneText[cutSceneIdx].text = text.Typing(i);
    //        if (skip)
    //        {
    //            skip = false;
    //            cutSceneText[cutSceneIdx].text = text;
    //            break;
    //        }
    //        yield return new WaitForSeconds(cutSceneSpeed);
    //    }
    //    cutSceneTextEnd = true;
    //}
    #endregion
}