using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLock : MonoBehaviour
{
    JointLimits jointLimits;

    private void Awake()
    {
        LockDoor();
    }

    public void LockDoor()
    {
        HingeJoint hingeJoint = GetComponent<HingeJoint>();
        jointLimits.max = 0;
        hingeJoint.limits = jointLimits;
    }

    public void UnlockDoor()
    {
        HingeJoint hingeJoint = GetComponent<HingeJoint>();
        jointLimits.max = 90;
        hingeJoint.limits = jointLimits;
    }
}
