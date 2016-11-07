using System;
using System.Collections.Generic;
using UnityEngine;
using AI.Requirements;

namespace AI.Tasks
{
	public class Inspect : ITask
	{
		private IInspectable inspectable;

		public Inspect (IInspectable inspectable)
		{
			this.inspectable = inspectable;
		}

		public static bool Handles(IRequirement requirement) {
			return requirement is Inspected;
		}

		public static ITask CreateTask(IRequirement requirement) {
			return new Inspect (((Inspected) requirement).Inspectable);
		}

		public void Execute (AICharacter character, IGoal goal) {
			Vector3 closestPoint = inspectable.ClosestPoint (character.Position);

			if ((closestPoint - character.Position).magnitude <= 1.0f) {
				character.Inspect (inspectable);
			} else {
				character.AddGoal (new Goal (new List<IRequirement> () { new AtPosition (closestPoint) }, goal));
			}
		}

		public float CalculateCost (AICharacter character) {
			return (inspectable.ClosestPoint (character.Position) - character.Position).magnitude;
		}

		public float ActionTime {
			get { return 0.0f; }
		}

		public List<IRequirement> Requirements {
			get { return null; }
		}
	}
}

