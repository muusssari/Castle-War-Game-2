using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class navmeshbaker : MonoBehaviour {

	public bool countOn = false;
	public float time = 5;
	public float count = 10;
	public bool d = false;
	public NavMeshSurface[] surfaces;

	private void Start()
	{
		for (int i = 0; i < surfaces.Length; i++)
		{
			surfaces[i].BuildNavMesh();
		}
	}
	public void RemakeNavMesh()
	{
		for (int i = 0; i < surfaces.Length; i++)
		{
			surfaces[i].BuildNavMesh();
		}
	}
	public void RemakeNavMeshOnDestroy()
	{
		for (int i = 0; i < surfaces.Length; i++)
		{
			surfaces[i].BuildNavMesh();
		}
	}
	private void LateUpdate()
	{
		if (countOn)
		{
			if (count <= 0)
			{
				for (int i = 0; i < surfaces.Length; i++)
				{
					surfaces[i].BuildNavMesh();
				}
				count = time;
			}
			
			count -= Time.deltaTime;
		}
		if (d)
		{
			if (count <= 0)
			{
				for (int i = 0; i < surfaces.Length; i++)
				{
					surfaces[i].BuildNavMesh();
				}
				count = time;
			}
			d = false;
		}
		
	}

}
