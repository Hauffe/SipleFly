using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanePilot : MonoBehaviour
{
    public float speed = 40.0f;
    public float decresedSpeed = 15.0f;
    public float verticalSpeed = 2f;
    public float horizontalSpeed = 1f;

    public float startTime;
    private float terrainHightWhereWeAre;

    void Start()
    {
        startTime = Time.time;
        Debug.Log("plane pilot script added to: " + gameObject.name);
    }


    void Update()
    {

        transform.position += transform.forward * Time.deltaTime * speed;
        speed -= transform.forward.y * Time.deltaTime * decresedSpeed;

        //mobile configurations
        Vector3 tilt = Input.acceleration;
        tilt = Quaternion.Euler(90, 0, 0) * tilt; // Landscape
        transform.Rotate(tilt.y * verticalSpeed, 0.0f, -tilt.x * horizontalSpeed);

        //pc configuration
        transform.Rotate(Input.GetAxis("Vertical") * verticalSpeed, 0.0f, -Input.GetAxis("Horizontal") * horizontalSpeed);

        terrainHightWhereWeAre = Terrain.activeTerrain.SampleHeight(transform.position);

        //Crash into the ground
        if (terrainHightWhereWeAre > transform.position.y)
        {
            if(Time.time - startTime > MainMenu.graterTime)
                MainMenu.graterTime = Time.time - startTime;

            Debug.Log("Time: " + MainMenu.graterTime);

            transform.position = new Vector3(transform.position.x,
                terrainHightWhereWeAre,
                transform.position.z);

            speed = -5;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }

    public void OnTriggerEnter(Collider obj)
    {
        Debug.Log("Colisão");
        if (obj.tag.Equals("Cubes"))
        {
            Destroy(obj.gameObject);
        }
    }
}
