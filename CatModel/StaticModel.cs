using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GiftToCatSeventeen.CatModel
{
    public class StaticModel
    {
        private Model model;

        public Vector3 Position { set; get; }
        public ModelMeshCollection Meshes { get { return model.Meshes; } }

        public StaticModel(Model mod)
        {
            model = mod;
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
    }
}
