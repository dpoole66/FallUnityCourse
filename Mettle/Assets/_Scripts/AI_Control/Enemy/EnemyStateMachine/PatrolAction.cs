using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mettle;


[CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
public class PatrolAction : Action {

    public override void Act(StateController controller) {
        Patrol(controller);
    }

    private void Patrol(StateController controller) {

    }
	
}
