﻿using UnityEngine;

public class MummyAnimationEvents : MonoBehaviour {

    private string victimsTag = "Player";
    private AreaOfEffectProjectile badBreath;
    private MummyBreath mummyBreath;

	// Use this for initialization
	void Awake () {
        badBreath = GetComponent<AreaOfEffectProjectile>();
        mummyBreath = GetComponentInChildren<MummyBreath>();
	}
	
    public void AECastBadBreath() {
        badBreath.CastAOE(victimsTag, transform.position);
        mummyBreath.ThrowBreath();
    }

}
