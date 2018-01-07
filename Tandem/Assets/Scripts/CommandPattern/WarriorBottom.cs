using UnityEngine;
using System.Collections;
using System;

namespace Controller
{
    public class WarriorBottom : BottomPlayer
    {
        
        public WarriorBottom(Rigidbody rigidBody)
        {
            rb = rigidBody;
        }

        public override void jump(float jumpSpeed, bool bJump)
        {
            if (bJump)
                rb.velocity = new Vector3(0f, jumpSpeed, 0f);
        }

        public override void moveForward(float vertical, float speed, float slidingSpeed, bool hasForce)
        {
            if (hasForce)
            {
                Vector3 movement = rb.transform.forward * vertical * slidingSpeed * Time.deltaTime;
                rb.MovePosition(rb.transform.position + movement);
            }
            else
            {
                base.moveForward(vertical, speed, slidingSpeed, hasForce);
            }
        }
    }
}
