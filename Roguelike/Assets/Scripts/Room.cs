using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public bool closeWhenEnter;
    public bool openWhenEnemyClear;

    public GameObject[] doors;

    public List<EnemyController> enemies = new List<EnemyController>();

    private bool roomActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!roomActive)
            return;

        if(openWhenEnemyClear && enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
                if (!enemies[i].IsAlive)
                {
                    enemies.RemoveAt(i);
                    i--;
                }

            if (enemies.Count == 0)
            {
                foreach (var door in doors)
                    door.SetActive(false);
                closeWhenEnter = false;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            CameraController.instance.Target = transform;

            if(closeWhenEnter)
            {
                foreach (var door in doors)
                    door.SetActive(true);
            }

            roomActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            roomActive = false;
    }
}
