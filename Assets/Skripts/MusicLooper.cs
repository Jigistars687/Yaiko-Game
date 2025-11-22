using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    [Header("Список треков (5 штук)")]
    public List<AudioClip> tracks;  // Перетащите сюда 5 ваших AudioClip в инспекторе

    private AudioSource audioSource;
    private int currentTrackIndex = 0;
    private Coroutine loopCoroutine;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = false;  // Мы сами будем управлять повторением
    }

    void Start()
    {
        if (tracks == null || tracks.Count == 0)
        {
            Debug.LogError("Не задан список треков!");
            return;
        }
        // Запускаем бесконечное проигрывание
        loopCoroutine = StartCoroutine(PlayTracksLoop());
    }

    private IEnumerator PlayTracksLoop()
    {
        while (true)
        {
            audioSource.clip = tracks[currentTrackIndex];
            audioSource.Play();

            // Ждём завершения трека
            yield return new WaitForSeconds(audioSource.clip.length);

            // Переходим к следующему треку (с оборачиванием)
            currentTrackIndex = (currentTrackIndex + 1) % tracks.Count;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            // Останавливаем корутину и аудио
            if (loopCoroutine != null)
            {
                StopCoroutine(loopCoroutine);
                loopCoroutine = null;
            }
            audioSource.Stop();
        }
    }
}
