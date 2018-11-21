using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class IKAim : MonoBehaviour {
    #region Data

    [SerializeField]
    Transform myRightHandMiddleFinger;
    Transform myRightHand;
    Animator anim;
    #endregion

    private void Awake() {
        anim = GetComponent<Animator>();
        myRightHand = myRightHandMiddleFinger.parent;
    }
    private void OnAnimatorIK() {
        Vector3 targetPosition = 
        anim.SetIKPosition(AvatarIKGoal.LeftHand, Vector3.zero); //Defining anchor position.
        anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, .4f);
    }
}
