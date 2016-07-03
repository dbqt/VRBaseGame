using UnityEngine;
using System.Collections;

public class Obstacles : MonoBehaviour {

	public GameObject[] ObstaclePrefabs;

	// Use this for initialization
	void Start () {
		int type = Mathf.FloorToInt(Random.Range(0f, ObstaclePrefabs.Length));
		int randRotation = Mathf.FloorToInt(Random.Range(0,6));

		GameObject o = Instantiate(ObstaclePrefabs[type], this.transform.position, this.transform.rotation) as GameObject;
		o.transform.Rotate(randRotation*60f,0f,0f, Space.Self);
		o.transform.parent = this.transform;
	}
	
}
