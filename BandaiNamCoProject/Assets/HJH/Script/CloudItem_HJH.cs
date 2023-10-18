using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudItem_HJH : BaseItem_LSW
{
    public GameObject smallCloud;
    public int cloudNum;
    public float jumpPower;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            itemManager.TriggerCount(itemNum);

            if (character != null)
            {
                character.lastUsedItem = itemNum;
            }
            CloudItemActivate(other.transform.parent.gameObject);
            ItemActivate();

        }
    }

    public void CloudItemActivate(GameObject player)
    {
        for(int i =0; i < cloudNum; i++)
        {
            Instantiate(smallCloud,transform.position,Quaternion.identity);
        }
        Rigidbody2D rigidbody2D = player.GetComponent<Rigidbody2D>();
        rigidbody2D.velocity = Vector3.zero;
        rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }
}
