using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image hpGraphic;
    public PlayerController player;

    // Update is called once per frame
    void Update()
    {
        hpGraphic.fillAmount = player.currentHealth / player.maxHealth;
    }
}
