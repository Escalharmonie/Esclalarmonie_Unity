using System.Collections.Generic;
using Serial.Models;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class SerialEmulator : MonoBehaviour
{
    [FormerlySerializedAs("OnSerialDataUpdated")] [SerializeField]
    public UnityEvent<SerialModel> OnSerialModelUpdated;
    
    // Update is called once per frame
    private void Update()
    {
        SerialModel model = new("2.0", "update", new Params(new List<Sensor>()));

        model.@params.sensors.Add(new Sensor(1,
            new List<SensedObject> { new(Input.GetKey(KeyCode.Q) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(2,
            new List<SensedObject> { new(Input.GetKey(KeyCode.A) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(3,
            new List<SensedObject> { new(Input.GetKey(KeyCode.W) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(4,
            new List<SensedObject> { new(Input.GetKey(KeyCode.S) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(5,
            new List<SensedObject> { new(Input.GetKey(KeyCode.E) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(6,
            new List<SensedObject> { new(Input.GetKey(KeyCode.D) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(7,
            new List<SensedObject> { new(Input.GetKey(KeyCode.R) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(8,
            new List<SensedObject> { new(Input.GetKey(KeyCode.F) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(9,
            new List<SensedObject> { new(Input.GetKey(KeyCode.T) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(10,
            new List<SensedObject> { new(Input.GetKey(KeyCode.G) ? 500 : 3000, 0) }));

        model.@params.sensors.Add(new Sensor(11,
            new List<SensedObject> { new(Input.GetKey(KeyCode.Y) ? 500 : 3000, 0) }));
        
        model.@params.sensors.Add(new Sensor(12,
            new List<SensedObject> { new(Input.GetKey(KeyCode.H) ? 500 : 3000, 0) }));
        
        OnSerialModelUpdated.Invoke(model);
    }
}