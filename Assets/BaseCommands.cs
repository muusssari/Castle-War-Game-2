using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCommands : MonoBehaviour {

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.transform.root.name == "Player_Worker(Clone)")
		{
			if (collision.collider.transform.root.GetComponent<Miner>().gold > 0)
			{
				PlayerStats.Gold += collision.collider.transform.root.GetComponent<Miner>().gold;
				GameStats.GetGold += collision.collider.transform.root.GetComponent<Miner>().gold;
				collision.collider.transform.root.GetComponent<Miner>().gold = 0;
			}
			if (collision.collider.transform.root.GetComponent<Miner>().food > 0)
			{
				PlayerStats.Food += collision.collider.transform.root.GetComponent<Miner>().food;
				GameStats.GetFood += collision.collider.transform.root.GetComponent<Miner>().food;
				collision.collider.transform.root.GetComponent<Miner>().food = 0;
			}
			if (collision.collider.transform.root.GetComponent<Miner>().wood > 0)
			{
				PlayerStats.Wood += collision.collider.transform.root.GetComponent<Miner>().wood;
				GameStats.GetWood += collision.collider.transform.root.GetComponent<Miner>().wood;
				collision.collider.transform.root.GetComponent<Miner>().wood = 0;
			}
			if (collision.collider.transform.root.GetComponent<Miner>().stone > 0)
			{
				PlayerStats.Stone += collision.collider.transform.root.GetComponent<Miner>().stone;
				GameStats.GetStone += collision.collider.transform.root.GetComponent<Miner>().stone;
				collision.collider.transform.root.GetComponent<Miner>().stone = 0;
			}

		}
		if (collision.collider.transform.root.name == "Enemy_Worker(Clone)")
		{
			if (collision.collider.transform.root.GetComponent<Miner>().gold > 0)
			{
				EnemyStats.Gold += collision.collider.transform.root.GetComponent<Miner>().gold;
				collision.collider.transform.root.GetComponent<Miner>().gold = 0;
			}


		}
	}


}
