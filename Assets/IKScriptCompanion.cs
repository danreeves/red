using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class IKScriptCompanion : MonoBehaviour {

    protected AvatarIKGoal hand;

    public bool ikActive = false;
    public Transform rightHandObj = null;
    public Transform lookObj = null;
    public bool rightHand = true;


    protected AvatarIKGoal ownHand;
    protected Animator animator;
    protected NavMeshAgent navAgent;
    public Transform heroHand;


    void Start ()
    {

        ownHand = AvatarIKGoal.LeftHand;
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();

        animator = GetComponent<Animator>();
        if (rightHand) {
            hand = AvatarIKGoal.RightHand;
        } else {
            hand = AvatarIKGoal.LeftHand;
        }
    }

    //a callback for calculating IK
    void OnAnimatorIK()
    {

        if (Input.GetKeyDown("j")) {
            ikActive = true;
        }
        if (Input.GetKeyUp("j")) {
            ikActive = false;
        }


        if(animator) {

            float distance = Vector3.Distance(animator.GetIKPosition(ownHand), heroHand.position);

            //if the IK is active, set the position and rotation directly to the goal.
            if(ikActive) {

                // Set the look target position, if one has been assigned
                if(lookObj != null) {
                    animator.SetLookAtWeight(1);
                    animator.SetLookAtPosition(lookObj.position);
                }

                // Set the right hand target position and rotation, if one has been assigned
                if(rightHandObj != null) {
                    animator.SetIKPositionWeight(hand, 1);
                    animator.SetIKRotationWeight(hand, 1);
                    animator.SetIKPosition(hand, rightHandObj.position);
                    animator.SetIKRotation(hand, rightHandObj.rotation);
                }

            }

            //if the IK is not active, set the position and rotation of the hand and head back to the original position
            else {
                animator.SetIKPositionWeight(hand, 0);
                animator.SetIKRotationWeight(hand, 0);
                animator.SetLookAtWeight(0);
            }

            if (distance < 2.0f & ikActive) {
                navAgent.stoppingDistance = 0.8f;
                navAgent.speed = 1.2f;
            } else {
                navAgent.stoppingDistance = 999.0f;
                navAgent.speed = 0.8f;
            }


        }
    }
}
