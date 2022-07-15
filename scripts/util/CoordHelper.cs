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
        float y = (sinRad * radius); 
        float x = (cosRad * radius); 
        return new Vector2(x,y);
    }

    public static Vector3 vec2_to_vec3Y(Vector2 vec2, float y = 0f){
        return new Vector3(vec2.x, y, vec2.y);
    }

    public static float[] polygon_point_rad(int vertices, float offset = 0f){
        float[] arr = new float[vertices];
            for ( int i=0; i<vertices; i++ ){
                float val = ((Mathf.Tau / vertices) * i) + offset;
                arr[i] = val;
            }
        return arr;
    }

    public static ArrayMesh CreatePolygonMesh(int vertices = 6, float thickness = 2f, float radius = 10f, float offset = 0f) {
		float[]? _radian_points = CoordHelper.polygon_point_rad(vertices, offset);
		ArrayMesh _arraymesh = new ArrayMesh();
		Godot.Collections.Array _arrayofarray = 
			new Godot.Collections.Array();
		
		_arrayofarray.Resize( (int) ArrayMesh.ArrayType.Max );

		Godot.Collections.Array<Vector3> _bottomface =
			new Godot.Collections.Array<Vector3>();
		Godot.Collections.Array<Vector3> _topface = 
			new	Godot.Collections.Array<Vector3>();

        _bottomface.Add(new Vector3(0,0,0));
        _topface.Add(new Vector3(0,thickness,0));

		// convert degrees to 3d vectors
		for( int i = 0; i<vertices ; i++ ){
			Vector2 point = CoordHelper.rad_to_xy(_radian_points[i], radius);
			_bottomface.Add(CoordHelper.vec2_to_vec3Y(point));
			_topface.Add(CoordHelper.vec2_to_vec3Y(point,thickness));	
		}

		_bottomface.Add(_bottomface[1]);
		_topface.Add(_topface[1]);

		// Save some memory
		_radian_points = null;

		// Add top and bottom surfaces as faces
		_arrayofarray[(int)ArrayMesh.ArrayType.Vertex] = _bottomface;
		_arraymesh.AddSurfaceFromArrays(Mesh.PrimitiveType.TriangleFan, _arrayofarray);

		_arrayofarray[(int)ArrayMesh.ArrayType.Vertex] = _topface;
		_arraymesh.AddSurfaceFromArrays(Mesh.PrimitiveType.TriangleFan, _arrayofarray);

		

		Godot.Collections.Array<Vector3> _sideface;

		// loop over the faces and add a surface foreach
        // ORDER MATTERS!
		for (int i = 1; i < (vertices+1) ; i++) {
			_sideface =	new	Godot.Collections.Array<Vector3>();
			_sideface.Add(_topface[i+1]);
			_sideface.Add(_topface[i]);
			_sideface.Add(_bottomface[i+1]);
			_sideface.Add(_bottomface[i]);

			_arrayofarray[(int)ArrayMesh.ArrayType.Vertex] = _sideface;
			_arraymesh.AddSurfaceFromArrays(Mesh.PrimitiveType.TriangleStrip, _arrayofarray);
		}

		return _arraymesh;
		
	}

    public static float PolygonFlatToFlatDistance(int vertices, float radius) =>
        (2f * radius * Mathf.Cos(Mathf.Pi / (float) vertices));


}
