using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class KnowledgeBase {
	private List<IInspectable> objects = new List<IInspectable>();

	public void Add(IInspectable inspectable)
	{
		if (!objects.Contains (inspectable)) {
			objects.Add (inspectable);
		}
	}

	public bool Knows(IInspectable inspectable) {
		return objects.Contains (inspectable);
	}
}
