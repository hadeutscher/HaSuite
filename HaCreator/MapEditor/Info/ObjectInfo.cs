using HaCreator.MapEditor.Instance;
using HaCreator.MapEditor.Instance.Shapes;
using HaCreator.Wz;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using MapleLib.WzLib.WzStructure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XNA = Microsoft.Xna.Framework;

namespace HaCreator.MapEditor.Info
{
    public class ObjectInfo : MapleDrawableInfo
    {
        private string _oS;
        private string _l0;
        private string _l1;
        private string _l2;
        private List<XNA.Point> footholdOffsets = null;
        private List<XNA.Point> ropeOffsets = null;
        private List<XNA.Point> chairOffsets = null;
        private List<List<XNA.Point>> footholdFullOffsets = null;
        private bool fullFootholdInfo = false;
        private bool connect;

        public ObjectInfo(Bitmap image, System.Drawing.Point origin, string oS, string l0, string l1, string l2, WzObject parentObject)
            : base(image, origin, parentObject)
        {
            this._oS = oS;
            this._l0 = l0;
            this._l1 = l1;
            this._l2 = l2;
            this.connect = oS.StartsWith("connect");
        }

        public static ObjectInfo Get(string oS, string l0, string l1, string l2)
        {
            WzImageProperty objInfoProp = Program.InfoManager.ObjectSets[oS][l0][l1][l2];
            if (objInfoProp.HCTag == null)
                objInfoProp.HCTag = ObjectInfo.Load((WzSubProperty)objInfoProp, oS, l0, l1, l2);
            return (ObjectInfo)objInfoProp.HCTag;
        }

