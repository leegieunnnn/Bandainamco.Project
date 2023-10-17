using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveItem_LJH : BaseItem_LSW
{
    [SerializeField] private float zoomLerpSec = 3f;
    [SerializeField] private int delayTime = 3;

    private Camera cam;
    private CamFollowe_HJH camFollow;

    private void Awake()
    {
        cam = Camera.main;
        camFollow = cam.GetComponent<CamFollowe_HJH>();
    }

    public override async void ItemActivate()
    {
        base.ItemActivate();
        itemManager.StartWave();

        if (itemManager.items[4].triggerCount != 1)
        {
            gameObject.SetActive(true);
            float size = Mathf.Min(itemManager.bgSize.x, itemManager.bgSize.y) / 2;
            Time.timeScale = 0f;
            Camera.main.cullingMask = ~((1 << 8));
            camFollow.camFollow = false;
            for (int i = 0; i < itemManager.zoomInOffObject.Length; i++)
            {
                itemManager.zoomInOffObject[i].SetActive(false);
            }
            //StartCoroutine(CameraZoomOut(size));
            await CameraZoomOut(size);
        }
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    private async UniTask CameraZoomOut(float camSize)
    {
        float elapsedTime = 0f;
        float originCamOrthSize = cam.orthographicSize;
        Vector3 originCamPos = cam.transform.position;
        while(elapsedTime < zoomLerpSec)
        {
            cam.orthographicSize = Mathf.Lerp(originCamOrthSize, camSize, elapsedTime / zoomLerpSec);
            cam.transform.position = Vector3.Lerp(originCamPos, Vector3.zero, elapsedTime / zoomLerpSec);
            elapsedTime += Time.fixedDeltaTime;

            await UniTask.Yield();
        }

        //await UniTask.Delay(delayTime * 1000);

        await CameraZoomIn(camFollow.firstCamSize, originCamPos);

    }
    private async UniTask CameraZoomIn(float camSize, Vector3 originCamPos)
    {
        float elapsedTime = 0f;
        float originCamOrthSize = cam.orthographicSize;
        Vector3 nowCamPos = cam.transform.position;
        
        while(elapsedTime < zoomLerpSec)
        {
            cam.orthographicSize = Mathf.Lerp(originCamOrthSize, camSize, elapsedTime/ zoomLerpSec);
            cam.transform.position = Vector3.Lerp(nowCamPos, originCamPos, elapsedTime / zoomLerpSec);
            elapsedTime += Time.fixedDeltaTime;

            await UniTask.Yield();
        }


        Camera.main.cullingMask = -1;
        Time.timeScale = 1f;
        camFollow.camFollow = true;
        gameObject.SetActive(false);
    }
}
