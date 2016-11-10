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
			RaycastHit[] castStar = Physics.SphereCastAll(character.transform.position, 165, Vector3.forward, 50);
			foreach (RaycastHit raycastHit in castStar) {
				if ( raycastHit.collider.GetComponentInChildren<AICharacter>() != character) {
					IInspectable inspectable = null;
					if (raycastHit.collider.GetComponentInChildren<IInspectable>() != null) {
						inspectable = raycastHit.collider.GetComponentInChildren<IInspectable> ();
					} else if (raycastHit.collider.GetComponentInParent<IInspectable>() != null) {
						inspectable = raycastHit.collider.GetComponentInParent<IInspectable> ();
					}
					if (inspectable != null && !inspectables.Contains(inspectable)) {
						inspectables.Add (inspectable);
					}
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

