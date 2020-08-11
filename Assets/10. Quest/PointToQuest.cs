using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToQuest : MonoBehaviour
{
    public static PointToQuest Instance { get; set; }
    [SerializeField] Transform target;
    [SerializeField] float rotationSpeed;
    [SerializeField] GameObject point;
    private Quaternion lookRotation;
    private Vector3 direction;
    private Vector3 posGoal;
    private bool isOn;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Update()
    {
        if(isOn)
        {
            if(point.activeSelf == false)
            {
                point.SetActive(true);
            }
            direction = (posGoal - transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            if (point.activeSelf == true)
            {
                point.SetActive(false);
            }
        }
    }

    public void UpdateTarget(Vector3 posVector)
    {
        posGoal = posVector;
    }
    public void OnOff(bool isOn)
    {
        this.isOn = isOn;
    }
}
