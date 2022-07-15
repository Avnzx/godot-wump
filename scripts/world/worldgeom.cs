using Godot;
using System;

public class worldgeom : Node
{
	private SceneManager? m_sceneManager;
	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		m_sceneManager = GetNode<SceneManager>("/root/SceneManager");

		_halfpolygon = CoordHelper.PolygonFlatToFlatDistance(6,15f); 

		// TODO: add a proper collider

		var color = new Color(
				(float) GD.RandRange(0.1, 1.0), 
				(float) GD.RandRange(0.1, 1.0), 
				(float) GD.RandRange(0.1, 1.0),1);

		var color2 = new Color(
				(float) GD.RandRange(0.3, 1.0), 
				(float) GD.RandRange(0.4, 1.0), 
				(float) GD.RandRange(0.5, 1.0),1);

		SpatialMaterial mat = new SpatialMaterial();
		SpatialMaterial mat2 = new SpatialMaterial();


		MeshInstance mi = new MeshInstance();
		ArrayMesh arrmesh1 =  CoordHelper.CreatePolygonMesh(vertices: 6, radius: 15f );
		mi.Mesh = arrmesh1;

		mat.AlbedoColor = color;
		mat2.AlbedoColor = color2;

		for (int i = 2; i < arrmesh1.GetSurfaceCount(); i++)
		{
			arrmesh1.SurfaceSetMaterial(i,mat);
		}
		arrmesh1.SurfaceSetMaterial(1,mat2);
		this.AddChild(mi);

		ArrayMesh arrmesh2 =  arrmesh1;
		MeshInstance mi2 = new MeshInstance();
		mi2.Mesh = arrmesh2;

		mi2.Translate(
			new Vector3(1.5f*15f,0,_halfpolygon/2));
		AddChild(mi2);

		MeshInstance mi3 = new MeshInstance();
		mi3.Mesh = mi2.Mesh;
		mi3.Translate(
			new Vector3(0,0,-_halfpolygon));
		AddChild(mi3);

		MeshInstance mi4 = new MeshInstance();
		mi4.Mesh = mi2.Mesh;
		mi4.Translate(
			new Vector3(-1.5f*15f,0,_halfpolygon/2));
		AddChild(mi4);



	}

    public override void _Input(InputEvent @event) {

		if (Input.IsActionPressed("toggle_map")) {
			m_sceneManager!.DeferredGotoScene("res://scenes/icosphere/Main.tscn");
		}


    }

	private float _halfpolygon;


}
