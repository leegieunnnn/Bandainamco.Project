using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowe_HJH : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public GameObject player;
    public float smoothing = 0.2f;
    [SerializeField] Vector2 minCameraBoundary;
    [SerializeField] Vector2 maxCameraBoundary;
    public bool camFollow = true;
    public float firstCamSize;
    // Start is called before the first frame update
    void Start()
    {
        firstCamSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {



    }
    private void LateUpdate()
    {
        if (camFollow)
        {
            Vector3 targetPos = new Vector3(player.transform.position.x, player.transform.position.y, this.transform.position.z);

            targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

            transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
        }

    }
}
