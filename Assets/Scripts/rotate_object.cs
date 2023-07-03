using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_object : MonoBehaviour
{
    public float rotation_speed = 60;

    void Update() {
        transform.Rotate(transform.rotation.x, rotation_speed * Time.deltaTime, transform.rotation.z);
    }
}
