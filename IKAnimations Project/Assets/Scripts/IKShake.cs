using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Michael-Vincent Mijares

//Video Reference
//https://www.youtube.com/watch?v=GYfeALySSq8&

//This script is following a tutorial for learning IK with Animations 
public class IKShake : MonoBehaviour {
    #region Data

    [SerializeField]
    Transform myRightHandMiddleFinger;
    Transform myRightHand;
    [SerializeField]
    IKShake otherGuy;
    Animator anim;

    static readonly Vector3 offset = new Vector3(.02f, .04f, 0);

    Vector3 myRightMiddleFingerPosition, myRightHandPosition;
    Quaternion myRightMiddleFingerRotation;

    [SerializeField]
    float startTime = .25f;
    [SerializeField]
    float endTime = .55f;
    #endregion

    float percentComplete {
        get {
            AnimatorStateInfo currentAnimation = anim.GetCurrentAnimatorStateInfo(0);
            float percent =  currentAnimation.normalizedTime % 1; // always get a value of 0 - 1 because animation is on loop. We get a value greater than 1 because animation is on loop
            percent -= startTime;
            percent /= endTime - startTime; //duration

            //out of bounds
            if(percent <= 0 || percent >= 1) {
                return 0;
            }
            percent *= 2; // 0 -> 2
            if(percent > 1) {
                percent = 2 - percent;
            }
            return percent;
        }
    }
    private void Awake() {
        anim = GetComponent<Animator>();
        myRightHand = myRightHandMiddleFinger.parent;
    }
    private void OnAnimatorIK(int layerIndex) {
        Vector3 targetPosition = otherGuy.myRightMiddleFingerPosition+ (otherGuy.myRightMiddleFingerRotation * offset); //from middle finger position to palm of hand considering the rotation of the hand.
        targetPosition += myRightMiddleFingerPosition + myRightMiddleFingerRotation * offset - myRightHandPosition;

        anim.SetIKPosition(AvatarIKGoal.RightHand, targetPosition); //anchor position, destination position
        anim.SetIKPositionWeight(AvatarIKGoal.RightHand, percentComplete); //Ratio between animation and reaching destination position.
        /*
        //example approaching from different angles
        anim.setIKRotation(AvatarIKGoal.RightHand, [need to calculate for this value]);
        anim.setIKRotationWeight(AvatarIKGoal.RightHand, percentComplete);

        //isnt really IK
        //example turning the head
        anim.SetLookAtPosition(targetPosition);
        anim.SetLookAtWeight(percentComplete);
         
        //lerping the body
        anim.bodyRotation = Quaternion.Lerp(transform.rotation, delta * Quaternion.LookRotation(temp, Vector3.up), percentComplete);
         */ 
    }

    //cache information for IK to use
    private void LateUpdate() {
        myRightMiddleFingerPosition = myRightHandMiddleFinger.position;
        myRightMiddleFingerRotation = myRightHandMiddleFinger.rotation;
        myRightHandPosition = myRightHand.position;
    }

}
