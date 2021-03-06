﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automaton : MonoBehaviour
{
	public bool stepped = false;
	public bool Black = false;
	public bool random = false;
	public GameObject white;
	public int SizeX = 100;
	public int SizeY = 100;
	public int type = 1;
	string binary;
	bool[] rules;
	bool[,] map;
	private Dictionary<string, bool> param;
	public List<GameObject> objs;

    void Start()
    {
		objs = new List<GameObject>();
	}

    public void SetVariables(bool _stepped, bool _random, int _X, int _Y, int _type)
    {
		Clean();
		stepped = _stepped;
		random = _random;
		SizeX = _X;
		SizeY = _Y;
		type = _type;
		GenerateBoard();
    }

	public void GenerateBoard()
    {
        RuleSetUp();
        BoardSetUp();
        FillBoard();
		if(stepped)
        {
			StartCoroutine(CreateTile());
		}
		else
        {
			CreateAllTiles();
		}
        
    }

    private void FillBoard()
    {
		for (int j = 1; j < SizeY; j++)
		{
			for (int i = 0; i < SizeX; i++)
			{
                CheckUp(i, j);
            }
        }
    }

    private void BoardSetUp()
    {
        if (random)
        {
            for (int i = 0; i < SizeX; i++)
            {
                if (UnityEngine.Random.Range(0f, 1f) > .5f)
                {
                    map[i, 0] = true;
                }
            }
        }
        else
        {
            for (int i = 0; i < SizeX; i++)
            {
                map[i, 0] = false;
            }
            map[SizeX / 2, 0] = true;
        }
    }

    void RuleSetUp()
    {
		binary = Convert.ToString(type, 2);
		while (binary.Length < 8)
		{
			binary = "0" + binary;
		}
		rules = new bool[8];
		for (int i = 0; i < binary.Length; i++)
		{
			if (binary[i] == '1')
			{
				rules[i] = true;
			}
			else
			{
				rules[i] = false;
			}
		}
		param = new Dictionary<string, bool>();
		param.Add("111", rules[0]);
		param.Add("110", rules[1]);
		param.Add("101", rules[2]);
		param.Add("100", rules[3]);
		param.Add("011", rules[4]);
		param.Add("010", rules[5]);
		param.Add("001", rules[6]);
		param.Add("000", rules[7]);

		map = new bool[SizeX, SizeY];
	}

	void CheckUp(int x, int y)
	{
		string number = "";

		if (x > 0)
		{
			if (map[x - 1, y - 1] == true)
			{
				number += "1";
			}
			else
			{
				number += "0";
			}
		}
		else
		{
			if (map[SizeX - 1, y - 1] == true)
			{
				number += "1";
			}
			else
			{
				number += "0";
			}
		}
		if (map[x, y - 1] == true)
		{
			number += "1";
		}
		else
		{
			number += "0";
		}
		if (x < SizeX - 2)
		{
			if (map[x + 1, y - 1] == true)
			{
				number += "1";
			}
			else
			{
				number += "0";
			}
		}
		else
		{
			if (map[0, y - 1] == true)
			{
				number += "1";
			}
			else
			{
				number += "0";
			}
		}
	

		if(param[number])
		{
			map[x, y] = true;
		}
		else
		{
			map[x, y] = false;
		}
	}

	IEnumerator CreateTile()
	{
		for (int j = 0; j < SizeY; j++)
		{
			for (int i = 0; i < SizeX; i++)
			{
			
				GameObject g = Instantiate(white, new Vector2(i, -j), Quaternion.identity);
				objs.Add(g);
				g.SetActive(true);
				if (!map[i, j])
				{
					if(!Black)
					{
						g.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red,Color.blue, (SizeY+SizeX)/(SizeY + SizeX));
					}
					else
					{
						g.GetComponent<SpriteRenderer>().color = Color.black;
					}
				}
				
			}
			yield return new WaitForSeconds(.001f);
		}
		StopCoroutine(CreateTile());
	}

	void CreateAllTiles()
    {
		for (int j = 0; j < SizeY; j++)
		{
			for (int i = 0; i < SizeX; i++)
			{

				GameObject g = Instantiate(white, new Vector2(i, -j), Quaternion.identity);
				g.SetActive(true);
				if (!map[i, j])
				{
					if (!Black)
					{
						g.GetComponent<SpriteRenderer>().color = Color.Lerp(Color.red, Color.blue, (SizeY + SizeX) / (SizeY + SizeX));
					}
					else
					{
						g.GetComponent<SpriteRenderer>().color = Color.black;
					}
				}

			}
		}
	}

	void Clean()
    {
		for(int i = 0; i < objs.Count; i++)
        {
			Destroy(objs[i]);
        }
		objs.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
