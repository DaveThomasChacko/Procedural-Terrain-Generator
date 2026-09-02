using TMPro;
using UnityEngine;

public class PlayerInputMovement : MonoBehaviour
{
    public float gravityforce = 1f;
    
    public float g = -9.81f;
   public CharacterController controller;
   public Terrain terrain;
   public bool isflying= true;
   public float speed = 50f;
   public TextMeshProUGUI enable_or_disableflighttext;
    void Start()
    {

    }
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move*speed* Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftControl) && isflying)
        {
            move += Vector3.down;
        }
        if (Input.GetKey(KeyCode.Space) && isflying)
        {
            move += Vector3.up;
        }
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (isflying)
            {
                EnableGroundView();
            }
            else
            {
                EnableFly();
            }
        }
        if (!isflying)
        {
            Vector3 gravityVector = new Vector3(0,-gravityforce,0);
            controller.Move(gravityVector);
        }
        transform.position += move * speed * Time.deltaTime;
    }
    void EnableGroundView()
    {
        enable_or_disableflighttext.text = "Ground Mode Enabled";
        controller.enabled=false;
        transform.position = new Vector3(terrain.GetComponent<TerrainGenerator>().width/2,terrain.GetComponent<TerrainGenerator>().depth+1,terrain.GetComponent<TerrainGenerator>().height/2);
        controller.enabled=true;
        isflying = false;
        speed = 10f;
    }
    void EnableFly()
    {
        enable_or_disableflighttext.text = "Edit Mode Enabled";
        isflying = true;
        speed = 50f;
    }
    
}