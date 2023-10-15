using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager_HJH : MonoBehaviour
{
    public GameObject doorPrefab; //문 프리팹
    public List<GameObject> doors; // 생성된 문들
    public int doorCount; // 문 개수
    public float doorInterval; // 문 사이의 간격
    public Sprite[] doorsSprite; //열린문 , 닫힌문
    public Transform startPoint; //문이 생기는 시작점
    public GameObject bg;
    int doorNum;

    #region 컷씬 파라미터
    [Header("컷씬 파라미터")]
    public float cutSceneTime;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        

        for(int i =0; i< doorCount; i++)
        {
            GameObject newDoor = GameObject.Instantiate(doorPrefab);
            doors.Add(newDoor);
            newDoor.transform.position = startPoint.position;
            newDoor.transform.parent = bg.transform;
            newDoor.transform.position = new Vector3(newDoor.transform.position.x + (i*doorInterval),newDoor.transform.position.y,newDoor.transform.position.z);
            if(GameManager.instance != null )
            {
                if(GameManager.instance.userData.stage < i)
                {
                    newDoor.GetComponent<SpriteRenderer>().sprite = doorsSprite[1];
                }
                else
                {
                    newDoor.GetComponent<SpriteRenderer>().sprite = doorsSprite[0];
                }
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
       for(int i =0; i<doorCount; i++)
        {
            if (Mathf.Abs( doors[i].transform.position.x - Camera.main.transform.position.x )< 1f)
            {
                doors[i].transform.GetChild(0).gameObject.SetActive(true);
                doorNum = i;
            }
            else
            {
                doors[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(GameManager.instance != null)
            {
                if(doorNum <= GameManager.instance.userData.stage)
                {
                    doors[doorNum].GetComponent<Animator>().SetTrigger("Open");
                    Invoke("MoveScene", 1f);
                }
            }
        }
    }
    public void MoveScene()
    {
        LoadingManager_HJH.LoadScene("GameScene");
    }
}
