using UnityEngine;

public class StageCamera_HJH : MonoBehaviour
{
    public float cameraMoverSpeed;
    public float startPoint;
    public float endPoint;


    public GameObject background;
    public Vector3 backgroundSize;
    // Start is called before the first frame update
    void Start()
    {
        Vector2 bgSpriteSize = background.GetComponent<SpriteRenderer>().sprite.rect.size;
        Vector2 localBGSize = bgSpriteSize / background.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
        backgroundSize = localBGSize;
        backgroundSize.x *= background.transform.lossyScale.x;
        backgroundSize.y *= background.transform.lossyScale.y;

        float cameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;
        startPoint = background.transform.position.x - backgroundSize.x / 2.0f + cameraWidth;
        endPoint = background.transform.position.x + backgroundSize.x / 2.0f - cameraWidth;

        transform.position = new Vector3(startPoint, 0, -10);
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

        transform.position = new Vector3(startPoint, 0, -10);
    }

    // Update is called once per frame
    void Update()
    {
        float stageMove = Input.GetAxis("Horizontal");
        gameObject.transform.position += new Vector3(stageMove * cameraMoverSpeed, 0, -10);
        gameObject.transform.position = new Vector3(Mathf.Clamp(gameObject.transform.position.x, startPoint, endPoint), 0, 0);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0, -10);

    }
}
