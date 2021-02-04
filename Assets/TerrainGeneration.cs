using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    public int width = 256;
    public int length = 256;
    public int height = 256;
    public float scale = 20f;
    Terrain terrain;
    public float offX = 250f;
    public float offY = 250f;
    void Start(){
        offX = Random.Range(0f, 5000f);
        offY = Random.Range(0f, 5000f);
    }
    void Update()
    {
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
        offX += Time.deltaTime * 5f;
    }

    // Update is called once per frame
    TerrainData GenerateTerrain(TerrainData data){
        data.heightmapResolution = width + 1;
        data.size = new Vector3(width, length, height);
        data.SetHeights(0,0,GenerateHeights());
        return data;
    }

    float[,] GenerateHeights(){
        float[,] heights = new float[width, height];
        for(int x=0;x<width;x++){
            for(int y=0;y<height;y++){
                heights[x,y] = CalcHeights(x,y);
            }
        }
        return heights;
    }

    float CalcHeights(int x, int y){
        float xCoord = (float)x/width * scale + offX;
        float yCoord = (float)y/height * scale + offY;
        
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
