using Godot;
using GoDotLog;
using GoDotTest;
using Shouldly;

public class CoordHelperTest : TestClass {
    private readonly ILog _log = new GDLog(nameof(CoordHelperTest));
    public CoordHelperTest(Node testscene) : base(testscene) {}

    [Test]
    public void test_polygon_point_rad() {
        float[] result = CoordHelper.polygon_point_rad(6);
        float[] expected = {
            0, 
            1.0471975511965976f,
            2.0943951023931953f,
            3.141592653589793f,
            4.1887902047863905f,
            5.235987755982988f
        };
        
        for ( int i=0; i<6; i++ ){
            result[i].ShouldBe(expected[i]);
        }

    }
}