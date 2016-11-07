using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace AI {
	[Serializable]
	public class TaskManager {

		public List<Type> taskTypes;


		public TaskManager() {
			taskTypes = new List<Type> ();

			taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.MoveToPosition"));
			taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.Explore"));
			taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.Inspect"));
			/*taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.BuyItem"));
			taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.FindVendor"));
			taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.Patrol"));
			taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.Explore"));*/
		//	taskTypes.Add (Assembly.GetExecutingAssembly().GetType("AI.Tasks.CraftItem"));
		}

		public List<ITask> Solve(IRequirement requirement) {
			List<ITask> solutions = new List<ITask> ();
			foreach (Type taskType in taskTypes) {
				if ((bool) taskType.GetMethod("Handles").Invoke(null, new IRequirement[1] { requirement } )) {
					solutions.Add ((ITask) taskType.GetMethod("CreateTask").Invoke(null, new IRequirement[1] { requirement }));
				}
			}

			return solutions;
		}
	}
}