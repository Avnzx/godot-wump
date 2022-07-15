using Godot;
using System;

public sealed class RoomFactory : Spatial {

    // Singleton, ensure that it is only instantiated ONCE
    private static readonly Lazy<RoomFactory> lazy =
        new Lazy<RoomFactory>(() => new RoomFactory());

    public static RoomFactory Instance { get { return lazy.Value; } }


    private RoomManager _manager = new RoomManager();
	private RoomFactory() {
        this.Name = "RoomList";
    }

    public void Initialise(NodePath path) {
        GetNode(path).AddChild(_manager,true);
        _manager.Roomlist = this.GetPath();
    }

    public MeshInstance NewRoom() {

		float _halfpolygon = CoordHelper.PolygonFlatToFlatDistance(6,15f); 

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
		this.AddChild(mi,true);
        mi.CreateConvexCollision();
        return mi;
    }

}