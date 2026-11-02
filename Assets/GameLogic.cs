using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public int coins = 0;
    bool won = false;
    [SerializeField]
    private TextMeshProUGUI coinsText;
    [SerializeField]
    private TextMeshProUGUI winText;
    [SerializeField]
    private Button button;
    [SerializeField]
    private EnemyMovement enemyMovement;
    [SerializeField]
    private PlayerController playerController;

    private bool paused = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetCountText();
        winText.gameObject.SetActive(false);
        button.gameObject.SetActive(false);

        button.onClick.AddListener(Restart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoin()
    {
        coins += 1;
        if (coins == 4 && won == false)
        {
            EndGame("Win");
        }
    }

    public void SetCountText()
    {
        coinsText.text = "Coins: " + coins.ToString();
    }

    public void EndGame(String End)
    {
        if (End == "Lose")
        {
            winText.GetComponent<TextMeshProUGUI>().text = "You Lose";
        }

        winText.gameObject.SetActive(true);
        Destroy(GameObject.FindGameObjectsWithTag("Enemy")[0]);
        button.gameObject.SetActive(true);
       

    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        if (paused) {
            //enemyNavMesh.enabled = true;
            enemyMovement.Pause(false);
            playerController.Pause(false);
            paused = false;
        } else
        {
            //enemyNavMesh.enabled = false;
            enemyMovement.Pause(true);
            playerController.Pause(true);
            paused = true;
        }
    }
}
