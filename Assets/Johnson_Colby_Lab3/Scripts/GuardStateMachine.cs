//------------------------------------------------------------------------------------------------

// Author: Colby Johnson

// Date: 12/11/2016

// Credit: Game Development Experiment 4 - NavMesh -  Full Sail University

//

// Purpose: A class which handles the Guard's State Machine

//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class GuardStateMachine : ByTheTale.StateMachine.MachineBehaviour {

	public override void AddStates () {

		AddState<PatrolState>();
		AddState<IdleState>();
		AddState<PauseState>();
		AddState<BecomeAngryState>();
		AddState<AngryState>();
		AddState<CalmingState>();

		SetInitialState<PatrolState>();

	}

	bool paused = false;
    ByTheTale.StateMachine.State lastState = null;


    public void Pause()
    {
        // toggle paused value
        paused = !paused;
    
        if (paused) {

            // store current state for use when unpausing
            lastState = currentState;
    
            // change state to Pause
            ChangeState<PauseState>();

        } else {

            // restore stored state when pausing earlier
            ChangeState(lastState.GetType());

        }

    }


}

public class GuardState : ByTheTale.StateMachine.State {

	protected Guard guard;

	public override void Enter () {

		guard = GetMachine<GuardStateMachine>().GetComponent<Guard>();

		guard.ResetTimeSinceLastTransition();

	}

}

// Create a class for the Patrol state
public class PatrolState : GuardState {

	public override void Enter () {

		base.Enter();

	}

	public override void Execute () {

		if (!guard.GetGuardReverse()) {

			guard.GetComponent<Renderer>().material.color = Color.green;

		} else if (guard.GetGuardReverse()) {

			guard.GetComponent<Renderer>().material.color = Color.blue;

		}

		guard.FindDestination();

	}

	public override void Exit () {

		

	}

}

// Create a class for the Idle state
public class IdleState : GuardState {

	public override void Enter () {

		base.Enter();

		guard.GetComponent<Renderer>().material.color = new Color(0f, 0.5f, 0f);

	}

	public override void Execute () {

		if (guard.GetTimePassed() >= guard.GetIdleTime()) {

			machine.ChangeState<PatrolState>();

		}

	}

	public override void Exit () {

		

	}

}

public class PauseState : GuardState {

	public override void Enter () {

		base.Enter();

		guard.GetComponent<Renderer>().material.color = Color.gray;

	}

	public override void Execute () {

		guard.FindDestination();

	}

	public override void Exit () {

		

	}

}

public class BecomeAngryState : GuardState {

	public override void Enter () {

		base.Enter();

		guard.ResetTimeSinceLastTransition();

	}

	public override void Execute () {

		guard.FindDestination();

		guard.GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, guard.GetLerpTime());
		guard.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);

		if (guard.GetTimePassed() >= guard.GetLerpTime()) {

			machine.ChangeState<BecomeAngryState>();

		}

	}

	public override void Exit () {

		

	}

}

public class AngryState : GuardState {

	public override void Enter () {

		base.Enter();

		guard.GetComponent<Renderer>().material.color = Color.red;

	}

	public override void Execute () {

		if (Input.GetKeyDown(KeyCode.Alpha3)) {

			machine.ChangeState<CalmingState>();

		}

		guard.FindDestination();

	}

	public override void Exit () {

		

	}

}

public class CalmingState : GuardState {

	public override void Enter () {

		base.Enter();

	}

	public override void Execute () {

		guard.FindDestination();

		guard.GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.green, guard.GetLerpTime());
		guard.transform.localScale = new Vector3 (1f, 0.5f, 1f);

	}

	public override void Exit () {

		

	}

}
