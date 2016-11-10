using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
		if (GetComponent<Collider> () != null) {
			return GetComponent<Collider> ().bounds.ClosestPoint(position);
		}
		List<Vector3> closestPoints = new List<Vector3> ();
		foreach (Collider collider in GetComponentsInChildren<Collider>()) {
			closestPoints.Add (collider.bounds.ClosestPoint(position));
		}
		closestPoints.Sort (delegate(Vector3 x, Vector3 y) {
			return (x - position).magnitude.CompareTo((y - position).magnitude);
		});
		return closestPoints.Count > 0 ? closestPoints[0] : Vector3.zero;
	}
}
