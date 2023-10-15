using System.Collections;
using UnityEngine;

public class StageCamera_HJH : MonoBehaviour
{
    public AudioSource[] walkAudio;
    int walkCount = 0;
    public float cameraMoverSpeed;
    public float startPoint;
    public float endPoint;

    public float upPower;
    public bool walk;
    public float walkTime;
    public float walkWaitTime;
    float currentTime;

    public GameObject background;
    public Vector3 backgroundSize;

    #region ÄÆ¾À ÆÄ¶ó¹ÌÅÍ
    [Header("ÄÆ¾À ÆÄ¶ó¹ÌÅÍ")]
    public float cutSceneTime;
    public float cameraSize;
    public GameObject leftDoor;
    public GameObject rightDoor;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartCutScene());
    }
    IEnumerator StartCutScene()
    {
        Time.timeScale = 0f;
        Camera cam = Camera.main;
        float x = cameraSize - cam.orthographicSize; 
        while (true)
        {
            cam.orthographicSize += x*(cutSceneTime *0.01f) ;
            yield return new WaitForSecondsRealtime(0.01f);
            if(cam.orthographicSize >= cameraSize)
            {
                cam.orthographicSize = cameraSize;
                break;
            }
        }
        Time.timeScale = 1f;
        FirstSetting();
    }
    public void FirstSetting()
    {
        Vector2 bgSpriteSize = background.GetComponent<SpriteRenderer>().sprite.rect.size;
        Vector2 localBGSize = bgSpriteSize / background.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        backgroundSize = localBGSize;
        backgroundSize.x *= background.transform.lossyScale.x;
        backgroundSize.y *= background.transform.lossyScale.y;
        float cameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        startPoint = background.transform.position.x - backgroundSize.x / 2.0f + cameraWidth;
        endPoint = background.transform.position.x + backgroundSize.x / 2.0f - cameraWidth;

        transform.position = new Vector3(0, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        float stageMove = Input.GetAxisRaw("Horizontal");
        if (!walk && stageMove != 0 && Time.timeScale != 0)
        {
            Debug.Log("?");
            StartCoroutine(Walk(stageMove));
            walkCount++;
            if(walkCount > 1)
            {
                walkCount = 0;
            }
        }
    }
    IEnumerator Walk(float stageMove)
    {
        walk = true;
        walkAudio[walkCount].Play();
        while (true)
        {
            currentTime += Time.deltaTime;
            if(currentTime > walkTime)
            {
                break;
            }
            yield return null;
            if(currentTime < walkTime / 2)
            {
                gameObject.transform.position += new Vector3(stageMove * cameraMoverSpeed, upPower, -10);
            }
            else
            {
                gameObject.transform.position += new Vector3(stageMove * cameraMoverSpeed, -upPower, -10);
            }
            gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, startPoint, endPoint), gameObject.transform.position.y, -10);
        }
        yield return new WaitForSeconds(walkWaitTime);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, -10);
        currentTime = 0;
        walk = false;

    }

}
