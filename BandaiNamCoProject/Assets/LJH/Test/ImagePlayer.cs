using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImagePlayer : MonoBehaviour
{
    [SerializeField] private List<Sprite> images;
    [SerializeField] private Image target;
    [SerializeField] private double secPer1Img;

    private int currIndex = 0;
    private bool stop = false;


    private async void PlayImages(bool isActive)
    {
        if (isActive)
        {

            for(int i=0;i < images.Count; i++)
            {
                if (stop) break;
                currIndex = i;
                target.sprite = images[i];
                await UniTask.Delay(TimeSpan.FromSeconds(secPer1Img), ignoreTimeScale: false); //10 s
                await UniTask.Yield();
            }
        }
        else
        {
            stop = true;

            for(int i= currIndex; i>=0; i--)
            {
                target.sprite = images[i];
                await UniTask.Delay(TimeSpan.FromSeconds(secPer1Img), ignoreTimeScale: false); //10 s
                await UniTask.Yield();
            }

            stop = false;
        }
    }

    public void Play()
    {
        PlayImages(true);
    }

    public void Stop()
    {
        PlayImages(false);
    }
}