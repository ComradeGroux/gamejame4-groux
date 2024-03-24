using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public Material[] extMat = new Material[4];
	public Material[] intMat = new Material[4];
	public int nbPlayer = 4;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
	}

	public Material[] getMat()
	{
		Material[] mat = new Material[2];

		mat[0] = extMat[nbPlayer - 1];
		mat[1] = intMat[nbPlayer - 1];

		nbPlayer--;
		return mat;
	}
}
