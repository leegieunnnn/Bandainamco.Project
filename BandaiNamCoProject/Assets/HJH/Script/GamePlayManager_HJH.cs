using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum EndingType
{
    Bad, Over, Good
}

public class GamePlayManager_HJH : ManagerBase
{
    public static GamePlayManager_HJH Instance;

    public CharacterMovement2D_LSW characterMovement2D;
    public GameObject player;
    // public ItemManager_LSW itemManager;
    public float currentTime;
    public TMP_Text timeText;

    public float goodEndingTime;

    public GameObject[] endings; //임시 나중에 지울것
    private EndingType endingType;
    private bool gameEnd;

    private void Awake()
    {
        Instance = this;
    }

    public override void Init()
    {
        base.Init();
    }


    public override void GameOver()
    {
        switch (endingType)
        {
            case EndingType.Good:
                endings[1].SetActive(true);
                break;
            case EndingType.Bad:
                endings[2].SetActive(true);
                break;
            case EndingType.Over:
                endings[0].SetActive(true);
                break;
        }
        base.GameOver();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (gameEnd || CameraManager.Instance.currCamera != CamValues.Character) return;

        Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);
        if (pos.x > Screen.width || pos.x < 0 || pos.y > Screen.height || pos.y < 0)
        {
            endingType = EndingType.Over;
            gameEnd = true;

            //Time.timeScale = 0f;
            //GameOver();
        }
        if (ItemManager_LJH.Instance.itemCount >= ItemManager_LJH.Instance.items.Length)
        {
            if (currentTime > goodEndingTime)
            {
                //Time.timeScale = 0f;
                endingType = EndingType.Bad;
                gameEnd = true;
                //BadEnding();
            }
            else
            {
                //Time.timeScale = 0f;
                endingType = EndingType.Good;
                gameEnd = true;
                //GoodEnding();
            }
        }
        if (gameEnd)
        {
            WorldManager.Instance.MainState = MainState.GameFinish;
        }


    }



    /*    public void GameOver()
        {
            endings[0].SetActive(true);
        }

        public void GoodEnding()
        {
            endings[1].SetActive(true);
        }

        public void BadEnding()
        {
            endings[2].SetActive(true);
        }*/
}
