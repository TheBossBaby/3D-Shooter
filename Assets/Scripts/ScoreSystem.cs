using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    static int _points;
    static TMP_Text _text;

    public static void AddPoint(int point)
    {
        _points += point;
        _text.SetText(_points.ToString());
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
    }
}
