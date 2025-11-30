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
    [SerializeField] private List<TMP_Text> recordTexts;
    private string recordsFilePath;

    void Start()
    {
        recordsFilePath = Path.Combine(Application.persistentDataPath, "time_records.json");
        ShowRecords();
    }

    void ShowRecords()
    {
        // Скрываем все текстовые объекты перед обновлением
        foreach (var text in recordTexts)
        {
            if (text != null)
            {
                text.text = "";
                text.gameObject.SetActive(false);
            }
        }

        var timeRecords = LoadTimeRecords();
        if (timeRecords.records == null || timeRecords.records.Count == 0)
        {
            if (recordTexts.Count > 0 && recordTexts[0] != null)
            {
                recordTexts[0].text = "Нет рекордов";
                recordTexts[0].color = Color.white;
                recordTexts[0].gameObject.SetActive(true);
            }
            return;
        }

        // Сортируем по возрастанию (лучшее время — первое)
        var sortedRecords = timeRecords.records.OrderBy(t => t).ToList();

        int maxRecords = Mathf.Min(recordTexts.Count, 10, sortedRecords.Count);
        for (int i = 0; i < maxRecords; i++)
        {
            float time = sortedRecords[i];
            int minutes = (int)(time / 60f);
            int seconds = (int)(time % 60f);
            int hundredths = (int)((time - Mathf.Floor(time)) * 100f);

            if (recordTexts[i] != null)
            {
                recordTexts[i].text = $"{i + 1}. {minutes:D2}:{seconds:D2}:{hundredths:D2}";
                recordTexts[i].gameObject.SetActive(true);

                // Цвета: 1 - зелёный, 2 - жёлтый, 3 - голубой, остальные - белый
                switch (i)
                {
                    case 0:
                        recordTexts[i].color = Color.green;
                        break;
                    case 1:
                        recordTexts[i].color = Color.yellow;
                        break;
                    case 2:
                        recordTexts[i].color = Color.cyan;
                        break;
                    default:
                        recordTexts[i].color = Color.white;
                        break;
                }
            }
        }
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

    public void ResetRecordes()
    {
        if (File.Exists(recordsFilePath))
        {
            File.Delete(recordsFilePath);
        }
        ShowRecords();
    }
}
