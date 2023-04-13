using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float speed = 6f;
    public int dmg = 50;

    private Vector3 direction;

    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();

        Vector2 offset = new Vector2(direction.x, direction.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController.instance.DamagePlayer();
        }

        Destroy(gameObject);
        AudioManager.instance.PlaySFX(5);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        AudioManager.instance.PlaySFX(5);
    }
}
