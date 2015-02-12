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

namespace HaCreator.MapEditor
{
    public abstract class MapleDrawableInfo
    {
        private Bitmap image;
        private Texture2D texture;
        private System.Drawing.Point origin;
        private IWzObject parentObject;
        int width;
        int height;

        public MapleDrawableInfo(Bitmap image, System.Drawing.Point origin, IWzObject parentObject)
        {
            this.Image = image;
            this.origin = origin;
            this.parentObject = parentObject;
        }

        public void CreateTexture(GraphicsDevice device)
        {
            texture = BoardItem.TextureFromBitmap(device, image);
        }

        public abstract BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding);

        public virtual Texture2D GetTexture(SpriteBatch sprite)
        {
            if (texture == null) CreateTexture(sprite.GraphicsDevice);
            return texture;
        }

        public virtual IWzObject ParentObject
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
        public MapleExtractableInfo(Bitmap image, System.Drawing.Point origin, IWzObject parentObject)
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
        //private List<XNA.Point> ropeOffsets = new List<XNA.Point>();
        private List<XNA.Point> chairOffsets = null;
        private List<List<XNA.Point>> footholdFullOffsets = null;
        private bool fullFootholdInfo = false;


        public ObjectInfo(Bitmap image, System.Drawing.Point origin, string oS, string l0, string l1, string l2, IWzObject parentObject)
            : base(image, origin, parentObject)
        {
            this._oS = oS;
            this._l0 = l0;
            this._l1 = l1;
            this._l2 = l2;
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
            WzSubProperty chairs = (WzSubProperty)parentObject["seat"];
            IWzImageProperty footholds = (IWzImageProperty)frame1["foothold"];
            if (footholds != null)
            {
                if (footholds is WzConvexProperty)
                {
                    result.fullFootholdInfo = false;
                    result.footholdOffsets = new List<XNA.Point>();
                    foreach (WzVectorProperty fhAnchor in footholds.WzProperties)
                        result.footholdOffsets.Add(WzInfoTools.VectorToXNAPoint(fhAnchor));
                }
                else
                {
                    result.fullFootholdInfo = true;
                    result.footholdFullOffsets = new List<List<XNA.Point>>();
                    List<XNA.Point> fhAnchorList = new List<XNA.Point>();
                    foreach (WzConvexProperty fh in footholds.WzProperties)
                    {
                        foreach (WzVectorProperty fhAnchor in fh.WzProperties)
                            fhAnchorList.Add(WzInfoTools.VectorToXNAPoint(fhAnchor));
                        result.footholdFullOffsets.Add(fhAnchorList);
                        fhAnchorList = new List<XNA.Point>();
                    }
                }
            }
            //IWzImageProperty ropes = (IWzImageProperty)frame1["ladder"];
            if (chairs != null)
            {
                result.chairOffsets = new List<XNA.Point>();
                foreach (WzVectorProperty chair in chairs.WzProperties)
                    result.chairOffsets.Add(WzInfoTools.VectorToXNAPoint(chair));
            }
            /*if (footholds != null)
                foreach (WzVectorProperty foothold in footholds.WzProperties)
                    result.footholdOffsets.Add(WzInfoTools.VectorToXNAPoint(foothold));*/
            /*if (ropes != null && ropes.WzProperties.Count > 0)
                if (ropes.WzProperties[0] is WzVectorProperty)
                    foreach (WzVectorProperty rope in ropes.WzProperties)
                        result.ropeOffsets.Add(WzInfoTools.VectorToXNAPoint(rope));
                else if (ropes.WzProperties[0] is WzConvexProperty)
                    foreach (WzConvexProperty convex in ropes.WzProperties)
                        foreach (WzVectorProperty rope in convex.WzProperties)
                            result.ropeOffsets.Add(WzInfoTools.VectorToXNAPoint(rope));
                else throw new Exception("wrong rope anchor type at ObjectInfo Load");*/
            return result;
        }

        public static void Reload(ObjectInfo objToReload)
        {
            objToReload = Load((WzSubProperty)objToReload.ParentObject, objToReload.oS,objToReload.l0,objToReload.l1,objToReload.l2);
        }

        private void CreateFootholdsFromAnchorList(Board board, List<FootholdAnchor> anchors)
        {
            for (int i = 0; i < anchors.Count - 1; i++)
            {
                FootholdLine fh = new FootholdLine(board, anchors[i], anchors[i + 1]);
                board.BoardItems.FootholdLines.Add(fh);
            }
        }

