using Godot;
using System;

public class worldgeom : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

		m_st.Begin(Mesh.PrimitiveType.Points);

		faces = new Vector3[] {
			CoordHelper.vec2_to_vec3(CoordHelper.deg_to_xy(90f)),
			CoordHelper.vec2_to_vec3(CoordHelper.deg_to_xy(180f))
		};


		for( int i = 0; i<6; i++ ){
			GD.Print(CoordHelper.polygon_point_rad(6)[i] / Mathf.Pi);
		}

		GD.Print(faces[0]);

		CSGPolygon poly = new CSGPolygon();
		this.AddChild( (CSGPolygon) poly);


	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
	private SurfaceTool m_st = new SurfaceTool();
	private Vector3[]? faces;
	private Vector3[]? poolvec;
}
