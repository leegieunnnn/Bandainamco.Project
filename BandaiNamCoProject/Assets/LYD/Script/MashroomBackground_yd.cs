using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MashroomBackground_yd : BaseItem_LJH
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //(collision.gameObject);
        }


        //   itemManager.StartCoroutine(itemManager.PlayerScale(collision.transform, scale, resetTime));
        base.OnTriggerEnter2D(collision);

    }

    //public 
    // Update is called once per frame
    void Update()
    {
        
    }
}
