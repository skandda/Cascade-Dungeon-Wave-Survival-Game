using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupManager : MonoBehaviour
{
    public AudioClip ammoGet;
    private void Start()
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTag")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().Ammopickup();
            StartCoroutine("PlaySound");
            gameObject.SetActive(false);
        }

    }

    IEnumerator PlaySound()
    {
        GameObject temp = new GameObject();
        temp.AddComponent<AudioSource>();
        temp.GetComponent<AudioSource>().clip = ammoGet;
        temp.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        temp.GetComponent<AudioSource>().Stop();
        Destroy(temp);
    }

    public void Update()
    {
        transform.Rotate(0f, 5f, 0f);
    }
}
