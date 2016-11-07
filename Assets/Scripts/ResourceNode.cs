using UnityEngine;
using System.Collections;

public class ResourceNode : MonoBehaviour, IInspectable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 Position {
		get { return transform.position; }
	}

	public Vector3 ClosestPoint(Vector3 position) {
		return GetComponent<Renderer> ().bounds.ClosestPoint(position);
	}
}
