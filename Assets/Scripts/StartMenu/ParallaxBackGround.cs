using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackGround : MonoBehaviour
{
    [SerializeField] private float speed = 0.1f;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    private void LateUpdate()
    {
        Vector3 offSet = new Vector3( Time.time * speed,0,0);
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offSet);
    }
}
