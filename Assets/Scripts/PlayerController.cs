using System;
using System.Collections.Generic;
using Action;
using AI;
using Cataclysm.Resources;
using UnityEngine;

[Serializable]
[RequireComponent(typeof(PlayerAi))]
public class PlayerController : MonoBehaviour {
	public static PlayerController Instance;
	//TODO: variables for save info
	/// <summary>
	/// The list of the player's cats. They start with one cat.
	/// </summary>
	public List<Entity.Entity> Cats = new List<Entity.Entity>
	{
		new Entity.Entity(spriteType: SpriteType.GrayCat, onesie: OnesieRegistry.DefaultOnesie)
	};
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
