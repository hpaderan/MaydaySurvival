using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectGenerator : MonoBehaviour
{
    public GameObject[] Tree;
    public GameObject[] Crystal;
    public GameObject[] Rock;
    public GameObject[] Grass;
    public GameObject[] SmallItems;

    public GroundGenerate groundScript;
    private int crystal, rocks, trees, grass, smallitems;
    public void ObjectNumber()
    {
        Debug.Log("generating numbers");
        crystal = Random.Range(0, 20);
        trees = Random.Range(10, 40);
        rocks = Random.Range(0, 30);
        grass = Random.Range(40, 110);
        smallitems = Random.Range(0, 30);
        while ((int)((crystal + rocks + trees) / 10) > 11)
        {
            crystal = Random.Range(0, 20);
            trees = Random.Range(10, 40);
            rocks = Random.Range(0, 30);
        }
        return;
    }

    public void LayOutObjects(GameObject[] objectArray, int ObjectNum, int i, int j)
    {
        int count = ObjectNum;
        //loop for each tile in the map
        Debug.Log(groundScript.MapSize);
        //for(int i = 0; i < groundScript.MapSize; i++)
        
            //for(int j = 0; j < groundScript.MapSize; j++)
            
                //Debug.Log("i is "+i+"j is "+j);
                Vector3 newranpos = RandomPos(i, j);
                //for (int k = 0; k < ObjectNum; k++)
                  //check if it collides with other object
                    //if yes then regenerate a position
                    Debug.Log("i and j are"+i+","+j);
                    newranpos = RandomPos(i, j);
                    while (CheckCollide(newranpos))
                    {
                        //Debug.Log("regenerate vector3");
                        newranpos = RandomPos(i, j);
                    }
                    //if not then generate this item
                    //Debug.Log("the position is "+newranpos.x+" "+ newranpos.y+" "+ newranpos.z);
                    Debug.Log("The newpos is " + newranpos);
                    SetOneObject(objectArray, newranpos);
                    //loop until the number is 0 in that tile
                
                //SetOneObject(objectArray, newranpos);
            
        

    }
    //generate inside one tile
    Vector3 RandomPos(int x, int y)
    {
        float x_pos = Random.Range((x * 680) - 340 + 30, (x * 680) + 340 - 30);
        float z_pos = Random.Range((y * 680) - 340 + 30, (y * 680) + 340 - 30);
        Vector3 randompostion = new Vector3(x_pos, 0f, z_pos);
        //Debug.Log("the position is " + x_pos +","+ "0f,"+z_pos);
        return randompostion;
    }

    //set one object into the random position of that position is not collided with anything
    void SetOneObject(GameObject[] obj, Vector3 newpos)
    {
        //randomly choose one in the array of object

        GameObject objChoose = obj[Random.Range(0, obj.Length)];
        newpos.y = objChoose.transform.position.y;
        //objChoose.transform.position = newpos;
        Debug.Log("Instantiating");
        Instantiate(objChoose, newpos, objChoose.transform.rotation);
        Debug.Log("Instantiate at"+ newpos);
        groundScript.UsedSpace.Add(newpos);
    }


    //check the usedspace list from ground generator
    bool CheckCollide(Vector3 currentpos)
    {
        for (int i = 0; i < groundScript.UsedSpace.Count; i++)
        {
            if (currentpos.x >= (groundScript.UsedSpace[i].x-25) && 
                currentpos.x <= (groundScript.UsedSpace[i].x + 25) &&
                currentpos.z >= (groundScript.UsedSpace[i].z - 25) &&
                currentpos.z <= (groundScript.UsedSpace[i].z + 25))
            {
                return true;
            }
        }
        return false;
    }

    public void GenerateAll()
    {
        Debug.Log("final create object");
        for (int i = 0; i < groundScript.MapSize; i++)
        {
            for (int j = 0; j < groundScript.MapSize; j++)
            {
                ObjectNumber();
                Debug.Log("the number of tree is " + trees);
                LayOutObjects(Tree, (int)trees / 10, i, j);
                Debug.Log("the number of crystal is " + crystal);
                LayOutObjects(Crystal, (int)crystal / 10, i, j);
                LayOutObjects(Rock, (int)rocks / 10, i, j);
                LayOutObjects(Grass, (int)grass / 10, i, j);
                LayOutObjects(SmallItems, (int)smallitems / 10, i, j);
            }
        }
    }

}
