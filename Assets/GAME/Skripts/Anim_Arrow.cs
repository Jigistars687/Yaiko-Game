using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim_Arrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      LeanTween.moveY(gameObject, gameObject.transform.position.y + 1f, 1.5f).setLoopPingPong();
    }

}
