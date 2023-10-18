using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase : MonoBehaviour
{
    public virtual void Init()
    {
        isInit = true;
    }
    public bool isInit;

    public virtual void GameOver() { }
}
