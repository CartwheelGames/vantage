using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode : MonoBehaviour {

	[SerializeField]
	private PathNode nextNode = null;

	GameObject GetNextNode () 
	{
		return nextNode.gameObject;
	}
}
