using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainItem_HJH : BaseItem_LJH
{
    CharacterMovement2D_LSW player;
    public float trainSpeed;
    public float railSpeed;
    public GameObject trainIcon;
    public GameObject trainRail;
    bool trainStart = false;
    bool left = false;
    bool playerOn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ItemManager_LJH.Instance.itemCount += 1;
            ItemManager_LJH.Instance.CurrItem = this;

            if (!myItem.isVisited)
            {
                WorldManager.Instance.MainState = MainState.Pause;
                if (myItem.needWholeCam)
                {
                    CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), true);
                }
                else
                {
                    CameraManager.Instance.CameraControlAfterItem(myItem.itemType.ToString(), false);

                }
            }
            Animator bubbleAni;
            if (bubble.TryGetComponent<Animator>(out bubbleAni))
            {
                bubbleAni.SetTrigger("Pop");
            }
            myItem.isVisited = true;
            player = other.GetComponent<CharacterMovement2D_LSW>();
            TrainActivate();
        }
    }

    void TrainActivate()
    {
        int ran = Random.Range(0, 2);
        trainStart = true;
        
        player.gameObject.tag = "Untagged";
        player.SetGravity(false);
        trainRail.gameObject.SetActive(true);
        if(ran == 0)
        {
            left = true;
            trainRail.transform.Rotate(0, 180, 0);
        }
        else 
        {
            left = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!trainStart)
        {
            return;
        }
        trainRail.transform.localScale += Vector3.right * Time.timeScale * railSpeed; // 철도 연출 나중에 수정해야 될듯
        if (left)
        {
            trainIcon.transform.position += Vector3.left * trainSpeed * Time.deltaTime;
        }
        else
        {
            trainIcon.transform.position += Vector3.right * trainSpeed * Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.gameObject.tag = "Player";
            playerOn = false;
            player.SetGravity(true);
        }
    }
    private void LateUpdate()
    {
        if (!trainStart)
        {
            return;
        }
        if (Mathf.Abs(trainIcon.transform.position.x) > DataManager.Instance.bgSize.x / 2)
        {
            gameObject.SetActive(false);
        }
        if (playerOn)
        {
            player.transform.position = trainIcon.transform.position;
        }

    }
}
