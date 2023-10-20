using Bitgem.VFX.StylisedWater;
using Cysharp.Threading.Tasks;
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
    public static ItemManager_LJH Instance;

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
    public WaterVolumeTransforms waveObject;

    private void Awake()
    {

    }

    Vector3 Return_RandomPosition()
    {
        float x = UnityEngine.Random.Range(-DataManager.Instance.bgSize.x / 2, DataManager.Instance.bgSize.x / 2);
        float y = UnityEngine.Random.Range(-DataManager.Instance.bgSize.y / 2, DataManager.Instance.bgSize.y / 2);
        Vector3 randomPostion = new Vector3(x, y, 0);
        return randomPostion;
    }

    public async override void Init()
    {
        await UniTask.WaitUntil(() => DataManager.Instance.isInit);

        Instance = this;

        for (int i = 0; i < items.Length; i++)
        {
            for (int j = 0; j < items[i].itemCount; j++)
            {
                GameObject item = Instantiate(items[i].prefab);
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
                    }
                    if ((pos - player.transform.position).magnitude < itemsDistance)
                    {
                        restart = true;
                    }
                    if (su > 100)
                    {
                        restart = false;
                    }
                    if (!restart)
                    {
                        break;
                    }
                }
                item.transform.position = pos;
                item.transform.parent = itemParent;
                item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y, item.transform.parent.position.z - 5);
                BaseItem_LJH baseItem = item.GetComponent<BaseItem_LJH>();
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
            if (isActive && i.myItem.isVisited) continue;
            i.gameObject.SetActive(isActive);
        }
    }
}
