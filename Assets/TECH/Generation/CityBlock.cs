using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBlock : MonoBehaviour
{
    /** CITYBLOCK
     * 
     * The Walls of the Maze
     * A city block should contain a bunch of buildings
     */

    //static variables
    public float buildingDensity = 1; //1 building per sq. meter
    //public GameObject[] buildings;
    public Building building;
    private Vector3 blockScale;

    Vector3 randomPosition()
    {
        //problem: buildings overhang past city blovk
        //solution: buildings cannot spawn on edges width of half of max width
        float overhang = Building.scale;

        //if overhang is bigger than block width, the randomization must be zero
        //(i.e. if the building is bigger than the city block in a dimension, it must placed in the center to minimize overhang)
        float x_rand = Random.Range(-.5f, .5f) * Mathf.Max( 0, blockScale.x - overhang );
        float z_rand = Random.Range(-.5f, .5f) * Mathf.Max( 0, blockScale.z - overhang );

        float x = transform.position.x +  x_rand;
        float y = transform.position.y + .5f * blockScale.y;
        float z = transform.position.z + z_rand;

        return new Vector3(x, y, z);

    }

    void generateBuildings()
    {
        int buildingNumber = (int) (buildingDensity * blockScale.x * blockScale.z);
        Debug.Log(buildingNumber + " buildings");
        for (int i = 0; i < buildingNumber; i++)
        {
            //ORDER HERE CHANGES EFFECT GREATLY
            Building b = Instantiate(building);
            b.Build( randomPosition(), new Vector2(blockScale.x, blockScale.z));
            b.transform.parent = transform;
            //b.Build(new Vector3(transform.position.x, transform.position.y + .5f * blockScale.y, transform.position.z));
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        blockScale = transform.lossyScale;
        generateBuildings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
