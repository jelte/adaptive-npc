using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AI;
using AI.Requirements;

public class UnitSelectionComponent : MonoBehaviour
{
    bool isSelecting = false;
	bool isMoveTo = false;
    Vector3 mousePosition1;
	Vector3 leftMousePosition;

    public GameObject selectionCirclePrefab;

	public List<SelectableUnitComponent> selectedUnits = new List<SelectableUnitComponent>();

    void Update()
    {
        // If we press the left mouse button, begin selection and remember the location of the mouse
        if( Input.GetMouseButtonDown(1) ) {
            isSelecting = true;
            mousePosition1 = Input.mousePosition;

			selectedUnits.ForEach (delegate(SelectableUnitComponent obj) {
				obj.ClearSelection();	
			});
			selectedUnits.Clear ();

			RaycastHit hit;
			if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
				SelectableUnitComponent selectableObject = hit.rigidbody.gameObject.GetComponent<SelectableUnitComponent> ();
				if (selectableObject != null) {
					selectedUnits.Add (selectableObject);
					if(selectableObject.SelectionCircle == null) {
						selectableObject.SelectionCircle = Instantiate( selectionCirclePrefab );
					}
					isSelecting = false;
				}
			}
        }
        // If we let go of the left mouse button, end selection
        if( Input.GetMouseButtonUp(1) )
        {
			foreach(SelectableUnitComponent selectableObject in FindObjectsOfType<SelectableUnitComponent>() ) {
				if( IsWithinSelectionBounds( selectableObject.gameObject ) && !selectedUnits.Contains(selectableObject) ) {
					selectedUnits.Add( selectableObject );
                }
            }
            isSelecting = false;
        }
		if ( Input.GetMouseButtonDown(0) ) {
			RaycastHit hit;
			if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
				leftMousePosition = hit.point;
			}
			isMoveTo = true;
		}

        // Highlight all objects within the selection box
        if( isSelecting ) {
			foreach(SelectableUnitComponent selectableObject in FindObjectsOfType<SelectableUnitComponent>()) {
                if( IsWithinSelectionBounds( selectableObject.gameObject ) ) {
                    if( selectableObject.SelectionCircle == null ) {
                        selectableObject.SelectionCircle = Instantiate( selectionCirclePrefab );
                    }
                }  else {
					selectableObject.ClearSelection ();
                }
            }
        }
    }

	void FixedUpdate()
	{
		if (isMoveTo) {
			isMoveTo = false;
			selectedUnits.ForEach (delegate(SelectableUnitComponent obj) {
				obj.GetComponent<AICharacter> ().AddGoal (new Goal (new List<IRequirement> () { new AtPosition (leftMousePosition) }, "MoveTo"));
			});
		}
	}

    public bool IsWithinSelectionBounds( GameObject gameObject )
    {
        if( !isSelecting )
            return false;

        Camera camera = Camera.main;
       	Bounds viewportBounds = Utils.GetViewportBounds( camera, mousePosition1, Input.mousePosition );
        return viewportBounds.Contains( camera.WorldToViewportPoint( gameObject.transform.position ) );
    }

    void OnGUI()
    {
        if( isSelecting )
        {
            // Create a rect from both mouse positions
            Rect rect = Utils.GetScreenRect( mousePosition1, Input.mousePosition );
            Utils.DrawScreenRect( rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ) );
            Utils.DrawScreenRectBorder( rect, 2, new Color( 0.8f, 0.8f, 0.95f ) );
        }
    }
}