using UnityEngine;

public class Base_Player : MonoBehaviour {

    public Vector3 positionOffset;
	public GameObject miner;

	//BuildingManager buildManager;

	private void Start()
    {
        //buildManager = BuildingManager.instance;
		Instantiate(miner, transform.position, Quaternion.identity);
	}

    public Vector3 GetBuildPosition()
    {
        return transform.position + positionOffset;
    }
}
