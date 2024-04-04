using System.Collections.Generic;
using System.Linq;
using Serial.Models;
using UI;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

namespace Serial
{
    public enum KeyState
    {
        Released,
        JustReleased,
        Pressed,
        JustPressed
    }
    
    public class SerialParser : MonoBehaviour
    {
        [SerializeField] private float2 DetectionDistance = new(1400,1600);
        [SerializeField] private KeysVisualManager VisualManager;

        [SerializeField] private List<bool> _previouslyActivatedSensors = new();

        [SerializeField] private UnityEvent<List<bool>> OnPressedKey;
        [SerializeField] private UnityEvent<List<KeyState>> OnKeysStateChanged;
        
        public void UpdateSerialData(SerialModel data)
        {
            var sensors = data.@params.sensors;

            var sensorNumber = sensors.Max(max => max.id);

            List<bool> activatedSensors = Enumerable.Repeat(false, sensorNumber).ToList();

            for (int i = 0; i < sensors.Count; i++)
            {
                SensedObject? sensedObject = sensors[i].objects.FirstOrDefault(obj => obj.status == 0);
                if (sensedObject is null)
                    continue;

                int sensorIndex = sensors[i].id - 1;

                bool wasPressed = false;

                bool isKeyPressed = false;
                if (sensorIndex < _previouslyActivatedSensors.Count)
                {
                    bool wasActivated = _previouslyActivatedSensors[sensorIndex];

                    if (wasActivated)
                    {
                        isKeyPressed = sensedObject.distance < DetectionDistance.y && sensedObject.distance >= 0;
                    }
                    else
                    {
                        isKeyPressed = sensedObject.distance < DetectionDistance.x && sensedObject.distance >= 0;
                    }
                }
                
                

                if (isKeyPressed)
                {
                    activatedSensors[sensors[i].id - 1] = true;
                }
            }

            if (_previouslyActivatedSensors == activatedSensors)
            {
                return;
            }

            List<KeyState> newKeyState = new();
            
            for (int i = 0; i < activatedSensors.Count; i++)
            {
                bool sensor = activatedSensors[i];
    
                if (_previouslyActivatedSensors.Count - 1 < i )
                {
                    newKeyState.Add(sensor ? KeyState.JustPressed : KeyState.JustReleased);
                    continue;
                }

                if (sensor == _previouslyActivatedSensors[i])
                {
                    newKeyState.Add(sensor ? KeyState.Pressed : KeyState.Released);
                    continue;
                }
                
                newKeyState.Add(sensor ? KeyState.JustPressed : KeyState.JustReleased);
                
            }
            _previouslyActivatedSensors = activatedSensors;
            
            OnPressedKey.Invoke(activatedSensors);
            
            OnKeysStateChanged.Invoke(newKeyState);
            
            VisualManager.UpdateKeyVisuals(activatedSensors);
        }
    }
}