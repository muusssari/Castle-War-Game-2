using UnityEngine;
using UnityEngine.EventSystems;

public class Platform : MonoBehaviour {

    public Color hoverColor;
    private Color startColor;
    private Renderer rend;
    public bool road = false;
    public Vector3 positionOffset;
	[Header("Switch Side")]
	public bool player = true;

    [Header("Optimal")]
    public GameObject tower = null;

    BuildingManager buildManager;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildingManager.instance;
    }

    public Vector3 GetBuildPosition ()
    {
        Vector3 i = transform.position + positionOffset;
        return i;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuild || !player)
        {
            return;
        }
		if (road && buildManager.towerToBuild.prefab == buildManager.standardAcherTowerPrefab)
		{
			return;
		}

        buildManager.BuildTowerOn(this);
		rend.material.color = Color.red;

	}
    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (!buildManager.CanBuild)
        {
            rend.material.color = Color.red;
            return;
        }
        if (tower != null || !player)
        {
            rend.material.color = Color.red;
			return;
        }
        if (buildManager.HasGold)
        {
            if (buildManager.towerToBuild.prefab == buildManager.standardBarricadePrefab && !road)
            {
                rend.material.color = Color.red;
            }
            else if (buildManager.towerToBuild.prefab == buildManager.standardAcherTowerPrefab && road)
            {
                rend.material.color = Color.red;
            }
            else
            {
                rend.material.color = hoverColor;
            }
            
        }
        else
        {
            rend.material.color = Color.red;
        }
        
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
