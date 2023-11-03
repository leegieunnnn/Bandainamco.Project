using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImagePlayer : MonoBehaviour
{
    [SerializeField] private List<Sprite> images;
    [SerializeField] private Image target;
    [SerializeField] private double secPer1Img;

    private async void PlayImages()
    {
        foreach (var i in images)
        {
            target.sprite = i;
            await UniTask.Delay(TimeSpan.FromSeconds(secPer1Img), ignoreTimeScale: false); //10 s
            await UniTask.Yield();
        }
    }

    public void Play()
    {
        PlayImages();
    }
}