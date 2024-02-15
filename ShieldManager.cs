using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    public AudioClip shieldGet;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTag")
        {
            
            GameObject.Find("Player").GetComponent<PlayerMovement>().ShieldCollection();
            StartCoroutine("PlaySound2");
            gameObject.SetActive(false);
        }

    }
    IEnumerator PlaySound2()
    {
        GameObject temp2 = new GameObject();
        temp2.AddComponent<AudioSource>();
        temp2.GetComponent<AudioSource>().clip = shieldGet;
        temp2.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        temp2.GetComponent<AudioSource>().Stop();
        Destroy(temp2);
    }

    public void Update()
    {
        transform.Rotate(5f, 0f, 0f);
    }
}

