using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GiftToCatSeventeen
{
    public class Camera
    {
        public Vector3 Position { set; get; }

        private Matrix rotationMatrix = Matrix.Identity;

        public bool EnableMouse { set; get; }
        private Vector2 lastMousePosition = Vector2.Zero;
        private bool lastMousePressed = false;
        
        public bool EnableKeyboard { set; get; }

        public bool EnableTarget { set; get; }
        private Vector3 target = Vector3.Zero;
        public Vector3 Target
        {
            set { if (EnableTarget) { target = value; } }
            get { return EnableTarget ? target : Vector3.Zero; }
        }
        public Matrix ViewMatrix
        {
            get
            {
                if (EnableTarget)
                {
                    Matrix view = Matrix.CreateLookAt(Position, target, Vector3.Up);
                    return view;
                }
                else
                {
                    Matrix view = Matrix.CreateTranslation(-Position);
                    view *= Matrix.Invert(rotationMatrix);
                    return view;
                }
            }
        }

        private Matrix projectionMatrix;
        public Matrix ProjectionMatrix { get { return projectionMatrix; } }

        public float CameraRotateSpeed { set; get; }
        public float CameraMoveSpeed { set; get; }

        public Camera(float width, float height)
        {
            CameraRotateSpeed = 0.4f;
            CameraMoveSpeed = 15.0f;
            EnableMouse = true;
            EnableKeyboard = true;

            float aspectRatio = width / height;
            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspectRatio, 0.1f, 10000);
        }

        public void RotateX(float step)
        {
            Matrix rot = Matrix.CreateFromAxisAngle(rotationMatrix.Right, step);
            rotationMatrix *= rot;
        }

        public void RotateY(float step)
        {
            Matrix rot = Matrix.CreateFromAxisAngle(rotationMatrix.Up, step);
            rotationMatrix *= rot;
        }

        public void RotateWorldY(float step)
        {
            Matrix rot = Matrix.CreateFromAxisAngle(Vector3.Up, step);
            rotationMatrix *= rot;
        }

        public void RotateZ(float step)
        {
            Matrix rot = Matrix.CreateFromAxisAngle(rotationMatrix.Forward, step);
            rotationMatrix *= rot;
        }

        public void MoveX(float step)
        {
            Position += rotationMatrix.Right * step;
        }

        public void MoveY(float step)
        {
            Position += rotationMatrix.Up * step;
        }

        public void MoveZ(float step)
        {
            Position += rotationMatrix.Forward * step;
        }

        public void HandleCamera(GameTime gameTime)
        {
            float crs = CameraRotateSpeed / gameTime.ElapsedGameTime.Milliseconds;
            float cms = CameraMoveSpeed / gameTime.ElapsedGameTime.Milliseconds;
            if (EnableKeyboard)
                HandleKeyboard(crs, cms);
            if(EnableMouse)
                HandleMouse(crs, cms);
        }

        private void HandleMouse(float crs, float cms)
        {
            MouseState ms = Mouse.GetState();
            if (lastMousePressed)
                if (ms.LeftButton == ButtonState.Pressed)
                {
                    float dx = lastMousePosition.X - ms.X;
                    float dy = lastMousePosition.Y - ms.Y;
                    RotateWorldY(dx * crs);
                    RotateX(dy * crs);
                }
            lastMousePressed = ms.LeftButton == ButtonState.Pressed;
            lastMousePosition.X = ms.X;
            lastMousePosition.Y = ms.Y;
        }

        private void HandleKeyboard(float crs, float cms)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up))
                RotateX(crs);
            if (ks.IsKeyDown(Keys.Down))
                RotateX(-crs);
            if (ks.IsKeyDown(Keys.Left))
                RotateWorldY(crs);
            if (ks.IsKeyDown(Keys.Right))
                RotateWorldY(-crs);
            if (ks.IsKeyDown(Keys.Q))
                RotateZ(-crs);
            if (ks.IsKeyDown(Keys.E))
                RotateZ(crs);

            if (ks.IsKeyDown(Keys.W))
                MoveZ(cms);
            if (ks.IsKeyDown(Keys.S))
                MoveZ(-cms);
            if (ks.IsKeyDown(Keys.A))
                MoveX(-cms);
            if (ks.IsKeyDown(Keys.D))
                MoveX(cms);
        }
    }
}
