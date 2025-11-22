using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CUBESANIM : MonoBehaviour
{
    [SerializeField] private GameObject _cube1;
    [SerializeField] private Transform _target_cube1;

    [SerializeField] private GameObject _cube2;
    [SerializeField] private Transform _target_cube2;

    [SerializeField] private GameObject _cube3;
    [SerializeField] private Transform _target_cube3;


    void Start()
    {
        Anim();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Anim()
    {
        LeanTween.move(_cube1, _target_cube1.position, 2.2f).setLoopPingPong();
        LeanTween.move(_cube2, _target_cube2.position, 2.5f).setLoopPingPong();
        LeanTween.move(_cube3, _target_cube3.position, 2.1f).setLoopPingPong();
    }
}
