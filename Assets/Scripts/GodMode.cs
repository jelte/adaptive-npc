using UnityEngine;
using System.Collections;
using AI;
using UnityStandardAssets.CrossPlatformInput;



public class GodMode : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update()
	{
		if (Input.GetMouseButtonDown (0)) {
			RaycastHit hitInfo = new RaycastHit ();
			if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
				if (hitInfo.rigidbody && hitInfo.rigidbody.gameObject.GetComponent<AICharacter> ()) {
					AICharacter character = hitInfo.rigidbody.gameObject.GetComponent<AICharacter> ();

					Debug.Log ("Character" + character.Position);
				} 
			} 
		}
	}
	// Fixed update is called in sync with physics
	private void FixedUpdate()
	{
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");

			// we use world-relative directions in the case of no main camera
		Vector3 move = v * Vector3.forward + h * Vector3.right;

		gameObject.transform.position += move;
	}
}
