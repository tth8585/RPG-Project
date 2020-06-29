using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingBar : MonoBehaviour
{
    private void Update()
    {
        Debug.Log(Loader.GetLoadingProgress());
    }
}
