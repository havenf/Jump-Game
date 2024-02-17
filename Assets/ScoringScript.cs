using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringScript : MonoBehaviour
{
    public double _playerOneScore;
    public double _playerTwoScore;

    // Start is called before the first frame update
    void Start()
    {
        _playerOneScore = 70d;
        _playerTwoScore = 30d;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RunScoreSystem());
    }

    private IEnumerator RunScoreSystem()
    {

        _playerOneScore -= 0.001;
        _playerTwoScore += 0.005;

        Debug.Log((int)_playerOneScore + " | " + (int)_playerTwoScore);
        yield return new WaitForSeconds(1);
    }
}
