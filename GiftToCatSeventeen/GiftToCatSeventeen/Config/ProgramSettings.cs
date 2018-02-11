using System.Drawing;

namespace GiftToCatSeventeen.Config
{
    public class ProgramSettings
    {
        private CatSettings windowsSettingFile = new CatSettings();

        private Rectangle displayRectangle = new Rectangle(0, 0, 0, 0);

        public int Width { set { displayRectangle.Width = value; } get { return displayRectangle.Width; } }
        public int Height { set { displayRectangle.Height = value; } get { return displayRectangle.Height; } }
        public Rectangle DispalyRectangle { get { return displayRectangle; } }
        public float AspectRatio { get { return displayRectangle.Height / displayRectangle.Width; } }
        
        public ProgramSettings()
        {
            Width = windowsSettingFile.Width;
            Height = windowsSettingFile.Height;
        }

        public void Save()
        {
            windowsSettingFile.Width = Width;
            windowsSettingFile.Height = Height;

            windowsSettingFile.Save();
        }
    }
}
