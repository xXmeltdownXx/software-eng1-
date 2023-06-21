using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    enum DroneState
    {
        DRONE_STATE_IDLE,
        DRONE_STATE_START_TAKINGOFF,
        DRONE_STATE_TAKINGOFF,
        DRONE_STATE_MOVING_UP,
        DRONE_STATE_FLYING,
        DRONE_STATE_START_LANDING,
        DRONE_STATE_LANDING,
        DRONE_STATE_LANDED,
        DRONE_STATE_WAIT_ENGINE_STOP,
    }

    DroneState state;

    Animator _Anim;

    Vector3 speed = new Vector3(0.0f, 0.0f, 0.0f);

    public float SpeedChanger = 1.0f;

    public bool IsIdle()
    {
        return (state == DroneState.DRONE_STATE_IDLE);
    }

    public void TakeOff()
    {
        state = DroneState.DRONE_STATE_START_TAKINGOFF;
    }

    public bool IsFlying()
    {
        return (state == DroneState.DRONE_STATE_FLYING);
    }

    public void Land()
    {
        state = DroneState.DRONE_STATE_START_LANDING;
    }
    void Start()
    {
        _Anim = GetComponent<Animator>();

        state = DroneState.DRONE_STATE_IDLE;
    }

    public void Move(float speedX, float speedZ)
    {
        speed.x = speedX;
        speed.z = speedZ;

        UpdateDrone();
    }

    void UpdateDrone()
    {

        switch (state)
        {
            case DroneState.DRONE_STATE_IDLE:
                break;
            case DroneState.DRONE_STATE_START_TAKINGOFF:
                _Anim.SetBool("TakeOff", true);
                state = DroneState.DRONE_STATE_TAKINGOFF;
                break;
            case DroneState.DRONE_STATE_TAKINGOFF:
                if (_Anim.GetBool("TakeOff") == false)
                {
                    state = DroneState.DRONE_STATE_MOVING_UP;
                }
                break;
            case DroneState.DRONE_STATE_MOVING_UP:
                if (_Anim.GetBool("MoveUp") == false)
                {
                    state = DroneState.DRONE_STATE_FLYING;
                }
                break;
            case DroneState.DRONE_STATE_FLYING:
                float angleZ = -30.0f * speed.x * 60.0f * Time.deltaTime;
                float angleX = 30.0f * speed.z * 60.0f * Time.deltaTime;

                Vector3 rotation = transform.localRotation.eulerAngles;
                transform.localPosition += speed * SpeedChanger * Time.deltaTime;
                transform.localRotation = Quaternion.Euler(angleX, rotation.y, angleZ);
                break;
            case DroneState.DRONE_STATE_START_LANDING:
                _Anim.SetBool("MoveDown", true);
                state = DroneState.DRONE_STATE_LANDING;
                break;
            case DroneState.DRONE_STATE_LANDING:
                if (_Anim.GetBool("MoveDown") == false)
                {
                    state = DroneState.DRONE_STATE_LANDED;
                }
                break;
            case DroneState.DRONE_STATE_LANDED:
                _Anim.SetBool("Land", true);
                state = DroneState.DRONE_STATE_WAIT_ENGINE_STOP;
                break;
            case DroneState.DRONE_STATE_WAIT_ENGINE_STOP:
                if (_Anim.GetBool("Land") == false)
                {
                    state = DroneState.DRONE_STATE_IDLE;
                }
                break;
        }
    }
}
