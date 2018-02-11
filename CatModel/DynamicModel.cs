using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkinnedModel;

namespace GiftToCatSeventeen.CatModel
{
    public class DynamicModel
    {
        private Model model;
        private SkinningData skinningData;
        private AnimationPlayer animationPlayer;
        private AnimationClip[] animationClips;

        public Vector3 Position { set; get; }
        public ModelMeshCollection Meshes { get { return model.Meshes; } }
        public Matrix[] SkinnedMartix { get { return animationPlayer.GetSkinTransforms(); } }

        public event EventHandler AnimationFinished;

        private int curAniID = -1;
        public int CurrectAnimationID
        {
            set
            {
                curAniID = value;
                animationPlayer.StartClip(animationClips[curAniID]);
            }
            get { return curAniID; }
        }

        public DynamicModel(Model mod)
        {
            model = mod;

             skinningData = model.Tag as SkinningData;
            if (skinningData == null)
                throw new InvalidOperationException("This model does not contain a SkinningData tag.");

            animationPlayer = new AnimationPlayer(skinningData);
            animationClips = new AnimationClip[skinningData.AnimationClips.Values.Count];
            skinningData.AnimationClips.Values.CopyTo(animationClips, 0);
            curAniID = 0;

            animationPlayer.AnimationFinished += new EventHandler(animationPlayer_AnimationFinished);

            animationPlayer.StartClip(animationClips[0]);
        }

        public void AddExtraTransform(string boneName, Matrix matrix, float lerpAmount)
        {
            animationPlayer.AddExtraTransforms(skinningData.BoneMap[boneName], matrix);
        }
        public void ClearExtraTransforms()
        {
            animationPlayer.ClearExtraTransforms();
        }

        void animationPlayer_AnimationFinished(object sender, EventArgs e)
        {
            this.AnimationFinished(this, null);
        }

        public void ChangeEffect(Effect effect)
        {
            //Clone for each part?
            /*foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    meshPart.Effect = effect.Clone();*/
            Effect cloneEffect = effect.Clone();
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    meshPart.Effect = cloneEffect;
        }

        public void Update(GameTime gameTime)
        {
            animationPlayer.Update(gameTime.ElapsedGameTime, Matrix.Identity);
        }
    }
}
