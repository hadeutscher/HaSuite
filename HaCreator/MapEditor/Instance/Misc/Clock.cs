using XNA = Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance.Misc
{
    public class Clock : MiscRectangle
    {
        public Clock(Board board, XNA.Rectangle rect)
            : base(board, rect)
        {
        }

        public override string Name
        {
            get { return "Clock"; }
        }
    }
}
