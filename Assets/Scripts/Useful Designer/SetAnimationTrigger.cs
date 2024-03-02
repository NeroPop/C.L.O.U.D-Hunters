using UnityEngine;

public class SetAnimationTrigger : MonoBehaviour
{
    public Animator anim;
    public string trigger;

    public void SetTrigger()
    {
        anim.SetTrigger(trigger);
    }
}
