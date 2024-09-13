using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] Vector3 axis = Vector3.up;
    
    public void Update()
    {
        transform.Rotate(axis, speed * Time.deltaTime);
    }
}
