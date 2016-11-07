using UnityEngine;
using System.Collections;

public interface IInspectable
{
 	Vector3 ClosestPoint(Vector3 position);

	Vector3 Position {
		get;
	}
}
