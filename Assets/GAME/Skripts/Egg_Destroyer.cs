using Unity.VisualScripting;
using UnityEngine;

public class Egg_Destroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
