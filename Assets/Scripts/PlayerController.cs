using System;
using AI;
using Entity;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(PlayerAi))]
public class PlayerController : MonoBehaviour {
	public static PlayerController Instance;
	//TODO: variables for save info
	public GameEntity[] Cats;
	//TODO: variables for inventory

	void Awake() {
		if(Instance == null) {
			Instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}
}
