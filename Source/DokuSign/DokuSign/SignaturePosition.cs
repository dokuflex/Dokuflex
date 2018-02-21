using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DokuSign
{
    public class SignaturePosition
    {
        public int SigWidth { get; set; }

        public int SigHeight { get; set; }

        public int SigPosX { get; set; }

        public int SigPosY { get; set; }

        public SignaturePosition()
        {

        }

        public SignaturePosition(int sigWidth,
            int sigHeight, int sigPosX, int sigPosY)
        {
            SigWidth = sigWidth;
            SigHeight = sigHeight;
            SigPosX = sigPosX;
            SigPosY = sigPosY;
        }
    }
}
