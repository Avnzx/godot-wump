using Godot;
using System;

class IcoSphere : Node {
    private ArrayMesh _array_mesh = new ArrayMesh();
    private SurfaceTool _surfacetool = new SurfaceTool();
    Godot.Collections.Array faces = new Godot.Collections.Array();


    public Vector3[] create_face(Vector3 lla0, Vector3 lla1, Vector3 lla2) {
        // cross language scripting is weird
        Vector3 corner1 = SphereGeom.lla_to_xyz(lla0);
        Vector3 corner2 = SphereGeom.lla_to_xyz(lla1);
        Vector3 corner3 = SphereGeom.lla_to_xyz(lla2);

        Godot.Collections.Array<Vector3> _corners = 
            new Godot.Collections.Array<Vector3>();
        Vector3[] corners = new Vector3[3];
        
        _corners.Add( corner1);
        _corners.Add( corner2);
        _corners.Add( corner3);

        Godot.Collections.Array _arrayofarray = new Godot.Collections.Array();

        _arrayofarray.Resize( (int) ArrayMesh.ArrayType.Max);
        _arrayofarray[ (int) ArrayMesh.ArrayType.Vertex ] = _corners;
        
        _array_mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles,_arrayofarray);

        _corners.CopyTo(corners,0);
        return corners;
    }
        

    public bool is_adjacent( 
        Godot.Collections.Array face1, Godot.Collections.Array face2) {
            int common = 0;

            // check for shared points
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++)
                {
                    if ( face1[i] == face2[j] ) {
                        common ++;
                    }
                }
            }
            return (common == 2);
    }



    public override void _Ready(){
        // Functions.Call();
       _surfacetool.Begin(Mesh.PrimitiveType.Triangles);

        Vector3[] lla = {
            new Vector3(0, -58.5f, 0),
            new Vector3(0, 58.5f, 0),
            new Vector3(180, 58.5f, 0),
            new Vector3(180, -58.5f, 0),
            new Vector3(90, -31.5f, 0),
            new Vector3(90, 31.5f, 0),
            new Vector3(-90, 31.5f, 0),
            new Vector3(-90, -31.5f, 0),
            new Vector3(-31.5f, 0, 0),
            new Vector3(31.5f, 0, 0),
            new Vector3(148.5f, 0, 0),
            new Vector3(-148.5f, 0, 0)
        };

        faces.Add(create_face(lla[1], lla[2], lla[6]));
        faces.Add(create_face(lla[1], lla[5], lla[2]));
        faces.Add(create_face(lla[5], lla[10], lla[2]));
        faces.Add(create_face(lla[2], lla[10], lla[11]));
        faces.Add(create_face(lla[2], lla[11], lla[6]));
        faces.Add(create_face(lla[7], lla[6], lla[11]));
        faces.Add(create_face(lla[8], lla[6], lla[7]));
        faces.Add(create_face(lla[8], lla[1], lla[6]));
        faces.Add(create_face(lla[9], lla[1], lla[8]));
        faces.Add(create_face(lla[9], lla[5], lla[1]));
        faces.Add(create_face(lla[9], lla[4], lla[5]));
        faces.Add(create_face(lla[4], lla[10], lla[5]));
        faces.Add(create_face(lla[10], lla[4], lla[3]));
        faces.Add(create_face(lla[10], lla[3], lla[11]));
        faces.Add(create_face(lla[11], lla[3], lla[7]));
        faces.Add(create_face(lla[0], lla[8], lla[7]));
        faces.Add(create_face(lla[0], lla[9], lla[8]));
        faces.Add(create_face(lla[4], lla[9], lla[0]));
        faces.Add(create_face(lla[4], lla[0], lla[3]));
        faces.Add(create_face(lla[3], lla[0], lla[7]));


        //GD.Print( is_adjacent(faces[0],faces[1]) );
        //GD.Print( is_adjacent(faces[2], faces[3]))
        //print(faces[0][0] == faces[1][0])

        //for face in faces:
        
       _surfacetool.GenerateNormals();
       _surfacetool.GenerateTangents();
        
        //var mat = preload("res://spheremat.tres")
        RandomNumberGenerator rng = new RandomNumberGenerator();

        for (int i = 0; i < _array_mesh.GetSurfaceCount(); i++) {
            SpatialMaterial mat = new SpatialMaterial();
            var color = new Color(
                rng.RandfRange(0.5f, 1.0f), 
                rng.RandfRange(0.2f, 1.0f), 
                rng.RandfRange(0.7f, 1.0f),1
            );

            mat.AlbedoColor = color;
            _array_mesh.SurfaceSetMaterial(i,mat);
        }

        SpatialMaterial _mat = new SpatialMaterial();
            var _color = new Color(1,1,1,1);

            _mat.AlbedoColor = _color;
            _array_mesh.SurfaceSetMaterial(0,_mat);

        
            
        
        
        GD.Print(faces[0]);
        // GD.Print( 
        //     SphereGeom.calc_surface_normal_newell_method(faces[0])
        //     ); 

        
        var mi = new MeshInstance();
        GD.Print(_array_mesh);
        mi.Mesh = _array_mesh;
        AddChild(mi);
        
        // creates child StaticBody, parenting a CollisionShape (MeshInstance  -> StaticBody -> CollisionShape)
        mi.CreateTrimeshCollision();
        
        
        var collider = new ConcavePolygonShape();
        var staticbody = new StaticBody();
        mi.AddChild(staticbody);
        //staticbody.add_child(collider)
        //mi.material_override = preload("res://spheremat.tres")

        GD.Print(_array_mesh.GetSurfaceCount());

        viz = new VectorVisualise(this);

        single_face = (Vector3[]) faces[0];
        _meshistc = mi;
        GD.Print( _meshistc.GlobalTransform.origin );
    }

    VectorVisualise? viz;
    MeshInstance? _meshistc; 

    Vector3[]? single_face;

    public override void _Process(float delta) {
        viz!.AddVisQueue(_meshistc!, 
            SphereGeom.calc_surface_normal_newell_method(single_face!));
    }
}
	
