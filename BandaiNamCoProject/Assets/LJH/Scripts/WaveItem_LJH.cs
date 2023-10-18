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
            //if (myItem.isVisited)
            //    CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), false);
            //else
            //    CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), true);

            
        }
    }
}
