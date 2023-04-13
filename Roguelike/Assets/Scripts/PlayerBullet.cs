using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 7f;
    public int dmg = 50;

    public Rigidbody2D rigidbody2d;

    public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2d.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Terrain") && !other.CompareTag("Pickup"))
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);

            if(other.CompareTag("Enemy"))
                other.GetComponent<EnemyController>().DamageEnemy(dmg);
            AudioManager.instance.PlaySFX(5);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
