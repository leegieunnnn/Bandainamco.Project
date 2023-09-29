using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using KoreanTyper;

public class ItemManager_LSW : MonoBehaviour
{
    #region CameraZoomOut
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
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
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

    #region CameraZoomOut
    public void CameraZoomOutFuncStart(Vector3 firstPos)
    {
        Time.timeScale = 0f;
        firstCamPos = firstPos;
        Vector3 bgSize = GetBGSize(bg);
        camFollow.camFollow = false;
        float bigSize = Mathf.Max(bgSize.x, bgSize.y);
        StartCoroutine(CameraZoomOut(bigSize / 2));
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
    IEnumerator CameraZoomOut(float camSize)
    {
        Camera cam = Camera.main;
        while (cam.orthographicSize < camSize || (cam.transform.position - Vector3.zero).magnitude > 0.1f)
        {
            if(cam.orthographicSize < camSize)
            {
                cam.orthographicSize += zoomOutSpeed * Time.unscaledDeltaTime;
            }
            if((cam.transform.position - Vector3.zero).magnitude > 0.1f)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, Vector3.zero, Time.fixedDeltaTime * 0.15f);
            }
            yield return null;
        }
        StartCoroutine(TextAni(whatText));

    }
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
        while (cam.orthographicSize > camSize || (cam.transform.position - firstCamPos).magnitude < 0.1f)
        {
            if (cam.orthographicSize > camSize)
            {
                cam.orthographicSize -= zoomInSpeed * Time.unscaledDeltaTime;
            }
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
    #endregion
}
