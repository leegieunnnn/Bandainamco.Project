using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowe_HJH : MonoBehaviour
{
    public float cameraSpeed = 5.0f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(0, dir.y * cameraSpeed * Time.deltaTime, 0.0f);
        if (dir.y > 0)
        {
            transform.Translate(moveVector);
        }

    }
}
