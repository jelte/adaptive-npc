using UnityEngine;
using System.Collections.Generic;

public class PositionTracker : MonoBehaviour 
{
	private List<Vector3> positions = new List<Vector3>();

	void Update()
	{
		Vector3 normalizedPosition = Normalize(transform.position);
		if (!positions.Contains (normalizedPosition)) {
			positions.Add (normalizedPosition);
		}
	}

	public List<Vector3> Positions {
		get { return positions; }
	}

	public bool Contains(Vector3 position) {
		return positions.Contains (Normalize (position));
	}

	public int Count
	{
		get { return positions.Count; }
	}

	private Vector3 Normalize(Vector3 position)
	{
		return new Vector3 (
			(int) position.x,
			(int) position.y,
			(int) position.z
		);
	}
}
