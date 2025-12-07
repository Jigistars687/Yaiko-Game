using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EggMovement eggMovement = other.GetComponentInParent<EggMovement>();
        if (eggMovement)
        {
            LevelManager.Instance.RestartLevel();
        }
    }
}
