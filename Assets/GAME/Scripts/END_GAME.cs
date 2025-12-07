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
        exitButton.SetActive(false);
        //endButton.SetActive(false);
        recordsFilePath = Path.Combine(Application.persistentDataPath, "time_records.json");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish") && !hasFinished)
        {
            WinSound.PlayOneShot(WinSound.clip);
            hasFinished = true;
            if (timer != null)
            {
                float finalTime = timer.GetElapsedTime();
                timer.enabled = false;
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
        exitButton.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void SaveTimeRecord(float newTime)
    {
        TimeRecords timeRecords = LoadTimeRecords();


        timeRecords.records.Add(newTime);


        float bestTime = timeRecords.records.Min();


        timeRecords.records = timeRecords.records.OrderBy(t => t).ToList();


        if (timeRecords.records.Count > MaxRecords)
        {

            List<float> filtered = new List<float> { bestTime };

            filtered.AddRange(timeRecords.records.Where(t => t != bestTime).Take(MaxRecords - 1));
            timeRecords.records = filtered.OrderBy(t => t).ToList();
        }


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
