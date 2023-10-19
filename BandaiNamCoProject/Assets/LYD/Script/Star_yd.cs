using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star_yd : BaseItem_LSW
{
    [SerializeField] private GameObject starImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 oriPos = collision.transform.position;
        Vector3 startPos = new Vector3(oriPos.x, oriPos.y + 5, oriPos.z);
        collision.transform.position = Vector3.Lerp(oriPos, startPos, 1f);
        //러프로 올라갈때 별이 움직이면서 올라가도록 

        //   itemManager.StartCoroutine(itemManager.PlayerScale(collision.transform, scale, resetTime));
        base.OnTriggerEnter2D(collision);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
