using UnityEngine;
using System.Collections;

public class runAnimations : MonoBehaviour {

    
    private Animator animator;
    public GameObject weapon;
    public GameObject IK;

    //animator parameters;
    private string WARRIORSPEED = "WarriorSpeed";
    private string ARCHERSPEED = "ArcherSpeed";
    private string WARRIORJUMP = "WarriorJump";
    private string ARCHERJUMP = "ArcherJump";
    private string ARCHERFLIP = "ArcherFlip";
    private string WARRIORFLIP = "WarriorFlip";

    // Use this for initialization
    void Start () {
        
        animator = GetComponent<Animator>();
        weapon.SetActive(false);
        IK.GetComponent<customIK>().enabled = false;
        
	}

    void Update()
    {
        float warriorVertical = Input.GetAxis("Vertical");
        float archerVertical = Input.GetAxis("Vertical2");
        bool warriorJump = Input.GetButton("Jump") && animator.GetBool("Grounded");
        bool archerJump = Input.GetButton("Jump2") && animator.GetBool("Grounded");
        bool warriorFlip = Input.GetAxis("Switch1") > 0;
        bool archerFlip = Input.GetAxis("Switch2") > 0;
        bool flip = warriorFlip && archerFlip;

        if (animator)
        {
            animator.SetFloat(WARRIORSPEED, Mathf.Abs(warriorVertical));
            animator.SetFloat(ARCHERSPEED, Mathf.Abs(archerVertical));
            animator.SetBool(ARCHERJUMP, archerJump);
            animator.SetBool(WARRIORJUMP, warriorJump);
            animator.SetBool(WARRIORFLIP, warriorFlip);
            animator.SetBool(ARCHERFLIP, archerFlip);
        }
        //activate archer bow and arm movements
        if (animator.GetBool("bottom") && !flip)
        {
            IK.GetComponent<customIK>().enabled = true;
        }
        else
        {
            IK.GetComponent<customIK>().enabled = false;
        }
        
    }
	
}
