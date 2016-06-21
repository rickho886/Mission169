﻿using UnityEngine;
using System;


public class HippieFreedom : MonoBehaviour, IReceiveDamage, IObserver {

    private PhysicsSlugEngine physic;
    private HippieAnimationManager animManager;

    private delegate void VoidNullFunction();
    private VoidNullFunction HippiesBrain;
    private VoidNullFunction HippiesBrainBackup;
    private float hippieSpeedWalkingFactor;
    private float hippieSpeedRunningFactor;
 
    void Awake () {
        animManager = GetComponent<HippieAnimationManager>();
        physic = GetComponent<PhysicsSlugEngine>();
        HippiesBrain = HippieTiedUp;
        hippieSpeedWalkingFactor = 10;
        hippieSpeedRunningFactor = 60;
	}
	
    void Update() {
        HippiesBrain();
    }

    private void HippieTiedUp() {
        // when I'm tied up I do nothing
    }
    public void OnDamageReceived(ProjectileProperties projectileProp, int newHP) {
        animManager.PlayFreeAnim(EndOfHippieFreedAnim);
    }

    private void EndOfHippieFreedAnim() {
        HippiesBrain = HippieWalkAround;
        animManager.PlayWalkingAnim(EndOfHippieWalkAnim);
    }

    private void HippieWalkAround() {
        physic.MoveForward(hippieSpeedWalkingFactor);
    }
    private void EndOfHippieWalkAnim() {
        transform.right = -transform.right;
    }
    public void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player" && HippiesBrain == HippieWalkAround)  {
            HippiesBrain = HippieOfferItem;
        }
    }
   
    private void HippieOfferItem() {
        animManager.PlayGiftAnim(EndOfHippieSalutAnim);
    }
    private void EndOfHippieSalutAnim() {
        HippiesBrain = HippieRunAway;
    }

    private bool runAwayStarted;
    private void HippieRunAway() {
        if (!runAwayStarted) {
            runAwayStarted = true;
        }
        physic.MoveForward(hippieSpeedRunningFactor);
    }

    void OnBecameInvisible() {
        DestroyObject(gameObject);
    }

    public void Observe(SlugEvents ev) {
        if (ev == SlugEvents.Fall) {
            HippiesBrainBackup = HippiesBrain;
            HippiesBrain = HippieTiedUp;
        } else if (ev == SlugEvents.HitGround) {
            HippiesBrain = HippiesBrainBackup;
        }
    }
}