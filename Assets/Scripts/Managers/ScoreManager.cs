using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager _instance;
    [SerializeField] private TextMeshProUGUI _scoreLabel;

    private int _score;
    public static int Score
    {
        get { return _instance._score; }
        set { _instance._score = value; }
    }


    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private void Update()
    {
        _scoreLabel.text = string.Format("Score: {0}", Score);
    }
}
