using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockItem_LSW : BaseItem_LSW
{ 


    public override void ItemActivate()
    {
        character.lastUsedItem = 1;
        character.coolTime -= 0.5f;
        Debug.Log("Cool Time Down");
        Debug.Log(character.coolTime);
        Debug.Log("Animaiton");
        gameObject.SetActive(false);

        // animation
        
    
        
    }

}
