using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum EndingType
{
    Bad, Over, Good
}

public class GamePlayManager_HJH : ManagerBase
{
    public static GamePlayManager_HJH Instance;

    public CharacterMovement2D_LSW characterMovement2D;
    public GameObject player;
    // public ItemManager_LSW itemManager;
    public float currentTime;
    public TMP_Text timeText;

    public float goodEndingTime;

    public GameObject[] endings; //임시 나중에 지울것
    private EndingType endingType;
    private bool gameEnd = false;
    private List<BaseItem_LJH> consumedItems;

    #region 시작부분
    public GameObject startCloud;
    public bool start = false;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    public override void Init()
    {
        consumedItems = new List<BaseItem_LJH>();
        base.Init();
    }


    public override void GameOver()
    {
        switch (endingType)
        {
            case EndingType.Good:
                endings[1].SetActive(true);
                break;
            case EndingType.Bad:
                endings[2].SetActive(true);
                break;
            case EndingType.Over:
                endings[0].SetActive(true);
                break;
        }
        base.GameOver();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startCloud.SetActive(false);
        }
        if(player.transform.position.y < DataManager.Instance.bgSize.y / 2 && !start)
        {
            start = true;
            CameraManager.Instance.SetCamera(CamValues.Character);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (WorldManager.Instance.MainState != MainState.Play)
        {
            return;
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);
        if (pos.x > 1f || pos.x < 0f || pos.y > 1f || pos.y < 0)
        {
            if (ItemManager_LJH.Instance.CurrItem != null)
            {
                if (ItemManager_LJH.Instance.CurrItem.myItem.itemType == ItemType.Lotus)
                {
                    characterMovement2D.SetPosition(Camera.main.ViewportToWorldPoint(new Vector2(0.5f, 0.5f)));
                    return;
                }
            }
            endingType = EndingType.Over;
            gameEnd = true;

            //Time.timeScale = 0f;
            //GameOver();
        }
        int itemCount = 0;
        for(int i = 0; i< ItemManager_LJH.Instance.items.Length; i++)
        {
            if (ItemManager_LJH.Instance.items[i].isVisited)
            {
                itemCount++;
            }
        }
        if (itemCount >= ItemManager_LJH.Instance.items.Length)
        {
            if (currentTime > goodEndingTime)
            {
                //Time.timeScale = 0f;
                endingType = EndingType.Bad;
                gameEnd = true;
                //BadEnding();
            }
            else
            {
                //Time.timeScale = 0f;
                endingType = EndingType.Good;
                gameEnd = true;
                //GoodEnding();
            }
        }
        if (gameEnd)
        {
            WorldManager.Instance.MainState = MainState.GameFinish;
        }


    }

    //아이템 먹은 후, 플레이어에게 나타날 효과를 switch문으로 정리
    //아이템 먹었을 때 : start = true
    //아이템 효과 끝날 때 : start = false
    public override void ItemEffect(ItemType itemType, bool start)
    {
        if (start)
        {
            switch (itemType)
            {
                case ItemType.Wave:
                    WorldManager.Instance.NotifyReset();
                    ItemManager_LJH.Instance.SetActiveItems(false);
                    SetPlayerGravity(false);
                    SetPlayerJumpPower(0.6f);
                    SetPlayerJumpCoolTime(0f);
                    break;
            }

        }
        else
        {
            switch (itemType)
            {
                case ItemType.Wave:
                    ItemManager_LJH.Instance.SetActiveItems(true);
                    SetPlayerGravity(true);
                    break;
            }

        }
        base.ItemEffect(itemType, start);
    }

    private void SetPlayerGravity(bool hasGravity)
    {
        characterMovement2D.SetGravity(hasGravity);
    }
    
    private void SetPlayerJumpPower(float multiplier)
    {
        characterMovement2D.jumpPower *= multiplier;
    }

    private void SetPlayerJumpCoolTime(float coolTime)
    {
        characterMovement2D.coolTime = coolTime;
    }

    public override void BackgroundEffect(ItemType itemType, bool start)
    {
        base.BackgroundEffect(itemType, start);
    }

    public void AddConsumedItem(BaseItem_LJH item)
    {
        if (consumedItems.Contains(item)) return;
        consumedItems.Add(item);
    }

    public bool ContainItem(BaseItem_LJH item)
    {
        return consumedItems.Contains(item);
    }

    public override void Reset()
    {
        characterMovement2D.Reset();
        base.Reset();
    }

    /*    public void GameOver()
        {
            endings[0].SetActive(true);
        }

        public void GoodEnding()
        {
            endings[1].SetActive(true);
        }

        public void BadEnding()
        {
            endings[2].SetActive(true);
        }*/
}
