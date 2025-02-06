using UnityEngine;

public class AddScore : MonoBehaviour
{
    public ScoreDisplay scoreDisplay;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scoreDisplay.AddScore(1);
        }
    }
}