﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Need this for calling UI scripts

public class Manager : MonoBehaviour
{

    [SerializeField]
    Transform UIPanel; //Will assign our panel to this variable so we can enable/disable it



    [SerializeField]
    Text timeText;
    [SerializeField]
    MeshRenderer beam;
    [SerializeField]
    Button readButton;

    bool isPaused; //Used to determine paused state
    public double time;
    private double speed;
    double interval;
    private CSVReader instance;


    void Start()
    {
        UIPanel.gameObject.SetActive(false); //make sure our pause menu is disabled when scene starts
        isPaused = false; //make sure isPaused is always false when our scene opens
        time = 0;
        timeText.text = time.ToString();

        //adjust speed here
        speed = 3;
        interval = speed;

    }

    void Update()
    {

        if (readButton.GetComponent<CSVReader>().data != null)
        {



            string[,] data = readButton.GetComponent<CSVReader>().data;

            Debug.Log("The value of distance is " + data[2, (int)time]);

            float distance = float.Parse(data[2, (int)time]);
            float degreesRotation = float.Parse(data[6, (int)time]);
            Debug.Log("The value of rotation is " + data[6, (int)time]);
            float degreesElevation = float.Parse(data[7, (int)time]);
            Debug.Log("The value of elevation is is " + data[7, (int)time]);

            if (data[3, (int)time].Equals("1"))
            {
                beam.material.color = Color.red;
            }
            else
            {
                beam.material.color = Color.cyan;
            }

         

            Vector3 scale = new Vector3();
            scale.x = 1;
            scale.y = 1;
            scale.z = distance / 100;

            //nextPos.Scale(scale);

            Vector3 rotation = new Vector3();
            rotation.x = degreesElevation;
            rotation.y = degreesRotation;
            rotation.z = 0;



            beam.transform.transform.rotation = Quaternion.Euler(degreesElevation, degreesRotation, 0);
            beam.transform.transform.localScale = scale;

            //update postion of beam 8 lines of data per column
            //time, start range, end range, high power, number of pulses, width, azimuth degrees, Elvation degrees
        }

        //If player presses escape and game is not paused. Pause game. If game is paused and player presses escape, unpause.
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
            {
                UnPause();
            }
        }
        time += interval;
        timeText.text = ((int)time).ToString();

    }

    public void Pause()
    {
        isPaused = true;
        pauseTime();
        UIPanel.gameObject.SetActive(true); //turn on the pause menu
    }

    public void UnPause()
    {
        isPaused = false;
        UIPanel.gameObject.SetActive(false); //turn off pause menu
        play();
    }

    public void ExitProgram()
    {
        Application.Quit();
    }

    public void Fastforward()
    {
        interval = 2 * speed;
    }

    public void Rewind()
    {
        interval = -2 * speed;
    }

    public void play()
    {
        interval = speed;

    }

    public void pauseTime()
    {
        interval = 0;
    }
}