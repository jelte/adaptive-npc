using System;
using UnityEngine;
using AI.Requirements;
using System.Collections.Generic;

namespace AI.Tasks
{
	public class Explore : ITask
	{
		private ExploreArea requirement;
		private List<IRequirement> requirements = new List<IRequirement> ();
		private int startCount = 0;
		
		public static bool Handles(IRequirement requirement) {
			return requirement is ExploreArea;
		}

		public static ITask CreateTask(IRequirement requirement) {
			return new Explore ((ExploreArea) requirement);
		}

		public Explore(ExploreArea requirement) {
			this.requirement = requirement;
		}

		public void Execute (AICharacter character, IGoal goal) {
			List<IInspectable> inspectables = FindInspectablesInView (character);
			inspectables.Sort (delegate(IInspectable x, IInspectable y) {
				return (character.Position - x.Position).magnitude.CompareTo((character.Position - y.Position).magnitude);	
			});
			IInspectable inspectable = inspectables.Find (delegate(IInspectable obj) {
				return !character.Knows(obj);
			});

			if (inspectable == null) {
				NavMeshAgent agent = character.gameObject.GetComponent<NavMeshAgent> ();
				NavMeshHit navHit;

				do {
					NavMesh.SamplePosition (UnityEngine.Random.insideUnitSphere * agent.speed + character.Position, out navHit, agent.speed, -1);
				} while (character.ExploredArea.Contains (navHit.position));

				character.AddGoal (new Goal (new List<IRequirement> () { new AtPosition (navHit.position) }, goal));
			} else {
				

				character.AddGoal (new Goal (new List<IRequirement> () { new Inspected (inspectable) }, goal));
			}
		}

		private List<IInspectable> FindInspectablesInView(AICharacter character) {
			List<IInspectable> inspectables = new List<IInspectable> ();
			RaycastHit[] castStar = Physics.SphereCastAll(character.transform.position, 360, Vector3.forward);
			foreach (RaycastHit raycastHit in castStar) {
				if (raycastHit.collider.GetComponentInChildren<IInspectable>() != null && raycastHit.collider.GetComponentInChildren<AICharacter>() != character) {
					inspectables.Add (raycastHit.collider.GetComponentInChildren<IInspectable> ());
				}
			}
			return inspectables;
		}

		public float CalculateCost (AICharacter character) {
			if (startCount == 0) {
				startCount = character.ExploredArea.Count;
			}
			return (float) requirement.Distance - (character.ExploredArea.Count - startCount);
		}

		public float ActionTime {
			get { return 0.0f; }
		}

		public List<IRequirement> Requirements {
			get { return this.requirements; }
		}
	}
}

