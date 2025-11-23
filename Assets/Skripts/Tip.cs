using TMPro;
using UnityEngine;

public class Tip : MonoBehaviour
{
    [SerializeField] private GameObject _Tip_Text;
    private float tipTimer = 0f;
    void Start()
    {
        
    }

    void TipShow()
    {
        tipTimer += Time.deltaTime;
        if (tipTimer >= 5f)
        {
            _Tip_Text.SetActive(false);
            this.enabled = false;
        }
    }
    void Update()
    {
        TipShow();
    }
}
