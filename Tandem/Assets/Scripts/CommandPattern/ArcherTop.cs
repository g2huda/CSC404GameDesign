using UnityEngine;
using System.Collections;
using System;

namespace Controller
{
    public class ArcherTop : TopPlayer
    {
        public ArcherTop(Rigidbody rigidBody)
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
