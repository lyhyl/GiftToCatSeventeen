using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GiftToCatSeventeen.CatModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace GiftToCatSeventeen.CatControl
{
    public class CatController
    {
        //Basic
        private Random random = new Random();
        private DynamicModel cat;
        private float walkSpeed = 10;

        //Greeting
        private Form displayForm;
        private GreetingForm greetingForm;

        //Miaow
        private List<SoundEffect> catSoundEffect = new List<SoundEffect>();
        private SoundEffectInstance catCurrentSound = null;

        public CatController(DynamicModel target, Form displayWin, ContentManager content)
        {
            IniaializeBasicRes(target);
            InitializeGreetingRes(displayWin);
            InitializeMiaowRes(content);
        }

        private void IniaializeBasicRes(DynamicModel target)
        {
            cat = target;
            cat.AnimationFinished += new EventHandler(cat_AnimationFinished);
        }

        private void InitializeGreetingRes(Form displayWin)
        {
            displayForm = displayWin;
            greetingForm = new GreetingForm();
        }

        private void InitializeMiaowRes(ContentManager content)
        {
            catSoundEffect.Add(content.Load<SoundEffect>("Sound/miaow00"));
            catSoundEffect.Add(content.Load<SoundEffect>("Sound/miaow01"));
        }

        public void FaceCamera(Vector3 cameraPoaition)
        {
            cat.ClearExtraTransforms();
            Matrix trans = CreateCatFaceCameraMatrix(-Math.PI * 0.5, Math.PI * 0.5, cameraPoaition);
            cat.AddExtraTransform("head", trans, 1);
        }

        private Matrix CreateCatFaceCameraMatrix(double cn, double cp, Vector3 camPos)
        {
            Vector3 cameraNegForward = camPos - cat.Position;
            Vector3 catForward = new Vector3(0, 0, 1);
            double xzLen = Math.Sqrt(cameraNegForward.X * cameraNegForward.X + cameraNegForward.Z * cameraNegForward.Z);
            double xzCross = cameraNegForward.X * catForward.Z - catForward.X * cameraNegForward.Z;
            double xzSign = Math.Asin(xzCross / xzLen);
            double xzDot = cameraNegForward.X * catForward.X + cameraNegForward.Z * catForward.Z;
            double xzAng = Math.Acos(xzDot / xzLen) * Math.Sign(xzSign);
            if (xzAng > cp) xzAng = cp;
            else if (xzAng < cn) xzAng = cn;
            Matrix faceMatY = Matrix.CreateRotationY((float)xzAng);

            return faceMatY;
        }

        private bool AnimationFinished { set; get; }
        private void cat_AnimationFinished(object sender, EventArgs e)
        {
            AnimationFinished = true;
        }

        private bool WaitingAnimationFinish;
        private bool WaitAnimationFinish()
        {
            if (!WaitingAnimationFinish)
            {
                WaitingAnimationFinish = true;
                AnimationFinished = false;
            }
            return AnimationFinished;
        }

        private bool CatStay()
        {
            if (cat.CurrectAnimationID != 0)
                cat.CurrectAnimationID = 0;
            return true;
        }
        private bool CatWagTail()
        {
            if (cat.CurrectAnimationID != 1)
                cat.CurrectAnimationID = 1;
            if (WaitAnimationFinish())
            {
                cat.CurrectAnimationID = 0;
                WaitingAnimationFinish = false;
                return true;
            }
            return false;
        }
        private bool CatGreet(string msg)
        {
            System.Drawing.Point dispos = new System.Drawing.Point(displayForm.Left, displayForm.Top);
            dispos.X += displayForm.Width >> 1;
            greetingForm.ShowGreeting(msg, dispos, GreetingForm.ShowSide.Top);
            return CatMiaow();
        }
        private bool CatMiaow()
        {
            if (cat.CurrectAnimationID != 4)
            {
                catCurrentSound = catSoundEffect[random.Next(catSoundEffect.Count)].CreateInstance();
                catCurrentSound.Play();
                cat.CurrectAnimationID = 4;
            }
            if (WaitAnimationFinish())
            {
                cat.CurrectAnimationID = 0;
                WaitingAnimationFinish = false;
                return true;
            }
            return false;
        }
        private bool CatWalk(Vector3 target)
        {
            Vector3 delta = target - cat.Position;
            float length = delta.Length();

            if (length < walkSpeed || WaitingAnimationFinish)
                if (WaitAnimationFinish())
                {
                    cat.CurrectAnimationID = 0;
                    WaitingAnimationFinish = false;
                    return true;
                }
            if (cat.CurrectAnimationID != 2)
                cat.CurrectAnimationID = 2;
            cat.Position += delta / length * walkSpeed;
            return false;
        }

        private Dictionary<string, int> commands = null;
        public Dictionary<string, int> Commands
        {
            get
            {
                if (commands == null)
                    CreateCommandsTable();
                return commands;
            }
        }

        private void CreateCommandsTable()
        {
            commands = new Dictionary<string, int>();
            commands.Add("Stay", 0);
            commands.Add("WagTail", 1);
            commands.Add("Miaow", 2);
            commands.Add("Greet", 3);
            commands.Add("Walk", 4);
        }

        public bool Execute(int cmd, params object[] para)
        {
            try
            {
                switch (cmd)
                {
                    case 0: return CatStay();
                    case 1: return CatWagTail();
                    case 2: return CatMiaow();
                    case 3: return CatGreet((string)para[0]);
                    case 4: return CatWalk((Vector3)para[0]);
                    default: return false;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentException("Plugin should pass vaild arguments to CatController", e);
            }
        }
    }
}
