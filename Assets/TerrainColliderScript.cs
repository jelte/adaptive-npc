using UnityEngine;
using System.Collections.Generic;

public class TerrainColliderScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Terrain") {
			List<Vector2> points = new List<Vector2>();

			int i = 0;
			gameObject.GetComponentInChildren<ResourceNode> ().gameObject.AddComponent<PolygonCollider2D> ();
			foreach (ContactPoint contact in collision.contacts) {
				points.Add(new Vector2(contact.point.x, contact.point.z));
			}

			points.Sort(delegate(Vector2 x, Vector2 y) {
				return x.normalized.x.CompareTo(y.normalized.y);
			});

			gameObject.GetComponentInChildren<ResourceNode> ().gameObject.GetComponent<PolygonCollider2D> ().CreatePrimitive (5);
		}
	}
}
