using Godot;
using System;

public class CoordHelper : Node
{

    public static Vector3 lla_to_xyz(float[] lla, float rad = 50f){
        float lon = lla[0];
	    float lat = lla[1];
        float alt = lla[2];
        float cosLat = Mathf.Cos(Mathf.Deg2Rad(lat)); 
        float sinLat = Mathf.Sin(Mathf.Deg2Rad(lat));
        float cosLon = Mathf.Cos(Mathf.Deg2Rad(lon));
        float sinLon = Mathf.Sin(Mathf.Deg2Rad(lon));
        float radius = rad + alt;
        float x = radius * cosLat * cosLon;
        float y = radius * sinLat;
        float z = radius * cosLat * sinLon;
        return new Vector3(x, y, z);
    }


    public static Vector2 deg_to_xy(float deg, float radius = 50f){
        float sinRad = Mathf.Sin(Mathf.Deg2Rad(deg));
        float cosRad = Mathf.Cos(Mathf.Deg2Rad(deg));
        float y = sinRad * radius; 
        float x = cosRad * radius; 
        return new Vector2(x,y);
    }

    // **
    public static Vector2 rad_to_xy(float rad, float radius = 50f){
        float sinRad = Mathf.Sin(rad);
        float cosRad = Mathf.Cos(rad);
        float y = sinRad * radius; 
        float x = cosRad * radius; 
        return new Vector2(x,y);
    }

    public static Vector3 vec2_to_vec3(Vector2 vec2, float z = 0f){
        return new Vector3(vec2.x, vec2.y, z);
    }

    public static float[] polygon_point_rad(int vertices, float offset = 0){
        float[] arr = new float[vertices];
            for ( int i=0; i<vertices; i++ ){
                float val = ((Mathf.Tau / vertices) * i) + offset;
                arr[i] = val;
            }
        return arr;
    }

}
