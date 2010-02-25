using System;
using System.Text;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Rhino.Mocks;
using Machine.Specifications;

namespace GenesisEngine.Specs.CameraSpecs
{
    [Subject(typeof(Camera))]
    public class when_view_parameters_are_set_by_yaw_pitch_roll : CameraContext
    {
        Because of = () =>
            _camera.SetViewParameters(new DoubleVector3(1, 2, 3), 4, 5, 6);

        It should_set_the_camera_location_correctly = () =>
            _camera.Location.ShouldEqual(new DoubleVector3(1, 2, 3));

        It should_set_the_camera_yaw_correctly = () =>
            _camera.Yaw.ShouldEqual(4f);

        It should_set_the_camera_pitch_correctly = () =>
            _camera.Pitch.ShouldEqual(5f);

        It should_set_the_camera_roll_correctly = () =>
        _camera.Roll.ShouldEqual(6f);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_view_parameters_are_set_by_look_at : CameraContext
    {
        Because of = () =>
            _camera.SetViewParameters(new DoubleVector3(0, 1, 1), DoubleVector3.Zero);

        It should_set_the_camera_location_correctly = () =>
            _camera.Location.ShouldEqual(new DoubleVector3(0, 1, 1));

        It should_set_the_camera_yaw_correctly = () =>
            _camera.Yaw.ShouldEqual(0f);

        It should_set_the_camera_pitch_correctly = () =>
            _camera.Pitch.ShouldEqual((float)(-Math.PI / 4));

        It should_set_the_camera_roll_correctly = () =>
        _camera.Roll.ShouldEqual(0f);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_view_parameters_are_set_by_look_at_toward_straight_up_singularity : CameraContext
    {
        Because of = () =>
            _camera.SetViewParameters(DoubleVector3.Zero, DoubleVector3.Up);

        It should_set_the_camera_yaw_correctly = () =>
            _camera.Yaw.ShouldEqual(0f);

        It should_set_the_camera_pitch_correctly = () =>
            _camera.Pitch.ShouldEqual(Camera.MaximumPitch);

        It should_set_the_camera_roll_correctly = () =>
        _camera.Roll.ShouldEqual(0f);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_view_parameters_are_set_by_look_at_toward_straight_down_singularity : CameraContext
    {
        Because of = () =>
            _camera.SetViewParameters(DoubleVector3.Zero, DoubleVector3.Down);

        It should_set_the_camera_yaw_correctly = () =>
            _camera.Yaw.ShouldEqual(0f);

        It should_set_the_camera_pitch_correctly = () =>
            _camera.Pitch.ShouldEqual(Camera.MinimumPitch);

        It should_set_the_camera_roll_correctly = () =>
        _camera.Roll.ShouldEqual(0f);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_projection_parameters_are_set : CameraContext
    {
        Because of = () =>
            _camera.SetProjectionParameters(1f, 1f, 1f, 1f, 2f);

        It should_do_something_awesome;
            // TODO
    }

    [Subject(typeof(Camera))]
    public class when_the_clipping_planes_are_set : CameraContext
    {
        Because of = () =>
            _camera.SetClippingPlanes(123, 456);

        It should_set_the_near_plane_of_the_view_frustum = () =>
            _camera.OriginBasedViewFrustum.Near.D.ShouldEqual(123f);

        It should_set_the_far_plane_of_the_view_frustum = () =>
            _camera.OriginBasedViewFrustum.Far.D.ShouldBeCloseTo(-456, 0.001f);
    }

    [Subject(typeof(Camera))]
    public class when_the_camera_yaw_is_set : CameraContext
    {
        Because of = () =>
            _camera.Yaw = 1;

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, 1, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_the_camera_pitch_is_set : CameraContext
    {
        Because of = () =>
            _camera.Pitch = 1;

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, 1, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_the_pitch_is_set_to_straight_up_or_more : CameraContext
    {
        Because of = () =>
            _camera.Pitch = (float)Math.PI / 2;

        It should_clamp_the_pitch_to_avoid_the_poles = () =>
            _camera.Pitch.ShouldEqual(Camera.MaximumPitch);
    }

    [Subject(typeof(Camera))]
    public class when_the_pitch_is_set_to_straight_down_or_more : CameraContext
    {
        Because of = () =>
            _camera.Pitch = -(float)Math.PI / 2;

        It should_clamp_the_pitch_to_avoid_the_poles = () =>
            _camera.Pitch.ShouldEqual(Camera.MinimumPitch);
    }

    [Subject(typeof(Camera))]
    public class when_the_camera_roll_is_set : CameraContext
    {
        Because of = () =>
            _camera.Roll = 1;

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, 1));
    }

    [Subject(typeof(Camera))]
    public class when_the_camera_location_is_set : CameraContext
    {
        Because of = () =>
            _camera.Location = new DoubleVector3(1, 2, 3);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(new DoubleVector3(1, 2, 3), _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_the_camera_is_reset_and_default_location_and_look_at_are_the_same : CameraContext
    {
        Establish context = () =>
        {
            _settings.CameraStartingLocation = DoubleVector3.Up;
            _settings.CameraStartingLookAt = DoubleVector3.Up;
        };

        Because of = () =>
            _camera.Reset();

        It should_set_the_camera_location_to_the_default_location = () =>
            _camera.Location.ShouldEqual(_settings.CameraStartingLocation);

        It should_zero_the_camera_roll = () =>
            _camera.Roll.ShouldEqual(0);

        It should_zero_the_camera_yaw = () =>
            _camera.Yaw.ShouldEqual(0);

        It should_zero_the_camera_pitch = () =>
            _camera.Pitch.ShouldEqual(0);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(DoubleVector3.Up, 0, 0, 0));
    }

    [Subject(typeof(Camera))]
    public class when_the_camera_is_reset_and_default_location_and_look_at_are_different : CameraContext
    {
        Establish context = () =>
        {
            _settings.CameraStartingLocation = DoubleVector3.Right;
            _settings.CameraStartingLookAt = DoubleVector3.Up;
        };

        Because of = () =>
            _camera.Reset();

        It should_zero_the_camera_roll = () =>
            _camera.Roll.ShouldEqual(0);

        It should_set_the_camera_yaw_correctly = () =>
            _camera.Yaw.ShouldEqual((float)Math.PI / 2);

        It should_set_the_camera_pitch_correctly = () =>
            _camera.Pitch.ShouldEqual((float)Math.PI / 4);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(DoubleVector3.Right, (float)Math.PI / 2, (float)Math.PI / 4, 0));
    }

    [Subject(typeof(Camera))]
    public class when_the_yaw_is_changed : CameraContext
    {
        Establish context = () =>
            _camera.Yaw = 1;

        Because of = () =>
            _camera.ChangeYaw(-0.5f);

        It should_change_the_yaw_relative_to_the_starting_yaw = () =>
            _camera.Yaw.ShouldEqual(0.5f);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(DoubleVector3.Zero, 0.5f, 0, 0));
    }

    [Subject(typeof(Camera))]
    public class when_the_pitch_is_changed : CameraContext
    {
        Establish context = () =>
            _camera.Pitch = 1;

        Because of = () =>
            _camera.ChangePitch(-0.5f);

        It should_change_the_pitch_relative_to_the_starting_pitch = () =>
            _camera.Pitch.ShouldEqual(0.5f);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(DoubleVector3.Zero, 0, 0.5f, 0));
    }

    [Subject(typeof(Camera))]
    public class when_the_pitch_is_changed_to_point_straight_up_or_more : CameraContext
    {
        Because of = () =>
            _camera.ChangePitch((float)Math.PI / 2);

        It should_clamp_the_pitch_to_avoid_the_poles = () =>
            _camera.Pitch.ShouldEqual(Camera.MaximumPitch);
    }

    [Subject(typeof(Camera))]
    public class when_the_pitch_is_changed_to_point_straight_down_or_more : CameraContext
    {
        Because of = () =>
            _camera.ChangePitch(-(float)Math.PI / 2);

        It should_clamp_the_pitch_to_avoid_the_poles = () =>
            _camera.Pitch.ShouldEqual(Camera.MinimumPitch);
    }

    [Subject(typeof(Camera))]
    public class when_moved_forward_horizontally : CameraContext
    {
        Establish context = () =>
        {
            _camera.ChangeYaw((float)Math.PI / 4);
            _camera.ChangePitch(1);
        };

        Because of = () =>
            _camera.MoveForwardHorizontally(1);

        It should_move_horizontally_in_the_direction_the_camera_is_facing = () =>
            _camera.Location.ShouldBeCloseTo(new DoubleVector3(-Math.Sqrt(.5f), 0, -Math.Sqrt(.5f)));

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_moved_backward_horizontally : CameraContext
    {
        Establish context = () =>
        {
            _camera.ChangeYaw((float)Math.PI / 4);
            _camera.ChangePitch(1);
        };

        Because of = () =>
            _camera.MoveBackwardHorizontally(1);

        It should_move_horizontally_opposite_the_direction_the_camera_is_facing = () =>
            _camera.Location.ShouldBeCloseTo(new DoubleVector3(Math.Sqrt(.5f), 0, Math.Sqrt(.5f)));

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_moved_left : CameraContext
    {
        Establish context = () =>
        {
            _camera.ChangeYaw((float)Math.PI / 4);
            _camera.ChangePitch(1);
        };

        Because of = () =>
            _camera.MoveLeft(1);

        It should_move_left = () =>
            _camera.Location.ShouldBeCloseTo(new DoubleVector3(-Math.Sqrt(.5f), 0, Math.Sqrt(.5f)));

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_moved_right : CameraContext
    {
        Establish context = () =>
        {
            _camera.ChangeYaw((float)Math.PI / 4);
            _camera.ChangePitch(1);
        };

        Because of = () =>
            _camera.MoveRight(1);

        It should_move_right = () =>
            _camera.Location.ShouldBeCloseTo(new DoubleVector3(Math.Sqrt(.5f), 0, -Math.Sqrt(.5f)));

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_moved_up : CameraContext
    {
        Establish context = () =>
        {
            _camera.ChangeYaw((float)Math.PI / 4);
            _camera.ChangePitch(1);
        };

        Because of = () =>
            _camera.MoveUp(1);

        It should_move_straight_up = () =>
            _camera.Location.ShouldBeCloseTo(DoubleVector3.Up);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    [Subject(typeof(Camera))]
    public class when_moved_down : CameraContext
    {
        Establish context = () =>
        {
            _camera.ChangeYaw((float)Math.PI / 4);
            _camera.ChangePitch(1);
        };

        Because of = () =>
            _camera.MoveDown(1);

        It should_move_straight_down = () =>
            _camera.Location.ShouldBeCloseTo(DoubleVector3.Down);

        It should_set_the_camera_view_transformation_correctly = () =>
            _camera.OriginBasedViewTransformation.ShouldEqual(GenerateOriginBasedViewMatrix(_camera.Location, _camera.Yaw, _camera.Pitch, _camera.Roll));
    }

    // TODO: other methods need coverage
     
    public class CameraContext
    {
        public static ISettings _settings;
        public static Camera _camera;

        Establish context = () =>
        {
            _settings = MockRepository.GenerateStub<ISettings>();
            _camera = new Camera(_settings);
        };

        public static Matrix GenerateOriginBasedViewMatrix(DoubleVector3 location, float yaw, float pitch, float roll)
        {
            Matrix yawMatrix = Matrix.CreateRotationY(yaw);
            Matrix pitchMatrix = Matrix.CreateRotationX(pitch);
            Matrix rollMatrix = Matrix.CreateRotationZ(roll);

            Vector3 rotation = Vector3.Transform(Vector3.Forward, pitchMatrix);
            rotation = Vector3.Transform(rotation, yawMatrix);
            Vector3 lookAt = rotation;
            Vector3 up = Vector3.Transform(Vector3.UnitY, rollMatrix);

            return Matrix.CreateLookAt(Vector3.Zero, lookAt, up);
        }
    }
}
