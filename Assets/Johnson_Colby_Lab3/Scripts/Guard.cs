//------------------------------------------------------------------------------------------------

// Author: Colby Johnson

// Date: 12/11/2016

// Credit: Game Development Experiment 4 - NavMesh -  Full Sail University

//

// Purpose: A class which handles the Guard

//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

	[SerializeField] private float idleTime = 2f;
	[SerializeField] private float lerpTime = 2f;
	[SerializeField] private PathNode[] pathNodeArray;
	[SerializeField] private AlarmNode alarmNode;
	private bool guardReverse = false;
	private UnityEngine.AI.NavMeshAgent myNavAgent;
	private int navIndex = 0;

	private GuardStateMachine gsm;

	private float timePassed = 0f;

	// Use this for initialization
	void Start () {

		myNavAgent = GetComponent("NavMeshAgent") as UnityEngine.AI.NavMeshAgent;

		gsm = GetComponent<GuardStateMachine>();

		ResetTimeSinceLastTransition();
	
	}
	
	// Update is called once per frame
	void Update () {

		UpdateTimePassed ();
	
	}

	void UpdateTimePassed () {

		timePassed += Time.deltaTime;

	}

	public float GetTimePassed () {

		return timePassed;

	}

	public float GetIdleTime () {

		return idleTime;

	}

	public float GetLerpTime () {

		return lerpTime;

	}

	public bool GetGuardReverse () {

		return guardReverse;

	}

	public void GuardReverseChange () {

		guardReverse = !guardReverse;

	}

	public void ResetTimeSinceLastTransition () {

		timePassed = 0f;

	}

	public void FindDestination () {

		if (gsm.IsCurrentState<PatrolState>() || gsm.IsCurrentState<CalmingState>()) {

			Vector3 newTravelPosition = pathNodeArray[navIndex].transform.position;

			myNavAgent.SetDestination(newTravelPosition);

		} else if (gsm.IsCurrentState<PauseState>()) {

			myNavAgent.SetDestination(gameObject.transform.position);

		} else if (gsm.IsCurrentState<BecomeAngryState>() || gsm.IsCurrentState<AngryState>()) {

			Vector3 newTravelPosition = alarmNode.transform.position;

			myNavAgent.SetDestination(newTravelPosition);

		}
	}

	void OnTriggerEnter (Collider col) {

		if (col.GetComponent<PathNode>() && !gsm.IsCurrentState<AngryState>() && !gsm.IsCurrentState<BecomeAngryState>()) {

			if (!guardReverse) {

				navIndex++;

				if (navIndex >= pathNodeArray.Length) {

					navIndex = 0;

				}

			} else if (guardReverse) {

				navIndex--;

				if (navIndex < 0) {

					navIndex = pathNodeArray.Length - 1;

				}

			}

			gsm.ChangeState<IdleState>();

		} else if (col.GetComponent<AlarmNode>()) {

			gsm.ChangeState<CalmingState>();

		}

	}

}
