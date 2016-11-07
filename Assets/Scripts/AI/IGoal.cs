using System;
using System.Collections.Generic;

namespace AI
{
	public interface IGoal
	{
		List<IRequirement> Requirements {
			get;
		}

		List<IGoal> Children {
			get;
		}

		IGoal Parent {
			get;
		}

		string Name {
			get;
		}
		int Cost {
			get ;
			set ;
		}
	}
}

