using UnityEngine;

public class Miner_Movement : MonoBehaviour {

    public float speed = 10f;
    public Transform target;
    public int wavepointIndex;
	public Vector3 offset;
    public string side;
    public int goldbag = 0;
	public Transform rotate;
	public GameObject chest = null;

    private void Start()
    {
        side = tag;
        if (side == "Enemy")
        {
            target = MinerWaypointsEnemy.MinerPoints[0];
            wavepointIndex = 0;
        }
        if (side == "Player")
        {
            target = MinerWaypoints.points[0];
            wavepointIndex = 0;
        }
		chest = transform.Find("chest").gameObject;
		chest.SetActive(false);
	}

    private void Update()
    {
		if (target != null)
		{
			Vector3 dire = target.position - transform.position;
			Quaternion lookingRotation = Quaternion.LookRotation(dire);
			Vector3 rotation = Quaternion.Lerp(rotate.rotation, lookingRotation, Time.deltaTime * 8f).eulerAngles;
			rotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
		}
		Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (side == "Enemy")
        {
            if (goldbag < 2 && wavepointIndex >= MinerWaypointsEnemy.MinerPoints.Length - 1)
            {
                goldbag = 50;
				chest.SetActive(true);
				GetNextWaypoint();
            }
            else
            {
                if (goldbag > 2)
                {
                    if (wavepointIndex == 0)
                    {
                        EnemyStats.Gold += goldbag;
                        goldbag = 0;
						chest.SetActive(false);
						GetNextWaypoint();
                    }
                    else
                    {
                        wavepointIndex--;
                        target = MinerWaypointsEnemy.MinerPoints[wavepointIndex];
                    }
                }
                else
                {
                    wavepointIndex++;
                    target = MinerWaypointsEnemy.MinerPoints[wavepointIndex];
                }
            }

        }
        else if (side == "Player")
        {
            if (goldbag < 2 && wavepointIndex >= MinerWaypoints.points.Length - 1)
            {
                goldbag = 50;
				chest.SetActive(true);
				GetNextWaypoint();
            }
            else
            {
                if (goldbag > 2)
                {
                    if (wavepointIndex == 0)
                    {
                        PlayerStats.Gold += goldbag;
                        goldbag = 0;
						chest.SetActive(false);
						GetNextWaypoint();
                    }
                    else
                    {
                        wavepointIndex--;
                        target = MinerWaypoints.points[wavepointIndex];
                    }
                }
                else
                {
                    wavepointIndex++;
                    target = MinerWaypoints.points[wavepointIndex];
                }
            }

        }

    }
}
