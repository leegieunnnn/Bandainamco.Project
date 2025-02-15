using Bitgem.VFX.StylisedWater;
using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemManager_LJH : ManagerBase
{
    [SerializeField] private List<BaseItem_LJH> spawnItems;
    [SerializeField] private Transform itemParent;
    public BaseBackground_LJH[] backgrounds;
    public Item_HJH[] items;
    public float itemsDistance;
    public GameObject player;
    public int itemCount;
    public float xyLine; //그림 끝이랑 너무 까깝지 않게 하기 위해서
    public static ItemManager_LJH Instance;
    public GameObject bubble;

    private BaseItem_LJH currItem;
    public BaseItem_LJH CurrItem
    {
        get { return currItem; }
        set
        {
            prevItem = currItem;
            currItem = value;
        }
    }
    public BaseItem_LJH prevItem;


    public BaseBackground_LJH currBackground;

    [Header("Wave")]
    public WaveCollider_LJH wave;
    public Transform bubbleParent;
    private List<Bubble_LJH> bubbles;

    private void Awake()
    {

    }

    Vector3 Return_RandomPosition()
    {
        float x = UnityEngine.Random.Range(-DataManager.Instance.bgSize.x / 2 + itemsDistance, DataManager.Instance.bgSize.x / 2 - xyLine);
        float y = UnityEngine.Random.Range(-DataManager.Instance.bgSize.y / 2 + itemsDistance, DataManager.Instance.bgSize.y / 2 - xyLine);
        Vector3 randomPostion = new Vector3(x, y, 0);
        return randomPostion;
    }

    public async override void Init()
    {
        await UniTask.WaitUntil(() => DataManager.Instance.isInit);

        Instance = this;

        bubbles = new List<Bubble_LJH>();
        bubbles.AddRange(bubbleParent.GetComponentsInChildren<Bubble_LJH>(true));

        for (int i = 0; i < items.Length; i++)
        {
            for (int j = 0; j < items[i].itemCount; j++)
            {
                GameObject item = Instantiate(items[i].prefab);
                GameObject bub = Instantiate(bubble);
                Vector3 pos;
                int su = 0; //무한루프 방지용
                while (true)
                {
                    su++;
                    pos = Return_RandomPosition();
                    bool restart = false;
                    for (int k = 0; k < spawnItems.Count; k++)
                    {
                        if ((pos - spawnItems[k].transform.position).magnitude < itemsDistance)
                        {
                            restart = true;
                            break;
                        }
                    } // 다른 아이템이랑 너무 가까울 때
                    if ((pos - player.transform.position).magnitude < itemsDistance)
                    {
                        restart = true;
                    } //플레이어랑 너무 가까울 때
                    if (su > 1000)
                    {
                        restart = false;
                    } // 너무 많이 반복할 때
                    if (!restart)
                    {
                        break;
                    }
                }
                item.transform.position = pos;
                item.transform.parent = itemParent;
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.parent.position.z - 5);
                bub.transform.position = item.transform.position;
                bub.transform.parent = item.transform;
                BaseItem_LJH baseItem = item.GetComponent<BaseItem_LJH>();
                baseItem.bubble = bub;
                baseItem.myItem = items[i];

                spawnItems.Add(baseItem);
            }
        }
        base.Init();
    }

    public override void Reset()
    {
        if (prevItem != null) prevItem.Reset();
        //if (currBackground != null) currBackground.Reset();
        base.Reset();
    }

    public void SetActiveItems(bool isActive)
    {
        foreach (var i in spawnItems)
        {
            if (isActive)
            {
                if (GamePlayManager_HJH.Instance.ContainItem(i))
                {
                    i.gameObject.SetActive(false);
                    continue;
                }
            }
            i.gameObject.SetActive(isActive);
        }
    }

    public void SetWave(Action callback = null)
    {
        wave.StartWave();
    }

    public void SetBubble(bool isSet)
    {
        if (isSet)
        {
            foreach (var bubble in bubbles)
                bubble.StartBubble();
        }
        else
        {
            foreach (var bubble in bubbles)
                bubble.FinishBubble();
        }
    }
}
