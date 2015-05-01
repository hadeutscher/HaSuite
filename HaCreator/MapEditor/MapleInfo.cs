/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Collections;
using XNA = Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using MapleLib.WzLib.WzStructure.Data;
using MapleLib.WzLib.WzStructure;
using HaCreator.MapEditor.TilesDesign;

namespace HaCreator.MapEditor
{
    public abstract class MapleDrawableInfo
    {
        private Bitmap image;
        private Texture2D texture;
        private System.Drawing.Point origin;
        private WzObject parentObject;
        int width;
        int height;

        public MapleDrawableInfo(Bitmap image, System.Drawing.Point origin, WzObject parentObject)
        {
            this.Image = image;
            this.origin = origin;
            this.parentObject = parentObject;
        }

        public void CreateTexture(GraphicsDevice device)
        {
            texture = BoardItem.TextureFromBitmap(device, image);
        }

        public abstract BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip);

        public virtual Texture2D GetTexture(SpriteBatch sprite)
        {
            if (texture == null) CreateTexture(sprite.GraphicsDevice);
            return texture;
        }

        public virtual WzObject ParentObject
        {
            get
            {
                return parentObject;
            }
            set
            {
                parentObject = value;
            }
        }

        public virtual Bitmap Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                texture = null;

                if (image != null)
                {
                    width = image.Width;
                    height = image.Height;
                }
            }
        }

        public virtual int Width
        {
            get
            {
                return width;
            }
        }

        public virtual int Height
        {
            get
            {
                return height;
            }
        }

        public virtual System.Drawing.Point Origin
        {
            get
            {
                return origin;
            }
            set
            {
                origin = value;
            }
        }
    }

    public abstract class MapleExtractableInfo : MapleDrawableInfo
    {
        public MapleExtractableInfo(Bitmap image, System.Drawing.Point origin, WzObject parentObject)
            : base(image, origin, parentObject)
        {
        }

        public override Bitmap Image
        {
            get
            {
                if (base.Image == null)
                    ParseImage();
                return base.Image;
            }
            set
            {
                base.Image = value;
            }
        }

        public abstract void ParseImage();
    }

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

        public static ObjectInfo Load(WzSubProperty parentObject)
        {
            string[] path = parentObject.FullPath.Split(@"\".ToCharArray());
            return Load(parentObject,WzInfoTools.RemoveExtension(path[path.Length - 4]), path[path.Length - 3], path[path.Length - 2], path[path.Length - 1]);
        }

        public static ObjectInfo Load(WzSubProperty parentObject, string oS, string l0, string l1, string l2)
        {
            WzCanvasProperty frame1 = (WzCanvasProperty)WzInfoTools.GetRealProperty(parentObject["0"]);
            ObjectInfo result = new ObjectInfo(frame1.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)frame1["origin"]),oS,l0,l1,l2, parentObject);
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
            if  (chairOffsets != null) foreach (XNA.Point chairPos in chairOffsets)
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

    public class TileInfo : MapleDrawableInfo
    {
        private string _tS;
        private string _u;
        private string _no;
        private int _mag;
        private int _z;
        private List<XNA.Point> footholdOffsets = new List<XNA.Point>();

        public TileInfo(Bitmap image, System.Drawing.Point origin, string tS, string u, string no, int mag, int z, WzObject parentObject)
            : base(image, origin, parentObject)
        {
            this._tS = tS;
            this._u = u;
            this._no = no;
            this._mag = mag;
            this._z = z;
        }

        public static TileInfo Load(WzCanvasProperty parentObject, WzSubProperty infoObject)
        {
            string[] path = parentObject.FullPath.Split(@"\".ToCharArray());
            int? mag = InfoTool.GetOptionalInt(infoObject["mag"]);
            return Load(parentObject,WzInfoTools.RemoveExtension(path[path.Length - 3]), path[path.Length - 2], path[path.Length - 1], mag.HasValue ? mag.Value : 1);
        }

        public static TileInfo Load(WzCanvasProperty parentObject, string tS, string u, string no, int? mag)
        {
            WzImageProperty zProp = parentObject["z"];
            int z = zProp == null ? 0 : InfoTool.GetInt(zProp);
            TileInfo result = new TileInfo(parentObject.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)parentObject["origin"]), tS, u, no, mag.HasValue ? mag.Value : 1, z, parentObject);
            WzConvexProperty footholds = (WzConvexProperty)parentObject["foothold"];
            if (footholds != null)
                foreach (WzVectorProperty foothold in footholds.WzProperties)
                    result.footholdOffsets.Add(WzInfoTools.VectorToXNAPoint(foothold));
            if (UserSettings.FixFootholdMispositions)
            {
                FixFootholdMispositions(result);
            }
            return result;
        }

        /* The sole reason behind this function's existence is that Nexon's designers are a bunch of incompetent goons.

         * In a nutshell, some tiles (mostly old ones) have innate footholds that do not overlap when snapping them to each other, causing a weird foothold structure.
         * I do not know how Nexon's editor overcame this; I am assuming they manually connected the footholds to sort that out. However, since HaCreator only allows automatic
         * connection of footholds, we need to sort these cases out preemptively here.
        */
        private static void FixFootholdMispositions(TileInfo result)
        {
            switch (result.u)
            {
                case "enV0":
                    MoveFootholdY(result, true, false, 60);
                    MoveFootholdY(result, false, true, 60);
                    break;
                case "enV1":
                    MoveFootholdY(result, true, true, 60);
                    MoveFootholdY(result, false, false, 60);
                    break;
                case "enH0":
                    MoveFootholdX(result, true, true, 90);
                    MoveFootholdX(result, false, false, 90);
                    break;
                case "slLU":
                    MoveFootholdX(result, true, false, -90);
                    MoveFootholdX(result, false, true, -90);
                    break;
                case "slRU":
                    MoveFootholdX(result, true, true, 90);
                    MoveFootholdX(result, false, false, 90);
                    break;
                case "edU":
                    MoveFootholdY(result, true, false, 0);
                    MoveFootholdY(result, false, false, 0);
                    break;
            }
        }

        private static void MoveFootholdY(TileInfo result, bool first, bool top, int height)
        {
            if (result.footholdOffsets.Count < 1)
                return;
            int idx = first ? 0 : result.footholdOffsets.Count - 1;
            int y = top ? 0 : (height * result.mag);
            if (result.footholdOffsets[idx].Y != y)
            {
                result.footholdOffsets[idx] = new XNA.Point(result.footholdOffsets[idx].X, y);
            }
        }

        private static void MoveFootholdX(TileInfo result, bool first, bool left, int width)
        {
            if (result.footholdOffsets.Count < 1)
                return;
            int idx = first ? 0 : result.footholdOffsets.Count - 1;
            int x = left ? 0 : (width * result.mag);
            if (result.footholdOffsets[idx].X != x)
            {
                result.footholdOffsets[idx] = new XNA.Point(x, result.footholdOffsets[idx].Y);
            }
        }

        public void ParseOffsets(TileInstance instance, Board board, int x, int y)
        {
            List<FootholdAnchor> anchors = new List<FootholdAnchor>();
            foreach (XNA.Point foothold in footholdOffsets)
            {
                FootholdAnchor anchor = new FootholdAnchor(board, x + foothold.X, y + foothold.Y, instance.LayerNumber, instance.PlatformNumber, true);
                anchors.Add(anchor);
                board.BoardItems.FHAnchors.Add(anchor);
                instance.BindItem(anchor, foothold);
            }
            for (int i = 0; i < anchors.Count - 1; i++)
            {
                FootholdLine fh = new FootholdLine(board, anchors[i], anchors[i + 1]);
                board.BoardItems.FootholdLines.Add(fh);
            }
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip)
        {
            TileInstance instance = new TileInstance(this, layer, board, x, y, z, layer.zMDefault);
            ParseOffsets(instance, board, x, y);
            return instance;
        }

        public BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, int zM, bool flip, bool parseOffsets)
        {
            TileInstance instance = new TileInstance(this, layer, board, x, y, z, zM);
            if (parseOffsets) ParseOffsets(instance, board, x, y);
            return instance;
        }

        public string tS
        {
            get
            {
                return _tS;
            }
            set
            {
                this._tS = value;
            }
        }

        public string u
        {
            get
            {
                return _u;
            }
            set
            {
                this._u = value;
            }
        }

        public string no
        {
            get
            {
                return _no;
            }
            set
            {
                this._no = value;
            }
        }

        public int mag
        {
            get { return _mag; }
            set { _mag = value; }
        }

        public List<XNA.Point> FootholdOffsets
        {
            get
            {
                return footholdOffsets;
            }
        }

        public int z { get { return _z; } set { _z = value; } }
    }

    public class MobInfo : MapleExtractableInfo
    {
        private string id;
        private string name;

        private WzImage LinkedImage;

        /*private int _level;
        private int _maxHP;
        private int _maxMP;
        private int _PADamage;
        private int _MADamage;
        private int _PDDamage;
        private int _MDDamage;
        private int _speed;
        private int _acc;
        private int _eva;
        private int _pushed;
        private int _exp;
        private int _hpRecovery;
        private int _mpRecovery;
        private bool _boss;
        private bool _undead;
        private bool _firstAttack;*/

        public MobInfo(Bitmap image, System.Drawing.Point origin, string id, string name, WzObject parentObject)
            : base(image, origin, parentObject)
        {
            this.id = id;
            this.name = name;
        }

        private void ExtractPNGFromImage(WzImage image)
        {
            WzCanvasProperty mobImage = WzInfoTools.GetMobImage(image);
            if (mobImage != null)
            {
                Image = mobImage.PngProperty.GetPNG(false);
                Origin = WzInfoTools.VectorToSystemPoint((WzVectorProperty)mobImage["origin"]);
            }
            else
            {
                Image = new Bitmap(1,1);
                Origin = new System.Drawing.Point();
            }
        }

        public override void ParseImage()
        {
            WzStringProperty link = (WzStringProperty)((WzSubProperty)((WzImage)ParentObject)["info"])["link"];
            if (link != null)
            {
                LinkedImage = (WzImage)Program.WzManager["mob"][link.Value + ".img"];
                ExtractPNGFromImage(LinkedImage);
            }
            else
            {
                ExtractPNGFromImage((WzImage)ParentObject);
            }
        }

        public static MobInfo Load(WzImage parentObject)
        {
            string id = /*WzInfoTools.RemoveLeadingZeros(*/WzInfoTools.RemoveExtension(parentObject.Name)/*)*/;
            return new MobInfo(null, new System.Drawing.Point(), id, WzInfoTools.GetMobNameById(id), parentObject);
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip)
        {
            if (Image == null) ParseImage();
            return new MobInstance(this, board, x, y, UserSettings.Mobrx0Offset, UserSettings.Mobrx1Offset, 20, null, UserSettings.defaultMobTime, flip, false, null, null);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int rx0Shift, int rx1Shift, int yShift, string limitedname, int? mobTime, MapleBool flip, MapleBool hide, int? info, int? team)
        {
            if (Image == null) ParseImage();
            return new MobInstance(this, board, x, y, rx0Shift, rx1Shift, yShift, limitedname, mobTime, flip, hide, info, team);
        }

        public string ID
        {
            get { return id; }
            set { this.id = value; }
        }

        public string Name
        {
            get { return name; }
            set { this.name = value; }
        }
    }

    public class NpcInfo : MapleExtractableInfo
    {
        private string id;
        private string name;

        private WzImage LinkedImage;

        public NpcInfo(Bitmap image, System.Drawing.Point origin, string id, string name, WzObject parentObject)
            : base(image, origin, parentObject)
        {
            this.id = id;
            this.name = name;
        }

        private void ExtractPNGFromImage(WzImage image)
        {
            WzCanvasProperty npcImage = WzInfoTools.GetNpcImage(image);
            if (npcImage != null)
            {
                Image = npcImage.PngProperty.GetPNG(false);
                Origin = WzInfoTools.VectorToSystemPoint((WzVectorProperty)npcImage["origin"]);
            }
            else
            {
                Image = new Bitmap(1, 1);
                Origin = new System.Drawing.Point();
            }
        }

        public override void ParseImage()
        {
            WzStringProperty link = (WzStringProperty)((WzSubProperty)((WzImage)ParentObject)["info"])["link"];
            if (link != null)
            {
                LinkedImage = (WzImage)Program.WzManager["npc"][link.Value + ".img"];
                ExtractPNGFromImage(LinkedImage);
            }
            else
            {
                ExtractPNGFromImage((WzImage)ParentObject);
            }
        }

        public static NpcInfo Load(WzImage parentObject)
        {
            string id = /*WzInfoTools.RemoveLeadingZeros(*/WzInfoTools.RemoveExtension(parentObject.Name)/*)*/;
            return new NpcInfo(null, new System.Drawing.Point(), id, WzInfoTools.GetNpcNameById(id), parentObject);
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip)
        {
            if (Image == null) ParseImage();
            return new NPCInstance(this, board, x, y, UserSettings.Npcrx0Offset, UserSettings.Npcrx1Offset, 8, null, 0, flip, false, null, null);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int rx0Shift, int rx1Shift, int yShift, string limitedname, int? mobTime, MapleBool flip, MapleBool hide, int? info, int? team)
        {
            if (Image == null) ParseImage();
            return new NPCInstance(this, board, x, y, rx0Shift, rx1Shift, yShift, limitedname, mobTime, flip, hide, info, team);
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = value;
            }
        }
    }

    public class PortalInfo : MapleDrawableInfo
    {
        private string type;

        public PortalInfo(string type, Bitmap image, System.Drawing.Point origin, WzObject parentObject)
            : base(image, origin, parentObject)
        {
            this.type = type;
        }

        public static PortalInfo Load(WzCanvasProperty parentObject)
        {
           PortalInfo portal = new PortalInfo(parentObject.Name, parentObject.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)parentObject["origin"]), parentObject);
           Program.InfoManager.Portals.Add(portal.type, portal);
           return portal;
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip)
        {
            switch (type)
            {
                case PortalType.PORTALTYPE_STARTPOINT:
                    return new PortalInstance(this, board, x, y, "sp", type, "", 999999999, null, null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_INVISIBLE:
                case PortalType.PORTALTYPE_VISIBLE:
                case PortalType.PORTALTYPE_COLLISION:
                case PortalType.PORTALTYPE_CHANGABLE:
                case PortalType.PORTALTYPE_CHANGABLE_INVISIBLE:
                    return new PortalInstance(this, board, x, y, "portal", type, "", 999999999, null, null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_TOWNPORTAL_POINT:
                    return new PortalInstance(this, board, x, y, "tp", type, "", 999999999, null, null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_SCRIPT:
                case PortalType.PORTALTYPE_SCRIPT_INVISIBLE:
                case PortalType.PORTALTYPE_COLLISION_SCRIPT:
                    return new PortalInstance(this, board, x, y, "portal", type, "", 999999999, "script", null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_HIDDEN:
                    return new PortalInstance(this, board, x, y, "portal", type, "", 999999999, null, null, null, null, null, null, "", null, null);
                case PortalType.PORTALTYPE_SCRIPT_HIDDEN:
                    return new PortalInstance(this, board, x, y, "portal", type, "", 999999999, "script", null, null, null, null, null, "", null, null);
                case PortalType.PORTALTYPE_COLLISION_VERTICAL_JUMP:
                case PortalType.PORTALTYPE_COLLISION_CUSTOM_IMPACT:
                case PortalType.PORTALTYPE_COLLISION_UNKNOWN_PCIG:
                    return new PortalInstance(this, board, x, y, "portal", type, "", 999999999, "script", null, null, null, null, null, "", null, null);
                default:
                    throw new Exception("unknown pt @ CreateInstance");
            }
        }

        public PortalInstance CreateInstance(Board board, int x, int y, string pn, string tn, int tm, string script, int? delay, MapleBool hideTooltip, MapleBool onlyOnce, int? horizontalImpact, int? verticalImpact, string image, int? hRange, int? vRange)
        {
            return new PortalInstance(this, board, x, y, pn, type, tn, tm, script, delay, hideTooltip, onlyOnce, horizontalImpact, verticalImpact, image, hRange, vRange);
        }

        public static PortalInfo GetPortalInfoByType(string type)
        {
            return Program.InfoManager.Portals[type];
        }
    }

    public class ReactorInfo : MapleExtractableInfo
    {
        private string id;

        private WzImage LinkedImage;

        public ReactorInfo(Bitmap image, System.Drawing.Point origin, string id, WzObject parentObject)
            : base(image, origin, parentObject)
        {
            this.id = id;
        }

        private void ExtractPNGFromImage(WzImage image)
        {
            WzCanvasProperty reactorImage = WzInfoTools.GetReactorImage(image);
            if (reactorImage != null)
            {
                Image = reactorImage.PngProperty.GetPNG(false);
                Origin = WzInfoTools.VectorToSystemPoint((WzVectorProperty)reactorImage["origin"]);
            }
            else
            {
                Image = new Bitmap(1, 1);
                Origin = new System.Drawing.Point();
            }
        }

        public override void ParseImage()
        {
            WzStringProperty link = (WzStringProperty)((WzSubProperty)((WzImage)ParentObject)["info"])["link"];
            if (link != null)
            {
                LinkedImage = (WzImage)Program.WzManager["reactor"][link.Value + ".img"];
                ExtractPNGFromImage(LinkedImage);
            }
            else
            {
                ExtractPNGFromImage((WzImage)ParentObject);
            }
        }

        public static ReactorInfo Load(WzImage parentObject)
        {
            return new ReactorInfo(null, new System.Drawing.Point(), /*WzInfoTools.RemoveLeadingZeros(*/WzInfoTools.RemoveExtension(parentObject.Name)/*)*/, parentObject);
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip)
        {
            if (Image == null) ParseImage();
            return new ReactorInstance(this, board, x, y, UserSettings.defaultReactorTime, "", flip);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int reactorTime, string name, bool flip)
        {
            if (Image == null) ParseImage();
            return new ReactorInstance(this, board, x, y, reactorTime, name, flip);
        }

        public string ID
        {
            get
            {
                return id;
            }
            set
            {
                this.id = value;
            }
        }
    }

    public class BackgroundInfo : MapleDrawableInfo
    {
        private string _bS;
        private bool _ani;
        private string _no;

        public BackgroundInfo(Bitmap image, System.Drawing.Point origin, string bS, bool ani, string no, WzObject parentObject)
            : base(image, origin, parentObject)
        {
            _bS = bS;
            _ani = ani;
            _no = no;
        }

        public static BackgroundInfo Load(WzImageProperty parentObject)
        {
            string[] path = parentObject.FullPath.Split(@"\".ToCharArray());
            return Load(parentObject,WzInfoTools.RemoveExtension(path[path.Length - 3]), path[path.Length - 2] == "ani", path[path.Length - 1]);
        }

        public static BackgroundInfo Load(WzImageProperty parentObject, string bS, bool ani, string no)
        {
            WzCanvasProperty frame0 = ani ?(WzCanvasProperty)WzInfoTools.GetRealProperty(parentObject["0"]) : (WzCanvasProperty)WzInfoTools.GetRealProperty(parentObject);
            return new BackgroundInfo(frame0.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)frame0["origin"]), bS, ani, no, parentObject);
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip)
        {
            return new BackgroundInstance(this, board, x, y, z, -100, -100, 0, 0, 0, 255, false, flip);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int z, int rx, int ry, int cx, int cy, BackgroundType type, int a, bool front, bool flip)
        {
            return new BackgroundInstance(this, board, x, y, z, rx, ry, cx, cy, type, a, front, flip);
        }

        public string bS
        {
            get
            {
                return _bS;
            }
            set
            {
                this._bS = value;
            }
        }

        public bool ani
        {
            get
            {
                return _ani;
            }
            set
            {
                this._ani = value;
            }
        }

        public string no
        {
            get
            {
                return _no;
            }
            set
            {
                this._no = value;
            }
        }
    }
}
