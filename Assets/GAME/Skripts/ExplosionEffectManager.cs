using UnityEngine;

public class ExplosionEffectManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] soundClips; // —писок звуков дл€ воспроизведени€

    private int currentClipIndex = 0;

    private void OnEnable()
    {
        currentClipIndex = 0;
        PlayNextClip();
    }

    private void PlayNextClip()
    {
        if (soundClips.Length == 0) return;

        audioSource.clip = soundClips[currentClipIndex];
        audioSource.Play();

        currentClipIndex++;
        if (currentClipIndex >= soundClips.Length)
            currentClipIndex = 0;
    }

    private void Update()
    {
        // ѕровер€ем, закончилс€ ли текущий звук, чтобы воспроизвести следующий
        if (!audioSource.isPlaying && gameObject.activeSelf)
        {
            PlayNextClip();
        }
    }
}
