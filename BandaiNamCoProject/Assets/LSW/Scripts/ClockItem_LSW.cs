using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockItem_LSW : BaseItem_LJH
{
    public float coolTimeReduce = 0.5f;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        GamePlayManager_HJH.Instance.characterMovement2D.coolTime -= coolTimeReduce;
        base.OnTriggerEnter2D(other);
    }

    //public override void ItemActivate()
    //{
    //    base.ItemActivate();
    //}

}
