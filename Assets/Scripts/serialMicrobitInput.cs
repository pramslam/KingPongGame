using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System.Threading;

namespace KingPong
{
    public class serialMicrobitInput : MonoBehaviour
    {
        public static string com = "COM6";
        public static int BaudRate = 115200;

        public string COM;
        public int BAUD;

        char[] strm = new char[15];

        string outputS = "";
        string outputS2 = "";

        public int outputI = 0;
        public int outputI2 = 0;

        int outputVI = 0;

        public float outputF = 0;
        public float outputV = 0;
        public int max = 1072;

        string s = null;

        private Thread _t1;

        public bool IsActive = true;


        SerialPort port;

        private void Awake()
        {
            serialMicrobitInput[] allMicros = FindObjectsOfType<serialMicrobitInput>();
            bool unique = true;
            foreach (serialMicrobitInput micro in allMicros)
            {
                if (micro.COM == COM && micro != this)
                {
                    unique = false;
                }
            }

            if (unique)
            {
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            try
            {
                //port = new SerialPort("COM6", 115200,Parity.None,8,StopBits.One);
                //port = new SerialPort("COM5", 115200);
                port = new SerialPort(COM, BAUD);

                //port = new SerialPort("COM20", 115200);
                port.Open();

                port.ReadTimeout = 1000;

                //port.Write("2");

                //port.DataReceived += Port_DataReceived;

                //StartCoroutine(NumberTwo());

                _t1 = new Thread(_Func);

                _t1.Start();
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        private void Port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //string s = port.ReadLine();
            //Debug.Log(s);

            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            string s = sp.ReadLine();

            Debug.Log("Data Received:");
            Debug.Log(sp.ReadLine());
        }

        private void _Func()
        {
            while (true)
            {
                Debug.Log("beep");
                if (port.IsOpen)
                {
                    //string sL = port.ReadExisting();

                    s = port.ReadLine();
                    Debug.Log(s);
                    //Debug.Log(sL);

                    //yield return s = ;
                    //calculate();
                    calculate(s);

                    outputF = (float)outputI / (float)max;
                }
                else
                {
                    Debug.Log("portIsClosed?");
                }
            }

        }

        private void OnApplicationQuit()
        {
            _t1.Abort();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                port = new SerialPort(COM, BAUD);
                port.Open();

                port.ReadTimeout = 1000;
                Debug.Log("try connect");
                string[] sa = SerialPort.GetPortNames();

                foreach (string s in sa)
                {
                    Debug.Log(s);
                }
            }

            //string s = null;
            //s = port.ReadLine();
            //try
            //{
            //	s = port.ReadLine();
            //}
            //catch
            //{
            //	Debug.LogError("CantReadLine");
            //}

            //port.Write("*");
            //port.Read(strm, 0, 15);



            //calculate();
        }

        void calculate()
        {
            if (s != null)
            {
                if (s.Contains("-"))
                {

                    s = s.Remove(s.IndexOf("-"), 1);

                    if (s.Contains("-"))
                    {
                        s = s.Remove(s.IndexOf("-"), 1);
                    }

                    int.TryParse(s, out outputI);
                    outputS = outputI.ToString();
                    int HLen = outputS.Length / 2;
                    outputS2 = outputS.Substring(0, HLen);
                    int.TryParse(outputS2, out outputI2);

                    outputI2 *= -1;



                    Debug.Log(HLen.ToString() + "   " + s + " " + outputS + " " + outputS2 + " " + outputI.ToString() + " " + outputI2.ToString());
                }
                else
                {
                    //Debug.Log(s);
                    //int HLen = s.Length / 2;
                    //outputS = s.Substring(HLen - 1, HLen);
                    int.TryParse(s, out outputI);
                    outputS = outputI.ToString();
                    int HLen = outputS.Length / 2;
                    outputS2 = outputS.Substring(0, HLen);
                    int.TryParse(outputS2, out outputI2);


                    Debug.Log(HLen.ToString() + "   " + s + " " + outputS + " " + outputS2 + " " + outputI.ToString() + " " + outputI2.ToString());
                }

                outputF = (float)outputI2 / (float)max;

            }
        }
        void calculate(System.String inp)
        {
            if (inp != null)
            {
                Debug.Log(inp);

                if (inp.Contains("(") && inp.Contains(")"))
                {
                    System.Int32 openI = inp.IndexOf("(");
                    System.Int32 Mid = inp.IndexOf(" ");
                    System.Int32 closeI = inp.IndexOf(")");

                    System.String PosS = inp.Substring(openI + 1, Mid - openI - 1);
                    System.String VelS = inp.Substring(Mid + 1, closeI - Mid - 1);

                    System.Int32.TryParse(PosS, out outputI);
                    System.Int32.TryParse(VelS, out outputI2);


                }

                //	if (inp.Contains("-"))
                //	{

                //	inp = inp.Remove(inp.IndexOf("-"), 1);

                //	if (inp.Contains("-"))
                //	{
                //	s = inp.Remove(inp.IndexOf("-"), 1);
                //	}

                //	int.TryParse(inp, out outputI);
                //	outputS = outputI.ToString();
                //	int HLen = outputS.Length / 2;
                //	outputS2 = outputS.Substring(0, HLen);
                //	int.TryParse(outputS2, out outputI2);

                //	outputI2 *= -1;



                //	Debug.Log(HLen.ToString() + "   " + inp + " " + outputS + " " + outputS2 + " " + outputI.ToString() + " " + outputI2.ToString());
                //	}
                //	else
                //	{
                //	//Debug.Log(inp);
                //	//int HLen = inp.Length / 2;
                //	//outputS = inp.Substring(HLen - 1, HLen);
                //	int.TryParse(inp, out outputI);
                //	outputS = outputI.ToString();
                //	int HLen = outputS.Length / 2;
                //	outputS2 = outputS.Substring(0, HLen);
                //	int.TryParse(outputS2, out outputI2);


                //	Debug.Log(HLen.ToString() + "   " + inp + " " + outputS + " " + outputS2 + " " + outputI.ToString() + " " + outputI2.ToString());
                //	}

                //	outputF = (float)outputI2 / (float)max;

            }
            Debug.Log(inp);
        }
    }
}