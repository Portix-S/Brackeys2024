using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,speed * Time.deltaTime * -360f);
    }
}
