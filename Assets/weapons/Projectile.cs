using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5.0f;
    public float speedMod = 1.0f;
    public float damage = 15.0f;
    public Rigidbody2D body;
    
    private Vector2 forward;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponentInChildren<Rigidbody2D>();
        forward = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = forward * (speed * speedMod);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyInfo>().TakeDamage(damage, body.velocity.normalized);
            Destroy(gameObject);
        }
    }
}
