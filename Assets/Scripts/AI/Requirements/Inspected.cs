using System;

namespace AI.Requirements
{
	public class Inspected : IRequirement
	{
		private IInspectable inspectable;

		public Inspected (IInspectable inspectable)
		{
			this.inspectable = inspectable;
		}

		public bool IsFullfilled(AICharacter character) {
			return character.Knows(inspectable);
		}

		public IInspectable Inspectable {
			get { return inspectable; }
		}
	}
}

