using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    public float maxHealth = 100;
    public float currentHealth;
    private HealthBar healthBar;
    ExplosionParticleHandler ePF;
    GameOverTrigger gameOverTrigger;
    Timer timer;

    void Start()
    {
        timer = FindAnyObjectByType<Timer>();
        if (ePF == null )
        {
            ePF = FindAnyObjectByType<ExplosionParticleHandler>();
        }
        else Debug.Log("Epf not found");
        gameOverTrigger = FindObjectOfType<GameOverTrigger>();
        if (gameOverTrigger == null)
        {
            Debug.Log("GameOvTRigg not found");
        }
        


        if (healthBarPrefab != null)
        {
            healthBar = Instantiate(healthBarPrefab).GetComponentInChildren<HealthBar>();
        }
        healthBar.target = transform;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);


       
    }

  public void TakeDamage(float damageAmount) 
    {
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
        }
    public void AddHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        healthBar.SetHealth(currentHealth);
    }

    public void PlayerDeath()
    {
        
        if (ePF.carHasTheBomb == true)
        {

            ePF.Explode(false);
            StartCoroutine(ShowMenuAfterDeath());

        }
        else
        { 
            StartCoroutine(ShowMenuAfterDeath());
        }
    }


    public void Initialize()
    {
       


    }
    public IEnumerator ShowMenuAfterDeath()
    {
        timer.isCounting = false;
        Debug.Log("Coroutine Entered.");
        yield return new WaitForSeconds(3);
        Debug.Log("ShowMenu Method Works!");
        if (gameOverTrigger != null)
        {
            gameOverTrigger.DisplayMenu();
        }
        else Debug.Log("gameOverTrigger = null, Is gameOverCanvas present?");
        Destroy(gameObject);
    }
}
