using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Instance.Misc
{
    public class SwimArea : MiscRectangle
    {
        string id;

        public SwimArea(Board board, XNA.Rectangle rect, string id)
            : base(board, rect)
        {
            this.id = id;
        }

        public string Identifier
        {
            get { return id; }
            set { id = value; }
        }

        public override string Name
        {
            get { return "SwimArea " + id; }
        }
    }
}
