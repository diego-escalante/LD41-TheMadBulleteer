using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {

	public AudioClip BigHit;
	public AudioClip SmallHit;
	public AudioClip TinyHit;
	public AudioClip ShotFired;
	public AudioClip ShieldRecharge;
	public AudioClip Click;
	public AudioClip Upgrade;
	public AudioClip ShieldUsed;

	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = Camera.main.GetComponent<AudioSource>();
	}
	
	public void playBigHit() {
		audioSource.PlayOneShot(BigHit);
	}

	public void playSmallHit() {
		audioSource.PlayOneShot(SmallHit);
	}

	public void playTinyHit() {
		audioSource.PlayOneShot(TinyHit);
	}

	public void playShotFired() {
		audioSource.PlayOneShot(ShotFired);
	}

	public void playShieldRecharge() {
		audioSource.PlayOneShot(ShieldRecharge);
	}

	public void playShieldUsed() {
		audioSource.PlayOneShot(ShieldUsed);
	}

	public void playClick() {
		audioSource.PlayOneShot(Click);
	}

	public void playUpgrade() {
		audioSource.PlayOneShot(Upgrade);
	}
}
