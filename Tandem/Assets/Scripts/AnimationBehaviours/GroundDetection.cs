using UnityEngine;
using System.Collections;

public class GroundDetection : StateMachineBehaviour {

    private int jumpable;


    private bool isGrounded(Transform transform)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -Vector3.up, out hit, 100.0f, jumpable))
        {
            if (hit.distance - (transform.localScale.y / 2) <= 0.0001)
            {
                return true;
            }
        }
        return false;
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        jumpable = LayerMask.GetMask("Jumpable");
        //animator.SetBool("WarriorJump", false);
        //animator.SetBool("ArcherJump", false);
        //animator.SetBool("Grounded", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //animator.SetBool("WarriorJump", false);
        //animator.SetBool("ArcherJump", false);
        if (isGrounded(animator.gameObject.transform))
        {
            animator.SetBool("Grounded", true);
        }
        else
        {
            animator.SetBool("Grounded", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
