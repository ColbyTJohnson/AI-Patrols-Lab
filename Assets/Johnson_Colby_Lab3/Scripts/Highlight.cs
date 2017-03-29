//------------------------------------------------------------------------------------------------

// Author: Colby Johnson

// Date: 12/11/2016

// Credit: Game Development Experiment 0 - Unity Scripting -  Full Sail University

//

// Purpose: A class which handles the highlighting for guards

//------------------------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class Highlight : MonoBehaviour {

    private bool highlightEnabled = false;
    private Color lastColor;

    // Accessor for enabling the highlight effect
    public void SetHighlightEnabled (bool enableIn) {        

        highlightEnabled = enableIn;

        if (highlightEnabled) {

            lastColor = gameObject.GetComponent<Renderer>().material.color;

        } else {

            gameObject.GetComponent<Renderer>().material.color = lastColor;

        }

    }

    // Update is called once per frame
    void Update() {

        if (highlightEnabled) {
    
            if (gameObject.GetComponent<Renderer>().material.color !=  Color.yellow) {

                lastColor = gameObject.GetComponent<Renderer>().material.color;

            }

            gameObject.GetComponent<Renderer>().material.color = Color.yellow;

        }

    }

}
