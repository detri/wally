using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingAttack : MonoBehaviour
{
    public float attackLength = 0.5f;
    public float damage = 10f;
    public Transform spriteTransform;
    
    private Vector3 startAngle;
    private Vector3 endAngle;
    private float attackProgress = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (attackProgress < attackLength)
        {
            attackProgress += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }

        var lerpedRotation = Vector3.Lerp(startAngle, endAngle, attackProgress / attackLength);
        transform.rotation = Quaternion.Euler(lerpedRotation);
    }

    public void SetupSwingArc(float arc)
    {
        var rotation = transform.rotation;
        startAngle = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z + arc / 2.0f);
        endAngle = new Vector3(0, rotation.eulerAngles.y, rotation.eulerAngles.z - arc / 2.0f);
        transform.rotation = Quaternion.Euler(startAngle);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyInfo>().TakeDamage(damage, new Vector2());
        }
    }

    public void SetScale(float newScale)
    {
        spriteTransform.localScale = new Vector3(newScale, newScale, 1f);
    }
}
