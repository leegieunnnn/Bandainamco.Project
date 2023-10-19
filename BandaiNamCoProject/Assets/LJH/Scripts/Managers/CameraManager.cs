using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;
public class CamValues
{
    public const string Character = "Character";
    public const string Whole = "Whole";
    public const string Wave = "Wave";

    public const int priorityOn = 1;
    public const int priorityOff = 0;
}

public class CameraManager : ManagerBase
{
    public static CameraManager Instance;
    [SerializeField] private SpriteRenderer bgSprite;
    [SerializeField] private CinemachineBrain brainCam;

    private CinemachineVirtualCamera[] virtualCams;
    private Dictionary<string, CinemachineVirtualCamera> virtualCamDic;
    public CinemachineVirtualCamera virtualCamera;
    public float orthographicSizeWhole;
    public string currCamera = CamValues.Character;

    public override void Init()
    {
        Instance = this;
        SetOrthographicSizeWhole();

        virtualCams = brainCam.GetComponentsInChildren<CinemachineVirtualCamera>();
        virtualCamDic = new Dictionary<string, CinemachineVirtualCamera>();

        foreach (var cam in virtualCams)
        {
            string camName = cam.name.Split("_")[1];
            virtualCamDic.Add(camName, cam);
        }
        base.Init();
    }

    private void SetCamera(string cameraName)
    {
        foreach (var cam in virtualCams)
            cam.Priority = CamValues.priorityOff;
        currCamera = cameraName;
        virtualCamDic[cameraName].Priority = CamValues.priorityOn;
    }

    public async void CameraControlAfterItem(string cameraName, bool isFirst = false)
    {
        if (isFirst)
        {
            SetCamera(CamValues.Whole);
        }
        else
        {
            SetCamera(cameraName);
            Camera.main.cullingMask = ~((1 << 7) | (1 << 8));
        }
        await UniTask.Delay(1000,true);
        UIManager.Instance.ControlCloud(async () =>
        {
            await UniTask.Delay(1000,true);
            SetCamera(CamValues.Character);
            StartCoroutine(AfterCameraChange());
        });
        //문구 보여주기
    }

    IEnumerator AfterCameraChange()
    {
        yield return new WaitForSecondsRealtime(2f);
        Camera.main.cullingMask = -1;
        WorldManager.Instance.MainState = MainState.Play;
    }

    private void SetOrthographicSizeWhole()
    {
        Vector2 bgSpriteSize = bgSprite.sprite.rect.size;
        Vector2 localbGSize = bgSpriteSize / bgSprite.sprite.pixelsPerUnit;
        Debug.Log("bgSpriteSize : " + bgSpriteSize);
        Debug.Log("bgSpriteSize / pixelsPerUnit : " + localbGSize);
        Vector3 worldbGSize = localbGSize;
        worldbGSize.x *= bgSprite.transform.lossyScale.x;
        worldbGSize.y *= bgSprite.transform.lossyScale.y;

        orthographicSizeWhole = Mathf.Min(worldbGSize.x, worldbGSize.y);
    }
}
