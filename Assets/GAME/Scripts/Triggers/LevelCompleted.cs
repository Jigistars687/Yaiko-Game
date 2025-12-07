using System.Collections;
using UnityEngine;

public class LevelCompleted : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EggMovement eggMovement = other.GetComponentInParent<EggMovement>();
        if (eggMovement)
        {
            StartCoroutine(LevelCompletedCouroutine());
        }
    }
    
    private IEnumerator LevelCompletedCouroutine()
    {
        AudioManager.Instance.PlayWinSound();
        yield return new WaitForSeconds(1f);
        LevelManager.Instance.LoadNextScene();
    }
}
