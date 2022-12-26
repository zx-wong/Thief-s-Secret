using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionText : MonoBehaviour
{
    public Text targetScore;

    // Start is called before the first frame update
    void Start()
    {
        targetScore.GetComponent<Text>().color = Color.white;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoringSystem.sManager.playerScore >= 15000) 
        {
            targetScore.color = Color.green;
        }

    }
}
