using UnityEngine;
using System.Collections.Generic;
using AI.Requirements;

namespace AI.Tasks {
	public class MoveToPosition : ITask {

		private Vector3 position;

		public static bool Handles(IRequirement requirement) {
			return requirement is AtPosition;
		}

		public static ITask CreateTask(IRequirement requirement) {
			return new MoveToPosition (((AtPosition)requirement).Position);
		}

		public MoveToPosition(Vector3 position) {
			this.position = position;
		}
			
		public void Execute (AICharacter character, IGoal goal) {
			NavMeshAgent agent = character.gameObject.GetComponent<NavMeshAgent> ();
			agent.SetDestination (this.position);
		}

		public float CalculateCost (AICharacter character) {
			return 0.0f;
		}

		public float ActionTime {
			get { return 1.0f; }
		}

		public List<IRequirement> Requirements {
			get { return null; }
		}
	}
}
