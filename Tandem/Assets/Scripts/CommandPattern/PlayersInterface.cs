using UnityEngine;
using System.Collections;
using System;

namespace Controller
{
    public abstract class TopPlayer
    {
        protected Rigidbody rb;
        public abstract void turn(float direction, float turnSpeed);
        public abstract void useWeapon();
    }

    public abstract class BottomPlayer
    {
        protected Rigidbody rb;
        public virtual void moveForward(float vertical, float speed, float slidingSpeed, bool hasForce)
        {
            Vector3 movement = rb.transform.forward * vertical * speed * Time.deltaTime;
            rb.MovePosition(rb.transform.position + movement);
        }
        public abstract void jump(float jumpSpeed, bool bJump);
    }

    
}
