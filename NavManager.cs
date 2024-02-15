using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavManager : MonoBehaviour
{
    public void onClickCredits()
    {
        SceneManager.LoadScene("Credits_Menu");
    }

    public void onClickH2P()
    {
        SceneManager.LoadScene("Controls_Menu");
    }

    public void onClickPlay()
    {
        SceneManager.LoadScene("Play_Menu");
    }

    public void OnClickHome()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
