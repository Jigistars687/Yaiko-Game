using UnityEngine;

public class Music : MonoBehaviour
{
    [SerializeField] private AudioSource _music;
    private const string VolumeKey = "MusicVolume";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        if (_music != null)
        {
            _music.volume = savedVolume;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
