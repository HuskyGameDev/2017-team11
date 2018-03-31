using System;
using AI;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(PlayerAi))]
public class PlayerController : MonoBehaviour {
	public static PlayerController Instance;
	//TODO: variables for save info
	public Entity.Entity[] Cats;
	//TODO: variables for inventory

	private void Awake() {
		if(Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
}
