using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IKAim : MonoBehaviour {
 

    private void Awake() {
        anim = GetComponent<Animator>();
        myRightHand = myRightHandMiddleFinger.parent;
    }
    private void OnAnimatorIK() {

    }
}
