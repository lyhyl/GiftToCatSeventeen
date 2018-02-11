using System;
using System.Collections.Generic;
using GiftToCatSeventeen.CatControl;
using GiftToCatSeventeen.CatModel;
using GiftToCatSeventeen.CatPluginManager;
using GiftToCatSeventeen.Config;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SysForms = System.Windows.Forms;
using SysImaging = System.Drawing.Imaging;

namespace GiftToCatSeventeen
{
    public class GiftToCatSeventeen : Game
    {
        private GraphicsDeviceManager graphics;
        private ProgramSettings programSettings;
        private SysForms.Form selfWinForm;

        private Renderer renderer;
        private Camera camera;
        private DynamicModel cat;
        private DynamicModel[] selfDynamicModels;
        private StaticModel[] selfStaticModels;

        public PluginManager PluginManager { get; set; }
        public DisplayForm DisplayForm { set; get; }

        public GiftToCatSeventeen(ProgramSettings settings)
        {
            programSettings = settings;

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Data";
        }

        protected override void BeginRun()
        {
            base.BeginRun();
            DisplayForm.Visible = true;
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            base.OnExiting(sender, args);
            PluginManager.Exit();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = programSettings.Width;
            graphics.PreferredBackBufferHeight = programSettings.Height;
            graphics.ApplyChanges();

            camera = new Camera(programSettings.Width, programSettings.Height);
            camera.EnableTarget = true;
            camera.Position = new Vector3(0, 600, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            selfWinForm = SysForms.Control.FromHandle(Window.Handle) as SysForms.Form;
            selfWinForm.Visible = false;
            selfWinForm.VisibleChanged += new EventHandler(gameForm_VisibleChanged);

            renderer = new Renderer(GraphicsDevice, Content);
            renderer.Camera = camera;

            cat = new DynamicModel(Content.Load<Model>("Cat/cat"));
            cat.Position = new Vector3(500, 0, -1200);
            cat.ChangeEffect(renderer.GetDynamicEffect(Content.Load<Texture2D>("Cat/OrangeCatSkin")));

            selfDynamicModels = new DynamicModel[] { cat };
            selfStaticModels = new StaticModel[] { };

            PluginManager.CatController = new CatController(cat, selfWinForm, Content);
        }

        private void gameForm_VisibleChanged(object sender, EventArgs e)
        {
            selfWinForm.Visible = false;
            selfWinForm.ShowInTaskbar = false;
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            cat.Update(gameTime);

            PluginManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            camera.Target = cat.Position + new Vector3(0, 309, 140);

            cat.ClearExtraTransforms();
            cat.AddExtraTransform("head", CreateCatFaceCameraMatrix(-Math.PI * 0.5, Math.PI * 0.5), 1);

            List<DynamicModel[]> dmodls = PluginManager.DynamicModels;
            List<StaticModel[]> smodls = PluginManager.StaticModels;
            dmodls.Add(selfDynamicModels);
            smodls.Add(selfStaticModels);
            renderer.Render(dmodls, smodls);

            base.Draw(gameTime);
        }

        private Matrix CreateCatFaceCameraMatrix(double minAng, double maxAng)
        {
            Vector3 camNegForward = camera.Position - (cat.Position + new Vector3(0, 500, -500));
            //Vector3 catForward = new Vector3(0, 0, 1);

            //Y-Z
            double yzLen = Math.Sqrt(camNegForward.Y * camNegForward.Y + camNegForward.Z * camNegForward.Z);
            double yzSign = Math.Asin(-camNegForward.Y / yzLen);
            double yzAng = Math.Acos(camNegForward.Z / yzLen) * Math.Sign(yzSign);
            yzAng = (yzAng > maxAng ? maxAng : (yzAng < minAng ? minAng : yzAng));
            Matrix faceMatUpDown = Matrix.CreateRotationX((float)yzAng);

            //X-Z
            double xzLen = Math.Sqrt(camNegForward.X * camNegForward.X + camNegForward.Z * camNegForward.Z);
            double xzSign = Math.Asin(camNegForward.X / xzLen);
            double xzAng = Math.Acos(camNegForward.Z / xzLen) * Math.Sign(xzSign);
            xzAng = (xzAng > maxAng ? maxAng : (xzAng < minAng ? minAng : xzAng));
            Matrix faceMatLeftRight = Matrix.CreateRotationY((float)xzAng);

            return faceMatUpDown * faceMatLeftRight;
        }

        public void RenderToBitmap(System.Drawing.Bitmap bitmap)
        {
            SysImaging.BitmapData bitmapdata =
                bitmap.LockBits(programSettings.DispalyRectangle,
                SysImaging.ImageLockMode.WriteOnly,
                SysImaging.PixelFormat.Format32bppArgb);

            unsafe
            {
                uint* numPtr = (uint*)bitmapdata.Scan0;
                foreach (uint color in renderer.RenderResultData)
                    *numPtr++ = color;
            }

            bitmap.UnlockBits(bitmapdata);
        }
    }
}
