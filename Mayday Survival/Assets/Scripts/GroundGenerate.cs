using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;
using UnityEngine;

public class GroundGenerate : MonoBehaviour
{
    [Serializable]
    public class Count {
        public int minimum;
        public int maximum;
        
        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }


    public int MapSize = 3;

    public GameObject[] Grounds;
    public GameObject[] ShipParts;
    int ShipCount = 4;
    //public GameObject Trees;
    //public Count TreeCount = new Count(min,max); this can be called as
    //TreeCount.minimum and TreeCount.maximum
    private Transform MapFolder;

    private List<Vector3> gridPositions = new List<Vector3>();
    //keep track on used space to avoid things spwaning in the same spot

    void InitialiseList()
    {
        gridPositions.Clear();
        for (int i = 0; i < MapSize; i++)
        {
            for (int j = 0; j < MapSize; j++)
            {
                float x_axis = Random.Range((i*680) -340 +30,(i*680)+340-30);
                float z_axis = Random.Range((j * 680) -340 + 30, (j * 680)+340 - 30);
                //randomly input i * j numbers of vector3 value into the list
                //gridPosition
                gridPositions.Add(new Vector3(x_axis, 0f, z_axis));
            }
        }

    }

    void MapSetUp()
    {
        MapFolder = new GameObject("Map").transform;
        //use the same logic to generate other stuff
        for (int i = 0; i < MapSize; i++)
        {
            for (int j= 0; j < MapSize; j++)
            {
                int Groundnum = Random.Range(0, Grounds.Length);
                GameObject toInstantiate = Grounds[Groundnum];
                GameObject instance;
                switch (Groundnum)
                {
                    case 0:
                        instance =
                            Instantiate(toInstantiate, 
                            new Vector3(((i * 680)- (float)26.64)
                            , 0f
                            , ((j * 680)+(float)9.56)), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(MapFolder);
                        break;

                    case 1:
                        instance =
                            Instantiate(toInstantiate,
                            new Vector3(((i * 680) - (float)5.85)
                            , 0f
                            , ((j * 680) - (float)20)), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(MapFolder);
                        break;

                    case 2:
                        instance =
                            Instantiate(toInstantiate,
                            new Vector3((i * 680)
                            , 0f
                            , (j * 680)), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(MapFolder);
                        break;
                }
            }
        }
    }

    Vector3 RandomPosition()
    {
        //pick a random index
        int randomIndex = Random.Range(0, gridPositions.Count);
        //get the position from the list, gridPosition, using the random index
        Vector3 randomPosition = gridPositions[randomIndex];
        //remove that position in the list so that objects don't spwan in the same spot
        gridPositions.RemoveAt(randomIndex);
        //return the Vector3 Position
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        //control the number of the object spwan
        int objectCount = Random.Range(minimum, maximum);

        for (int i = 0; i < objectCount; i++)
        {
            //get the position
            Vector3 randomPosition = RandomPosition();
            //choose a game object from an array
            GameObject objChoose = tileArray[Random.Range(0,tileArray.Length)];
            randomPosition.y = objChoose.transform.position.y;
            //draw this objectout
            Instantiate(objChoose, randomPosition, objChoose.transform.rotation);
 

        }
    }

    public void SetupScene(int level)
    {
        //
        MapSetUp();
        InitialiseList();
        LayoutObjectAtRandom(ShipParts, ShipCount, ShipCount);
        //LayoutObjectAtRandom this will be used as the things on the map
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
