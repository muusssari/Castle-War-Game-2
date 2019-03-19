using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineGiveGold : MonoBehaviour {

	public int goldMineUp = 1;
	private void OnCollisionEnter(Collision collision)
	{
		
		if (collision.collider.transform.root.name == "Player_Worker(Clone)")
		{
			collision.collider.transform.root.GetComponent<Miner>().gold = 20 * goldMineUp;
		}
		if (collision.collider.transform.root.name == "Enemy_Worker(Clone)")
		{
			collision.collider.transform.root.GetComponent<Miner>().gold = 20;
		}
	}
}
