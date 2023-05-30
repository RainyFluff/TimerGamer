using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    [Header("Revert time")]
    GameObject player;
    Camera kamera;
    Vector3 newPlayerPosition;
    Vector3 temporaryPlayerPosition;
    float timer;
    float timer2;
    float arrayNumber;
    float revertSpeed = 40;
    public Vector3[] playerPositions;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        kamera = Camera.main;
        playerPositions = new Vector3[30];
    }

    // Update is called once per frame
    void Update()
    {
        arrayNumber = (Time.time%3) * 10;
        int arrayNumber2 = (int)arrayNumber;
        Debug.Log(arrayNumber2);
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (arrayNumber2 == 29)
            {
                temporaryPlayerPosition = playerPositions[0];
            }

            else
            {
                temporaryPlayerPosition = playerPositions[arrayNumber2 + 1];
            }
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        }

        if(Time.time - timer > 0.1f)
        {
            timer = Time.time;
            if(arrayNumber == 0)
            {
                playerPositions[30] = player.transform.position;
            }
            else
            {
                playerPositions[arrayNumber2] = player.transform.position;
            }
        }

        if(temporaryPlayerPosition != new Vector3(0, 0, 0))
        {
            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<CapsuleCollider>().enabled = false;
            player.GetComponent<Rigidbody>().useGravity = false;
            player.transform.position = Vector3.MoveTowards(player.transform.position, temporaryPlayerPosition, revertSpeed * Time.deltaTime);
        }
        else
        {
            player.GetComponent<Movement>().enabled = true;
            player.GetComponent<CapsuleCollider>().enabled = true;
            player.GetComponent<Rigidbody>().useGravity = true;
        }

        if(player.transform.position == temporaryPlayerPosition)
        {
            temporaryPlayerPosition = new Vector3(0, 0, 0);
        }
    }
    


}
