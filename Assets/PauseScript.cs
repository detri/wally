using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseScript : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool paused;
    public bool canPause = true;
    public bool pauseMenuActive = true;
    public PlayerInput playerInput;
    
    private bool prevPaused = false;
    private float prevTimeScale;

    public void Pause()
    {
        paused = true;
        pauseMenuActive = false;
        canPause = false;
    }

    public void Resume()
    {
        paused = false;
        pauseMenuActive = true;
        canPause = true;
    }

    public void Start()
    {
        prevTimeScale = Time.timeScale;
        pauseMenu.SetActive(false);
    }
    
    public void Update()
    {
        if (paused != prevPaused)
        {
            if (paused)
            {
                pauseMenu.SetActive(pauseMenuActive);
                playerInput.enabled = false;
                prevTimeScale = Time.timeScale;
                Time.timeScale = 0f;
            }
            else
            {
                pauseMenu.SetActive(false);
                playerInput.enabled = true;
                Time.timeScale = prevTimeScale;
            }
        }

        prevPaused = paused;
    }
}
