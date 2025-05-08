using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