        public void ParseOffsets(ObjectInstance instance, Layer layer, Board board, int x, int y)
        {
            List<FootholdAnchor> anchors = new List<FootholdAnchor>();
            bool ladder = l0 == "ladder";
            if (footholdOffsets != null || footholdFullOffsets != null)
            {
                if (!fullFootholdInfo)
                {
                    foreach (XNA.Point foothold in footholdOffsets)
                    {
                        FootholdAnchor anchor = new FootholdAnchor(board, x + foothold.X, y + foothold.Y, layer.LayerNumber, false);
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
                            FootholdAnchor anchor = new FootholdAnchor(board, x + foothold.X, y + foothold.Y, layer.LayerNumber, false);
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
                Chair chair = new Chair(board, x + chairPos.X, y + chairPos.Y, false);
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

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding)
        {
            ObjectInstance instance = new ObjectInstance(this, layer, board, x, y, z, false, false, false, false, null, null, null, null, null, null, null, flip, beforeAdding);
            ParseOffsets(instance, layer, board, x, y);
            return instance;
        }

        public BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, MapleBool r, MapleBool hide, MapleBool reactor, MapleBool flow, int? rx, int? ry, int? cx, int? cy, string name, string tags, List<ObjectInstanceQuest> questInfo, bool flip, bool beforeAdding, bool parseOffsets)
        {
            ObjectInstance instance = new ObjectInstance(this, layer, board, x, y, z, r, hide, reactor, flow, rx, ry, cx, cy, name, tags, questInfo, flip, beforeAdding);
            if (parseOffsets) ParseOffsets(instance, layer, board, x, y);
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

/*        public List<XNA.Point> RopeOffsets
        {
            get
            {
                return ropeOffsets;
            }
        }*/
    }

    public class TileInfo : MapleDrawableInfo
    {
        private string _tS;
        private string _u;
        private string _no;
        private List<XNA.Point> footholdOffsets = new List<XNA.Point>();

        public TileInfo(Bitmap image, System.Drawing.Point origin, string tS, string u, string no, IWzObject parentObject)
            : base(image, origin, parentObject)
        {
            this._tS = tS;
            this._u = u;
            this._no = no;
        }

        public static TileInfo Load(WzCanvasProperty parentObject)
        {
            string[] path = parentObject.FullPath.Split(@"\".ToCharArray());
            return Load(parentObject,WzInfoTools.RemoveExtension(path[path.Length - 3]), path[path.Length - 2], path[path.Length - 1]);
        }

        public static TileInfo Load(WzCanvasProperty parentObject, string tS, string u, string no)
        {
            TileInfo result = new TileInfo(parentObject.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)parentObject["origin"]), tS,u,no, parentObject);
            WzConvexProperty footholds = (WzConvexProperty)parentObject["foothold"];
            if (footholds != null)
                foreach (WzVectorProperty foothold in footholds.WzProperties)
                    result.footholdOffsets.Add(WzInfoTools.VectorToXNAPoint(foothold));
            return result;
        }

        public static void Reload(TileInfo objToReload)
        {
            objToReload = Load((WzCanvasProperty)objToReload.ParentObject, objToReload.tS,objToReload.u,objToReload.no);
        }

        public void ParseOffsets(TileInstance instance, Board board, Layer layer, int x, int y)
        {
            List<FootholdAnchor> anchors = new List<FootholdAnchor>();
            foreach (XNA.Point foothold in footholdOffsets)
            {
                FootholdAnchor anchor = new FootholdAnchor(board, x + foothold.X, y + foothold.Y, layer.LayerNumber, false);
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

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding)
        {
            TileInstance instance = new TileInstance(this, layer, board, x, y, z, beforeAdding);
            ParseOffsets(instance, board, layer, x, y);
            return instance;
        }

        public BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding, bool parseOffsets)
        {
            TileInstance instance = new TileInstance(this, layer, board, x, y, z, beforeAdding);
            if (parseOffsets) ParseOffsets(instance, board, layer, x, y);
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

        public List<XNA.Point> FootholdOffsets
        {
            get
            {
                return footholdOffsets;
            }
        }
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

        public MobInfo(Bitmap image, System.Drawing.Point origin, string id, string name, IWzObject parentObject)
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
                LinkedImage = (WzImage)Program.WzManager.GetWzFileByName("mob")[link.Value + ".img"];
                ExtractPNGFromImage(LinkedImage);
            }
            else ExtractPNGFromImage((WzImage)ParentObject);
        }

        public static MobInfo Load(WzImage parentObject)
        {
            string id = /*WzInfoTools.RemoveLeadingZeros(*/WzInfoTools.RemoveExtension(parentObject.Name)/*)*/;
            return new MobInfo(null, new System.Drawing.Point(), id, WzInfoTools.GetMobNameById(id), parentObject);
        }

        public static void Reload(MobInfo objToReload)
        {
            if (objToReload.Image == null)
                objToReload = Load((WzImage)objToReload.ParentObject);
            else
            {
                objToReload = Load((WzImage)objToReload.ParentObject);
                objToReload.ParseImage();
            }
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding)
        {
            if (Image == null) ParseImage();
            return new MobInstance(this, board, x, y, x - UserSettings.Mobrx0Offset, x + UserSettings.Mobrx1Offset, null, UserSettings.defaultMobTime, flip, false, null, null, beforeAdding);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int rx0, int rx1, string limitedname, int? mobTime, MapleBool flip, MapleBool hide, int? info, int? team, bool beforeAdding)
        {
            if (Image == null) ParseImage();
            return new MobInstance(this, board, x, y, x - UserSettings.Mobrx0Offset, x + UserSettings.Mobrx1Offset, limitedname, UserSettings.defaultMobTime, flip, hide, info, team, beforeAdding);
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

        /*public int level
        {
            get { return _level; }
            set { _level = value; }
        }

        public int maxHP
        {
            get { return _maxHP; }
            set { _maxHP = value; }
        }

        public int maxMP
        {
            get { return _maxMP; }
            set { _maxMP = value; }
        }

        public int PADamage
        {
            get { return _PADamage; }
            set { _PADamage = value; }
        }

        public int MADamage
        {
            get { return _MADamage; }
            set { _MADamage = value; }
        }

        public int PDDamage
        {
            get { return _PDDamage; }
            set { _PDDamage = value; }
        }

        public int MDDamage
        {
            get { return _MDDamage; }
            set { _MDDamage = value; }
        }

        public int speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public int acc
        {
            get { return _acc; }
            set { _acc = value; }
        }

        public int eva
        {
            get { return _eva; }
            set { _eva = value; }
        }

        public int pushed
        {
            get { return _pushed; }
            set { _pushed = value; }
        }

        public int exp
        {
            get { return _exp; }
            set { _exp = value; }
        }

        public int hpRecovery
        {
            get { return _hpRecovery; }
            set { _hpRecovery = value; }
        }

        public int mpRecovery
        {
            get { return _mpRecovery; }
            set { _mpRecovery = value; }
        }

        public bool boss
        {
            get { return _boss; }
            set { _boss = value; }
        }

        public bool undead
        {
            get { return _undead; }
            set { _undead = value; }
        }

        public bool firstAttack
        {
            get { return _firstAttack; }
            set { _firstAttack = value; }
        }*/
    }

    public class NpcInfo : MapleExtractableInfo
    {
        private string id;
        private string name;

        private WzImage LinkedImage;

        public NpcInfo(Bitmap image, System.Drawing.Point origin, string id, string name, IWzObject parentObject)
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
                LinkedImage = (WzImage)Program.WzManager.GetWzFileByName("npc")[link.Value + ".img"];
                ExtractPNGFromImage(LinkedImage);
            }
            else ExtractPNGFromImage((WzImage)ParentObject);
        }

