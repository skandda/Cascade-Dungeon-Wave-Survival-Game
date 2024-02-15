using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShotManager : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTag")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().TripleShot();
            gameObject.SetActive(false);
        }

    }

    public void Update()
    {
        transform.Rotate(0f, 5f, 0f);
    }
}
