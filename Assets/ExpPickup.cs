using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;
    [SerializeField] private float expValue = 5.0f;
    [SerializeField] private float magnetTime = 1.0f;
    private float curMagnetTime = 0.0f;
    private bool isPickedUp = false;
    
    // Start is called before the first frame update
    void Start()
    {
        DetermineSprite();
    }

    private void Update()
    {
        if (isPickedUp && curMagnetTime < magnetTime)
        {
            curMagnetTime += Time.deltaTime;
        }
    }

    private void DetermineSprite()
    {
        if (expValue > 10.0f)
        {
            spriteRenderer.sprite = sprites[1];
        } else if (expValue > 50.0f)
        {
            spriteRenderer.sprite = sprites[2];
        } else if (expValue > 100.0f)
        {
            spriteRenderer.sprite = sprites[3];
        }

        spriteRenderer.sprite = sprites[0];
    }

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerPickup"))
        {
            Debug.Log("Touched exp pickup");
            var player = other.gameObject.GetComponentInParent<PlayerController>();
            StartCoroutine(Magnetize(player));
            isPickedUp = true;
        }
    }

    private IEnumerator Magnetize(PlayerController player)
    {
        var startPos = transform.position;
        
        while (!Mathf.Approximately(transform.position.x, player.Center.x) ||
                   !Mathf.Approximately(transform.position.y, player.Center.y))
        {
            transform.position = Vector3.Lerp(startPos, player.Center, curMagnetTime / magnetTime);
            yield return null;
        }

        player.GainExp(expValue);
        Destroy(gameObject);
    }

    public void ChangeValue(float newValue)
    {
        expValue = newValue;
        DetermineSprite();
    }
}
