using UnityEngine;
using System.Collections;
using Controller;

public class InputHandler : MonoBehaviour {

    public float jumpSpeed = 5f;
    public float turnSpeed = 3f;
    //warrios movement speeds
    public float onMoltenSpeed = 1f;
    public float warriorSpeed = 4f;
    private bool warriorHasForce;
    //archer movment speeds
    public float onIceSpeed = 2f;
    public float archerSpeed = 4f;
    private bool archerHasForce;

    private Rigidbody rb;

    private Animator animator;

    //animator parameters;
    private string WARRIORSPEED = "WarriorSpeed";
    private string ARCHERSPEED = "ArcherSpeed";
    private string WARRIORJUMP = "WarriorJump";
    private string ARCHERJUMP = "ArcherJump";
    private string FLIP = "flip";

    //players states
    TopPlayer archerTop, warriorTop;
    BottomPlayer archerBottom, warriorBottom;
     
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        archerTop = new ArcherTop(rb);
        archerBottom = new ArcherBottom(rb);
        warriorTop = new WarriorTop(rb);
        warriorBottom = new WarriorBottom(rb);
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate()
    {
        float warriorVertical = Input.GetAxis("Vertical");
        float archerVertical = Input.GetAxis("Vertical2");
        bool warriorJump = Input.GetButton("Jump") && animator.GetBool("Grounded");
        bool archerJump = Input.GetButton("Jump2") && animator.GetBool("Grounded");
        float warriorTurn = Input.GetAxis("Horizontal");
        float archerTurn = Input.GetAxis("Horizontal2");
        bool flip = Input.GetAxis("Switch1") > 0 && Input.GetAxis("Switch2") > 0;

        if (animator)
        {
            animator.SetFloat(WARRIORSPEED, Mathf.Abs(warriorVertical));
            animator.SetFloat(ARCHERSPEED, Mathf.Abs(archerVertical));
            animator.SetBool(ARCHERJUMP, archerJump);
            animator.SetBool(WARRIORJUMP, warriorJump);
            animator.SetBool(FLIP, flip);
        }

        //check if warrior is at bottom
        if (animator.GetBool("bottom") && !flip)
        {
            archerTop.turn(archerTurn, turnSpeed);
            warriorBottom.jump(jumpSpeed, warriorJump);
            warriorBottom.moveForward(warriorVertical, warriorSpeed, onMoltenSpeed, warriorHasForce);
        }

        if (!animator.GetBool("bottom") && !flip)
        {
            warriorTop.turn(warriorTurn, turnSpeed);
            archerBottom.jump(jumpSpeed, archerJump);
            archerBottom.moveForward(archerVertical, archerSpeed, onIceSpeed, archerHasForce);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Icy Ground") archerHasForce = true;
        if (collision.gameObject.tag == "Molten Ground") warriorHasForce = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Icy Ground") archerHasForce = false;
        if (collision.gameObject.tag == "Molten Ground") warriorHasForce = false;
    }
}
