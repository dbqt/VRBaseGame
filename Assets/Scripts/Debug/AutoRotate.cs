using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public Vector3 RotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.Rotate(RotationSpeed);
	}
}
