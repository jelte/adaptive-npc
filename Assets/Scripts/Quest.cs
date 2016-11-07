using UnityEngine;
using System.Collections;
using AI;

abstract public class Quest : ScriptableObject {

	abstract public Goal Goal { get; }
}
