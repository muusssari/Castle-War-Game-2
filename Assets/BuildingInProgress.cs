using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BuildingInProgress : MonoBehaviour {

	private GameObject model;
	private GameObject build;
	public bool building = false;
	public float count = 0;
	public GameObject navMesh;
	public GameObject findimage;
	public GameObject canvas;
	public Image HealtBar;

	private void Start()
	{
		findimage = transform.Find("CanvasProgress/Healtbg/HealtBar").gameObject;
		canvas = transform.Find("CanvasProgress").gameObject;
		canvas.SetActive(false);
		HealtBar = findimage.GetComponent<Image>();
		build = transform.Find("Building").gameObject;
		model = transform.Find("Model/").gameObject;
		navMesh = GameObject.Find("PlayerBase");
	}
	private void Update()
	{
		if (!building)
		{
			canvas.SetActive(true);
			HealtBar.fillAmount = count/100;
			if (count > 1)
			{
				
				build.transform.GetChild(0).gameObject.SetActive(true);
				GetComponent<NavMeshObstacle>().enabled = true;
			}
			if (count > 30)
			{
				build.transform.GetChild(1).gameObject.SetActive(true);
			}
			if (count > 60)
			{
				build.transform.GetChild(2).gameObject.SetActive(true);
			}
			if (count >= 80)
			{
				build.transform.GetChild(3).gameObject.SetActive(true);
			}
			if (count >= 100)
			{
				model.SetActive(true);
				building = true;
				navMesh.GetComponent<navmeshbaker>().RemakeNavMesh();
				build.SetActive(false);
				canvas.SetActive(false);
				if (gameObject.name == "WallMiddle(Clone)")
				{
					GetComponent<BoxCollider>().size = new Vector3(1, 3, GetComponent<BoxCollider>().size.z);
				}
				if (gameObject.name == "Hut(Clone)")
				{
					PlayerStats.Maxpop += 10;
				}
			}
		}
	}
}
