using System.Collections.Generic;
using System.Linq;
using Serial.Models;
using UI;
using UnityEngine;

namespace Serial
{
    public class SerialParser : MonoBehaviour
    {
        [SerializeField] private float DetectionDistance = 1000;
        [SerializeField] private KeysVisualManager VisualManager;

        [SerializeField] private List<bool> _previouslyActivatedSensors = new();
        
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

                bool isKeyPressed = sensedObject.distance < DetectionDistance;

                if (isKeyPressed)
                {
                    activatedSensors[sensors[i].id - 1] = true;
                }
            }

            if (_previouslyActivatedSensors == activatedSensors)
            {
                return;
            }
            
            _previouslyActivatedSensors = activatedSensors;

            string pressedString = "";
            for (int index = 0; index < activatedSensors.Count; index++)
            {
                bool key = activatedSensors[index];

                pressedString += $"[{index + 1}: {key}], ";
            }

            Debug.Log(pressedString);
            VisualManager.UpdateKeyVisuals(activatedSensors);
        }
    }
}