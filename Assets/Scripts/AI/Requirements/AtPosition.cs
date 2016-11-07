using UnityEngine;
using System.Collections;

namespace AI.Requirements {
	public class AtPosition : IRequirement {

		private Vector3 position;

		public AtPosition(Vector3 position) {
			this.position = position;
		}

		public Vector3 Position {
			get { return position; }
		}
			
		public bool IsFullfilled(AICharacter character) {
			return (character.Position - position).magnitude <= 1.0f;
		}
	}
}