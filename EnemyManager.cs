using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth = 3;
    public Text ScoreUI;
    public int score;
    public NavMeshAgent agent;
    public Transform target;
    public int randomPower;
    public GameObject MedPack;
    public GameObject Shield;
    public GameObject Strength;
    public GameObject TriShotP;
    public GameObject Ammo;
    GameObject deathSpawner;

    Vector3 objectSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            if(PlayerMovement.instance.StrengthCollect)
            {
                currentHealth = currentHealth - 4;
            }
            else if(!PlayerMovement.instance.StrengthCollect)
            {
                currentHealth--;
                Destroy(other.gameObject);

            }

        }
    }
    private void Start()
    {
        currentHealth = maxHealth;

        ScoreUI = GameObject.Find("ScoreText").GetComponent<Text>();

        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;

        deathSpawner = GameObject.Find("PickUpSpawner");
    }
    public void Update()
    {
        agent.destination = target.position;
        if (currentHealth <= 0)
        {
            mobspawnScript.instance.RemoveEnemyFromList(this.gameObject.transform.parent.gameObject);
            deathSpawner.transform.position = gameObject.transform.position;
            deathSpawner.transform.rotation = gameObject.transform.rotation;
            Destroy(gameObject);
            RandomPowerUp();
            PlayerMovement.instance.totalScore += score;

            ScoreUI.text = "Score: " + PlayerMovement.instance.totalScore.ToString();
        }

    }
    public void RandomPowerUp()
    {
        
     
        RaycastHit hit;
        if (Physics.Raycast(deathSpawner.transform.position, new Vector3(0, -1, 0), out hit))
        {
            objectSpawn = hit.point;
        }
     
        randomPower = Random.Range(1, 20);
        switch(randomPower)
        {
           
            case 1:
                Instantiate(MedPack, objectSpawn + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, -90f));
                break;
            case 2:
                Instantiate(Shield, objectSpawn + new Vector3(0, -.5f, 0), Quaternion.Euler(0, 0, -90f));
                break;
            case 3:
                Instantiate(TriShotP, objectSpawn + new Vector3(0, 0f, 0), deathSpawner.transform.rotation);
                break;
            case 4:
                Instantiate(Strength, objectSpawn + new Vector3(0, 0, 0), deathSpawner.transform.rotation);
                break;
            case 5:
            case 6:
            case 7:
                Instantiate(Ammo, objectSpawn + new Vector3(0, .3f, 0), deathSpawner.transform.rotation);
                break;
            default:
                break;
        }
     
    }


}
