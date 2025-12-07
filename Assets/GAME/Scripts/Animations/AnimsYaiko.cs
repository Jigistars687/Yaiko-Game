using UnityEngine;

public class AnimsYaiko : MonoBehaviour
{
    [SerializeField] private Animator YaikoAnims;

    //private int AnimsCount = 0;
    //private int MaxAnims = 1;
    
    //void SetAnimTag()
    //{
    //    if (CanAnim())
    //    {
    //        YaikoAnims.SetTrigger("OnGround");
    //        Debug.Log("SetAnimTag");
    //        AnimsCount++;
    //    }
    //}

    //bool CanAnim()
    //{
    //    return AnimsCount < MaxAnims;
    //}


    //void ResetJumpCount()
    //{
    //    AnimsCount = 0;
    //    Debug.Log("Reset");
    //}
    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Ground")
        {
            YaikoAnims.SetTrigger("OnGround");
            //SetAnimTag();
            Debug.Log("Collide");
        }
    }
}
