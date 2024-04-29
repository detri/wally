using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool paused = false;
    
    private int seconds = 0;
    private int minutes = 0;
    private int prevSeconds = 0;
    private int prevMinutes = 0;
    private float elapsedTime = 0.0f;
    private TextMeshProUGUI text;

    public int Seconds => Mathf.FloorToInt(elapsedTime);
    public float ElapsedTime => elapsedTime;

    public event Action<int> Tick;
    
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            return;
        }
        elapsedTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(elapsedTime / 60.0f);
        seconds = Mathf.FloorToInt(elapsedTime % 60.0f);

        if (prevMinutes != minutes || prevSeconds != seconds)
        {
            Tick?.Invoke(Seconds);
            text.text = Format();
        }
        
        prevMinutes = minutes;
        prevSeconds = seconds;
    }

    public string Format()
    {
        return $"{minutes:00}:{seconds:00}";
    }
}
