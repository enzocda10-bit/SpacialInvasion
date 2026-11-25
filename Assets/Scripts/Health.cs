using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public float maxHealth = 100;
    public Image healthBarFill;

    public float currentHealth;

    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float deathSoundVolume = 0.8f;
    [SerializeField] private Vector2 shootPitch;

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

            float pitch = Random.Range(shootPitch.x, shootPitch.y);
            AudioManager.Instance.PlaySFX(deathSound, deathSoundVolume, pitch);

            if (gameObject.name == "Boss")
            {
                SceneManager.LoadScene("Win");
            }

            if (gameObject.name == "Player")
            {
                SceneManager.LoadScene("Lose");
            }
        }
    }
}
