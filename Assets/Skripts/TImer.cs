using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TImer : MonoBehaviour
{
    private float elapsedTime = 0f;

    [SerializeField] private TMP_Text timeText;

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (timeText != null)
        {
            timeText.text = elapsedTime.ToString("F2"); 
        }
    }

    public void ResetTimer()
    {
        elapsedTime = 0f;
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }
}
