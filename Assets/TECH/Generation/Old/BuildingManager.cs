using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public Transform locations;
    public Transform[] buildings;
    public float buildingScale = 1;
    public float buildingHeight = 1;
    public float windowBorder = 1;
    public int maxSegments;
    public bool generateWindows;
    
    public bool rotateStuffs;
    public float baseYRotation;
    public float rotationRange = 60;
    public GameObject building;
    public Material material;
    public GameObject windowObj;
    public GameObject windowObj2;



    // Start is called before the first frame update
    void Start()
    {
        buildings = new Transform[locations.childCount];
        CreateBuildings();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateBuildings()
    {
        //for eah building location, create a building of random size
        //each building will have 1-4 taller parts
        for(int i = 0; i < locations.childCount; i++)//create a building
        {
            //GameObject capsule = new GameObject();
            buildings[i] = new GameObject().transform;
            Transform parent = Instantiate(building, buildings[i]).transform;
            ScaleRandomly(parent);
            Vector3 basePos = new Vector3(locations.GetChild(i).position.x, buildings[i].transform.localScale.y / 2, locations.GetChild(i).position.z);
            parent.position = basePos;
            int subSegments = Random.Range(0, maxSegments);
            if(material) parent.gameObject.GetComponent<MeshRenderer>().material = material;
            if (generateWindows) PlaceWindows(parent, i);

            for (int j = 0; j < subSegments; j++)
            {
                //Transform seg = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
                Transform seg = Instantiate(building).transform;
                ScaleRandomly(seg);
                if (material) seg.gameObject.GetComponent<MeshRenderer>().material = material;
                PlaceSubSeg(parent, seg);
                if(generateWindows) PlaceWindows(seg, i);
                parent = seg;

            }
            if (rotateStuffs) buildings[i].RotateAround(basePos, new Vector3(0, 1, 0), Random.Range(-rotationRange + baseYRotation, rotationRange + baseYRotation));//buildings[i].Rotate(Random.Range(0, maxSegments * 10), Random.Range(0, maxSegments * 10), Random.Range(0, maxSegments) * 10);
            Debug.Log("Building finish");
        }
    }

    void ScaleRandomly(Transform res)
    {
        float x = buildingScale * Random.Range(1, 6);
        float y = buildingScale * buildingHeight * Random.Range(2, 3.5f);
        float z = buildingScale * Random.Range(1, 6);
        res.localScale = new Vector3(x, y, z);
    }


    void PlaceSubSeg(Transform parent, Transform res)
    {
        float x_scale = Random.Range(parent.lossyScale.x / 10, parent.lossyScale.x);
        float y_scale = buildingScale * buildingHeight * Random.Range(2, 3.5f);
        float z_scale = Random.Range(parent.lossyScale.z / 10, parent.lossyScale.z);
        res.localScale = new Vector3(x_scale, y_scale, z_scale);

        float x = parent.position.x + (Random.Range(-1, 2) * (parent.lossyScale.x - res.localScale.x)/2 );
        float z = parent.position.z + (Random.Range(-1, 2) * (parent.lossyScale.z - res.localScale.z) / 2);
        float y = parent.position.y + parent.lossyScale.y/2 + res.localScale.y/2;
        res.position = new Vector3(x, y, z);
        res.SetParent(parent);
    }

    void PlaceWindows(Transform parent, int listNum)
    {
        //num windows per axis
        float colx = Random.Range(2, 6);
        float rows = Random.Range(2, 6);
        float colz = Random.Range(2, 6);

        //space grid for each axis (includes window length + windowBorder)
        float space_x = (parent.lossyScale.x - (3 * windowBorder)) / colx;
        float space_y = (parent.lossyScale.y - (3 * windowBorder)) / rows;
        float space_z = (parent.lossyScale.z - (3 * windowBorder)) / colz;

        //window dimensions
        float winHeight = space_y - windowBorder;
        float winlengthX = space_x - windowBorder;
        float winlengthZ = space_z - windowBorder;

        //place windows:
        if(winHeight <= 0) return;

        GameObject window = windowObj;
        window.transform.localScale = new Vector3(winlengthX, winHeight, 1);

        GameObject window2 = windowObj2;
        window2.transform.localScale = new Vector3(winlengthZ, winHeight, 1);


        for (int row = 1; row <= rows; row++)
        {
            //for zplane 
            if(winlengthX > 0)
            {
                for(int col = 1; col <= colx; col++)
                {
                    float xpos = parent.position.x - parent.lossyScale.x / 2 - winlengthX / 2 + windowBorder + (col * space_x);
                    float ypos = parent.position.y - parent.lossyScale.y / 2 - winHeight / 2 + windowBorder + (row * space_y);
                    float zpos = parent.position.z - (parent.lossyScale.z / 2 + (.05f * buildingScale));
                    Instantiate(window, new Vector3(xpos, ypos, zpos), window.transform.rotation, buildings[listNum]);
                  //  window.transform.SetParent(parent);
                   
                }
            }
            //for xplane
            if (winlengthZ > 0)
            {
                for (int col = 1; col <= colz; col++)
                {
                    float xpos = parent.position.x - (parent.lossyScale.x / 2 + (.05f * buildingScale)); 
                    float ypos = parent.position.y - parent.lossyScale.y / 2 - winHeight / 2 + windowBorder + (row * space_y);
                    float zpos = parent.position.z - parent.lossyScale.z / 2 - winlengthZ / 2 + windowBorder + (col * space_z);
                    Instantiate(window2, new Vector3(xpos, ypos, zpos), window2.transform.rotation, buildings[listNum]);
                //    window2.transform.SetParent(parent);

                }
            }
        }
        //Debug.Log("window placed");
        //float x_pos = (parent.position.x - parent.lossyScale.x/2 - winlengthX/2) + windowBorder + (colNUm * space_x)
    }

}
