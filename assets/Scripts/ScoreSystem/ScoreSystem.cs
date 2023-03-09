using System;
using UnityEngine;

public class ScoreSystem
{
    public int _currentScore { get; set; } 
    public int _maxScore { get; set; }
    public event Action maxScoreReached; 

    public void AddScore(int score)
    {
        _currentScore = Mathf.Min(_currentScore + score, _maxScore);
        if (_currentScore >= _maxScore)
        {
            maxScoreReached?.Invoke();
        }
    }
    
    public void ReceivedClick(GameObject gameObject)
    {
        AddScore(1);
    }
}

    