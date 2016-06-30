using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowPath : MonoBehaviour {

	[Header("Follow Parameters")]
	public float speed;

	[Header("Generation Parameters")]
	public GameObject StraightPipe;
	public GameObject CurvedPipe;

	private Queue<GameObject> PipeFollowQueue;
	private Queue<GameObject> PipeGenerationQueue;

	private Vector3 _lastPosition;
	private Quaternion _lastRotation;

	private bool _first;
	// Use this for initialization
	void Start () {

		// init
		iTween.Init(this.gameObject);
		_lastPosition = Vector3.zero;
		_lastRotation = Quaternion.identity;
		_first = true;
		PipeGenerationQueue = new Queue<GameObject>();
		PipeFollowQueue = new Queue<GameObject>();

		// generate
		for(int i = 0; i < 4; i++)
		{
			CreateNextPipe();
		}

		// start following
		FollowNextPath();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FollowNextPath()
	{
		RefreshPipe();
		if(PipeFollowQueue.Count <= 0) return; // End follow path

		GameObject nextPipe = PipeFollowQueue.Dequeue();
		Transform[] references = nextPipe.GetComponent<PipeReferencing>().References;
		iTween.MoveTo(this.gameObject, iTween.Hash(
			"name", "p", 
			"path", references, 
			"speed", speed, 
			"easetype", "linear", 
			"orienttopath", true,
			"oncomplete", "FollowNextPath",
			"movetopath", false
		));
	}

	// Delete last pipe and add one new pipe to the queue.
	public void RefreshPipe()
	{
		CreateNextPipe();
		// skip first destroy
		if(_first)
		{
			_first = false;
			return;
		}
		Destroy(PipeGenerationQueue.Dequeue(), 10f);
	}

	// Add one new pipe and queues it.
	private void CreateNextPipe()
	{
		GameObject newPipe = RandomPipe();
		foreach (Transform child in newPipe.transform) {
            if(child.name == "EndReference")
            {
            	_lastPosition = child.position;
            	break;
            }
        }
		PipeGenerationQueue.Enqueue(newPipe);
		PipeFollowQueue.Enqueue(newPipe);
	}

	// Instantiate one new pipe randomly.
	private GameObject RandomPipe()
	{
		int random = Random.Range(0,2);
		switch(random)
		{
			case 0:
				return Instantiate(StraightPipe, _lastPosition, _lastRotation) as GameObject;
			case 1:
				GameObject newPipe = Instantiate(CurvedPipe, _lastPosition, _lastRotation) as GameObject;

				int randRotation = Random.Range(0,6);

				// Calculate next rotation.
				GameObject g = new GameObject("g");
				g.transform.rotation = newPipe.transform.rotation;
				g.transform.Rotate(randRotation*60f,0f,0f, Space.Self);
				g.transform.Rotate(0f,90f,0f, Space.Self);
				_lastRotation = g.transform.rotation;
				Destroy(g);

				newPipe.transform.Rotate(randRotation*60f, 0f, 0f, Space.Self);

				return newPipe;
			default:
				return Instantiate(StraightPipe, _lastPosition, _lastRotation) as GameObject;
			break;
		}
	}

}
