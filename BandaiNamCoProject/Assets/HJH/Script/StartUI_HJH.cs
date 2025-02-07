using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI_HJH : MonoBehaviour
{
    public GameObject quitPopUp;           
    public AudioSource audio;
    public GameObject[] mouseOverImage;
    public float fadeSpeed;
    public GameObject optionCanvas;
    public Slider volumeSlider;


    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = GameManager.instance.userData.volume;
        volumeSlider.onValueChanged.AddListener(VolumeChange);
    }
    void VolumeChange(float value)
    {
        GameManager.instance.Volume = value;
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
            //else if(quitPopUp != null)
            //{
            //    quitPopUp.SetActive(true);
            //}
            else if (quitPopUp.activeInHierarchy)
            {
                QuitOffButton();
            }
        }
    }

    public void StartButton()
    {
        if(audio != null)
        {
            audio.Play();
        }
        Invoke("MoveScene", 0.01f);
    }

    public void NewGameButton()
    {
        GameManager.instance.userData = new UserData_HJH();
        if (audio != null)
        {
            audio.Play();
        }
        Invoke("MoveScene", 0.01f);
    }
    public void MoveScene()
    {
        LoadingManager_HJH.LoadScene("StageScene");
    }
    public void QuitApp()
    {
        Application.Quit();
    }

    public void QuitButton()
    {
        if (!quitPopUp.activeInHierarchy) {
            quitPopUp.SetActive(true);
        }
    }

    public void QuitOffButton()
    {
        if (quitPopUp.activeInHierarchy)
        {
            quitPopUp.SetActive(false);
        }
    }

    public void OptionButton()
    {
        if (!optionCanvas.activeInHierarchy)
        {
            optionCanvas.SetActive(true);
        }
    }

    public void OptionOffButton()
    {
        if(optionCanvas.activeInHierarchy)
        {
            optionCanvas.SetActive(false);
        }
    }

    public void PointOver(int a)
    {
        StopAllCoroutines();
        for(int i =0; i<mouseOverImage.Length; i++)
        {
            if(i == a)
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


}