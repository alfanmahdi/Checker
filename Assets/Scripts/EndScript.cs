using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    public static bool GameIsEnd = false;
    public GameObject endMenuUI;
    
    void Awake()
    {
        endMenuUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleEnd()
    {
        endMenuUI.SetActive(!GameIsEnd);
    }

    public void Home()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
