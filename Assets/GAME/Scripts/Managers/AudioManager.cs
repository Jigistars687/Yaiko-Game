using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("������ ������ (5 ����)")]
    public List<AudioClip> tracks;
    
    [SerializeField] private AudioSource _winSound; 

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private Coroutine loopCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);            
        }
        else
        {
            Destroy(gameObject);
        }
        
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    void Start()
    {
        if (tracks == null || tracks.Count == 0)
        {
            Debug.LogError("�� ����� ������ ������!");
            return;
        }
        loopCoroutine = StartCoroutine(PlayTracksLoop());
    }

    private IEnumerator PlayTracksLoop()
    {
        while (true)
        {
            audioSource.clip = tracks[currentTrackIndex];
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
        }
    }

    public void PlayWinSound()
    {
        _winSound.Play(); 
    }
}
