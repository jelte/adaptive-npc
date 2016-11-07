using UnityEngine;
using System.Collections;

namespace AI {
	
	public interface IRequirement {
		bool IsFullfilled(AICharacter character);
	}
}