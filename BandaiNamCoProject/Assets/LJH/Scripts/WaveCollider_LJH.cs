using Bitgem.VFX.StylisedWater;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollider_LJH : MonoBehaviour
{
    private WaterVolumeTransforms parentWater;

    private void Awake()
    {
        parentWater = GetComponentInParent<WaterVolumeTransforms>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.transform.CompareTag(TagStrings.PlayerTag))
        {
            Debug.Log("Wave Collision : " + collision.collider.name);
            //if (!parentWater.isFinished)
            //   parentWater.SendFinishWave();
        }
    }
}
