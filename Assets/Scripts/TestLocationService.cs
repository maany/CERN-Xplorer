using UnityEngine;
using System.Collections;
using System;

[ExecuteInEditMode]
public class TestLocationService : MonoBehaviour
{
    public bool isLocationEnabled;
    public float longitude;
    public float latitude;
    public float distance;
    const float EarthRadius = 6371;
    public GameObject map;
    private GoogleMap googleMap;
    int count = 0;
    IEnumerator Start()
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            isLocationEnabled = true;
        }
        googleMap = map.GetComponent<GoogleMap>();
        
        // Stop service if there is no need to query location updates continuously
       // Input.location.Stop();
    }
    float Haversine(ref float lastLatitude, ref float lastLongitude)
    {
        float newLatitude = Input.location.lastData.latitude;
        float newLongitutde = Input.location.lastData.longitude;
        float delLat = (newLatitude - lastLatitude) * Mathf.Deg2Rad;
        float delLon = (newLongitutde - lastLongitude) * Mathf.Deg2Rad;
        float a = Mathf.Pow(Mathf.Sin(delLat / 2), 2) + Mathf.Cos(newLatitude * Mathf.Deg2Rad) * Mathf.Cos(newLatitude * Mathf.Deg2Rad) * Mathf.Pow(Mathf.Sin(delLon / 2), 2);
        lastLatitude = newLatitude;
        lastLongitude = newLongitutde;
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return EarthRadius * c;
    }
    void Update()
    {
        if (isLocationEnabled)
        {
            //GeoCoordinate oldCoordinate = GeoCoordinate(latitude, longtitude);
            float delDist = Haversine(ref latitude, ref longitude) * 1000f;
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
            if (delDist > 0f)
            {
                distance += delDist;
            }
            Debug.Log("Location is " + latitude + ", " + longitude + ", " + distance);
            count++;
            if (count >= 120)
            {
                googleMap.centerLocation.latitude = latitude;
                googleMap.centerLocation.longitude = longitude;
                googleMap.Refresh();
                count = 0;
            }
        }
        Debug.Log("Updating");
    }
    void OnGUI()
    {
        if (isLocationEnabled)
        {
            GUIStyle mystyle = new GUIStyle();
            mystyle.fontSize = 50;
            GUI.Label(new Rect(Screen.width / 5, Screen.height / 10, Screen.width - 80, Screen.height - 40), "lat : " + latitude + " long : " + longitude + " dist : " + distance + " TIme : " +  DateTime.Now.ToShortTimeString(),mystyle);
           // GUI.Label(new Rect(200, 15, 75, 25), "latitude : " + longitude);
        }else
        {
            GUI.Label(new Rect(Screen.width/5, Screen.height/10, Screen.width - 80, Screen.height - 40), "Location is disabled. restart application!!");
            Debug.Log("Location is disabled");
        }
        
    }
}