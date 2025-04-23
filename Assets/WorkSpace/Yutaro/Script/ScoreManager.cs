using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int redScore;
    public int whiteScore;

    [SerializeField] private Text redText;
    [SerializeField] private Text whiteText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        redScore = 0;
        whiteScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        redText.text = redScore.ToString();
        whiteText.text = whiteScore.ToString();
    }
}
