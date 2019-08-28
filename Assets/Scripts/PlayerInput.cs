using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DLO   {
    public class PlayerInput : MonoBehaviour
    {
        private float speed = 100.0f;
        public bool isPlayerOne = true;
        public float yMax = 31.0f;
        public float yMin = -31.0f;

        private string Axis1 = "Fire1";
        private string Axis2 = "Fire2";

        public serialMicrobitInput Microbit = null;

        public bool useButtons = true;

        public bool usePositionJoystick = true;

        public string JoystickName = "";

        public bool FlipInput = false;

        public string PotentialComPort = "COM1";

        public bool UseDelta = false;

        private void Start()
        {
            StartCoroutine(GetMeMyMicrobit());
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                speed = speed + 10;
                Debug.Log(speed);
            }

            if (Input.GetKeyDown(KeyCode.O))
            {
                speed = speed - 10;
                Debug.Log(speed);
            }

            if (useButtons)
            {
                float input2 = 0f;

                if (Input.GetAxis(Axis1) > 0)
                {
                    input2 = 1;
                    Debug.Log(Input.GetAxis(Axis1).ToString());
                }
                else if (Input.GetAxis(Axis2) > 0)
                {
                    input2 = -1;
                    Debug.Log(Input.GetAxis(Axis2).ToString());
                }

                Vector3 Pos2 = gameObject.transform.position;
                Pos2.y += speed * input2;
                Pos2.y = Mathf.Clamp(Pos2.y, yMin, yMax);
                gameObject.transform.position = Pos2;
            }
            else
            {
                if (Microbit == null || usePositionJoystick)
                {

                    float input = 0f;

                    if (!string.IsNullOrEmpty(JoystickName))
                    {
                        input = Input.GetAxisRaw(JoystickName);
                    }
                    else
                    {
                        Debug.LogWarning("joystick name is not valid");
                    }

                    Vector3 Pos = gameObject.transform.position;
                    // Pos.y += speed * Time.deltaTime * input;
                    float pos = yMax * input;
                    if (FlipInput) pos = -pos;
                    Pos.y = Mathf.Clamp(pos, yMin, yMax);
                    gameObject.transform.position = Pos;
                }
                else
                {
                    if (!UseDelta)
                    {
                        Vector3 Pos = gameObject.transform.position;
                        Pos.y = Microbit.outputF * yMax;
                        gameObject.transform.position = Pos;
                    }
                    else
                    {
                        Vector3 Pos = gameObject.transform.position;
                        Pos.y += (float)Microbit.outputI2 * speed / 1000f;
                        Pos.y = Mathf.Clamp(Pos.y, yMin, yMax);
                        gameObject.transform.position = Pos;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha9) && !isPlayerOne)
            {
                FlipInput = !FlipInput;
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) && isPlayerOne)
            {
                FlipInput = !FlipInput;
            }
        }

        IEnumerator GetMeMyMicrobit()
        {
            yield return new WaitForSeconds(0.5f);
            if (Microbit == null)
            {
                yield return new WaitForSeconds(3);
                serialMicrobitInput[] AllMicros = FindObjectsOfType<serialMicrobitInput>();
                foreach (serialMicrobitInput micro in AllMicros)
                {
                    if (PotentialComPort == micro.COM)
                    {
                        Microbit = micro;
                    }
                }

                if (Microbit == null)
                {
                    StartCoroutine(GetMeMyMicrobit());
                }
            }
            else
            {
                yield return new WaitForSeconds(3);
            }
        }
    }
}