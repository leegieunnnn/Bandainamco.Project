using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockItem_LSW : BaseItem_LSW
{
    public float coolTimeReduce = 0.5f;

    public override void ItemActivate()
    {
        base.ItemActivate();
        character.coolTime -= coolTimeReduce;
    }

}
