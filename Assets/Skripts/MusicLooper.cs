using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    [Header("Список треков (5 штук)")]
    public List<AudioClip> tracks;

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private Coroutine loopCoroutine;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;
    }

    void Start()
    {
        if (tracks == null || tracks.Count == 0)
        {
            Debug.LogError("Не задан список треков!");
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            if (loopCoroutine != null)
            {
                StopCoroutine(loopCoroutine);
                loopCoroutine = null;
            }
            audioSource.Stop();
        }
    }
}
