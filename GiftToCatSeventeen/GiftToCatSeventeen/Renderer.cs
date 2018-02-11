using System.Collections.Generic;
using GiftToCatSeventeen.CatModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GiftToCatSeventeen
{
    public class Renderer
    {
        private RenderTarget2D renderTarget;
        private RenderTarget2D normalDepthRenderTarget;
        private Effect falloffEffect, falloffSkinnedEffect, postprocessEffect;

        private GraphicsDevice graphicsDevice;
        private ContentManager content;
        private Color backgroundColor;

        public Camera Camera { set; get; }

        public Renderer(GraphicsDevice graphicsDevice, ContentManager content)
        {
            this.graphicsDevice = graphicsDevice;
            this.content = content;
            
            PresentationParameters pp = graphicsDevice.PresentationParameters;
            renderTarget = new RenderTarget2D(graphicsDevice,
                pp.BackBufferWidth, pp.BackBufferHeight, false,
                pp.BackBufferFormat, pp.DepthStencilFormat,
                pp.MultiSampleCount, pp.RenderTargetUsage);

            normalDepthRenderTarget = new RenderTarget2D(graphicsDevice,
                pp.BackBufferWidth, pp.BackBufferHeight, false,
                pp.BackBufferFormat, pp.DepthStencilFormat,
                pp.MultiSampleCount, pp.RenderTargetUsage);

            backgroundColor = new Color(0, 0, 0, 1);

            falloffEffect = content.Load<Effect>("shader/FalloffEffect");
            falloffEffect.Parameters["TextureEnabled"].SetValue(false);
            falloffEffect.Parameters["Color"].SetValue(Color.Orange.ToVector4());

            falloffSkinnedEffect = content.Load<Effect>("shader/FalloffSkinnedEffect");
            falloffSkinnedEffect.Parameters["TextureEnabled"].SetValue(false);
            falloffSkinnedEffect.Parameters["Color"].SetValue(Color.Orange.ToVector4());

            postprocessEffect = content.Load<Effect>("Shader/postprocessEffect");
            postprocessEffect.Parameters["EdgeWidth"].SetValue(1.0f);
            postprocessEffect.Parameters["EdgeIntensity"].SetValue(1.0f);
            Viewport vp = graphicsDevice.Viewport;
            postprocessEffect.Parameters["ScreenResolution"].SetValue(new Vector2(vp.Width, vp.Height));
        }

        public Effect GetDynamicEffect(Texture2D texture)
        {
            if (texture == null)
            {
                falloffSkinnedEffect.Parameters["TextureEnabled"].SetValue(false);
                falloffSkinnedEffect.Parameters["Color"].SetValue(Color.Orange.ToVector4());
            }
            else
            {
                falloffSkinnedEffect.Parameters["Texture"].SetValue(texture);
                falloffSkinnedEffect.Parameters["TextureEnabled"].SetValue(true);
            }
            return falloffSkinnedEffect;
        }

        public Effect GetStaticEffect(Texture2D texture)
        {
            if (texture == null)
            {
                falloffEffect.Parameters["Color"].SetValue(Color.Orange.ToVector4());
                falloffEffect.Parameters["TextureEnabled"].SetValue(false);
            }
            else
            {
                falloffEffect.Parameters["Texture"].SetValue(texture);
                falloffEffect.Parameters["TextureEnabled"].SetValue(true);
            }
            return falloffEffect;
        }

        public uint[] RenderResultData
        {
            get
            {
                uint[] buffer = new uint[renderTarget.Width * renderTarget.Height];
                renderTarget.GetData(buffer);
                return buffer;
            }
        }

        public void Render(List<DynamicModel[]> dmodls, List<StaticModel[]> smodls)
        {
            graphicsDevice.SetRenderTarget(normalDepthRenderTarget);
            graphicsDevice.Clear(Color.Black);

            foreach (DynamicModel[] dmodl in dmodls)
                foreach (DynamicModel dmod in dmodl)
                    DrawModelNormal(dmod);
            foreach (StaticModel[] smodl in smodls)
                foreach (StaticModel smod in smodl)
                    DrawModelNormal(smod);

            graphicsDevice.SetRenderTarget(renderTarget);
            graphicsDevice.Clear(backgroundColor);

            foreach (DynamicModel[] dmodl in dmodls)
                foreach (DynamicModel dmod in dmodl)
                    DrawModelFalloff(dmod);
            foreach (StaticModel[] smodl in smodls)
                foreach (StaticModel smod in smodl)
                    DrawModelFalloff(smod);

            graphicsDevice.SetRenderTarget(null);
        }

        private void DrawModelNormal(DynamicModel model)
        {
            Matrix[] bones = model.SkinnedMartix;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    effect.CurrentTechnique = effect.Techniques[0];
                    effect.Parameters["amPalette"].SetValue(bones);
                    effect.Parameters["World"].SetValue(Matrix.CreateTranslation(model.Position));
                    effect.Parameters["View"].SetValue(Camera.ViewMatrix);
                    effect.Parameters["Projection"].SetValue(Camera.ProjectionMatrix);
                }
                mesh.Draw();
            }
        }
        private void DrawModelNormal(StaticModel model)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    effect.CurrentTechnique = effect.Techniques[0];
                    effect.Parameters["World"].SetValue(Matrix.CreateTranslation(model.Position));
                    effect.Parameters["View"].SetValue(Camera.ViewMatrix);
                    effect.Parameters["Projection"].SetValue(Camera.ProjectionMatrix);
                }
                mesh.Draw();
            }
        }
        private void DrawModelFalloff(DynamicModel model)
        {
            Matrix[] bones = model.SkinnedMartix;
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    effect.CurrentTechnique = effect.Techniques[1];
                    effect.Parameters["CameraDirection"].SetValue(new Vector3(0, 0, 1));
                    effect.Parameters["amPalette"].SetValue(bones);
                    effect.Parameters["World"].SetValue(Matrix.CreateTranslation(model.Position));
                    effect.Parameters["View"].SetValue(Camera.ViewMatrix);
                    effect.Parameters["Projection"].SetValue(Camera.ProjectionMatrix);
                }
                mesh.Draw();
            }
        }
        private void DrawModelFalloff(StaticModel model)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (Effect effect in mesh.Effects)
                {
                    effect.CurrentTechnique = effect.Techniques[1];
                    effect.Parameters["CameraDirection"].SetValue(new Vector3(0, 0, 1));
                    effect.Parameters["World"].SetValue(Matrix.CreateTranslation(model.Position));
                    effect.Parameters["View"].SetValue(Camera.ViewMatrix);
                    effect.Parameters["Projection"].SetValue(Camera.ProjectionMatrix);
                }
                mesh.Draw();
            }
        }
    }
}
