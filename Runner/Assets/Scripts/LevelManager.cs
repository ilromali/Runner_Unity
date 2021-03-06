using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject activeUI;
    public GameObject deathUI;


    private bool canUpdate = true;
    private void Awake()
    {
#if UNITY_EDITOR
        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        
        Application.targetFrameRate = 60;
#endif
    }

    private Player player;
    // Start is called before the first frame update
    void Start()
    {

        player = FindObjectOfType<Player>();
        deathUI.SetActive(false);
        Time.timeScale = 1;
        canUpdate = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (player.IsDead && canUpdate)
        {
            Die();
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Pause()
    {
        Time.timeScale = 0;
    }
    public void Die()
    {

            canUpdate = false;
            Time.timeScale = 0;
            activeUI.SetActive(false);
            deathUI.SetActive(true);

            Score score = activeUI.GetComponentInChildren<Score>();
            int playerScore = (int)score.PlayerScore;
            // this thing gets called forever...
            if(SaveManager.Instance != null)
            {
                SaveManager.Instance.AttemptToSetHighscore(playerScore);
                SaveManager.Instance.AddMoney(playerScore);
            }

            deathUI.GetComponent<DeathScreen>().PrintPlayerScore(playerScore);
            
        
    }
    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
