using UnityEngine;
using System.Collections;
using System;

namespace Controller
{
    public class WarriorTop : TopPlayer
    {
        public WarriorTop(Rigidbody rigidBody)
        {
            rb = rigidBody;
        }
        public override void turn(float direction, float turnSpeed)
        {
            rb.transform.Rotate(Vector3.up * direction * turnSpeed);
        }

        public override void useWeapon()
        {
            throw new NotImplementedException();
        }
    }
}