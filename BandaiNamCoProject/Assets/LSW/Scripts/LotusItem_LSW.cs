using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusItem_LSW : BaseItem_LSW
{
   

    public override void ItemActivate()
    {
        // animation
        character.lastUsedItem = 2;
        Debug.Log("Item Activated");
        Debug.Log("Animaiton");
        // After used
        gameObject.SetActive(false);
        //Destroy(gameObject)   
    }


}
