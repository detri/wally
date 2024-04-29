using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public Camera playerCamera;
    public Animator animator;
    public float baseHealth = 50.0f;
    public float currentHealth = 50.0f;
    public int currentLevel = 1;
    public float currentExp = 0.0f;
    public float expToLevel = 25.0f;
    public float maxHealthMod = 1.0f;
    public GameObject damageNumbers;
    public UpgradeManager upgradeManager;
    public WeaponManager weaponManager;
    public PauseScript pauseManager;
    public CharacterData characterData;
    public float aimDirection = 0.0f;

    public float maxHealth;
    
    public Vector3 Center => _renderer.bounds.center;
    
    private Rigidbody2D body;
    private SpriteRenderer _renderer;
    private PlayerInput playerInput;

    private static readonly int Walking = Animator.StringToHash("Walking");

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        maxHealth = baseHealth;
        currentHealth = maxHealth;
        _renderer = GetComponent<SpriteRenderer>();
        weaponManager.AddWeapon(characterData.startingWeapon);
    }

    // Update is called once per frame
    void Update()
    {
        var cameraTransform = playerCamera.transform;
        var pos = transform.position;
        cameraTransform.position =
            new Vector3(pos.x, pos.y, cameraTransform.position.z);
        
        animator.SetBool(Walking, body.velocity != Vector2.zero);

        if (body.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (body.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    
    public void OnPause(InputValue value)
    {
        if (pauseManager.canPause)
        {
            pauseManager.paused = !pauseManager.paused;
        }
    }

    public void OnMove(InputValue value)
    {
        var movement = value.Get<Vector2>();
        movement.Normalize();
        body.velocity = movement * moveSpeed;
    }

    public void OnLook(InputValue value)
    {
        if (playerInput.currentControlScheme == "KBMouse")
        {
            var mousePosition = value.Get<Vector2>();
            var lookDir = playerCamera.ScreenToWorldPoint(mousePosition) - Center;
            aimDirection = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        } else if (playerInput.currentControlScheme == "Gamepad")
        {
            var lookDir = value.Get<Vector2>();
            aimDirection = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        }
        else
        {
            aimDirection = 0.0f;
        }
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        var dmgObject = Instantiate(damageNumbers, Center, damageNumbers.transform.rotation);
        dmgObject.GetComponent<TextMeshPro>().text = $"{amount}";
        dmgObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2f,2f), 2);
        Destroy(dmgObject, 2.0f);
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }

    public void GainExp(float amount)
    {
        currentExp += amount;
        if (currentExp >= expToLevel)
        {
            currentExp -= expToLevel;
            expToLevel *= 1.2f;
            LevelUp();
        }
    }

    public void LevelUp()
    {
        currentLevel++;
        upgradeManager.LevelUp();
    }

    public void SetMaxHealthMod(float newVal)
    {
        maxHealthMod = newVal;
        maxHealth = baseHealth * maxHealthMod;
    }
}
