using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager gameManager;

    //Powerups
    //[HideInInspector]
    public int pickUpCount;

    //Panels
    [Header("Panels")]
    public GameObject endPanel;

    private void Awake()
    {
        //Singleton
        gameManager = this;

        //Disable panel on beginnig
        endPanel.SetActive(false);

        //Restart TimeScale
        Time.timeScale = 1.0f;
    }

    private IEnumerator Start()
    {
        //the start will start after 0.05
        yield return new WaitForSeconds(.05f);

        //get all powerups
        pickUpCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
    }

    /// <summary>
    /// Loose Game Panel
    /// </summary>
    public void LoseGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "GAME OVER";

        //set time to 0
        Time.timeScale = 0;
    }

    /// <summary>
    /// Win Game Panel
    /// </summary>
    public void WinGame()
    {
        endPanel.SetActive(true);
        endPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "YOU WIN";

        //set time to 0
        Time.timeScale = 0;
    }

    /// <summary>
    /// change to scene to sceneName
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName)
    {
        // change to scene
        SceneManager.LoadScene(sceneName);
        Debug.Log("Go to Menu");
    }
}