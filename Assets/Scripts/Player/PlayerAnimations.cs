using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    [SerializeField] Animator animator;
    int tiltDirHash = Animator.StringToHash("tiltDir");
    int isFiringHash = Animator.StringToHash("isFiring");
    int isBombingHash = Animator.StringToHash("isBombing");

    // Tilt (Player.cs)
    public void StopTilt() {
        animator.SetInteger(tiltDirHash, 0);
    }

    public void TiltForward() {
        animator.SetInteger(tiltDirHash, 1);
    }

    public void TiltBackward() {
        animator.SetInteger(tiltDirHash, -1);
    }

    
    // Fire (PlayerShotManager.cs)
    public void StartFiring() {
        animator.SetBool(isFiringHash, true);
    }

    public void StopFiring() {
        animator.SetBool(isFiringHash, false);
    }

    // Bomb (PlayerShotManager.cs)
    public void StartBombing() {
        animator.SetTrigger(isBombingHash);
    }
}
