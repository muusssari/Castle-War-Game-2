using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour {

    public static int Gold;
    public int startGold = 400;
    public int gold = 0;

    private void Start()
    {
        Gold = startGold;
    }
    void Update()
    {
        gold = Gold;
    }
}
