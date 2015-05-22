using HaCreator.MapEditor.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance.Shapes
{
    public class Rope : IContainsLayerInfo
    {
        private Board board;
        private RopeAnchor firstAnchor;
        private RopeAnchor secondAnchor;
        private RopeLine line;

        private int _page; //aka layer
        private bool _ladder;
        private bool _uf; //deciding if you can climb over the end of the rope (usually true)
        //according to koolk it stands for "Upper Foothold"
        public Rope(Board board, int x, int y1, int y2, bool ladder, int page, bool uf)
        {
            this.board = board;
            this._page = page;
            this._ladder = ladder;
            this._uf = uf;
            this.firstAnchor = new RopeAnchor(board, x, y1, this);
            this.secondAnchor = new RopeAnchor(board, x, y2, this);
            this.line = new RopeLine(board, firstAnchor, secondAnchor);
            Create();
        }

        public void Remove(List<UndoRedoAction> undoPipe)
        {
            lock (board.ParentControl)
            {
                firstAnchor.Selected = false;
                secondAnchor.Selected = false;
                board.BoardItems.RopeAnchors.Remove(firstAnchor);
                board.BoardItems.RopeAnchors.Remove(secondAnchor);
                board.BoardItems.RopeLines.Remove(line);
                if (undoPipe != null)
                {
                    undoPipe.Add(UndoRedoManager.RopeRemoved(this));
                }
            }
        }

        public void Create()
        {
            lock (board.ParentControl)
            {
                board.BoardItems.RopeAnchors.Add(firstAnchor);
                board.BoardItems.RopeAnchors.Add(secondAnchor);
                board.BoardItems.RopeLines.Add(line);
            }
        }

        public int LayerNumber { get { return _page; } set { _page = value; } }
        public int PlatformNumber { get { return -1; } set { return; } }
        public bool ladder { get { return _ladder; } set { _ladder = value; } }
        public bool uf { get { return _uf; } set { _uf = value; } }

        public RopeAnchor FirstAnchor { get { return firstAnchor; } }
        public RopeAnchor SecondAnchor { get { return secondAnchor; } }
    }
}
