using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_yd : BaseItem_LSW
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        
     //   itemManager.StartCoroutine(itemManager.PlayerScale(collision.transform, scale, resetTime));
        base.OnTriggerEnter2D(collision);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
