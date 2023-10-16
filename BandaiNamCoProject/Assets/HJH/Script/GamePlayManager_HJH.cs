using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GamePlayManager_HJH : MonoBehaviour
{
    public GameObject player;
    public ItemManager_LSW itemManager;
    public float currentTime;
    public TMP_Text timeText;

    public bool gameEnd;
    public float goodEndingTime;

    public GameObject[] endings; //임시 나중에 지울것

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!gameEnd)
        {
            currentTime += Time.deltaTime;
            timeText.text = currentTime.ToString();
        }
        Vector3 pos = Camera.main.WorldToViewportPoint(player.transform.position);
        if(Time.timeScale > 0)
        {
            if (pos.x < 0f || pos.x > 1 || pos.y < 0 || pos.y > 1)
            {
                gameEnd = true;
                Time.timeScale = 0f;
                GameOver();
            }
            if (itemManager.itemCount >= itemManager.items.Length)
            {
                if (currentTime > goodEndingTime)
                {
                    gameEnd = true;
                    Time.timeScale = 0f;
                    BadEnding();
                }
                else
                {
                    gameEnd = true;
                    Time.timeScale = 0f;
                    GoodEnding();
                }
            }
        }
    }

    public void GameOver()
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
    }
}