        private static ObjectInfo Load(WzSubProperty parentObject, string oS, string l0, string l1, string l2)
        {
            WzCanvasProperty frame1 = (WzCanvasProperty)WzInfoTools.GetRealProperty(parentObject["0"]);
            ObjectInfo result = new ObjectInfo(frame1.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)frame1["origin"]), oS, l0, l1, l2, parentObject);
            WzImageProperty chairs = parentObject["seat"];
            WzImageProperty ropes = frame1["rope"];
            WzImageProperty ladders = frame1["ladder"];
            WzImageProperty footholds = frame1["foothold"];
            if (footholds != null)
            {
                if (footholds is WzConvexProperty)
                {
                    result.fullFootholdInfo = false;
                    result.footholdOffsets = new List<XNA.Point>();
                    foreach (WzVectorProperty fhAnchor in footholds.WzProperties)
                    {
                        result.footholdOffsets.Add(WzInfoTools.VectorToXNAPoint(fhAnchor));
                    }
                }
                else
                {
                    result.fullFootholdInfo = true;
                    result.footholdFullOffsets = new List<List<XNA.Point>>();
                    List<XNA.Point> fhAnchorList = new List<XNA.Point>();
                    foreach (WzConvexProperty fh in footholds.WzProperties)
                    {
                        foreach (WzVectorProperty fhAnchor in fh.WzProperties)
                        {
                            fhAnchorList.Add(WzInfoTools.VectorToXNAPoint(fhAnchor));
                        }
                        result.footholdFullOffsets.Add(fhAnchorList);
                        fhAnchorList = new List<XNA.Point>();
                    }
                }
            }
            if (chairs != null)
            {
                result.chairOffsets = new List<XNA.Point>();
                foreach (WzVectorProperty chair in chairs.WzProperties)
                {
                    result.chairOffsets.Add(WzInfoTools.VectorToXNAPoint(chair));
                }
            }
            if (ropes != null || ladders != null)
            {
                result.ropeOffsets = new List<XNA.Point>();
                if (ropes != null)
                {
                    foreach (WzVectorProperty rope in ropes.WzProperties)
                    {
                        result.ropeOffsets.Add(WzInfoTools.VectorToXNAPoint(rope));
                    }
                }
                if (ladders != null)
                {
                    foreach (WzVectorProperty ladder in ladders.WzProperties)
                    {
                        result.ropeOffsets.Add(WzInfoTools.VectorToXNAPoint(ladder));
                    }
                }
            }
            return result;
        }

        private void CreateFootholdsFromAnchorList(Board board, List<FootholdAnchor> anchors)
        {
            for (int i = 0; i < anchors.Count - 1; i++)
            {
                FootholdLine fh = new FootholdLine(board, anchors[i], anchors[i + 1]);
                board.BoardItems.FootholdLines.Add(fh);
            }
        }

        public void ParseOffsets(ObjectInstance instance, Board board, int x, int y)
        {
            List<FootholdAnchor> anchors = new List<FootholdAnchor>();
            bool ladder = l0 == "ladder";
            if (footholdOffsets != null || footholdFullOffsets != null)
            {
                if (!fullFootholdInfo)
                {
                    foreach (XNA.Point foothold in footholdOffsets)
                    {
                        FootholdAnchor anchor = new FootholdAnchor(board, x + foothold.X, y + foothold.Y, instance.LayerNumber, instance.PlatformNumber, true);
                        board.BoardItems.FHAnchors.Add(anchor);
                        instance.BindItem(anchor, foothold);
                        anchors.Add(anchor);
                    }
                    CreateFootholdsFromAnchorList(board, anchors);
                }
                else
                {
                    foreach (List<XNA.Point> anchorList in footholdFullOffsets)
                    {
                        foreach (XNA.Point foothold in anchorList)
                        {
                            FootholdAnchor anchor = new FootholdAnchor(board, x + foothold.X, y + foothold.Y, instance.LayerNumber, instance.PlatformNumber, true);
                            board.BoardItems.FHAnchors.Add(anchor);
                            instance.BindItem(anchor, foothold);
                            anchors.Add(anchor);
                        }
                        CreateFootholdsFromAnchorList(board, anchors);
                        anchors.Clear();
                    }
                }
            }
            if (chairOffsets != null) foreach (XNA.Point chairPos in chairOffsets)
                {
                    Chair chair = new Chair(board, x + chairPos.X, y + chairPos.Y);
                    board.BoardItems.Chairs.Add(chair);
                    instance.BindItem(chair, chairPos);
                }
            /*foreach (XNA.Point rope in ropeOffsets) //second thought: what the fuck is this even good for? rope origins aren't multilined anyway, why add them as board items?
            {
                RopeLadderAnchor ropeAnchor = new RopeLadderAnchor(board, x + rope.X, y + rope.Y, layer.LayerNumber, ladder, false);
                board.BoardItems.RopeAnchors.Add(ropeAnchor);
                instance.BindItem(ropeAnchor, rope);
            }*/
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip)
        {
            ObjectInstance instance = new ObjectInstance(this, layer, board, x, y, z, layer.zMDefault, false, false, false, false, null, null, null, null, null, null, null, flip);
            ParseOffsets(instance, board, x, y);
            return instance;
        }

        public BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, int zM, MapleBool r, MapleBool hide, MapleBool reactor, MapleBool flow, int? rx, int? ry, int? cx, int? cy, string name, string tags, List<ObjectInstanceQuest> questInfo, bool flip, bool parseOffsets)
        {
            ObjectInstance instance = new ObjectInstance(this, layer, board, x, y, z, zM, r, hide, reactor, flow, rx, ry, cx, cy, name, tags, questInfo, flip);
            if (parseOffsets) ParseOffsets(instance, board, x, y);
            return instance;
        }

        public string oS
        {
            get
            {
                return _oS;
            }
            set
            {
                this._oS = value;
            }
        }

        public string l0
        {
            get
            {
                return _l0;
            }
            set
            {
                this._l0 = value;
            }
        }

        public string l1
        {
            get
            {
                return _l1;
            }
            set
            {
                this._l1 = value;
            }
        }

        public string l2
        {
            get
            {
                return _l2;
            }
            set
            {
                this._l2 = value;
            }
        }

        public List<XNA.Point> FootholdOffsets
        {
            get
            {
                return footholdOffsets;
            }
        }

        public List<XNA.Point> ChairOffsets
        {
            get
            {
                return chairOffsets;
            }
        }

        public List<XNA.Point> RopeOffsets
        {
            get
            {
                return ropeOffsets;
            }
        }

        public bool Connect { get { return connect; } }
    }

}
