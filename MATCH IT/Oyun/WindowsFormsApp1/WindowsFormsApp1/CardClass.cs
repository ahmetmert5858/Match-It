using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public class CardClass
    {
        public int ID;
        public PictureBox pictureBox;
        public Image pictureImage;
        public bool IsFinded = false;
        public event EventHandler Click;

        private void Card_Click(object sender, EventArgs e)
        {
            OnClick(EventArgs.Empty);
        }

        protected virtual void OnClick(EventArgs e)
        {
            Click?.Invoke(this, e);
        }
        public void ShowCard()
        {
            if (pictureBox != null)
            {
                pictureBox.Image = pictureImage;
            }
        }

        public void UnvisibleCard()
        {
            if (!IsFinded && pictureBox != null)
            {
                pictureBox.Image = null;
            }
        }
        public void CardPicture(PictureBox newPictureBox)
        {
            this.pictureBox = newPictureBox;
            pictureImage = pictureBox.Image;
            pictureBox.Click += Card_Click;
            pictureBox.Image = null;
        }
        
    }
}
