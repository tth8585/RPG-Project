using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    const float MIN_CAM_SIZE = 5f;
    const float MAX_CAM_SIZE = 15f;
    public Transform player;
    Camera camSize;
    private void Start()
    {
        camSize = GetComponent<Camera>();
    }
    private void LateUpdate()
    {
        Vector3 newPos = player.position;
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    public void PlusCamView()
    {
        camSize.orthographicSize = Mathf.Clamp(camSize.orthographicSize + 1f, MIN_CAM_SIZE, MAX_CAM_SIZE);
    }

    public void MinusCamView()
    {
        camSize.orthographicSize = Mathf.Clamp(camSize.orthographicSize - 1f, MIN_CAM_SIZE, MAX_CAM_SIZE);
    }
}
