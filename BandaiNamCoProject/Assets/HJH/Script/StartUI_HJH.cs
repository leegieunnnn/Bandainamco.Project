using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI_HJH : MonoBehaviour
{
    public GameObject quitPopUp;
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(quitPopUp != null)
            {
                quitPopUp.SetActive(true);
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
    public void MoveScene()
    {
        LoadingManager_HJH.LoadScene("GameScene");
    }
    public void QuitApp()
    {
        Application.Quit();
    }
}