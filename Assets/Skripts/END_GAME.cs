using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Linq;

//[System.Serializable]
//public class TimeRecords
//{
//    public List<float> records = new List<float>();
//}

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject exitButton;
    private bool hasFinished = false;
    //[SerializeField] GameObject endButton;
    [SerializeField] AudioSource WinSound;
    [SerializeField] TImer timer;
    [SerializeField] TMPro.TMP_Text finalTimeText;
    [SerializeField] GameObject Confettis;

    private string recordsFilePath;
    private const int MaxRecords = 10;

    void Start()
    {
        exitButton.SetActive(false); // Скрываем кнопку при старте
        //endButton.SetActive(false);
        recordsFilePath = Path.Combine(Application.persistentDataPath, "time_records.json");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish") && !hasFinished)
        {
            WinSound.PlayOneShot(WinSound.clip); // Проигрываем звук победы
            hasFinished = true;
            if (timer != null)
            {
                float finalTime = timer.GetElapsedTime();
                timer.enabled = false; // Останавливаем таймер
                if (finalTimeText != null)
                {
                    finalTimeText.text = $"Время: {finalTime:F2}";
                    finalTimeText.gameObject.SetActive(true);
                }
                SaveTimeRecord(finalTime);
            }
            Invoke("ShowExitButton", 5f);
            Invoke("ShowEndButton", 0f);
            Confettis.SetActive(true);
        }
    }

    void ShowEndButton()
    {
        //endButton.SetActive(true);
    }

    void ShowExitButton()
    {
        exitButton.SetActive(true); // Показываем кнопку спустя 5 секунд
        Cursor.lockState = CursorLockMode.None; // Блокирует курсор в центре экрана
        Cursor.visible = true;
    }

    void SaveTimeRecord(float newTime)
    {
        TimeRecords timeRecords = LoadTimeRecords();

        // Добавляем новый рекорд
        timeRecords.records.Add(newTime);

        // Находим лучший рекорд (минимальное время)
        float bestTime = timeRecords.records.Min();

        // Сортируем по времени (от меньшего к большему)
        timeRecords.records = timeRecords.records.OrderBy(t => t).ToList();

        // Если больше MaxRecords, удаляем самые старые, кроме лучшего
        if (timeRecords.records.Count > MaxRecords)
        {
            // Сохраняем лучший рекорд
            List<float> filtered = new List<float> { bestTime };
            // Добавляем остальные, кроме лучшего, до лимита
            filtered.AddRange(timeRecords.records.Where(t => t != bestTime).Take(MaxRecords - 1));
            timeRecords.records = filtered.OrderBy(t => t).ToList();
        }

        // Сохраняем в файл
        string json = JsonUtility.ToJson(timeRecords, true);
        File.WriteAllText(recordsFilePath, json);
    }

    TimeRecords LoadTimeRecords()
    {
        if (File.Exists(recordsFilePath))
        {
            string json = File.ReadAllText(recordsFilePath);
            return JsonUtility.FromJson<TimeRecords>(json);
        }
        return new TimeRecords();
    }
}
