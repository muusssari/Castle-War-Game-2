using UnityEngine;

public class test : MonoBehaviour {

    public float speed = 10f;
    public Transform target;
    public int wavepointIndex;
    public string side;
    public int goldbag = 0;

    private void Start()
    {
        side = tag;
        if (side == "Player")
        {
            target = MinerWaypoints.points[0];
            wavepointIndex = 0;
        }
    }

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (goldbag < 2 && wavepointIndex >= MinerWaypoints.points.Length - 1)
        {
            goldbag = 50;
            GetNextWaypoint();
        }
        else
        {
            if(goldbag > 2)
            {
                if (wavepointIndex == 0)
                {
                    goldbag = 0;
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
