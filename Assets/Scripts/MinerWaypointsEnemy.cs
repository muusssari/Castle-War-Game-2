using UnityEngine;

public class MinerWaypointsEnemy : MonoBehaviour {

    public static Transform[] MinerPoints;

    private void Awake()
    {
        MinerPoints = new Transform[transform.childCount];
        for (int i = 0; i < MinerPoints.Length; i++)
        {
            MinerPoints[i] = transform.GetChild(i);
        }
    }
}
