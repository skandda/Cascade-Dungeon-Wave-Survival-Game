using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mobspawnScript : MonoBehaviour
{
    public static mobspawnScript instance;
    public AudioClip nextWave;
    AudioSource audioSource;
    public enum MobTypes { Grunt, Heavy, Ranged };
    public Text WaveCount;
    public GameObject grunt;
    public float gruntSpawnRate;
    public GameObject heavy;
    public float heavySpawnRate;
    public GameObject ranged;
    public float rangedSpawnRate;
    public float cooldownTime;
    public int currentWave;
    public Text WaveIncoming;

    public bool canDoCooldown;

    public int numberOfEnemies;
    public int enemyIncreaseRate;
    
    public List<GameObject> waveSpawnerList;

    public List<GameObject> currentEnemies;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
        currentWave = 1;

        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemies.Count == 0 && canDoCooldown)
        {
            canDoCooldown = false;
            StartCoroutine("CoolDown");
        }
    }
    void Spawn()
    {
        for(int i = 0; i < numberOfEnemies; i++)
        {
            // Find a random index between zero and one less than the number of spawn points.
            int spawnPointIndex = Random.Range(0, waveSpawnerList.Count);

            // Decide which enemy spawns based on percent chance
            int enemyPercentIndex = Random.Range(0, 100);

            if (enemyPercentIndex >= 0 && enemyPercentIndex <= gruntSpawnRate)
            {
                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.

                GameObject currEnemy = (GameObject)Instantiate(grunt, waveSpawnerList[spawnPointIndex].transform.position, waveSpawnerList[spawnPointIndex].transform.rotation);

                currentEnemies.Add(currEnemy);
            }
            else if (enemyPercentIndex > gruntSpawnRate && enemyPercentIndex <= gruntSpawnRate + heavySpawnRate)
            {
                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                GameObject currEnemy = (GameObject)Instantiate(heavy, waveSpawnerList[spawnPointIndex].transform.position, waveSpawnerList[spawnPointIndex].transform.rotation);

                currentEnemies.Add(currEnemy);
            }
            else
            {
                // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
                GameObject currEnemy = (GameObject)Instantiate(ranged, waveSpawnerList[spawnPointIndex].transform.position, waveSpawnerList[spawnPointIndex].transform.rotation);

                currentEnemies.Add(currEnemy);
            }
        }

        canDoCooldown = true;
    }

    IEnumerator CoolDown()
    {
        WaveIncoming.gameObject.SetActive(true);
        audioSource.clip = nextWave;
        audioSource.Play();
        yield return new WaitForSeconds(cooldownTime);
        audioSource.Stop();
        currentWave++;
        CrossSceneManager.instance.EndWave = currentWave;
        WaveCount.text = currentWave.ToString();
        numberOfEnemies += enemyIncreaseRate;
        WaveIncoming.gameObject.SetActive(false);

        Spawn();
    }

    public void RemoveEnemyFromList(GameObject Enemy)
    {
        currentEnemies.Remove(Enemy);
    }
}


