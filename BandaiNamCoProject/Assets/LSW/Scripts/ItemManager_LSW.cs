using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KoreanTyper;
using System;
using Unity.Mathematics;

[Serializable]
public class Item_HJH
{
    //오브젝트 프리팹
    public GameObject prefab;
    //오브젝트 개수
    public int itemCount;
    //부딪힌 수
    public int triggerCount;
    //줌 될 위치
    public Transform zoomPosition;
}

public class ItemManager_LSW : MonoBehaviour
{

    public Item_HJH[] items;
    public GameObject bg;
    public CamFollowe_HJH camFollow;
    public float zoomOutSpeed;
    public float zoomInSpeed;
    public TMP_Text subText;
    public string whatText;
    public bool nowText = false;
    public bool skip = false;
    public bool endText = false;
    public float typingSpeed = 0.1f;
    Vector3 firstCamPos;
    public Vector3 bgSize;
    // Start is called before the first frame update
    void Start()
    {
        bgSize = GetBGSize(bg);
        for (int i = 0; i < items.Length; i++)
        {
            for(int j = 0; j < items[i].itemCount; j++)
            {
                GameObject item = Instantiate(items[i].prefab);
                item.transform.position = Return_RandomPosition();
                item.transform.parent = bg.transform;
                item.GetComponent<BaseItem_LSW>().itemNum = i;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (nowText)
        {
            if(Input.GetMouseButtonDown(0))
            {
                skip = true;
            }
        }
        if(endText)
        {
            if(Input.GetMouseButtonDown (0))
            {
                endText = false;
                subText.gameObject.SetActive(false);
                StartCoroutine(CameraZoomIn(camFollow.firstCamSize));
            }
        }
        
    }

    public void TriggerCount(int su)
    {
        items[su].triggerCount++;
        if (items[su].triggerCount == 1)
        {
            CameraZoomOutFuncStart(su);
        }
    }
    Vector3 Return_RandomPosition()
    {

        float x = UnityEngine.Random.Range(-bgSize.x / 2, bgSize.x / 2);
        float y = UnityEngine.Random.Range(-bgSize.y/2, bgSize.y / 2);
        Vector3 randomPostion = new Vector3(x, y, 0);
        return randomPostion;
    }

    #region CameraZoomOut
    public void CameraZoomOutFuncStart(int itemIdx)
    {
        Time.timeScale = 0f;
        Camera.main.cullingMask = ~((1 << 7) | (1 << 8));
        firstCamPos = Camera.main.transform.position;
        camFollow.camFollow = false;
        float bigSize = Mathf.Max(bgSize.x, bgSize.y);
        StartCoroutine(CameraZoomOut(items[itemIdx].zoomPosition));
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
    IEnumerator CameraZoomOut(Transform cameraPoint)
    {
        Camera cam = Camera.main;
        while ((cam.transform.position - cameraPoint.position).magnitude > 0.1f)
        {
            if ((cam.transform.position - cameraPoint.position).magnitude > 0.1f)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, cameraPoint.position, Time.fixedDeltaTime * 0.15f);
            }
            yield return null;
        }
        StartCoroutine(TextAni(whatText));

    }
    //IEnumerator CameraZoomOut(float camSize) 구버전
    //{
    //    Camera cam = Camera.main;
    //    while (cam.orthographicSize < camSize || (cam.transform.position - Vector3.zero).magnitude > 0.1f)
    //    {
    //        if(cam.orthographicSize < camSize)
    //        {
    //            cam.orthographicSize += zoomOutSpeed * Time.unscaledDeltaTime;
    //        }
    //        if((cam.transform.position - Vector3.zero).magnitude > 0.1f)
    //        {
    //            cam.transform.position = Vector3.Lerp(cam.transform.position, Vector3.zero, Time.fixedDeltaTime * 0.15f);
    //        }
    //        yield return null;
    //    }
    //    StartCoroutine(TextAni(whatText));

    //}
    IEnumerator TextAni(string text)
    {
        subText.gameObject.SetActive(true);
        int typeLength = text.GetTypingLength();
        nowText = true;
        for (int i = 0; i < typeLength + 1; i++)
        {
            subText.text = text.Typing(i);
            if (skip)
            {
                skip = false;
                nowText = false;
                subText.text = text;
                break;
            }
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
        nowText = false;
        endText = true;
    }
    IEnumerator CameraZoomIn(float camSize)
    {
        Camera cam = Camera.main;
        Camera.main.cullingMask = -1;
        while ((cam.transform.position - firstCamPos).magnitude > 0.1f)
        {
            if ((cam.transform.position - firstCamPos).magnitude > 0.1f)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, firstCamPos, Time.fixedDeltaTime * 0.15f);
            }
            yield return null;
        }
        Time.timeScale = 1f;
        Camera.main.transform.position = firstCamPos;
        camFollow.camFollow = true;
    }
    //IEnumerator CameraZoomIn(float camSize) 구버전
    //{
    //    Camera cam = Camera.main;
    //    while (cam.orthographicSize > camSize || (cam.transform.position - firstCamPos).magnitude < 0.1f)
    //    {
    //        if (cam.orthographicSize > camSize)
    //        {
    //            cam.orthographicSize -= zoomInSpeed * Time.unscaledDeltaTime;
    //        }
    //        if ((cam.transform.position - firstCamPos).magnitude > 0.1f)
    //        {
    //            cam.transform.position = Vector3.Lerp(cam.transform.position, firstCamPos, Time.fixedDeltaTime * 0.15f);
    //        }

    //        yield return null;
    //    }
    //    Camera.main.cullingMask = -1;
    //    Time.timeScale = 1f;
    //    Camera.main.transform.position = firstCamPos;
    //    camFollow.camFollow = true;
    //}
    #endregion
}
