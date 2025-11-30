using Unity.Cinemachine;
using UnityEngine;

public class TELEPORT_SCRIPTS : MonoBehaviour
{
    [Header("tp")]
    [SerializeField] private Transform _nest;
    [SerializeField] private Transform _Finish_Tag;
    [SerializeField] private Transform _Lava_TP;
    [SerializeField] private Transform _Finish;
    [SerializeField] private Transform _finishCameraTransform;

    [Header("Camera")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Light _MainDiectionalLight;

    private Rigidbody Egg_rb;
    private bool finished = false;

    void Start()
    {
        Egg_rb = GetComponent<Rigidbody>();
    }

    public void TryTeleport(Collision collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "nest":
                TeleportTo(_nest);
                _MainDiectionalLight.intensity = 0;
                RenderSettings.ambientIntensity = 0.37f;
                break;
            case "Finish Tag":
                TeleportTo(_Finish_Tag);
                _MainDiectionalLight.intensity = 1;
                RenderSettings.ambientIntensity = 0.91f;
                break;
            case "Lava":
                TeleportTo(_Lava_TP);
                break;
            case "Finish":
                finished = true;
                TeleportTo(_Finish);
                _MainDiectionalLight.intensity = 0;
                RenderSettings.ambientIntensity = 0.37f;
                if (_camera != null)
                {
                    var cinemachineBrain = _camera.GetComponent<CinemachineBrain>();
                    if (cinemachineBrain != null)
                        cinemachineBrain.enabled = false;
                    _camera.transform.position = _finishCameraTransform.position;
                    _camera.transform.rotation = _finishCameraTransform.rotation;
                }
                break;
        }
    }

    private void TeleportTo(Transform target)
    {
        if (target != null)
        {
            transform.position = target.position;
            if (Egg_rb != null)
                Egg_rb.linearVelocity = Vector3.zero;
        }
    }
}
