using Godot;
using System;

public class playercontroller : KinematicBody {
    public override void _Ready()
    {
        m_camera = GetNode<Camera>("../Camera");
        m_body = this;
    }

    // public override void _UnhandledInput(InputEvent @event) {

    // };

    public override void _PhysicsProcess(float delta) {
        Vector2 m_input = Input.GetVector(
            "key_left",
            "key_right", 
            "key_backward", 
            "key_forward");


        

        // vector * scalar, move based on camera view
        // guaranteed to be normalized prev to * m_speed
        if (m_camera != null){
            m_horizontalVelocity = m_camera.Transform.basis.y * m_input.y;
            m_horizontalVelocity += m_camera.Transform.basis.x * m_input.x;
            m_horizontalVelocity = m_horizontalVelocity * m_speed;
        }
        
        if (m_input != Vector2.Zero) {
            m_velocity.x = 
                Mathf.Lerp(m_velocity.x, m_horizontalVelocity.x, m_accel);
            m_velocity.z =  
                Mathf.Lerp(m_velocity.z, m_horizontalVelocity.z, m_accel); 
        } else {
            m_velocity.x =
                Mathf.Lerp(m_velocity.x, m_horizontalVelocity.x, m_deaccel);
            m_velocity.z =
                Mathf.Lerp(m_velocity.z, m_horizontalVelocity.z, m_deaccel);
        }

        // rotate to angle of movement
        if( m_input != Vector2.Zero ) {
            m_looktarget = m_horizontalVelocity.Normalized();
            m_looktarget.y = 0;
            float lookdir = Mathf.Atan2( -m_looktarget.x, -m_looktarget.z );
            this.Rotation = new Vector3(0,lookdir,0); 

            // Apply a rotation based on speed
            this.Rotation -= new Vector3( 
                (((Mathf.Abs(m_velocity.x) + Mathf.Abs(m_velocity.z)) / m_speed) * m_maxVelAngle),0,0);
        } else {
            // return to standing upright as we are not moving
            this.Rotation = this.Rotation * new Vector3(0,1,1); 
        }


            
        

        // Apply gravity (accel)
        m_velocity.y += m_gravity * delta;
        // human terminal velocity & speed of sound
        m_velocity.y = Mathf.Clamp(m_velocity.y, -56, 350);

        // Actually apply gravity and movement
        m_velocity = MoveAndSlide(m_velocity, Vector3.Up);
    }


    private Vector3 m_velocity = Vector3.Zero;
    private Vector3 m_horizontalVelocity = Vector3.Zero;
    private Vector3 m_looktarget = Vector3.Forward;
    KinematicBody? m_body;
    Camera? m_camera;
    private float m_gravity = 
        (float) ProjectSettings.GetSetting("physics/3d/default_gravity");


    [Export]
    private float m_maxVelAngle = (float) (1f/3f);
    private const float m_speed = 7;
    [Export]
    private float m_deaccel = 0.2f;
    [Export]
    private float m_accel = 0.2f;

    

}
