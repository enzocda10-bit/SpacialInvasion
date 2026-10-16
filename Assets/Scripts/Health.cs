using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private Image healthBarFill;

    [SerializeField] private float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBarFill.fillAmount = currentHealth / maxHealth;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
