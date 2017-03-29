//------------------------------------------------------------------------------------------------

// Author: Colby Johnson

// Date: 12/11/2016

// Credit: Game Development Experiment 4 - NavMesh -  Full Sail University

// Credit: Game Development Experiment 0 - Unity Scripting -  Full Sail University

//

// Purpose: A class which handles the Debug Manager

//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class DebugManager : MonoBehaviour {

	private Guard[] guardArray;
	private int curGuard = 0;

    private bool pathNodesVisible = true;

	private PathNode[] pathNodeArray;

	// Use this for initialization
	void Start () {

		guardArray = GameObject.FindObjectsOfType<Guard>();

		guardArray[curGuard].GetComponent<Highlight>().SetHighlightEnabled(true);

		pathNodeArray = GameObject.FindObjectsOfType<PathNode>();

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.RightArrow)) {

            if (curGuard >= 0) {

                guardArray[curGuard].GetComponent<Highlight>().SetHighlightEnabled(false);

            }

            // increment the index
            curGuard++;

            if (curGuard >= guardArray.Length) {

                curGuard = 0;

            }

            // Show the status
            Debug.Log("DebugManager selected object #" + curGuard);

            // Enable highlight on currently selected object in the array
            guardArray[curGuard].GetComponent<Highlight>().SetHighlightEnabled(true);

		} else if (Input.GetKeyDown(KeyCode.LeftArrow)) {

            if (curGuard <= guardArray.Length) {

                guardArray[curGuard].GetComponent<Highlight>().SetHighlightEnabled(false);

            }

            // decrement the index
            curGuard--;

            if (curGuard <= 0) {

                curGuard = guardArray.Length - 1;

            }

            // Show the status
            Debug.Log("DebugManager selected object #" + curGuard);

            // Enable highlight on currently selected object in the array
            guardArray[curGuard].GetComponent<Highlight>().SetHighlightEnabled(true);

        } else if (Input.GetKeyDown(KeyCode.H)) {

        	if (pathNodesVisible == true) {

        		foreach (PathNode pathNode in pathNodeArray) {

        			pathNode.GetComponent<Renderer>().enabled = false;

        		}

				pathNodesVisible = false;

        	} else {

        		foreach (PathNode pathNode in pathNodeArray) {

        			pathNode.GetComponent<Renderer>().enabled = true;

        		}

				pathNodesVisible = true;

        	}

        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {

			GuardStateMachine gsm = guardArray[curGuard].GetComponent<GuardStateMachine>();

			guardArray[curGuard].GetComponent<Highlight>().SetHighlightEnabled(false);
        	gsm.Pause();

        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {

			GuardStateMachine gsm = guardArray[curGuard].GetComponent<GuardStateMachine>();

			if (gsm.IsCurrentState<AngryState>()) {

				gsm.ChangeState<CalmingState>();

			} else if (!gsm.IsCurrentState<BecomeAngryState>() && !gsm.IsCurrentState<AngryState>() && !gsm.IsCurrentState<CalmingState>()) {

				gsm.ChangeState<BecomeAngryState>();

			}

        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {

        	GuardStateMachine gsm = guardArray[curGuard].GetComponent<GuardStateMachine>();

        	if (gsm.IsCurrentState<PatrolState>()) {

        		guardArray[curGuard].GuardReverseChange();

        	}

        }
	
	}

}
