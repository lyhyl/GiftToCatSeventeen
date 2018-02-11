using System.ComponentModel;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class Player : Form
    {
        public delegate void HandleEvent();
        public event HandleEvent Exit;

        public Player()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
        }
    }
}
