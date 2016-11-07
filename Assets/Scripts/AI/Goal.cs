using UnityEngine;
using System;
using System.Collections.Generic;

namespace AI {
	[Serializable]
	public class Goal : IGoal {

		public string name;
		public IGoal parent;
		private List<IRequirement> requirements;
		public int cost = 1;
		public bool executing;

		public List<IGoal> children;

		public Goal(List<IRequirement> requirements) {
			this.requirements = requirements;
			this.children = new List<IGoal> ();

			if (this.requirements.Count > 0 && null != this.requirements [0]) {
				this.name = this.requirements [0].GetType ().Name;
			}
		}

		public Goal(List<IRequirement> requirements, string name) : this(requirements) {
			this.name = name;
		}

		public Goal(List<IRequirement> requirements, IGoal parent) : this(requirements) {
			this.parent = parent;
			this.parent.Children.Add (this);
		}

		public List<IRequirement> Requirements {
			get { 
				return this.requirements;
			}
			set {
				this.requirements = value;
			}
		}

		public IGoal Parent {
			get { return parent; }
		}

		public List<IGoal> Children {
			get { return children; }
		}

		public string Name {
			get { return name; }
		}

		public int Cost {
			get {
				int cost = this.cost;
				children.ForEach (delegate(IGoal child) {
					cost += child.Cost;
				});
				return cost;
			}
			set {
				cost = value;
			}
		}
	}
}