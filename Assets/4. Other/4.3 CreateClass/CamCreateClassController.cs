
using UnityEngine;

public class CamCreateClassController : MonoBehaviour
{
    [SerializeField] Transform[] transformTarget;

    bool isMovingA;
    bool isMovingB;
    bool isMovingC;
    Vector3 _velocity = Vector3.zero;
    float moveSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //transform
            isMovingA = true;
            isMovingB = false;
            isMovingC = false;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //transform
            isMovingA = false;
            isMovingB = false;
            isMovingC = true;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //transform
            isMovingA = false;
            isMovingB = true;
            isMovingC = false;
        }

        if (isMovingA)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transformTarget[0].position, ref _velocity, Time.deltaTime * moveSpeed);
        }
        if (isMovingB)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transformTarget[1].position, ref _velocity, Time.deltaTime * moveSpeed);
        }
        if (isMovingC)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transformTarget[2].position, ref _velocity, Time.deltaTime * moveSpeed);
        }
    }
}
