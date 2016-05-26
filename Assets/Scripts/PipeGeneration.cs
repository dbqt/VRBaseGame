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

		for(int i = 0; i < 5; i++)
		{
			AddNextPipe();
			Debug.Log(i);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

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

	private void RefreshPipe()
	{
		AddNextPipe();
		Destroy(_pipePool.Dequeue());
	}

	private GameObject RandomPipe()
	{
		int random = Random.Range(0,2);
		switch(random)
		{
			case 0:
				return Instantiate(StraightPipe, _lastPosition, _lastRotation) as GameObject;
			case 1:
				
				int randRotation = Random.Range(0,6);
				
				GameObject newPipe = Instantiate(CurvedPipe, _lastPosition, _lastRotation) as GameObject;
				newPipe.transform.Rotate(randRotation*60f, 0f, 0f);
				Vector3 t = newPipe.transform.localRotation.eulerAngles;
				t.y += 90f;
				_lastRotation.eulerAngles = t;
				return newPipe;
			default:
				return Instantiate(StraightPipe, _lastPosition, _lastRotation) as GameObject;
			break;

		}
	}
}
