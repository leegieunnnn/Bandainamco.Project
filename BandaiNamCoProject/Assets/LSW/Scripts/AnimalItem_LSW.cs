using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AnimalItem_LSW : BaseItem_LSW
{
    public override void UseSkill()
    {
        // Use Skill

        // After used
        gameObject.SetActive(false);
        //Destroy(gameObject);
       
    }

   
    public void Start()
    {

    }

    
}
