using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameController : MonoBehaviour
{
    [SerializeField] 
    public GameObject pausePanel;

    void Start()
    {
        PauseGame();
    }

    void Update()
    {
        if(Input.GetKeyDown (KeyCode.Escape)) 
        {

            if (!pausePanel.activeInHierarchy) 
            {
                PauseGame();
            }
            else if (pausePanel.activeInHierarchy) 
            {
                ContinueGame();   
            }
        } 
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    } 

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }
}
