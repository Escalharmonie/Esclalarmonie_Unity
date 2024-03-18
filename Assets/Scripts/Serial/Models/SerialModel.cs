using System;
using System.Collections.Generic;

namespace Serial.Models
{
    
    // SerialModel myDeserializedClass = JsonConvert.DeserializeObject<SerialModel>(myJsonResponse);
    [Serializable]
    public class SensedObject
    {
        public int distance;
        public int status;

        public SensedObject(int distance, int status)
        {
            this.distance = distance;
            this.status = status;
        }
    }

    [Serializable]
    public class Params
    {
        public List<Sensor> sensors;

        public Params(List<Sensor> sensors)
        {
            this.sensors = sensors;
        }
    }

    [Serializable]
    public class Sensor
    {
        public int id;
        public List<SensedObject> objects;

        public Sensor(int id, List<SensedObject> objects)
        {
            this.id = id;
            this.objects = objects;
        }
    }
    
    [Serializable]
    public class SerialModel
    {
        public string jsonrpc;
        public string method;
        public Params @params;

        public SerialModel(string jsonrpc, string method, Params @params)
        {
            this.jsonrpc = jsonrpc;
            this.method = method;
            this.@params = @params;
        }
    }
    
    
}
