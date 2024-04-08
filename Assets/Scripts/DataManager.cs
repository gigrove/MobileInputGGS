using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class DataManager : MonoBehaviour
{
    private string _datapath;
    private string _xmlAccelerationData;
    private float _timer = 0f;
    private bool doDataThingy = true;

   
    public List<DataSnapshot> data = new List<DataSnapshot>();

    private void Awake()
    {
        _datapath = Application.persistentDataPath + "/Acceleration_Data/";
        _xmlAccelerationData = _datapath + "AccelerationData.xml";
    }


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_datapath);
        NewDirectory();
    }

    public void NewDirectory()
    {
        if (File.Exists(_datapath))
        {
            Debug.Log("Directory exists...");
            return;
        }
        Directory.CreateDirectory(_datapath);
        Debug.Log("New Directory created!");
    }

    private void FixedUpdate()
    {
        _timer += 1;

        if (doDataThingy && _timer % 5 == 0)
        {
            Debug.Log($"{_timer} updates: saving data points...");
            AddAccelerationData();
        }
        if (_timer > 500 && doDataThingy)
        {
            doDataThingy = false;
            StopAndSerialize();
        }
    }



    private void AddAccelerationData()
    {
        data.Add(new DataSnapshot());
    }

    
    public void StopAndSerialize()
    {
        Debug.Log($"Stop initiated at {_timer}, attempting to serialize...");

      
        var xmlSerializer = new XmlSerializer(typeof(List<DataSnapshot>));

        using(FileStream stream = File.Create(_xmlAccelerationData))
        {
            
            xmlSerializer.Serialize(stream, data);
        }
        Debug.Log("Serialization complete.");
    }

}
