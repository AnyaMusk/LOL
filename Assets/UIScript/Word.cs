using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Word : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI wordText;
    public WordContent wordContent;

    private void Awake()
    {
        wordText.text = wordContent.word;
    }
}



[System.Serializable]
public class WordContent
{
    public string word;
    public int points;
}

