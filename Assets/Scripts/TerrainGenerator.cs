using NUnit.Framework;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject player;
    public int depth = 20;
    public int height = 256;
    public int width = 256;
    public float scale = 30f;
    public float Xoffset;
    public float Yoffset;
    public float offsetcontrolspeed=0.1f;
    public int depthchanger=1;
    public float scalechanger=0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    
    }

    // Update is called once per frame
    void Update()
    {
        Terrain terr = GetComponent<Terrain>();
        terr.terrainData = GenerateTerrain(terr.terrainData);
        if(player.GetComponent<PlayerInputMovement>().isflying)
        {
            //Random Generated offset
        if (Input.GetKeyDown(KeyCode.R))
        {
            Xoffset = Random.Range(0f,9999f);
            Yoffset = Random.Range(0f,9999f);
        }
        // Control the offset with J, L, I, K keys
        if (Input.GetKey(KeyCode.J) && player.GetComponent<PlayerInputMovement>().isflying)
        {
            Xoffset += offsetcontrolspeed;
        }
        if (Input.GetKey(KeyCode.L))
        {
            Xoffset -= offsetcontrolspeed;
        }
        if (Input.GetKey(KeyCode.I))
        {
            Yoffset += offsetcontrolspeed;
        }
        if (Input.GetKey(KeyCode.K))
        {
            Yoffset -= offsetcontrolspeed;
        }
        //Control Depth of Terrain
        if (Input.GetKey(KeyCode.Q))
        {
            depth += depthchanger;
        }
        if (Input.GetKey(KeyCode.E))
        {
            depth -= depthchanger;
        }
        //Control Scale of Terrain
        if (Input.GetKey(KeyCode.LeftBracket))
        {
            scale += scalechanger;
        }
        if (Input.GetKey(KeyCode.RightBracket))
        {
            scale -= scalechanger;
        }
        }
    }
    TerrainData GenerateTerrain(TerrainData terrData)
    {
        terrData.heightmapResolution = width +1;
        terrData.size = new Vector3(width, depth, height);
        terrData.SetHeights(0,0, GenerateHeights());
        return terrData;
    }
    float[,] GenerateHeights()
    {
        float[,] heights = new float[width,height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                heights[x,y] = CalculateHeight(x,y);
            }
        }
        return heights;
    }
    float CalculateHeight(int x ,int y)
    {
        float X_coord =(float) x / width * scale + Xoffset;
        float Y_coord = (float)y / width * scale + Yoffset;
        return Mathf.PerlinNoise(X_coord,Y_coord);
    }
}
