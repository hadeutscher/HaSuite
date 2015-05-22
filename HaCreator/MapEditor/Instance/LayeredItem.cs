using HaCreator.MapEditor.UndoRedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.MapEditor.Instance
{
    public abstract class LayeredItem : BoardItem, IContainsLayerInfo
    {
        private Layer layer;
        private int zm;

        public LayeredItem(Board board, Layer layer, int zm, int x, int y, int z)
            : base(board, x, y, z)
        {
            this.layer = layer;
            layer.Items.Add(this);
            this.zm = zm;
        }

        public override void RemoveItem(List<UndoRedoAction> undoPipe)
        {
            lock (board.ParentControl)
            {
                layer.Items.Remove(this);
                base.RemoveItem(undoPipe);
            }
        }

        public override void InsertItem()
        {
            lock (board.ParentControl)
            {
                layer.Items.Add(this);
                base.InsertItem();
            }
        }

        public Layer Layer
        {
            get
            {
                return layer;
            }
            set
            {
                lock (board.ParentControl)
                {
                    layer.Items.Remove(this);
                    layer = value;
                    layer.Items.Add(this);
                    Board.BoardItems.Sort();
                }
            }
        }

        public int LayerNumber
        {
            get { return Layer.LayerNumber; }
            set
            {
                lock (board.ParentControl)
                {
                    Layer = Board.Layers[value];
                }
            }
        }

        public override bool CheckIfLayerSelected(SelectionInfo sel)
        {
            return (sel.selectedLayer == -1 || Layer.LayerNumber == sel.selectedLayer) && (sel.selectedPlatform == -1 || PlatformNumber == sel.selectedPlatform);
        }

        public int PlatformNumber { get { return zm; } set { zm = value; } }
    }
}
