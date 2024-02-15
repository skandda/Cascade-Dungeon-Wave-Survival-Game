using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthManager : MonoBehaviour
{
    public AudioClip powGet;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerTag")
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().StrengthCollection();
            StartCoroutine("PlaySound4");
            gameObject.SetActive(false);
            
        }

    }
    IEnumerator PlaySound4()
    {
        GameObject temp4 = new GameObject();
        temp4.AddComponent<AudioSource>();
        temp4.GetComponent<AudioSource>().clip = powGet;
        temp4.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        temp4.GetComponent<AudioSource>().Stop();
        Destroy(temp4);
    }

    public void Update()
    {
        transform.Rotate(0f, 5f, 0f);
    }
}
