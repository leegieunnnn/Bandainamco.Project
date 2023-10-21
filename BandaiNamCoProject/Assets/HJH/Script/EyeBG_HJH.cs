using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBG_HJH : MonoBehaviour
{
    public float eyeCoolTime;
    public float eyeRemainTime;
    float currentTime;
    public GameObject eyeCanvas;
    bool nowEye;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldManager.Instance.MainState == MainState.Play && CameraManager.Instance.currCamera == CamValues.Character)
        {
            if (ItemManager_LJH.Instance.items[4].isVisited)
            {
                currentTime += Time.deltaTime;
                if (nowEye)
                {
                    if (currentTime > eyeRemainTime)
                    {
                        nowEye = false;
                        eyeCanvas.SetActive(false);
                        Camera.main.cullingMask = -1;
                        currentTime = 0;
                    }
                }
                else
                {
                    if (currentTime > eyeCoolTime)
                    {
                        nowEye = true;
                        eyeCanvas.SetActive(true);
                        Camera.main.cullingMask = ~(1 << 7);
                        currentTime = 0;
                    }
                }

            }
        }
        else
        {
            nowEye = false;
            eyeCanvas.SetActive(false);
            currentTime = 0;
        }
    }
}
