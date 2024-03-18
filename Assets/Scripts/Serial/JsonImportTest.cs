using System.Collections.Generic;
using Serial.Models;
using UnityEngine;
using UnityEngine.Events;

namespace Serial
{
    public class JsonImportTest : MonoBehaviour
    {
        [TextArea(3,50)]
        [SerializeField] private string jsonString = ""; 

        [SerializeField]
        private UnityEvent<SerialModel> OnReadSerialData;

        private SerialModel model;
        
        // Start is called before the first frame update
        void Start()
        {
            SerialModel serialModel = new("2.0", "update", new Params(new List<Sensor>
            {
                new Sensor(1, new List<SensedObject> { new(109, 0), new(1518, 7) }),
                new Sensor(2, new List<SensedObject> { new(93, 0) }),
                new Sensor(3, new List<SensedObject> { new(114, 0), new(1563, 7) }),
                new Sensor(4, new List<SensedObject> { new(117, 7) })
            }));

            var rootJson = JsonUtility.ToJson(serialModel);
            
            model = JsonUtility.FromJson<SerialModel>(jsonString);
            
            OnReadSerialData.Invoke(model);
        }

        // Update is called once per frame
        // void Update()
        // {
        //     OnReadSerialData.Invoke(model);
        // }
    }
}
