using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public DroneController droneController;

    public Button FlyButton;
    public Button LandButton;

    //public GameObject Controls;
    struct DroneAnimationControls
    {
        public bool moving;
        public bool interpolatingAsc;
        public bool interpolatingDesc;
        public float axis;
        public float direction;
    }

    DroneAnimationControls movingLeft;
    DroneAnimationControls movingBackward;


    void Start()
    {
        FlyButton.onClick.AddListener(EventOnClickFLyButton);
        LandButton.onClick.AddListener(EventOnClickLandButton);
        LandButton.gameObject.SetActive(false);
    }


    void Update()
    {
        float speedX = Input.GetAxis("Horizontal");
        float speedY = Input.GetAxis("Vertical");

        //UpdateControls(ref movingLeft);
        //UpdateControls(ref movingBackward); 

        droneController.Move(speedX, speedY);
        //droneController.Move(movingLeft.axis * movingLeft.direction, movingBackward.axis * movingBackward.direction);   
    }

    /*void UpdateControls(ref DroneAnimationControls controls)
    {
        if (controls.moving || controls.interpolatingAsc || controls.interpolatingDesc)
        {
            if (controls.interpolatingAsc)
            {
                controls.axis += .05f;

                if (controls.axis >= 1.0f)
                {
                    controls.axis = 1.0f;
                    controls.interpolatingAsc = false;
                    controls.interpolatingDesc = true;
                }
            }
            else if (!controls.moving)
            {
                controls.axis -= .05f;

                if (controls.axis <= 0.0f)
                {
                    controls.axis = 0.0f;
                    controls.interpolatingDesc = false;
                }
            }

        }
    }
    */
    void EventOnClickFLyButton()
    {
        if (droneController.IsIdle())
        {
            droneController.TakeOff();
            FlyButton.gameObject.SetActive(false);
            LandButton.gameObject.SetActive(true);
            //Controls.SetActive(true);
        }
    }


    void EventOnClickLandButton()
    {
        if (droneController.IsFlying())
        {
            droneController.Land();
            FlyButton.gameObject.SetActive(true);
            LandButton.gameObject.SetActive(false);
        }
    }

    /*
    public void EventOnLeftButtonPressed()
    {
        movingLeft.moving = true;
        movingLeft.interpolatingAsc = true;
        movingLeft.direction = -1.0f;
    }

    public void EventOnLeftButtonReleased()
    {
        movingLeft.moving = false; 
    }
    public void EventOnRightButtonPressed()
    {
        movingLeft.moving = true;
        movingLeft.interpolatingAsc = true;
        movingLeft.direction = 1.0f;
    }

    public void EventOnRightButtonReleased()
    {
        movingLeft.moving = false;
    }
    public void EventOnForwardButtonPressed()
    {
        movingBackward.moving = true;
        movingBackward.interpolatingAsc = true;
        movingBackward.direction = 1.0f;
    }
    public void EventOnForwardButtonReleased()
    {
        movingBackward.moving = false;
    }
    public void EventOnBackwardButtonPressed()
    {
        movingBackward.moving = true;
        movingBackward.interpolatingAsc = true;
        movingBackward.direction = -1.0f;
    }
    public void EventOnBackwardButtonReleased()
    {
        movingBackward.moving = false;
    } */

}
