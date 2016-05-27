using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PipeGeneration : MonoBehaviour {

	public GameObject StraightPipe;
	public GameObject CurvedPipe;
	public Transform PlayerReference;

	private Queue<GameObject> _pipePool;
	private Vector3 _lastPosition;
	private Quaternion _lastRotation;

	// Use this for initialization
	void Start () {
		_lastPosition = Vector3.zero;
		_lastRotation = Quaternion.identity;
		_pipePool = new Queue<GameObject>();

		for(int i = 0; i < 10; i++)
		{
			AddNextPipe();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Add one new pipe and queues it.
	private void AddNextPipe()
	{
		GameObject newPipe = RandomPipe();
		foreach (Transform child in newPipe.transform) {
            if(child.name == "EndReference")
            {
            	_lastPosition = child.position;
            	break;
            }
        }
		_pipePool.Enqueue(newPipe);
	}

	// Delete last pipe and add one new pipe to the queue.
	private void RefreshPipe()
	{
		AddNextPipe();
		Destroy(_pipePool.Dequeue());
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
