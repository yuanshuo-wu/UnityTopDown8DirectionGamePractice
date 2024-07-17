using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSword : MonoBehaviour
{

    public float rotateSpeed;


    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime));
    }
}
