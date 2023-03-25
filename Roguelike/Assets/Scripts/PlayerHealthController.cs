using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHP;
    public int currentHP;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int dmg = 1)
    {
        currentHP -= dmg;

        if(currentHP <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);
        }
    }
}
