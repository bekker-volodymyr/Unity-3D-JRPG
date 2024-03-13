using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSumDirection : MonoBehaviour
{
    private float speed = 15f;

    void Update()
    {
        this.transform.Rotate(Vector3.right, speed * Time.deltaTime);
    }
}
