using UnityEngine;
using System.IO;
using System.Linq;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class TimeRecords
{
    public List<float> records = new List<float>();
}


public class Records : MonoBehaviour
{
    [SerializeField] private TMP_Text recordsText;
    private string recordsFilePath;

    void Start()
    {
        recordsFilePath = Path.Combine(Application.persistentDataPath, "time_records.json");
        ShowRecords();
    }

    void ShowRecords()
    {
        if (recordsText == null) return;

        var timeRecords = LoadTimeRecords();
        if (timeRecords.records == null || timeRecords.records.Count == 0)
        {
            recordsText.text = "Нет рекордов";
            return;
        }

        // Сортируем по возрастанию (лучшее время — первое)
        var sortedRecords = timeRecords.records.OrderBy(t => t).ToList();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < sortedRecords.Count; i++)
        {
            float time = sortedRecords[i];
            int minutes = (int)(time / 60f);
            int seconds = (int)(time % 60f);
            int hundredths = (int)((time - Mathf.Floor(time)) * 100f);
            sb.AppendLine($"{i + 1}. {minutes:D2}:{seconds:D2}:{hundredths:D2}");
        }
        recordsText.text = sb.ToString();
    }

    TimeRecords LoadTimeRecords()
    {
        if (File.Exists(recordsFilePath))
        {
            try
            {
                string json = File.ReadAllText(recordsFilePath);
                var loaded = JsonUtility.FromJson<TimeRecords>(json);
                if (loaded != null && loaded.records != null)
                    return loaded;
            }
            catch
            {
              
            }
        }
        return new TimeRecords();
    }
}
