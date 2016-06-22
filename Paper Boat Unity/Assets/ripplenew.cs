using UnityEngine;
using System.Collections;

public class ripplenew : MonoBehaviour {

	public GameObject prefab;
	GameObject prefabClone;
	Ray ray;
	RaycastHit hit;

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction*10, Color.red);
		if (Input.GetMouseButtonDown(0))
		{
			if (Physics.Raycast(ray, out hit, 10))
			{
				if (hit.collider.name == "Collider" || hit.collider.name == "Plane")
				{
					PrefabForm ();
				}
			}
		}
	}

	public void PrefabForm()
	{
		prefabClone = Instantiate(prefab, hit.point, Quaternion.identity) as GameObject;	
	}
}
