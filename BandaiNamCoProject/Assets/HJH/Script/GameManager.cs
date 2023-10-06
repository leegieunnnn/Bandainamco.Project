using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserData_HJH
{

}

public class GameManager : MonoBehaviour
{
    public UserData_HJH userData;
    public static GameManager instance = null;

    #region 볼륨 조절 관련
    [SerializeField]
    float volume;

    public List<AudioSource> audios;

    public float Volume
    {
        get
        {
            return volume;
        }
        set
        {
            volume = value;
            if (volume < 0)
            {
                volume = 0;
            }
            else if (volume > 1)
            {
                volume = 1;
            }
            for (int i = 0; i < audios.Count; i++)
            {
                if (audios[i] != null)
                {
                    audios[i].volume = volume;
                }
                else
                {
                    audios.RemoveAt(i);
                    i--;
                }
            }
        }
    }
    private void FindAudioSource()
    {
        List<AudioSource> audioSources = new List<AudioSource>();
        GameObject[] all = FindObjectsOfType<GameObject>();
        AudioSource myAudio = gameObject.GetComponent<AudioSource>();
        foreach (GameObject obj in all)
        {
            AudioSource audio;
            if (obj.TryGetComponent<AudioSource>(out audio))
            {
                if (audio != myAudio)
                {
                    audioSources.Add(audio);
                    audio.volume = volume;
                }
            }
        }
        audios = audioSources;
    }
    #endregion
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindAudioSource();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
