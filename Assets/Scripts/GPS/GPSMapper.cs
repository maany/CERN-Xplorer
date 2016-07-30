using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GPSMapper : MonoBehaviour
{

    public GameObject map;
    public float lat;
    public float lon;
    float lastlat, lastlon;
    public GameObject latText;
    public GameObject lonText;
    public bool isLocationEnabled;
    // Use this for initialization
    void Start()
    {
        
        Input.location.Start(); // enable the mobile device GPS
       // map = GameObject.FindGameObjectWithTag("MainCamera");
        if (Input.location.isEnabledByUser)
        { // if mobile device GPS is enabled
            float lat = Input.location.lastData.latitude; //get GPS Data
            float lon = Input.location.lastData.longitude;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.location.isEnabledByUser)
        //{
        //    isLocationEnabled = true;
        //    float lat = Input.location.lastData.latitude;
        //    float lon = Input.location.lastData.longitude;
        //    Debug.Log("Lon:" + lon.ToString() + " Lat:" + lat.ToString());
        //    if (lastlat != lat || lastlon != lon)
        //    {
        //        map.GetComponent<GoogleMap>().centerLocation.latitude = lat;
        //        map.GetComponent<GoogleMap>().centerLocation.longitude = lon;
        //        //latText.GetComponent().text = "Lat" + lat.ToString();
        //        //lonText.GetComponent().text = "Lon" + lon.ToString();
        //        map.GetComponent<GoogleMap>().Refresh();
        //    }
        //    lastlat = lat;
        //    lastlon = lon;
        //}


        float lat = Input.location.lastData.latitude;
        float lon = Input.location.lastData.longitude;
        if (lastlat != lat || lastlon != lon)
        {
            map.GetComponent<GoogleMap>().centerLocation.latitude = lat;
            map.GetComponent<GoogleMap>().centerLocation.longitude = lon;
            map.GetComponent<GoogleMap>().Refresh();
        }
        lastlat = lat;
        lastlon = lon;
    }

}