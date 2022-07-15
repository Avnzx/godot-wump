using Godot;
using System;

class SphereGeom : Node {


	public static Vector3 calc_surface_normal 
		(Vector3 vert1, Vector3 vert2, Vector3 vert3) {
		Vector3 U = (vert2 - vert1);
		Vector3 V = (vert3 - vert1);

		var x = (U.y * V.z) - (U.z * V.y);
		var y = (U.z * V.x) - (U.x * V.z);
		var z = (U.x * V.y) - (U.y * V.x);

		return new Vector3(x, y, z).Normalized();
	}

	// Newell's Method of calculating normals
	public static Vector3 calc_surface_normal_newell_method 
		( Vector3[ ] vert_arr ) {

		var normal = Vector3.Zero;

		var curr_vert = new Vector3();
		var next_vert = new Vector3();
		
		for (int i = 0; i < vert_arr.Length; i++)
		{
			curr_vert = vert_arr[i];
			next_vert = vert_arr[(i + 1) % vert_arr.Length];

			normal.x = normal.x + ((curr_vert.y - next_vert.y) * (curr_vert.z + next_vert.z));
			normal.y = normal.y + ((curr_vert.z - next_vert.z) * (curr_vert.x + next_vert.x));
			normal.z = normal.z + ((curr_vert.x - next_vert.x) * (curr_vert.y + next_vert.y));
		}
		
		return normal.Normalized();
	}


	public static Vector3 lla_to_xyz (Vector3 lla, float radius = 50f) {
		float lon = lla.x;
		float lat = lla.y;
		float alt = lla.z;

		float cosLat = MathF.Cos(Mathf.Deg2Rad(lat));
		float sinLat = MathF.Sin(Mathf.Deg2Rad(lat));
		float cosLon = MathF.Cos(Mathf.Deg2Rad(lon));
		float sinLon = MathF.Sin(Mathf.Deg2Rad(lon));
		float rad = radius + alt;
		float x = rad * cosLat * cosLon;
		float y = rad * sinLat;
		float z = rad * cosLat * sinLon;

		return new Vector3(x, y, z);
	}

	public static Vector3 mid_point 
		(Vector3 lla1, Vector3 lla2) {
		
		var lon1 = lla1.x;
		var lat1 = lla1.y;
		var lon2 = lla2.x;
		var lat2 = lla2.y;

		var dLon = Mathf.Deg2Rad(lon2 - lon1);;

		// convert to radians
		lat1 = Mathf.Deg2Rad(lat1);
		lat2 = Mathf.Deg2Rad(lat2);
		lon1 = Mathf.Deg2Rad(lon1);

		var Bx = Mathf.Cos(lat2) * Mathf.Cos(dLon);
		var By = Mathf.Cos(lat2) * Mathf.Cos(dLon);
		var lat3 = Mathf.Atan2(Mathf.Sin(lat1) + Mathf.Sin(lat2), Mathf.Sqrt((Mathf.Cos(lat1) + Bx) * (Mathf.Cos(lat1) + Bx) + By * By));
		var lon3 = lon1 + Mathf.Atan2(By, Mathf.Cos(lat1) + Bx);

		// return lla vector
		return new Vector3(Mathf.Rad2Deg(lon3), Mathf.Rad2Deg(lat3), (lla1.z + lla2.z) / 2);

		
	}

}