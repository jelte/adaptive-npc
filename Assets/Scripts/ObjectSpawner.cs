using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject prefab;
	private float timer = 0;
	public int spawnTime = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer > spawnTime) {
			timer = 0;
			Instantiate( prefab );
		}
	}
}
