<<<<<<< HEAD
<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;

public class enemyManagerScript : MonoBehaviour
{
    public float time;
    enum obj{petal, duck, fish, island, crocodile, loglog};
    public GameObject[] objects;
    GameObject currentObject;
    public GameObject slip;
    int objNum;

	//static int timer = 0;
	static int sec ;			//Global timer for game to control spawn enemies

	void Start ()
    {
			sec = 1;		//Will set to zero as the game starts
			InitiateSpawn ();		//Enemies will start spwaning
    }

	public void InitiateSpawn()
	{
		if (sec <= 30) 
		{
			StartCoroutine ("spawnPetalIsland");
		} 
		else if (sec >= 31 && sec <= 60 )
		{
			StartCoroutine ("spawnDuckFish"); 
		}
		else if (sec >= 61 && sec <= 90 )
		{
			StartCoroutine ("spawnCrocodile"); 
		}
			
		StartCoroutine ("spawnSlip");
		StartCoroutine ("IncreaseTime");
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

	IEnumerator IncreaseTime()
	{
		yield return new WaitForSeconds (1);
		Debug.Log (sec);
		sec += 1;
		StartCoroutine ("IncreaseTime");
	}

//	IEnumerator spawnObj2()
//	{
//		int num = Random.Range(0, 10);
//		if (num % 3 == 1 && num != 0)
//		{
//			objNum = (int)obj.petal;
//		}
//		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
//		currentObject.transform.parent = transform;
//		yield return new WaitForSeconds(time);
//		time = Random.Range(5.0f, 7.0f);
//		StartCoroutine("spawnObj2");
//	}

	IEnumerator spawnPetalIsland()
	{
		int num = Random.Range(0, 2);
		if (num == 0)
		{
			objNum = (int)obj.petal;
		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.petal;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();


	}

	IEnumerator spawnDuckFish()
	{
		int num = Random.Range(0, 4);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
	}

	IEnumerator spawnCrocodile()
	{
		int num = Random.Range(0, 5);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		else  if (num == 4)
		{
			Debug.Log ("crocodile aya");
			objNum = (int)obj.crocodile;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
	}

	IEnumerator spawnLog()
	{
		int num = Random.Range(0, 6);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		else  if (num == 4)
		{
			objNum = (int)obj.crocodile;
		}
		else  if (num == 5)
		{
			Debug.Log ("Log aya");
			objNum = (int)obj.loglog;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
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

	void EnemyPhases()
	{
		if (sec <= 30) 
		{
			StartCoroutine ("spawnPetalIsland");
		} 
		else if (sec >= 31 && sec <= 90 )
		{
			StartCoroutine ("spawnDuckFish"); 
		}
		else if (sec >= 91 && sec <= 150 )
		{
			StartCoroutine ("spawnCrocodile"); 
		}
		else if (sec >= 151 && sec <= 210 )
		{
			StartCoroutine ("spawnLog"); 
		}

	}
}
=======
﻿using UnityEngine;
using System.Collections;

public class enemyManagerScript : MonoBehaviour
{
    public float time;
    enum obj{petal, duck, fish, island, crocodile, loglog};
    public GameObject[] objects;
    GameObject currentObject;
    public GameObject slip;
    int objNum;

	//static int timer = 0;
	static int sec ;			//Global timer for game to control spawn enemies

	void Start ()
    {
			sec = 1;		//Will set to zero as the game starts
			InitiateSpawn ();		//Enemies will start spwaning
    }

	public void InitiateSpawn()
	{
		if (sec <= 30) 
		{
			StartCoroutine ("spawnPetalIsland");
		} 
		else if (sec >= 31 && sec <= 60 )
		{
			StartCoroutine ("spawnDuckFish"); 
		}
		else if (sec >= 61 && sec <= 90 )
		{
			StartCoroutine ("spawnCrocodile"); 
		}
			
		StartCoroutine ("spawnSlip");
		StartCoroutine ("IncreaseTime");
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

	IEnumerator IncreaseTime()
	{
		yield return new WaitForSeconds (1);
		Debug.Log (sec);
		sec += 1;
		StartCoroutine ("IncreaseTime");
	}

//	IEnumerator spawnObj2()
//	{
//		int num = Random.Range(0, 10);
//		if (num % 3 == 1 && num != 0)
//		{
//			objNum = (int)obj.petal;
//		}
//		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
//		currentObject.transform.parent = transform;
//		yield return new WaitForSeconds(time);
//		time = Random.Range(5.0f, 7.0f);
//		StartCoroutine("spawnObj2");
//	}

	IEnumerator spawnPetalIsland()
	{
		int num = Random.Range(0, 2);
		if (num == 0)
		{
			objNum = (int)obj.petal;
		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.petal;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();


	}

	IEnumerator spawnDuckFish()
	{
		int num = Random.Range(0, 4);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
	}

	IEnumerator spawnCrocodile()
	{
		int num = Random.Range(0, 5);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		else  if (num == 4)
		{
			Debug.Log ("crocodile aya");
			objNum = (int)obj.crocodile;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
	}

	IEnumerator spawnLog()
	{
		int num = Random.Range(0, 6);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		else  if (num == 4)
		{
			objNum = (int)obj.crocodile;
		}
		else  if (num == 5)
		{
			Debug.Log ("Log aya");
			objNum = (int)obj.loglog;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
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

	void EnemyPhases()
	{
		if (sec <= 30) 
		{
			StartCoroutine ("spawnPetalIsland");
		} 
		else if (sec >= 31 && sec <= 90 )
		{
			StartCoroutine ("spawnDuckFish"); 
		}
		else if (sec >= 91 && sec <= 150 )
		{
			StartCoroutine ("spawnCrocodile"); 
		}
		else if (sec >= 151 && sec <= 210 )
		{
			StartCoroutine ("spawnLog"); 
		}

	}
}
>>>>>>> 7ea9a5d22a3a80ce97dda380bb5e27ca7595d1ae
=======
﻿using UnityEngine;
using System.Collections;

public class enemyManagerScript : MonoBehaviour
{
    public float time;
    enum obj{petal, duck, fish, island, crocodile, loglog};
    public GameObject[] objects;
    GameObject currentObject;
    public GameObject slip;
    int objNum;

	static int sec ;			//Global timer for game to control spawn enemies

	void Start ()
    {
			sec = 210;		//Will set to zero as the game starts
			StartCoroutine ("IncreaseTime");
			StartCoroutine("spawnSlip");
			EnemyPhases();
    }
	IEnumerator IncreaseTime()
	{
		yield return new WaitForSeconds (1);
		Debug.Log (sec);
		sec += 1;
		if (sec >= 281)
		{
			sec = 210;
			EnemyPhases();
		}
		StartCoroutine ("IncreaseTime");
	}

    IEnumerator spawnSlip()
    {
        yield return new WaitForSeconds(2.0f);	
		if (!(sec >= 211 && sec <= 230) )
		{
        Instantiate(slip,slip.transform.position, Quaternion.identity);
		}
		StartCoroutine("spawnSlip");
    }
	
	void EnemyPhases()
	{
		if (sec <= 30) 
		{
			StartCoroutine ("spawnPetalIsland");
		} 
		else if (sec >= 31 && sec <= 90 )
		{
			StartCoroutine ("spawnDuckFish"); 

		}
		else if (sec >= 91 && sec <= 150 )
		{
			StartCoroutine ("spawnCrocodile"); 
		}
		else if (sec >= 151 && sec <= 210 )
		{
			StartCoroutine ("spawnLog"); 
		}
		else if (sec >= 211 && sec <= 230 )
		{
			StartCoroutine ("BonusLevel"); 
		}
		else if (sec >= 231 && sec <= 275)
		{
			StartCoroutine ("spawnLog"); 
		}

		
	}

	IEnumerator spawnPetalIsland()
	{
		int num = Random.Range(0, 2);
		if (num == 0)
		{
			objNum = (int)obj.petal;
		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.petal;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
	}

	IEnumerator spawnDuckFish()
	{
		int num = Random.Range(0, 4);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
	}

	IEnumerator spawnCrocodile()
	{
		int num = Random.Range(0, 5);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		else  if (num == 4)
		{
			Debug.Log ("crocodile aya");
			objNum = (int)obj.crocodile;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
	}

	IEnumerator spawnLog()
	{
		int num = Random.Range(0, 6);
		if (num == 0)
		{
			objNum = (int)obj.petal;

		}
		else if (num == 1)
		{
			objNum = (int)obj.island;
		}
		else if (num == 2)
		{
			objNum = (int)obj.fish;
		}
		else  if (num == 3)
		{
			objNum = (int)obj.duck;
		}
		else  if (num == 4)
		{
			objNum = (int)obj.crocodile;
		}
		else  if (num == 5)
		{
			objNum = (int)obj.loglog;
		}
		currentObject = Instantiate(objects[objNum],objects[objNum].transform.position, Quaternion.identity) as GameObject;
		currentObject.transform.parent = transform;
		yield return new WaitForSeconds(2);
		time = Random.Range(2.0f, 4.0f);
		EnemyPhases ();
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

	IEnumerator BonusLevel()
	{
		yield return new WaitForSeconds(0.5f);
		Debug.Log("Bonus level");
		SlipPattern();
		EnemyPhases();
	}

	void SlipPattern()
	{
			Instantiate(slip,slip.transform.position, Quaternion.identity);	
	}

}
>>>>>>> 977c60876a858f4a762d9222b0fb239bece70d01
