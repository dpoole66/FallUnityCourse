using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mettle;


[CreateAssetMenu(menuName = "PluggableAI/State")]       
public class ApproachAction : Action {

    public override void Act(StateController controller) {
        Approach(controller);
    }

    private void Approach(StateController controller) {

    }
}
