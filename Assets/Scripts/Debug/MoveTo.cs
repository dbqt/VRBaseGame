using UnityEngine;
using System.Collections;

public class MoveTo : MonoBehaviour {

	public GameObject player;
	public Transform[] references;
	public float speed;

	private int _index = 0;

	// Use this for initialization
	void Start () {
		player.transform.position = references[_index].position;
		_index++;
		iTween.Init(player);
		StartMove();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StartMove()
	{
		if(_index >= references.Length) return;
		iTween.MoveTo(player, iTween.Hash("name", "p", "path", references, "speed", speed, "easetype", "linear", "orienttopath", true));
		_index++;
	}
}
