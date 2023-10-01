using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusItem_LSW : BaseItem_LSW
{
    GameObject player;
    
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

    }

    public override void ItemActivate()
    {
        if (player != null)
        {
            // Get the player's position
            Vector3 playerPosition = player.transform.position;
        
            if (playerPosition.x > itemManager.bgSize.x/2 || playerPosition.x < - itemManager.bgSize.x/2)
                {
                    if (playerPosition.y > itemManager.bgSize.y/2 || playerPosition.y < -itemManager.bgSize.y/2)
                    {
                        player.transform.position = new Vector3 (0,0,0);
                    }
                }
        }
               
           
        // animation

        // After used
        gameObject.SetActive(false);
        //Destroy(gameObject)   
    }

    public override void ItemDeactivate()
    {
        return;
    }



}
