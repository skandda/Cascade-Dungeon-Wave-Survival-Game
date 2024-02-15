using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitManager : MonoBehaviour
{
    public AudioClip medGet;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerTag")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().MedKitCollect();
            StartCoroutine("PlaySound3");
            gameObject.SetActive(false);
            
        }

    }
    IEnumerator PlaySound3()
    {
        GameObject temp3 = new GameObject();
        temp3.AddComponent<AudioSource>();
        temp3.GetComponent<AudioSource>().clip = medGet;
        temp3.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        temp3.GetComponent<AudioSource>().Stop();
        Destroy(temp3);
    }
    public void Update()
    {
        transform.Rotate(5f, 0f, 0f);
    }
}
