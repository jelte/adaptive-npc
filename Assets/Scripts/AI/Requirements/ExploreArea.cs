using UnityEngine;
using System.Collections;
using AI;

namespace AI.Requirements {
	public class ExploreArea : IRequirement {

		private int distance;
		private int currentExploredArea = -1;

		public ExploreArea(int distance) {
			this.distance = distance;
		}

		public bool IsFullfilled(AICharacter character) {
			if (currentExploredArea < 0) {
				currentExploredArea = character.ExploredArea.Count;
			}
			return character.ExploredArea.Count - currentExploredArea >= distance;
		}

		public int Distance {
			get { return distance; }
		}
	}
}
