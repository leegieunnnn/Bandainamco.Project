using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockItem_LSW : BaseItem_LSW
{ 
    void Update()
    {

    }

    public override void ItemActivate()
    {  
        // Use Skill
        character.coolTime -= 0.5f;
        Debug.Log("Cool Time Down");
        // animation

       
    }

    public override void ItemDeactivate()
    {
        character.coolTime += 0.5f;

    }



}
