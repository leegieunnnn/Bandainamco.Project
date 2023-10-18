using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class ItemManager_LJH : ManagerBase
{
    [SerializeField] private List<GameObject> spawnItems;
    [SerializeField] private Transform itemParent;
    public Item_HJH[] items;
    public float itemsDistance;
    public GameObject player;
    public int itemCount;
    public static ItemManager_LJH Instance;

    public BaseItem_LJH currItem;

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
                item.GetComponent<BaseItem_LJH>().myItem = items[i];

                spawnItems.Add(item);
            }
        }
        base.Init();
    }
}
