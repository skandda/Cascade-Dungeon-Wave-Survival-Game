using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    CharacterController characterController;
    public static PlayerMovement instance;
    public float speed = 6.0f;
    public float gravity = -9.8f;
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    Rigidbody rb;
    public AudioClip shotSound;
    public float currentHealth;
    public float shieldHealth;
    public float maxHealth = 1000;
    public Image HealthOWOBAR;
    public Text healthP;
    public bool StrengthCollect;
    public float StrengthTime = 3f;
    public float TotalStrengthTime = 0f;
    public float TriShotTime = 7f;
    public float TotalTriShotTime = 0f;
    public Text StrengthUI;
    public Image StrengthImg;
    public Image ShieldUI;
    public Text ShieldUIHealth;
    public int currActiveAmmo;
    public int maxActiveAmmo;
    public int currAmmoReserve;
    public int maxAmmoReserve;
    public Text AmmoCount;
    public int reloadCount;
    public Text ReloadUI;
    public bool Reloading;
    private CharacterController _charCont;
    public bool TriShotBool;
    public float TripleShotDelay = 0.25f;
    public Image TriShotImg;
    public Text TriShotNum;
    public int totalScore;
    bool hit = false;
    string hitTag;

    void Start()
    {
        _charCont = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
        instance = this;

       Cursor.lockState = CursorLockMode.Locked;
       Cursor.visible = false;
    }

    void Update()
    {
        CheckInput();
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charCont.Move(movement);

        HealthOWOBAR.fillAmount = (float)currentHealth / (float)maxHealth;
        HealthPercent();

     if (Input.GetKeyDown(KeyCode.Escape))
     {
         Cursor.lockState = CursorLockMode.None;
         Cursor.visible = true;
     }


        if (StrengthCollect)
        {
           StrengthTime -= Time.deltaTime;
           StrengthUI.text = StrengthTime.ToString("0");

            if (StrengthTime <= TotalStrengthTime)
            {
                StrengthCollect = false;
                StrengthTime = 7f;
                StrengthUI.gameObject.SetActive(false);
                StrengthImg.gameObject.SetActive(false);
            }
        }
        if (TriShotBool)
        {
            TriShotTime -= Time.deltaTime;
            TriShotNum.text = TriShotTime.ToString("0");

            if (TriShotTime <= TotalTriShotTime)
            {
                TriShotBool = false;
                TriShotTime = 3f;
                TriShotNum.gameObject.SetActive(false);
                TriShotImg.gameObject.SetActive(false);
            }
        }
    }


    IEnumerator Fire()
    {
        if(currActiveAmmo >= 1 && !Reloading)
        {
            StartCoroutine("PlayShotSound");
            if (TriShotBool)
            {
                    GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet.GetComponent<BulletScript>().speed = 1000;
                    yield return new WaitForSeconds(TripleShotDelay);
                    GameObject bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet2.GetComponent<BulletScript>().speed = 1000;
                    yield return new WaitForSeconds(TripleShotDelay);
                    GameObject bullet3 = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                    bullet3.GetComponent<BulletScript>().speed = 1000;
            }
            else if(!TriShotBool)
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bullet.GetComponent<BulletScript>().speed = 1000;
                currActiveAmmo--;
                AmmoCount.text = currActiveAmmo.ToString() + "/" + currAmmoReserve.ToString();
            }
        }
    }

    IEnumerator PlayShotSound()
    {
        GameObject temp = new GameObject();
        temp.AddComponent<AudioSource>();
        temp.GetComponent<AudioSource>().clip = shotSound;
        temp.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(2);
        temp.GetComponent<AudioSource>().Stop();
        Destroy(temp);
    }
    IEnumerator Reload()
    {
        Reloading = true;
        ReloadUI.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        ReloadUI.gameObject.SetActive(false);
        if (maxAmmoReserve > 0)
        {
            reloadCount = 12 - currActiveAmmo;
            if (currAmmoReserve - reloadCount < 0)
            {
                currActiveAmmo = currActiveAmmo + currAmmoReserve;
                currAmmoReserve = 0;
                AmmoCount.text = currActiveAmmo.ToString() + "/" + currAmmoReserve.ToString();
                Reloading = false;
            }
            else
            {
                currAmmoReserve = currAmmoReserve - reloadCount;
                currActiveAmmo = currActiveAmmo + reloadCount;
                AmmoCount.text = currActiveAmmo.ToString() + "/" + currAmmoReserve.ToString();
                Reloading = false;
            }

        }

    }
    void CheckInput()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Fire());
        }
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Bighead" || other.gameObject.tag == "Heavy" || other.gameObject.tag == "SBullet") && hit == false)
        {
            hitTag = other.gameObject.tag;

            hit = true;
            if (shieldHealth >= 1)
            {
                shieldHealth--;
                ShieldUIHealth.text = shieldHealth.ToString();
                if(shieldHealth < 1)
                {
                    ShieldUI.gameObject.SetActive(false);
                    ShieldUIHealth.gameObject.SetActive(false);
                }

            }
            else switch (hitTag)
                {
                    case "Bighead":
                        currentHealth -= 75;
                        break;
                    case "Heavy":
                        currentHealth -= 150;
                        break;
                    case "SBullet":
                        currentHealth -= 50;
                        Destroy(other.gameObject);
                        break;
                    default:
                        break;
                }
            if(currentHealth < 1)
            {
                GameEnd();
            }
            Invoke("ResetHit", 1f);
        }
    }

    void GameEnd()
    {
        SceneManager.LoadScene("DeathScreen_Menu");
    }

    void ResetHit()
    {
        hit = false;
    }

    public void HealthPercent()
    {
        int healthPS = (int)currentHealth;
        healthP.text = healthPS.ToString();

    }

    public void MedKitCollect()
    {
        if(currentHealth + 100 > 1000)
        {
            currentHealth = 1000;
        }
        else if(currentHealth + 100 <= 1000)
        {
            currentHealth = currentHealth + 100;
        }
    }

    public void StrengthCollection()
    {
        StrengthCollect = true;
        StrengthTime = 7f;
        StrengthUI.gameObject.SetActive(true);
        StrengthImg.gameObject.SetActive(true);
    }

    public void ShieldCollection()
    {
        shieldHealth = 3;
        ShieldUIHealth.text = shieldHealth.ToString();
        ShieldUI.gameObject.SetActive(true);
        ShieldUIHealth.gameObject.SetActive(true);
    }

    public void Ammopickup()
    {
        currAmmoReserve = currAmmoReserve + 60;
        AmmoCount.text = currActiveAmmo.ToString() + "/" + currAmmoReserve.ToString();
    }

    public void TripleShot()
    {
       TriShotBool = true;
       TriShotTime = 3f;
       TriShotImg.gameObject.SetActive(true);
       TriShotNum.gameObject.SetActive(true);
    }
}
