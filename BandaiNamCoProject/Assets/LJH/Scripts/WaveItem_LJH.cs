using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveItem_LJH : BaseItem_LJH
{
    [SerializeField] private float zoomLerpSec = 3f;
    [SerializeField] private int delayTime = 3;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            base.OnTriggerEnter2D(other);
            ItemManager_LJH.Instance.waveObject.StartWave();

            WorldManager.Instance.NotifyItemEffect(myItem.itemType, true);
        }
    }
}
