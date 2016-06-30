using UnityEngine;
using System.Collections;

public class AutoForward : MonoBehaviour {

	public float ForwardSpeed;
	public float MaxSpeed;

	public PipeGeneration _PipeGeneration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		this.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * ForwardSpeed);
		if(this.gameObject.GetComponent<Rigidbody>().velocity.magnitude > MaxSpeed)
		{
			this.gameObject.GetComponent<Rigidbody>().velocity = this.gameObject.GetComponent<Rigidbody>().velocity.normalized * MaxSpeed;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "EndReference")
		{
			_PipeGeneration.RefreshPipe();
		}
	}
}
