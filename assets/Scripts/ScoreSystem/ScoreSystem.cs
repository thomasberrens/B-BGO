using UnityEngine;

public class ScoreSystem
{
    private bool _isActive;
    private int _currentScore = 0;
    private int _maxScore = 5;

    public string AddScore(int score)
    {
        _currentScore += score;
        _currentScore = Mathf.Min(_currentScore, _maxScore);
        if (_currentScore >= _maxScore)
        {
            return "GOED GEDAAN" + _maxScore;
        }
        else
        {
            return "Current Score" + _currentScore + "out of " + _currentScore;
        }
    }

    public void ReceivedClick(GameObject gameObject)
    {
        AddScore(1);
    }
}

    