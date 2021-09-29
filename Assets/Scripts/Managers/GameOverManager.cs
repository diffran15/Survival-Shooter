using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text warningText;
    public PlayerHealth playerHealth;       
    public float restartDelay = 5f;
    public bool isDead = false;
    Animator anim;                          
    float restartTimer;                    

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDead)
        {
            restartTimer += Time.deltaTime;

            if (restartTimer >= restartDelay)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (playerHealth.currentHealth <= 0 && !isDead)
        {
            isDead = true;
            anim.SetTrigger("GameOver");
        }
    }

    public void ShowWarning(float enemyDistance)
    {
        warningText.text = string.Format("!\n{0} m", Mathf.RoundToInt(enemyDistance));
        anim.SetTrigger("Warning"); 
    }
}