using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollisionDetection : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI BallCountText;
    public TextMeshProUGUI TimeText;
    public int Score = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            ScoreText.text = "Score:" + (Score++);
        }
    }
}
