using UnityEngine;
using System.Collections;

public class UserInputWaterRipple : MonoBehaviour {

	private int waveNumber;
	public float distanceX, distanceZ;
	public float[] waveAmplitude;
	public float magnitudeDivider;
	public Vector2[] impactPos;
	public float[] distance;
	public float speedWaveSpread;


//	private int waveNumber;
//	public float distanceX, distanceZ;
//	public float waveAmplitude;
//	public float magnitudeDivider;
//
//	public Vector2 impactPos;
//	public float distance;

	Mesh mesh;

	Ray ray;
	RaycastHit hit;

	//int i = 1;

//	public float speedWaveSpread;

	// Use this for initialization
	void Start () 
	{
		mesh = GetComponent<MeshFilter>().mesh;
	}
	
	// Update is called once per frame
	void Update () 
	{

		for (int i=0; i<8; i++){
			
			waveAmplitude[i] = GetComponent<Renderer>().material.GetFloat("_WaveAmplitude" + (i+1));
			if (waveAmplitude[i] > 0)
				
			{
				distance[i] += speedWaveSpread;
				GetComponent<Renderer>().material.SetFloat("_Distance" + (i+1), distance[i]);
				GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + (i+1), waveAmplitude[i] * 0.98f);
			}
			if (waveAmplitude[i] < 0.01)
			{
				GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + (i+1), 0);
				distance[i] = 0;
			}
			
		}



		if (Input.GetMouseButtonDown(0))	
		{
			Debug.Log("hit");

			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction*10, Color.red);

			if (Physics.Raycast(ray, out hit, 5000))				// Original - if (Physics.Raycast(ray, out hit, 50))
			{
				Debug.Log(hit.collider.name);
				if (hit.collider.name == "Plane")
				{
					Debug.Log("hit - collider");

					waveNumber++;
					if (waveNumber == 9){
						waveNumber = 1;
					}
					waveAmplitude[waveNumber-1] = 0;
					distance[waveNumber-1] = 0;
					
					distanceX = this.transform.position.x - 9000*hit.point.x;
					distanceZ = this.transform.position.z -  9000*hit.point.z;
					impactPos [waveNumber - 1].x = hit.point.x;
					impactPos[waveNumber - 1].y = hit.point.z;
					
					GetComponent<Renderer>().material.SetFloat("_xImpact" + waveNumber, hit.point.x);
					GetComponent<Renderer>().material.SetFloat("_zImpact" + waveNumber,  hit.point.z);
					
					GetComponent<Renderer>().material.SetFloat("_OffsetX" + waveNumber, distanceX / mesh.bounds.size.x * 12.5f);
					GetComponent<Renderer>().material.SetFloat("_OffsetZ" + waveNumber, distanceZ / mesh.bounds.size.z * 12.5f);
					
					GetComponent<Renderer>().material.SetFloat("_WaveAmplitude" + waveNumber, 2 * magnitudeDivider);
					

				}
			}
		}

	}

}
