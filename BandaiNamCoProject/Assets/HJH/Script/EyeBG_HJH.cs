using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBG_HJH : MonoBehaviour
{
    public float eyeCoolTime;
    public float eyeRemainTime;
    float currentTime;
    public GameObject player;
    bool nowEye;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ItemManager_LJH.Instance.items[5].isVisited)
        {
            currentTime += Time.deltaTime;
            if (nowEye)
            {
                if(currentTime > eyeRemainTime)
                {
                    nowEye = false;
                    Camera.main.cullingMask = -1;
                }
            }
            else
            {
                if (currentTime > eyeCoolTime)
                {
                    nowEye = true;
                    Camera.main.cullingMask = ~(1 << 7);
                }
            }

        }
    }
}
