using System.Collections;
using UnityEngine;

public class EyeItem_HJH : BaseItem_LJH
{
    public float zoomOutSpeed = 1;
    public float zoomInSpeed = 1;
    public float eyeTime = 5f;
    Vector3 firstCamPos;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }
    IEnumerator CameraZoomOut(float camSize)
    {
        Camera cam = Camera.main;
        firstCamPos = cam.transform.position;
        while (cam.orthographicSize < camSize || (cam.transform.position - Vector3.zero).magnitude > 0.1f)
        {
            if (cam.orthographicSize < camSize)
            {
                cam.orthographicSize += zoomOutSpeed * Time.unscaledDeltaTime;
            }
            if ((cam.transform.position - Vector3.zero).magnitude > 0.1f)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, Vector3.zero, Time.fixedDeltaTime * 0.15f);
            }
            yield return null;
        }
        yield return new WaitForSecondsRealtime(eyeTime);
        StartCoroutine(CameraZoomIn(cam.GetComponent<CamFollowe_HJH>().firstCamSize));
    }
    IEnumerator CameraZoomIn(float camSize)
    {
        Camera cam = Camera.main;
        while (cam.orthographicSize > camSize || (cam.transform.position - firstCamPos).magnitude < 0.1f)
        {
            if (cam.orthographicSize > camSize)
            {
                cam.orthographicSize -= zoomInSpeed * Time.unscaledDeltaTime;
            }
            if ((cam.transform.position - firstCamPos).magnitude > 0.1f)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, firstCamPos, Time.fixedDeltaTime * 0.15f);
            }
            yield return null;
        }
        Camera.main.cullingMask = -1;
        Time.timeScale = 1f;
        Camera.main.transform.position = firstCamPos;
        cam.GetComponent<CamFollowe_HJH>().camFollow = true;
        gameObject.SetActive(false);
    }
}
