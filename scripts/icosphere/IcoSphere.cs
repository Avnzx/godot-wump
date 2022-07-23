using Godot;
using System;

public class IcoSphere : Node {
    public ArrayMesh _array_mesh = new ArrayMesh();
    private SurfaceTool _surfacetool = new SurfaceTool();
    Godot.Collections.Array faces = new Godot.Collections.Array();
    public Godot.Collections.Array facecolliders = new Godot.Collections.Array();
    IcoSphereGeom _geometry = new IcoSphereGeom();

    public IcoSphere(){
        // GD.Print(_geometry.faces);
    }


    public override void _Ready(){
       _surfacetool.Begin(Mesh.PrimitiveType.Triangles);
       faces = _geometry.faces;

        CreateShape();

    }



    public void create_face(Vector3 xyz0, Vector3 xyz1, Vector3 xyz2) {
        Godot.Collections.Array<Vector3> _corners = 
            new Godot.Collections.Array<Vector3>();
        
        _corners.Add(xyz0);
        _corners.Add(xyz1);
        _corners.Add(xyz2);

        Godot.Collections.Array _arrayofarray = new Godot.Collections.Array();

        _arrayofarray.Resize( (int) ArrayMesh.ArrayType.Max);
        _arrayofarray[ (int) ArrayMesh.ArrayType.Vertex ] = _corners;
        
        _array_mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles,_arrayofarray);
    }

     public void create_face(Vector3[] points) {
        Godot.Collections.Array<Vector3> _corners = 
            new Godot.Collections.Array<Vector3>();
        
        _corners.Add(points[0]);
        _corners.Add(points[1]);
        _corners.Add(points[2]);

        Godot.Collections.Array _arrayofarray = new Godot.Collections.Array();

        _arrayofarray.Resize( (int) ArrayMesh.ArrayType.Max);
        _arrayofarray[ (int) ArrayMesh.ArrayType.Vertex ] = _corners;
        
        _array_mesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Triangles,_arrayofarray);
    }


    public void CreateShape() {
        _surfacetool.GenerateNormals();
        _surfacetool.GenerateTangents();

        for (int i = 0; i < faces.Count; i++) {
            // disable the warning, it is inaccurate
            #pragma warning disable CS8604
            create_face(faces[i] as Godot.Vector3[]);
            #pragma warning restore CS8604
        }

            
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
            var _color = new Color(0.4f,0.1f,0.7f,0.4f);

            _mat.AlbedoColor = _color;
            _array_mesh.SurfaceSetMaterial(0,_mat);

        for (int i = 0; i < _array_mesh.GetSurfaceCount(); i++) {
            _array_mesh.SurfaceSetMaterial(i,_mat);
        }
        
        var mi = new MeshInstance();
        GD.Print(_array_mesh);
        mi.Mesh = _array_mesh;
        AddChild(mi);
        // draw some nice blue lines on the edges of the colliders
        GetTree().DebugCollisionsHint = true;
        

        // go through each face and create a new ConcaveCollision
        // Shape for them, allowing the faces to be selected
        for (int i = 0; i < _array_mesh.GetSurfaceCount(); i++) {
            Vector3[] surface = (Vector3[]) _array_mesh.SurfaceGetArrays(i)[0];

            StaticBody surfacebody = new StaticBody();
            surfacebody.Name = "facestaticbody";
            this.AddChild(surfacebody,true);

            facecolliders.Add(surfacebody);

            Vector3[] shapepoints = new Vector3[3];
            surface.CopyTo(shapepoints,0);

            ConcavePolygonShape collidershape = new ConcavePolygonShape();
            collidershape.Data = shapepoints;
            
            uint shapeowner = surfacebody.CreateShapeOwner(surfacebody);
            surfacebody.ShapeOwnerAddShape(shapeowner, collidershape);

        }
        
        
        var collider = new ConcavePolygonShape();
        var staticbody = new StaticBody();
        // mi.AddChild(staticbody);

        // VectorVisualise viz = new VectorVisualise(mi);

        // Vector3[] single_face = (Vector3[]) faces[0];
        // MeshInstance _meshistc = mi;

        // for (int i = 0; i < _array_mesh.GetSurfaceCount(); i++) {
        //     SpatialMaterial _materl =  (SpatialMaterial) _array_mesh.SurfaceGetMaterial(i);

        //     viz!.AddVisQueue(_meshistc!, 
        //         SphereGeom.calc_surface_normal_newell_method((Vector3[]) faces[i])*-Vector3.One,
        //         _materl.AlbedoColor);
        
        // }

    // viz!.AddVisQueue(_meshistc!, Vector3.Up);
    }




       


}