using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int maxHP;
    public int currentHP;

    public float invincibleDuration = 1f;
    private float invincibleCounter;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;

        UIController.instance.HPSlider.maxValue = maxHP;
        UIController.instance.HPSlider.value = currentHP;

        UIController.instance.HPText.text = string.Format("{0} / {1}", currentHP, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter >= 0)
            invincibleCounter -= Time.deltaTime;
    }

    public void DamagePlayer(int dmg = 1)
    {
        if (invincibleCounter >= 0)
            return;

        currentHP -= dmg;
        PlayerController.instance.OnDamagePlayer(currentHP);
        UIController.instance.HPSlider.value = currentHP;
        UIController.instance.HPText.text = string.Format("{0} / {1}", currentHP, maxHP);
        invincibleCounter = invincibleDuration;

        if (currentHP <= 0)
        {
            PlayerController.instance.gameObject.SetActive(false);

            UIController.instance.deathScreen.SetActive(true);
        }
    }

    public void HealPlayer(int heal = 1)
    {
        currentHP += heal;

        if (currentHP > maxHP)
            currentHP = maxHP;

        PlayerController.instance.OnHealPlayer();
        UIController.instance.HPSlider.value = currentHP;
        UIController.instance.HPText.text = string.Format("{0} / {1}", currentHP, maxHP);
    }

    public void MakeInvincible(float duration = 1f)
    {
        invincibleCounter = duration;
    }
}
