using System;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using Serial.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Serial
{
    public class SerialSlotReader : MonoBehaviour
    {
        [SerializeField]
        private string PortName = "COM6";

        [SerializeField]
        private int BaudRate = 9600;

        [SerializeField]
        private UnityEvent<SerialModel> OnReadSerialData;
        
        [SerializeField]
        private UnityEvent OnSerialConnectionAborted;

        Thread? _ioThread;
        private SerialPort? _serialPort;

        private SerialModel? _model;
        private bool _shouldInvokeEvent = true;

        string _slotMessage = "";
        // Start is called before the first frame update
        void Start()
        {
            BeginReadSerial(PortName, BaudRate);
        }
        
        void OnApplicationQuit()
        {
            if (_ioThread is not null)
            {
                _ioThread.Abort();
            }
        }

        public void BeginReadSerial(string portName, int baudRate)
        {
            if (_ioThread == null)
            {
                _ioThread = new Thread(() => ReadSerial(portName,baudRate));
                _ioThread.Start();
                return;
            }

            if (_ioThread.IsAlive)
            {
                _ioThread.Abort();
            }

            _ioThread = null;
            _ioThread = new Thread(() => ReadSerial(portName,baudRate));
            _ioThread.Start();
        }
        
        private void ReadSerial(string portName, int baudrate)
        {
            _serialPort = new SerialPort(portName, baudrate); 
            
            if (SerialPort.GetPortNames().Contains(portName))
            {
                _serialPort.Open();
            }
            else
            {
                Debug.LogWarning("Chosen port name does not exists");
            }

            while (true)
            {
                if (_serialPort != null  && _serialPort.IsOpen)
                {
                    try
                    {
                        _slotMessage = _serialPort.ReadLine();
                    }
                    catch (Exception e)
                    {
                        _shouldInvokeEvent = false;
                        break;
                    }

                    // Debug.Log(slotMessage);

                    if (_slotMessage != null)
                    {
                        var reader = new StringReader(_slotMessage);
                        _slotMessage = reader.ReadLine();
                    }

                    int newSlotIndex = 0;

                    SerialModel? data = null;
                    try
                    {
                        data = JsonUtility.FromJson<SerialModel>(_slotMessage);
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning("Oupsy doupsy your serial port is a bit clumsy");
                    }


                    if (data is not null)
                    {
                        _model = data;
                        _shouldInvokeEvent = true;
                    }
                }
                else
                {
                    Debug.Log("");
                    break;
                }
            }
            
            OnSerialConnectionAborted.Invoke();
        }

        // Update is called once per frame
        void Update()
        {
            if (_shouldInvokeEvent)
            {
                if (_model != null)
                {
                    OnReadSerialData.Invoke(_model);
                }
                _shouldInvokeEvent = false;
            }
        }
    }
}