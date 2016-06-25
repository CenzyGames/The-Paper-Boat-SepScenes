using UnityEngine;
using System.Collections;

public class enemyManagerScript : MonoBehaviour
{
    public float time;
    enum obj{petal, duck, fish, island};
    public GameObject[] objects;
    GameObject currentObject;
    public GameObject slip;
    int objNum;

	void Start ()
    {
		//if (GameObject.Find("Palm2").gameObject.activeInHierarchy == false)
	//	{
			InitiateSpawn ();
//		Debug.Log("Objects - Enemy instn");
		//}
    }

	public void InitiateSpawn()
	{
		StartCoroutine ("spawnObj");
		StartCoroutine ("spawnSlip");
		StartCoroutine ("spawnObj2");
	}

	public void StartSpawn()
	{
		if (PlayerPrefs.GetInt ("Instruct63") == 1) {
		//	InitiateSpawn ();
		}
	}

    IEnumerator spawnSlip()
    {
        yield return new WaitForSeconds(2.0f);		//Initually 0.5f - 25-06-2016
        Instantiate(slip,slip.transform.position, Quaternion.identity);
		Debug.Log ("pos - " + slip.transform.position);
        StartCoroutine("spawnSlip");
    }

    IEnumerator spawnObj()
    {
        int num = Random.Range(0, 10);
        if (num % 3 == 0 && num != 0)
        {
            objNum = (int)obj.duck;
        }
        else if (num % 4 == 0 && num != 0)
        {
            objNum = (int)obj.fish;
        }
        else if (num == 7)
        {
            objNum = (int)obj.island;
        }
        else
        {
            objNum = (int)obj.petal;
        }
        currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
        currentObject.transform.parent = transform;
        yield return new WaitForSeconds(time);
        time = Random.Range(2.0f, 4.0f);
        StartCoroutine("spawnObj");
    }

	IEnumerator spawnObj2()
	{
		int num = Random.Range(0, 10);
		if (num % 3 == 1 && num != 0)
		{
			objNum = (int)obj.petal;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(time);
		time = Random.Range(5.0f, 7.0f);
		StartCoroutine("spawnObj2");
	}
}
