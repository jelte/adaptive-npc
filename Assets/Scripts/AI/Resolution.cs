using System;

namespace AI
{
	[Serializable]
	public class Resolution
	{
		private IGoal goal;
		private IRequirement requirement;
		private ITask task;
		
		public Resolution (IGoal goal, IRequirement requirement, ITask task)
		{
			this.goal = goal;
			this.requirement = requirement;
			this.task = task;
		}

		public IGoal Goal {
			get { return goal; }
		}

		public ITask Task {
			get { return task; }
		}

		public bool IsResolved(AICharacter character)
		{
			return requirement != null && requirement.IsFullfilled (character);
		}

		public float CalculateCost(AICharacter character)
		{
			return task.CalculateCost (character) + goal.Cost;
		}

		public float ActionTime {
			get { return task.ActionTime; }
		}
	}
}

