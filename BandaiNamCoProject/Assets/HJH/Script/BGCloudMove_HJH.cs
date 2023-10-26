using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGCloudMove_HJH : MonoBehaviour
{
    public float startX;
    public float endX;
    //float speed;
    // Start is called before the first frame update
    void Start()
    {
        //speed = Random.Range(0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * 10 * Time.deltaTime;
        if(transform.position.x > endX)
        {
            transform.position = new Vector3(startX,transform.position.y,transform.position.z);
        }
    }
}
