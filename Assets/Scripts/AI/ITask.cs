using System.Collections.Generic;

namespace AI {
	public interface ITask {
		/* static bool Handles(Requirement requirement); */
	 	/* static Task CreateTask(Requirement requirement); */

		void Execute (AICharacter character, IGoal goal);
		float CalculateCost (AICharacter character);

		List<IRequirement> Requirements {
			get;
		}

	 	float ActionTime {
			get;
		}
	}
}