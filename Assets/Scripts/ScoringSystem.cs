using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject scoreText;
    public static ScoringSystem sManager;
    public int playerScore = 0;
    public int totalCost = 0;

    // Start is called before the first frame update
    void Start()
    {
        sManager = this;
        scoreText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScore > 0)
        {
            scoreText.SetActive(true);
            scoreText.GetComponent<Text>().text = "$ " + playerScore;
        }


    }

    public void increaseMoney(int increase)
    {
        playerScore += increase;
    }

}
