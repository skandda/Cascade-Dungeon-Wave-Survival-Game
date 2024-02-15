using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    public bool isSpider = false;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

}
