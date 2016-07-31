using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Maps
{
    class Helper
    {
        public float distanceToCenter = 12.8f;
        public Vector3 XYZfromLatLon(float lon, float lat)
        {
            float latRad = Mathf.Deg2Rad * (lat);
            float lonRad = Mathf.Deg2Rad * (lon);

            float x = distanceToCenter * Mathf.Cos(latRad) * Mathf.Cos(lonRad);
            float y = distanceToCenter * Mathf.Cos(latRad) * Mathf.Sin(lonRad);
            float z = distanceToCenter * Mathf.Sin(latRad);

            return new Vector3(x, z, y);
        }
        public double DistanceBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            float R = 6371000; // m
            double sLat1 = Math.Sin(Mathf.Deg2Rad*lat1);
            double sLat2 = Math.Sin(Mathf.Deg2Rad*lat2);
            double cLat1 = Math.Cos(Mathf.Deg2Rad*lat1);
            double cLat2 = Math.Cos(Mathf.Deg2Rad*lat2);
            double cLon = Math.Cos(Mathf.Deg2Rad*lon1 - Mathf.Deg2Rad*lon2);

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = R * d;

            return dist;
        }
      public double DistanceXYZBetweenPlaces(Vector3 place1, Vector3 place2)
        {
            return Vector3.Distance(place1, place2);
        }

        public double BearingBetweenPlaces(double lon1, double lat1, double lon2, double lat2)
        {
            double y = Math.Sin(Mathf.Deg2Rad*lon2 - Mathf.Deg2Rad*lon1) * Math.Cos(Mathf.Deg2Rad*lat2);
            double x = Math.Cos(Mathf.Deg2Rad*lat1) * Math.Sin(Mathf.Deg2Rad*lat2) - Math.Sin(Mathf.Deg2Rad*lat1) * Math.Cos(Mathf.Deg2Rad*lat2) * Math.Cos(Mathf.Deg2Rad*lon2 - Mathf.Deg2Rad*lon1);
            double bearing = Math.Atan2(y, x);
            return bearing;
        }
        public double[] convertXZ(double lon1, double lat1, double lon2, double lat2,Vector3 position1, Vector3 position2)
        {
            // double ratio = DistanceBetweenPlaces(lon1,lat1,lon2,lat2)/DistanceXYZBetweenPlaces(position1,position2);
            double ratio = 0.00874684;//114.326917;
            double bearing = BearingBetweenPlaces(lon1, lat1, lon2, lat2);
            double distance = DistanceBetweenPlaces(lon1, lat1, lon2, lat2);
            double x = Math.Sin(-bearing) * distance * ratio;
            double z = -Math.Cos(bearing) * distance * ratio;
            Debug.Log("X" + x.ToString() + "Z" + z.ToString());
            double[] xz = { x, z };
            return xz;
        }
    }
}
