﻿using UnityEngine;

public class BlinkAtTheEnd : StateMachineBehaviour {

    private Blink blink;

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (blink == null) {
            blink = animator.GetComponent<Blink>();
        }

        if (stateInfo.normalizedTime > stateInfo.length) {
            blink.BlinkPlease(() => {
                Transform t = animator.transform;
                while (t.parent != null && t.parent.tag == "enemy") {
                    t = t.parent;
                }
                t.gameObject.SetActive(false);
            });
        }

	}

}
