using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Music_Slider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private AudioSource _music; // источник музыки
    [SerializeField] private GameObject Setting_Canvas;

    private const string VolumeKey = "MusicVolume";

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        _slider.value = savedVolume;
        _music.volume = savedVolume;
        _slider.onValueChanged.AddListener(OnSliderChanged);
        OnSliderChanged(_slider.value); // обновить сразу при старте
    }

    private void OnSliderChanged(float value)
    {
        _music.volume = value; // громкость 0Ц1
        _text.text = Mathf.RoundToInt(value * 100f).ToString(); // вывод 0Ц100
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1f;
            if (Setting_Canvas != null)
            {
                Setting_Canvas.SetActive(false);
            }
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}

