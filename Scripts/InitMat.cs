using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitMat : MonoBehaviour
{
	public GameManager gameManager;
	private Material intMat;
	private Material extMat;

	// Start is called before the first frame update
	void Start()
	{
		Material[] mat = gameManager.GetComponent<GameManager>().getMat();

		SkinnedMeshRenderer mesh = GetComponent<Renderer>().GetComponentInChildren<SkinnedMeshRenderer>();
		mesh.materials[0] = mat[0];
		mesh.materials[1] = mat[1];
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
