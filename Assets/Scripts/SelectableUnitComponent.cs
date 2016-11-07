using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;


public class SelectableUnitComponent : MonoBehaviour
{
    private GameObject selectionCircle;

	public void ClearSelection() {
		if (selectionCircle != null) {
			Destroy (selectionCircle.gameObject);
			selectionCircle = null;
		}
	}

	public GameObject SelectionCircle {
		get { return selectionCircle; }
		set { 
			selectionCircle = value;
			if (selectionCircle != null) {
				selectionCircle.transform.SetParent (GetComponentInParent<Transform> (), false);
				selectionCircle.transform.eulerAngles = new Vector3 (90, 0, 0);
			}
		}
	}
}