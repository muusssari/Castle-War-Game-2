using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Healt : MonoBehaviour {

    public float healt = 5;
	private float starthealt;
    public GameObject DeathEffect;
	public GameObject findimage;
	public GameObject canvas;
	public Image HealtBar;
	public float defenceShield;
	public float defenceArmor;
	public GameObject fading = null;
	private GameObject Shop;


	private void Start()
	{
		findimage = transform.Find("Canvas/Healtbg/HealtBar").gameObject;
		canvas = transform.Find("Canvas").gameObject;
		canvas.SetActive(false);
		starthealt = healt;
		HealtBar = findimage.GetComponent<Image>();
		if (gameObject.layer == 11)
		{
			Shop = GameObject.Find("ShopUI");
		}
		
	}
	
	void Update () {
		if (healt < starthealt)
		{
			canvas.SetActive(true);
		}
		HealtBar.fillAmount = healt / starthealt;
		if (Input.GetKeyDown(KeyCode.K) && gameObject.name == "EnemyBase")
		{
			GetHit(100);
		}
	}
	public void GetShot(float damage)
	{
		healt -= damage - defenceShield/2;
		if (healt <= 0)
		{
			if (gameObject.layer != 11 && gameObject.tag == "Player")
			{
				PlayerStats.Popnow -= 1;
				GameStats.UnitsDied += 1;
			}
			if (gameObject.layer != 11 && gameObject.tag == "Enemy")
			{
				PlayerStats.Popnow -= 1;
				GameStats.UnitKilled += 1;
			}
			if (gameObject.name == "Hut(Clone)")
			{
				PlayerStats.Maxpop -= 10;
			}
			Destroy(gameObject);
			GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, transform.rotation);
			Destroy(effect, 2f);
			return;
		}
	}
	public void GetHit(float damage)
    {
        healt -= damage - (defenceShield * defenceArmor);
		if (healt <= 0)
		{
			if (gameObject.layer != 11 && gameObject.tag == "Player")
			{
				PlayerStats.Popnow -= 1;
				GameStats.UnitsDied += 1;
			}
			if (gameObject.layer != 11 && gameObject.tag == "Enemy")
			{
				GameStats.UnitKilled += 1;
			}
			Destroy(gameObject);
			if (gameObject.layer == 11)
			{
				Shop.GetComponent<UI_Master>().d = true;
				if (gameObject.name == "Hut(Clone)")
				{
					PlayerStats.Maxpop -= 10;
				}
			}
			
			if (DeathEffect != null)
			{
				GameObject effect = (GameObject)Instantiate(DeathEffect, transform.position, transform.rotation);
				Destroy(effect, 2f);
				

			}

			if (fading != null)
			{
				if (this.transform.name == "PlayerBase")
				{
					GameStats.Win = "You Lost!";
					fading.GetComponent<Fading>().StartFade();
				}
				if (this.transform.name == "EnemyBase")
				{
					GameStats.Win = "You Won!!";
					fading.GetComponent<Fading>().StartFade();
				}
			}
			return;
		}
	}
}
