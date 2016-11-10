using System;
using System.Collections.Generic;
using UnityEngine;
using AI.Requirements;

namespace AI
{
	public class AICharacter : MonoBehaviour, ICharacter, IInspectable
	{
		public List<IGoal> goals = new List<IGoal>();
		public GameObject target;
		public GameObject targetPrefab;

		private Resolution resolution;
		private DecisionManager decisionManager;
		private KnowledgeBase knowledgeBase;

		private float timer;

		void Awake() {
			decisionManager = new DecisionManager (this);
			knowledgeBase = new KnowledgeBase ();

			if (null == GetComponent<Rigidbody> ()) {
				gameObject.AddComponent<Rigidbody>();
				gameObject.GetComponent<Rigidbody> ().freezeRotation = true;
			}
			if (null == GetComponent<NavMeshAgent> ()) {
				gameObject.AddComponent<NavMeshAgent>();
			}

			if (null == GetComponent<PositionTracker> ()) {
				gameObject.AddComponent<PositionTracker>();
			}
			if (null == target) {
				target = Instantiate (targetPrefab);
			}

			AddGoal (new Goal (new List<IRequirement> () { new ExploreArea(50) }));
		}

		void FixedUpdate() {
			CleanUpGoals ();

			if (resolution == null) {
				resolution = decisionManager.DecideAction ();
				if (resolution != null) {
					resolution.Task.Execute (this, resolution.Goal);
					timer = 0;
				}
			} else {
				timer += Time.deltaTime;
				if (resolution.IsResolved (this) || timer >= resolution.ActionTime) {
					this.resolution = null;
				}
			}

		}        

		void CleanUpGoals()
		{
			goals.RemoveAll (delegate(IGoal goal) {
				bool shouldRemove = goal.Requirements.FindAll(delegate(IRequirement requirement) {
					return !requirement.IsFullfilled(this);
				}).Count == 0;
				if (shouldRemove && null != goal.Parent) {
					goal.Parent.Children.Remove(goal);
				}
				return shouldRemove;
			});
		}

		public void AddGoal(IGoal goal) 
		{
			goals.Add (goal);
			decisionManager.IndexResolutions (goal);
		}

		public bool Knows(IInspectable inspectable) 
		{
			return knowledgeBase.Knows(inspectable);
		}

		public void Inspect(IInspectable inspectable)
		{
			knowledgeBase.Add (inspectable);
		}

		public Vector3 Position {
			get { return transform.position; }
		}

		public PositionTracker ExploredArea {
			get { return GetComponent<PositionTracker> (); }
		}

		public Vector3 ClosestPoint(Vector3 position) {
			return GetComponentInChildren<Renderer> ().bounds.ClosestPoint(position);
		}
	}
}

