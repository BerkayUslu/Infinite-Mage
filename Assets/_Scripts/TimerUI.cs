using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _text;

    private void OnEnable()
    {
    }

    void Start()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        int minutes = (int)Time.timeSinceLevelLoad / 60;
        int seconds = (int)Time.timeSinceLevelLoad % 60;

        _text.text= minutes + "min" + seconds + "sec";
    }
}
