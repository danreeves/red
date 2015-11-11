using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class Companion : MonoBehaviour {

    protected AvatarIKGoal ownHand;
    protected Animator animator;
    protected NavMeshAgent navAgent;
    private bool jDown = false;
    public Transform heroHand;

	// Use this for initialization
	void Start () {
        ownHand = AvatarIKGoal.LeftHand;
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void OnAnimatorIK () {

        if (Input.GetKeyDown("j")) {
            jDown = true;
        }
        if (Input.GetKeyUp("j")) {
            jDown = false;
        }

        float distance = Vector3.Distance(animator.GetIKPosition(ownHand), heroHand.position);
        Debug.Log(distance);
        if (distance < 1.5f & jDown) {
            navAgent.stoppingDistance = 0.0f;
            navAgent.speed = 1.2f;
        } else {
            navAgent.stoppingDistance = 999.0f;
            navAgent.speed = 0.8f;
        }

	}
}
