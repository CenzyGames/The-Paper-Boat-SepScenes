﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class levelManagerScript : MonoBehaviour {
    public GameObject[] levels;
	// New game object array to be created to have snowlevels
	public GameObject[] snowLevels;

    GameObject level;
    public GameObject[] poolObj;
    List<GameObject> activeSets;
    int i;
    float pos;
    public int randomNum;
    int spawnCount;

    string[] names;
    int plainCount;

	float timer = 1.0f;
	int minutes =1;
	int seconds=1;

	bool increaseSpeed = true;

	void Start ()
    {
        activeSets = new List<GameObject>();
        poolObj = new GameObject[5];
        names = new string[3];
        pos = 20;

		/*Instantiates levels at the beginning*/
        for (i = 0; i < 5; i++)
        {
            if (i > 2)
            {
                InstantiateLevel(pos, 0);
            }
            else
            {
                InstantiateLevel(pos, i);
                names[i] = level.name;
            }
            poolObj[i] = level;
        }

        setLevels();//sets the initial levels
		timer = 1.0f;
		seconds=1;
        /*move the sets*/
        StartCoroutine("moveLevel");
//		Debug.Log("MOVE");
    }

	void TimeIncrease()
	{
		minutes =(int) timer / 60;
		seconds =(int) timer % 60;
	//	seconds = seconds/10;
		if (seconds >= 1 && seconds <= 5 && increaseSpeed == true) 
		{
			if(seconds >= 5)
				increaseSpeed = false;
			timer = timer + Time.deltaTime/10;
		}
		else if(seconds <= 5 && seconds >= 10 && increaseSpeed == false)
		{		
			if(seconds >= 10)
			{
				increaseSpeed = true;
				timer = 1.0f;
			}
			timer = timer - Time.deltaTime;
		}
	}

    IEnumerator moveLevel()
    {
		//TimeIncrease();
		activeSets[0].gameObject.transform.Translate(-0.025f*seconds,0,0);
		activeSets[1].gameObject.transform.Translate(-0.025f*seconds,0,0);
		activeSets[2].gameObject.transform.Translate(-0.025f*seconds,0,0);

//		Debug.Log("seconds - "+ seconds);
		//activeSets[0].gameObject.transform.Translate(-0.01f*seconds,0,0);
		//activeSets[1].gameObject.transform.Translate(-0.01f*seconds,0,0);
		//activeSets[2].gameObject.transform.Translate(-0.01f*seconds,0,0);

        if (activeSets[0].gameObject.transform.position.x < -4.01f)
        {
			/*pools new level*/
        	activeSets[0].gameObject.SetActive(false);
            activeSets[0].gameObject.transform.position = new Vector3(20, 0, 0);
            string s = activeSets[0].gameObject.name;
            activeSets.Remove(activeSets[0]);
            spawnNewLevel(s);
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine("moveLevel");
    }

    public void spawnNewLevel(string name)
    {
        randomNum = Random.Range(0, 10);
        string objName = names[0];
        spawnCount++;

        if (spawnCount > 2)
        {
            if (randomNum %3 == 0 && randomNum !=0)
            {
                objName = names[1];
            }
            else if (randomNum == 7 || randomNum == 0)
            {
                objName = names[2];
            }
            spawnCount = 0;
        }
        createNewLevel(objName);
    }

    void InstantiateLevel(float position, int index)
    {
		// Here we will write if else for material swap
		if (PlayerPrefs.GetInt ("environment") == 0) 
		{
			level = Instantiate(levels[index], transform.position, Quaternion.identity) as GameObject;
		} 
		else if (PlayerPrefs.GetInt ("environment") == 1) 
		{
			level = Instantiate(snowLevels[index], transform.position, Quaternion.identity) as GameObject;
		}

        
        level.transform.parent = transform;
        level.transform.position = new Vector3(position, 0, 0);
        level.SetActive(false);
    }

    void setLevels()
    {
        pos = 0;
        poolObj[2].SetActive(true);
        poolObj[2].transform.position = new Vector3(pos, 0, 0);
        pos += 4;
        activeSets.Add(poolObj[2]);
        for (i = 0; i < 4; i++)
        {
            if (poolObj[i].name == names[0])
            {
                poolObj[i].SetActive(true);
                poolObj[i].transform.position = new Vector3(pos, 0, 0);
                pos += 4;
                activeSets.Add(poolObj[i]);
            }
        }  

    }

    void createNewLevel(string name)
    {
        foreach (GameObject gj in poolObj)
        {
            if (gj.name == name)
            {
                if (!gj.activeSelf)
                {
                   // print(gj.name);
                    gj.SetActive(true);

					// Condition for material swap

					if (PlayerPrefs.GetInt ("environment") == 0)
					{
						gj.transform.position = new Vector3 (3.9875f * (levels.Length - 1), 0, 0);
					} 
					else 
					{
						gj.transform.position = new Vector3(3.9875f * (snowLevels.Length - 1), 0, 0);
					}  

					activeSets.Add(gj);
                    break;
                }
            }     
        }
    }
}
