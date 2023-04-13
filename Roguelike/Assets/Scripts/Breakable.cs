using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public GameObject[] brokenPieces;
    public int maxPieces = 3;

    public bool shouldDropItem;
    public GameObject[] dropPool;
    public float dropChance;

    public int breakSound = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DropItem()
    {
        if (shouldDropItem)
        {
            float dropRng = Random.Range(0f, 100f);

            if(dropChance > dropRng)
            {
                Instantiate(dropPool[Random.Range(0, dropPool.Length)], transform.position, transform.rotation);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if ((other.gameObject.CompareTag("Player") && PlayerController.instance.isDashing))
        {
            AudioManager.instance.PlaySFX(breakSound);
            Destroy(gameObject);

            int nPieces = Random.Range(1, maxPieces);
            for(int i = 0; i < nPieces; i++)
                Instantiate(brokenPieces[Random.Range(0, brokenPieces.Length)], transform.position, transform.rotation);

            DropItem();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("EnemyBullet"))
        {
            AudioManager.instance.PlaySFX(breakSound);
            Destroy(gameObject);

            int nPieces = Random.Range(1, maxPieces);
            for (int i = 0; i < nPieces; i++)
                Instantiate(brokenPieces[Random.Range(0, brokenPieces.Length)], transform.position, transform.rotation);

            DropItem();
        }
    }
}
