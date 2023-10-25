using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishItem_HJH : BaseItem_LJH
{
    public float fishTime;
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            other.GetComponent<CharacterMovement2D_LSW>().fish = true;
            other.GetComponent<CharacterMovement2D_LSW>().SetGravity(false);
            other.transform.GetChild(0).gameObject.SetActive(false);
            other.transform.GetChild(1).gameObject.SetActive(true);
            Fish(other.gameObject);
        }
        base.OnTriggerEnter2D(other);

    }

    public async void Fish(GameObject player)
    {
        await UniTask.Delay((int)(fishTime * 1000));
        if(player.GetComponent<CharacterMovement2D_LSW>().fish)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.GetComponent<CharacterMovement2D_LSW>().fish = false;
            player.GetComponent<CharacterMovement2D_LSW>().SetGravity(true);
            player.transform.GetChild(0).gameObject.SetActive(true);
            player.transform.GetChild(1).gameObject.SetActive(false);
        }

    }
}
