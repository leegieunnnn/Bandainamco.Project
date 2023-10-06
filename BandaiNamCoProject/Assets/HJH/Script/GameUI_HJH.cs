using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI_HJH : MonoBehaviour
{
    public float fadeSpeed;
    public GameObject[] mouseOverImage;
    public GameObject pauseCanvas;
    public GameObject optionCanvas;
    public Slider volumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.onValueChanged.AddListener(VolumeChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionCanvas.activeInHierarchy)
            {
                OptionOffButton();
            }
            else if (!pauseCanvas.activeInHierarchy && Time.timeScale != 0f)
            {
                PauseOnButton();
            }
            else if (pauseCanvas.activeInHierarchy)
            {
                PauseOffButton();
            }
        }
    }
    public void PauseOnButton()
    {
        Time.timeScale = 0f;
        pauseCanvas.SetActive(true);
    }

    public void PauseOffButton()
    {
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
    }

    public void PointOver(int a)
    {
        StopAllCoroutines();
        for (int i = 0; i < mouseOverImage.Length; i++)
        {
            if (i == a)
            {
                StartCoroutine(FadeIn(mouseOverImage[i]));
            }
            else
            {
                StartCoroutine(FadeOut(mouseOverImage[i]));
            }
        }
    }
    IEnumerator FadeIn(GameObject img)
    {
        Image image = img.GetComponent<Image>();
        float alpha = 0;
        Color color = image.color;
        while (alpha < 1f)
        {
            alpha += 0.01f;
            yield return new WaitForSeconds(fadeSpeed);
            color.a = alpha;
            image.color = color;
        }
    }

    IEnumerator FadeOut(GameObject img)
    {
        Image image = img.GetComponent<Image>();
        Color color = image.color;
        float alpha = color.a;
        while (alpha > 0f)
        {
            alpha -= 0.01f;
            yield return new WaitForSeconds(fadeSpeed);
            color.a = alpha;
            image.color = color;
        }
    }

    public void OptionOnButton()
    {
        optionCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void OptionOffButton()
    {
        optionCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    void VolumeChange(float value)
    {
        GameManager.instance.Volume = value;
    }

    public void QuitButton()
    {
        Time.timeScale = 1f;
        LoadingManager_HJH.LoadScene("StartScene");
    }
}
