using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Startup : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject newSaveFileMenu;
    
    void Start()
    {
        try
        {
            SaveSystem.LoadData();
        }
        catch (SerializationException)
        {
            newSaveFileMenu.SetActive(true);
        }

        if (SaveSystem.SavedGame != null)
        {
            mainMenu.SetActive(true);
        }
    }
}
