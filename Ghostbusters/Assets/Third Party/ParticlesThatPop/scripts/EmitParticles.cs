using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitParticles : MonoBehaviour {

	private ParticleSystem TheParticles;
	// Use this for initialization
	void Start () {
		TheParticles = GetComponentInChildren<ParticleSystem>();

		if(TheParticles == null)
		{
			Debug.Log("No Particles Attached. Drag a particle system from Prefabs onto the game object named TheHolder.");
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void EmitSomeParticles()
	{
		int HowManyParticles = 144; // change this number for more or less particles.
		TheParticles.Emit(HowManyParticles);
	}
}
