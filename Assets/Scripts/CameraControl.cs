using UnityEngine;

public class CameraControl : MonoBehaviour {


    public float panSpeed = 40f;
    public float panBorderThickness = 20f;
    private Vector3 pos;
    public float minY = 10f;
    public float maxY = 80f;
    public bool moveCamera = false;
	public float buildmode = 1;
	private float scrollSpeed;
	private float speeder = 1f;
	public GameObject build = null;

	void Awake()
    {
        pos = transform.position;
		build = GameObject.Find("GameMaster");
    }
    void Update () {

		build = GameObject.Find("GameMaster").GetComponent<GrindSystem>().building;

		if (build != null)
		{
			buildmode = 0;
		}
		else
		{
			buildmode = 1;
		}

		if (transform.position.y > 70)
		{
			speeder = 2f;
			if (transform.position.y > 90)
			{
				speeder = 2.5f;
			}
		}
		else
		{
			speeder = 1f;
		}

        if (Input.GetKey("space"))
        {
            transform.position = pos;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            if (transform.position.x > 10)
            {
                transform.Translate(Vector3.left * panSpeed * Time.deltaTime * speeder, Space.World);
            }
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
        if (transform.position.x < 580)
            {
                transform.Translate(Vector3.right * panSpeed * Time.deltaTime * speeder, Space.World);
            }
                
        }
        if (Input.GetKey("d") || Input.mousePosition.x > Screen.width - panBorderThickness)
        {
            if (transform.position.z < 520)
            {
                transform.Translate(Vector3.forward * panSpeed * Time.deltaTime * speeder, Space.World);
            }
                
        }
        if (Input.GetKey("a") || Input.mousePosition.x < panBorderThickness)
        {
            if (transform.position.z > 0)
            {
                transform.Translate(Vector3.back * panSpeed * Time.deltaTime * speeder, Space.World);
            }
            
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

		if (transform.position.y < minY)
		{
			transform.position = new Vector3(transform.position.x, minY, transform.position.z);
		}
        if (scroll > 0 && transform.position.y > minY)
        {
            transform.Translate(Vector3.forward * 800f * buildmode * Time.deltaTime, Space.Self);
        }
        if (scroll < 0 && transform.position.y < maxY)
        {
            transform.Translate(Vector3.back * 700f * buildmode * Time.deltaTime, Space.Self);
        }
    }
}
