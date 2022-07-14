using Godot;
using System;

public class worldgeom : Node
{

	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	


		// TODO: add a proper collider
		MeshInstance mi = new MeshInstance();
		mi.Mesh = CoordHelper.CreatePolygonMesh(vertices: 6, radius: 10f);
		this.AddChild(mi);


	}


}