        public static NpcInfo Load(WzImage parentObject)
        {
            string id = /*WzInfoTools.RemoveLeadingZeros(*/WzInfoTools.RemoveExtension(parentObject.Name)/*)*/;
            return new NpcInfo(null, new System.Drawing.Point(), id, WzInfoTools.GetNpcNameById(id), parentObject);
        }

        public static void Reload(NpcInfo objToReload)
        {
            if (objToReload.Image == null)
                objToReload = Load((WzImage)objToReload.ParentObject);
            else
            {
                objToReload = Load((WzImage)objToReload.ParentObject);
                objToReload.ParseImage();
            }
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding)
        {
            if (Image == null) ParseImage();
            return new NPCInstance(this, board, x, y, x - UserSettings.Npcrx0Offset, x + UserSettings.Npcrx1Offset, null, 0, flip, false, null, null, beforeAdding);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int rx0, int rx1, string limitedname, int? mobTime, MapleBool flip, MapleBool hide, int? info, int? team, bool beforeAdding)
        {
            if (Image == null) ParseImage();
            return new NPCInstance(this, board, x, y, x - UserSettings.Mobrx0Offset, x + UserSettings.Mobrx1Offset, limitedname, mobTime, flip, hide, info, team, beforeAdding);
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
        public static MapleTable<PortalType> ptByShortName = CreatePortalTypeHashtable();

        private PortalType type;

        public PortalInfo(PortalType type, Bitmap image, System.Drawing.Point origin, IWzObject parentObject)
            : base(image, origin, parentObject)
        {
            this.type = type;
        }

        public static MapleTable<PortalType> CreatePortalTypeHashtable()
        {
            MapleTable<PortalType> result = new MapleTable<PortalType>();
            result.Add("sp", PortalType.PORTALTYPE_STARTPOINT);
            result.Add("pi", PortalType.PORTALTYPE_INVISIBLE);
            result.Add("pv", PortalType.PORTALTYPE_VISIBLE);
            result.Add("pc", PortalType.PORTALTYPE_COLLISION);
            result.Add("pg", PortalType.PORTALTYPE_CHANGABLE);
            result.Add("pgi", PortalType.PORTALTYPE_CHANGABLE_INVISIBLE);
            result.Add("tp", PortalType.PORTALTYPE_TOWNPORTAL_POINT);
            result.Add("ps", PortalType.PORTALTYPE_SCRIPT);
            result.Add("psi", PortalType.PORTALTYPE_SCRIPT_INVISIBLE);
            result.Add("pcs", PortalType.PORTALTYPE_COLLISION_SCRIPT);
            result.Add("ph", PortalType.PORTALTYPE_HIDDEN);
            result.Add("psh", PortalType.PORTALTYPE_SCRIPT_HIDDEN);
            result.Add("pcj", PortalType.PORTALTYPE_COLLISION_VERTICAL_JUMP);
            result.Add("pci", PortalType.PORTALTYPE_COLLISION_CUSTOM_IMPACT);
            result.Add("pcig", PortalType.PORTALTYPE_COLLISION_UNKNOWN_PCIG);
            return result;
        }


        public static PortalInfo Load(WzCanvasProperty parentObject)
        {
           PortalInfo portal = new PortalInfo((PortalType)ptByShortName[parentObject.Name], parentObject.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)parentObject["origin"]), parentObject);
           Program.InfoManager.Portals[(int)ptByShortName[parentObject.Name]] = portal;
           return portal;
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding)
        {
            switch (type)
            {
                case PortalType.PORTALTYPE_STARTPOINT:
                    return new PortalInstance(this, board, x, y, beforeAdding, "sp", type, "", 999999999, null, null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_INVISIBLE:
                case PortalType.PORTALTYPE_VISIBLE:
                case PortalType.PORTALTYPE_COLLISION:
                case PortalType.PORTALTYPE_CHANGABLE:
                case PortalType.PORTALTYPE_CHANGABLE_INVISIBLE:
                    return new PortalInstance(this, board, x, y, beforeAdding, "portal", type, "", 999999999, null, null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_TOWNPORTAL_POINT:
                    return new PortalInstance(this, board, x, y, beforeAdding, "tp", type, "", 999999999, null, null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_SCRIPT:
                case PortalType.PORTALTYPE_SCRIPT_INVISIBLE:
                case PortalType.PORTALTYPE_COLLISION_SCRIPT:
                    return new PortalInstance(this, board, x, y, beforeAdding, "portal", type, "", 999999999, "script", null, null, null, null, null, null, null, null);
                case PortalType.PORTALTYPE_HIDDEN:
                    return new PortalInstance(this, board, x, y, beforeAdding, "portal", type, "", 999999999, null, null, null, null, null, null, "", null, null);
                case PortalType.PORTALTYPE_SCRIPT_HIDDEN:
                    return new PortalInstance(this, board, x, y, beforeAdding, "portal", type, "", 999999999, "script", null, null, null, null, null, "", null, null);
                case PortalType.PORTALTYPE_COLLISION_VERTICAL_JUMP:
                case PortalType.PORTALTYPE_COLLISION_CUSTOM_IMPACT:
                case PortalType.PORTALTYPE_COLLISION_UNKNOWN_PCIG:
                    return new PortalInstance(this, board, x, y, beforeAdding, "portal", type, "", 999999999, "script", null, null, null, null, null, "", null, null);
                default:
                    throw new Exception("unknown pt @ CreateInstance");
            }
        }

        public PortalInstance CreateInstance(Board board, int x, int y, bool beforeAdding, string pn, string tn, int tm, string script, int? delay, MapleBool hideTooltip, MapleBool onlyOnce, int? horizontalImpact, int? verticalImpact, string image, int? hRange, int? vRange)
        {
            return new PortalInstance(this, board, x, y, beforeAdding, pn, type, tn, tm, script, delay, hideTooltip, onlyOnce, horizontalImpact, verticalImpact, image, hRange, vRange);
        }

        public static PortalInfo GetPortalInfoByType(PortalType type)
        {
            return Program.InfoManager.Portals[(int)type];
        }
    }

    public class ReactorInfo : MapleExtractableInfo
    {
        private string id;

        private WzImage LinkedImage;

        public ReactorInfo(Bitmap image, System.Drawing.Point origin, string id, IWzObject parentObject)
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
                LinkedImage = (WzImage)Program.WzManager.GetWzFileByName("reactor")[link.Value + ".img"];
                ExtractPNGFromImage(LinkedImage);
            }
            else ExtractPNGFromImage((WzImage)ParentObject);
        }

        public static ReactorInfo Load(WzImage parentObject)
        {
            return new ReactorInfo(null, new System.Drawing.Point(), /*WzInfoTools.RemoveLeadingZeros(*/WzInfoTools.RemoveExtension(parentObject.Name)/*)*/, parentObject);
        }

        public static void Reload(ReactorInfo objToReload)
        {
            if (objToReload.Image == null)
                objToReload = Load((WzImage)objToReload.ParentObject);
            else
            {
                objToReload = Load((WzImage)objToReload.ParentObject);
                objToReload.ParseImage();
            }
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding)
        {
            if (Image == null) ParseImage();
            return new ReactorInstance(this, board, x, y, UserSettings.defaultReactorTime, "", flip, beforeAdding);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int reactorTime, string name, bool flip, bool beforeAdding)
        {
            if (Image == null) ParseImage();
            return new ReactorInstance(this, board, x, y, reactorTime, name, flip, beforeAdding);
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

        public BackgroundInfo(Bitmap image, System.Drawing.Point origin, string bS, bool ani, string no, IWzObject parentObject)
            : base(image, origin, parentObject)
        {
            _bS = bS;
            _ani = ani;
            _no = no;
        }

        public static BackgroundInfo Load(IWzImageProperty parentObject)
        {
            string[] path = parentObject.FullPath.Split(@"\".ToCharArray());
            return Load(parentObject,WzInfoTools.RemoveExtension(path[path.Length - 3]), path[path.Length - 2] == "ani", path[path.Length - 1]);
        }

        public static BackgroundInfo Load(IWzImageProperty parentObject, string bS, bool ani, string no)
        {
            WzCanvasProperty frame0 = ani ?(WzCanvasProperty)WzInfoTools.GetRealProperty(parentObject["0"]) : (WzCanvasProperty)WzInfoTools.GetRealProperty(parentObject);
            return new BackgroundInfo(frame0.PngProperty.GetPNG(false), WzInfoTools.VectorToSystemPoint((WzVectorProperty)frame0["origin"]), bS, ani, no, parentObject);
        }

        public static void Reload(BackgroundInfo objToReload)
        {
                objToReload = Load((IWzImageProperty)objToReload.ParentObject,objToReload.bS,objToReload.ani,objToReload.no);
        }

        public override BoardItem CreateInstance(Layer layer, Board board, int x, int y, int z, bool flip, bool beforeAdding)
        {
            return new BackgroundInstance(this, board, x, y, z, -100, -100, 0, 0, 0, 255, false, flip, beforeAdding);
        }

        public BoardItem CreateInstance(Board board, int x, int y, int z, int rx, int ry, int cx, int cy, BackgroundType type, int a, bool front, bool flip, bool beforeAdding)
        {
            return new BackgroundInstance(this, board, x, y, z, rx, ry, cx, cy, type, a, front, flip, beforeAdding);
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
