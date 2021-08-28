using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{

    private Animator Anim;

    // Start is called before the first frame update
    void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(bool walk)
    {
        Anim.SetBool(AnimationTag.WalkParameter, walk);
    }
    
    public void Run(bool run)
    {
        Anim.SetBool(AnimationTag.RunParameter, run);
    }
    
    public void Attack()
    {
        Anim.SetTrigger(AnimationTag.AttackTrigger);
    }
    
    public void Dead()
    {
        Anim.SetTrigger(AnimationTag.DeadTrigger);
    }

}
