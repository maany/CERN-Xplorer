using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LocationManager : MonoBehaviour
{

    public GameObject map;
    public float lat;
    public float lon;
    float lastlat, lastlon;
    public GameObject latText;
    public GameObject lonText;
    // Use this for initialization
    void Start()
    {
        Input.location.Start(); // enable the mobile device GPS
        //map = GameObject.FindGameObjectWithTag("Map");
        if (Input.location.isEnabledByUser)
        { // if mobile device GPS is enabled
            float lat = Input.location.lastData.latitude; //get GPS Data
            float lon = Input.location.lastData.longitude;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //      
        //        if (Input.location.isEnabledByUser) {
        //            float lat = Input.location.lastData.latitude;
        //            float lon = Input.location.lastData.longitude;
        //            DebugConsole.Log ("Lon:" + lon.ToString () + " Lat:" + lat.ToString ());
        //            if (lastlat != lat || lastlon != lon) {
        //                map.GetComponent ().centerLocation.latitude = lat;
        //                map.GetComponent ().centerLocation.longitude = lon;
        //                latText.GetComponent ().text = "Lat" + lat.ToString ();
        //                lonText.GetComponent ().text = "Lon" + lon.ToString ();
        //                map.GetComponent ().Refresh ();
        //            }
        //            lastlat = lat;
        //            lastlon = lon;
        //        }
        //      

        //      
        if (lastlat != lat || lastlon != lon)
        {
            map.GetComponent<GoogleMap>().centerLocation.latitude = lat;
            map.GetComponent<GoogleMap>().centerLocation.longitude = lon;
            map.GetComponent<GoogleMap>().Refresh();
        }
        lastlat = lat;
        lastlon = lon;
        //      

    }
}