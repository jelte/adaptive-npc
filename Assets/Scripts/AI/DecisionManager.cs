using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
	[Serializable]
	public class DecisionManager
	{
		private AICharacter character;
		private TaskManager taskManager;

		public List<Resolution> resolutions;

		public DecisionManager (AICharacter character)
		{
			this.character = character;
			this.taskManager = new TaskManager ();
			resolutions = new List<Resolution> ();
		}

		public void IndexResolutions(IGoal goal) {
			goal.Requirements.ForEach(delegate(IRequirement requirement) {
				List<ITask> tasks = taskManager.Solve (requirement);
				if (tasks.Count == 0) {
					Debug.Log("Teach me how to resolve "+requirement.GetType().Name);
				}

				tasks.ForEach(delegate(ITask task) {
					resolutions.Add(new Resolution (goal, requirement, task));
				});
			});
		}

		public Resolution DecideAction()
		{
			resolutions.RemoveAll (delegate(Resolution resolution) {
				return !character.goals.Contains(resolution.Goal);
			});

			List<Resolution> unresolved = resolutions.FindAll (delegate(Resolution resolution) {
				return !resolution.IsResolved(character);
			});
			unresolved.Sort (delegate(Resolution x, Resolution y) { // Sort resolutions
				return x.CalculateCost(character).CompareTo(y.CalculateCost(character));
			});
			return unresolved.Count > 0 ? unresolved [0] : null; // Fetch cheapest resolution
		}
	}
}

