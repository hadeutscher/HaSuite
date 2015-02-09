/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

//uncomment line below to use XNA's Z-order functions
//#define UseXNAZorder

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MapleLib.WzLib.WzStructure.Data;

namespace HaCreator.MapEditor
{
    public partial class MultiBoard : UserControl, IServiceProvider
    {
        #region Settings
        private const int ScrollbarWidth = 16;

        public static float FirstSnapVerification = UserSettings.SnapDistance * 20;
        public static Color InactiveColor = CreateTransparency(Color.White, UserSettings.NonActiveAlpha);
        public static Color RopeInactiveColor = CreateTransparency(UserSettings.RopeColor, UserSettings.NonActiveAlpha);
        public static Color FootholdInactiveColor = CreateTransparency(UserSettings.FootholdColor, UserSettings.NonActiveAlpha);
        public static Color ChairInactiveColor = CreateTransparency(UserSettings.ChairColor, UserSettings.NonActiveAlpha);
        public static Color ToolTipInactiveColor = CreateTransparency(UserSettings.ToolTipColor, UserSettings.NonActiveAlpha);
        public static Color MiscInactiveColor = CreateTransparency(UserSettings.MiscColor, UserSettings.NonActiveAlpha);
        public static Color BackgroundCopyColor = CreateTransparency(Color.White, UserSettings.NonActiveAlpha / 2);

        public static Color CreateTransparency(Color orgColor, int alpha)
        {
            return new Color(orgColor.R, orgColor.B, orgColor.G, alpha);
        }

        public static void RecalculateSettings()
        {
            /*float*/ int alpha = UserSettings.NonActiveAlpha/* / 255f*/;
            FirstSnapVerification = UserSettings.SnapDistance * 20;
            InactiveColor = CreateTransparency(Color.White, alpha);
            RopeInactiveColor = CreateTransparency(UserSettings.RopeColor, alpha);
            FootholdInactiveColor = CreateTransparency(UserSettings.FootholdColor, alpha);
            ChairInactiveColor = CreateTransparency(UserSettings.ChairColor, alpha);
            ToolTipInactiveColor = CreateTransparency(UserSettings.ToolTipColor, alpha);
        }
        #endregion

        #region Fields
        private bool deviceReady = false;
        private GraphicsDevice DxDevice;
        private SpriteBatch sprite;
        private int frameCount = 0;
        private int lastFps = 0;
        private PresentationParameters pParams = new PresentationParameters();
        private Texture2D pixel;
        private List<Board> boards = new List<Board>();
        private Board selectedBoard = null;
        private IGraphicsDeviceService graphicsDeviceService;
        private ContentManager contentMan;
        //private SpriteFont defaultFont;
        private FontEngine fontEngine;
        private Form form;
        #endregion

        public MultiBoard()
        {
            InitializeComponent();
            ResetDock();
        }

        #region Initialization
        public void Start()
        {
            if (deviceReady) return;
            if (selectedBoard == null) throw new Exception("Cannot start without a selected board");
            Visible = true;
            FPSReset.Enabled = true;
            Renderer.Enabled = true;
            ResetDock();
            AdjustScrollBars();
            PrepareDevice();
            pixel = CreatePixel();
            form = FindForm();
            deviceReady = true;
        }

        private void AdjustScrollBars()
        {
            hScrollBar.LargeChange = 0;
            vScrollBar.LargeChange = 0;
            if (MapSize.X > DxContainer.Width)
            {
                hScrollBar.Enabled = true;
                hScrollBar.Maximum = MapSize.X - DxContainer.Width;
                hScrollBar.Minimum = 0;
                if (hScrollBar.Maximum < selectedBoard.hScroll)
                {
                    hScrollBar.Value = hScrollBar.Maximum - 1;
                    selectedBoard.hScroll = hScrollBar.Value;
                }
                else { hScrollBar.Value = selectedBoard.hScroll; }
            }
            else { hScrollBar.Enabled = false; hScrollBar.Value = 0; hScrollBar.Maximum = 0; }
            if (MapSize.Y > DxContainer.Height)
            {
                vScrollBar.Enabled = true;
                vScrollBar.Maximum = MapSize.Y - DxContainer.Height;
                vScrollBar.Minimum = 0;
                if (vScrollBar.Maximum < selectedBoard.vScroll)
                {
                    vScrollBar.Value = vScrollBar.Maximum - 1;
                    selectedBoard.vScroll = vScrollBar.Value;
                }
                else { vScrollBar.Value = selectedBoard.vScroll; }
            }
            else { vScrollBar.Enabled = false; vScrollBar.Value = 0; vScrollBar.Maximum = 0; }
        }

        private void PrepareDevice()
        {
            pParams.BackBufferWidth = Math.Max(DxContainer.Width, 1);
            pParams.BackBufferHeight = Math.Max(DxContainer.Height, 1);
            pParams.BackBufferFormat = SurfaceFormat.Color;
            //pParams.EnableAutoDepthStencil = true;
            pParams.DepthStencilFormat = DepthFormat.Depth24;
            pParams.DeviceWindowHandle = DxContainer.Handle;
            pParams.IsFullScreen = false;
            //pParams.AutoDepthStencilFormat = DepthFormat.Depth24;
            /*try
            {
                DxDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware, DxContainer.Handle, pParams);
            }
            catch
            {
                DxDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.NullReference, DxContainer.Handle, pParams);
            }*/
            try
            {
                GraphicsProfile profile = GraphicsProfile.Reach;
                if (GraphicsAdapter.DefaultAdapter.IsProfileSupported(GraphicsProfile.HiDef))
                    profile = GraphicsProfile.HiDef;
                else if (!GraphicsAdapter.DefaultAdapter.IsProfileSupported(GraphicsProfile.Reach))
                    throw new NotSupportedException();
                DxDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, profile, pParams);
            }
            catch
            {
                HaRepackerLib.Warning.Error("Graphics adapter is not supported");
                Environment.Exit(1);
            }
            graphicsDeviceService = new GraphicsDeviceService(DxDevice);
            contentMan = new ContentManager(this);
            //defaultFont = contentMan.Load<SpriteFont>("Arial");
            fontEngine = new FontEngine(UserSettings.FontName, UserSettings.FontStyle, UserSettings.FontSize, DxDevice);
            sprite = new SpriteBatch(DxDevice);
        }

        private void ResetDock()
        {
            vScrollBar.Location = new System.Drawing.Point(Width - ScrollbarWidth, 0);
            hScrollBar.Location = new System.Drawing.Point(0, Height - ScrollbarWidth);
            vScrollBar.Size = new System.Drawing.Size(ScrollbarWidth, Height - ScrollbarWidth);
            hScrollBar.Size = new System.Drawing.Size(Width - ScrollbarWidth, ScrollbarWidth);
            DxContainer.Location = new System.Drawing.Point(0, 0);
            DxContainer.Size = new System.Drawing.Size(Width - ScrollbarWidth, Height - ScrollbarWidth);
        }

        private bool needsReset = false;

        private void ResetDevice()
        {
            if (form.WindowState == FormWindowState.Minimized) return;
            pParams.BackBufferHeight = DxContainer.Height;
            pParams.BackBufferWidth = DxContainer.Width;
            pParams.BackBufferFormat = SurfaceFormat.Color;
            //pParams.EnableAutoDepthStencil = true;
            pParams.DepthStencilFormat = DepthFormat.Depth24;
            pParams.DeviceWindowHandle = DxContainer.Handle;
            //pParams.AutoDepthStencilFormat = DepthFormat.Depth24;
            DxDevice.Reset(pParams);
        }

        private Texture2D CreatePixel()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(1, 1);
            bmp.SetPixel(0, 0, System.Drawing.Color.White);
            return BoardItem.TextureFromBitmap(DxDevice, bmp);
        }
        #endregion

        #region Overrides
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int oldvalue = vScrollBar.Value;
            int scrollValue = e.Delta / 10;
            if (vScrollBar.Value - scrollValue < vScrollBar.Minimum)
                vScrollBar.Value = vScrollBar.Minimum;
            else if (vScrollBar.Value - scrollValue > vScrollBar.Maximum)
                vScrollBar.Value = vScrollBar.Maximum;
            else
                vScrollBar.Value -= scrollValue;
            vScrollBar_Scroll(null, null);
            base.OnMouseWheel(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Width == 0 && Height == 0) return;
            ResetDock();
            if (deviceReady)
            {
                //ResetDevice();
                needsReset = true;
            }
            if (selectedBoard != null) AdjustScrollBars();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (!deviceReady)
            {
                base.OnPaint(e);
                return;
            }
            RenderFrame();
        }

        #endregion

        #region Methods
        public Board CreateBoard(Point mapSize, Point centerPoint, int layers)
        {
            Board newBoard = new Board(mapSize, centerPoint, this, ApplicationSettings.theoreticalVisibleTypes, ApplicationSettings.theoreticalEditedTypes);
            newBoard.CreateLayers(layers);
            return newBoard;
        }

        public void DrawLine(SpriteBatch sprite, Vector2 start, Vector2 end, Color color)
        {
            int width = (int)Vector2.Distance(start, end);
            float rotation = (float)Math.Atan2((double)(end.Y - start.Y), (double)(end.X - start.X));
            sprite.Draw(pixel, new Rectangle((int)start.X, (int)start.Y, width, UserSettings.LineWidth), null, color, rotation, new Vector2(0f, 0f), SpriteEffects.None, 1f);
        }

        public void DrawRectangle(SpriteBatch sprite, Rectangle rectangle, Color color)
        {
            //clockwise
            Vector2 pt1 = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 pt2 = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 pt3 = new Vector2(rectangle.Right, rectangle.Bottom);
            Vector2 pt4 = new Vector2(rectangle.Left, rectangle.Bottom);
            DrawLine(sprite, pt1, pt2, color);
            DrawLine(sprite, pt2, pt3, color);
            DrawLine(sprite, pt3, pt4, color);
            DrawLine(sprite, pt4, pt1, color);
        }

        public void FillRectangle(SpriteBatch sprite, Rectangle rectangle, Color color)
        {
            sprite.Draw(pixel, rectangle, color);
        }

        public void RenderFrame()
        {
            if (!deviceReady || form.WindowState == FormWindowState.Minimized) return;
            frameCount++;
            if (needsReset)
            {
                ResetDevice();
            }
            DxDevice.Clear(ClearOptions.Target, Color.White, 1.0f, 0); // Clear the window to black
#if UseXNAZorder
            sprite.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.FrontToBack, SaveStateMode.None);
#else
            //sprite.Begin(/*SpriteBlendMode.AlphaBlend*/);
            sprite.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
#endif
            selectedBoard.RenderBoard(sprite);
            if (selectedBoard.MapSize.X < DxContainer.Width)
            {
                DrawLine(sprite, new Vector2(MapSize.X, 0), new Vector2(MapSize.X, DxContainer.Height), Color.Black);
            }
            if (selectedBoard.MapSize.Y < DxContainer.Height)
            {
                DrawLine(sprite, new Vector2(0, MapSize.Y), new Vector2(DxContainer.Width, MapSize.Y), Color.Black);
            }
            sprite.End();
            try
            {
                DxDevice.Present();
            }
            catch (DeviceLostException)
            {
            }
            catch (DeviceNotResetException)
            {
                try
                {
                    ResetDevice();
                }
                catch (DeviceLostException)
                {
                }
            }
            
        }

        public bool IsItemInRange(int x, int y, int w, int h, int xshift, int yshift)
        {
            return x + xshift + w > 0 && y + yshift + h > 0 && x + xshift < DxContainer.Width && y + yshift < DxContainer.Height;
        }

        public new object GetService(Type serviceType)
        {
            if (serviceType == typeof(Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService))
                return this.graphicsDeviceService;
            else
                return base.GetService(serviceType);
        }
        #endregion

        #region Handlers
        private void ResetFps(object sender, EventArgs e)
        {
            lastFps = frameCount;
            frameCount = 0;
            
            //unrelated to resetting fps, just using this already made 1-second timer
            //GC.Collect();
            //GC.WaitForPendingFinalizers();
        }

        private void Renderer_Tick(object sender, EventArgs e)
        {
            System.Drawing.Point mouse = PointToClient(Cursor.Position);
            if (VirtualToPhysical(selectedBoard.Mouse.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, 0) != mouse.X || VirtualToPhysical(selectedBoard.Mouse.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, 0) != mouse.Y)
            {
                Point oldPos = new Point(selectedBoard.Mouse.X, selectedBoard.Mouse.Y);
                Point newPos = new Point(PhysicalToVirtual(mouse.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, 0), PhysicalToVirtual(mouse.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, 0));
                selectedBoard.Mouse.Move(newPos.X, newPos.Y);
                if (MouseMoved != null)
                    MouseMoved(selectedBoard, oldPos, newPos, new Point(mouse.X, mouse.Y));
                if (deviceReady)
                    RenderFrame();
            }
            //if (deviceReady)
                //RenderFrame();
        }

        public void SetHScrollbarValue(int value)
        {
            hScrollBar.Value = value;
        }

        public void SetVScrollbarValue(int value)
        {
            vScrollBar.Value = value;
        }

        private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            selectedBoard.vScroll = vScrollBar.Value;
        }

        private void hScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            selectedBoard.hScroll = hScrollBar.Value;
        }
        #endregion

        #region Properties
        public bool DeviceReady
        {
            get { return deviceReady; }
            set { deviceReady = value; }
        }

        /*public SpriteFont ArialFont
        {
            get { return defaultFont; }
        }*/

        public ContentManager ContentMan
        {
            get { return contentMan; }
        }

        public FontEngine FontEngine
        {
            get { return fontEngine; }
        }

        public int maxHScroll
        {
            get { return hScrollBar.Maximum; }
        }

        public int maxVScroll
        {
            get { return vScrollBar.Maximum; }
        }

        public GraphicsDevice Device
        {
            get { return DxDevice; }
        }

        public List<Board> Boards
        {
            get
            {
                return boards;
            }
        }

        public int FPS
        {
            get
            {
                return lastFps;
            }
        }

        public Texture2D LinePixel
        {
            get
            {
                return pixel;
            }
        }

        public Board SelectedBoard
        {
            get
            {
                return selectedBoard;
            }
            set
            {
                selectedBoard = value;
                if (value != null) AdjustScrollBars();
            }
        }

        public Point MapSize
        {
            get
            {
                return selectedBoard.MapSize;
            }
        }
        #endregion

        #region Human I\O Handling
        private BoardItem GetHighestItem(List<BoardItem> items)
        {
            if (items.Count < 1) return null;
            int highestZ = -1;
            BoardItem highestItem = null;
            int zSum;
            foreach (BoardItem item in items)
            {
                zSum = (item is LayeredItem) ? ((LayeredItem)item).Layer.LayerNumber * 100 + item.Z : 900 + item.Z;
                if (zSum > highestZ)
                {
                    highestZ = zSum;
                    highestItem = item;
                }
            }
            return highestItem;
        }

        public static int PhysicalToVirtual(int location, int center, int scroll, int origin)
        {
            return location - center + scroll + origin;
        }

        public static int VirtualToPhysical(int location, int center, int scroll, int origin)
        {
            return location + center - scroll - origin;
        }

        public static bool IsItemUnderRectangle(BoardItem item, Rectangle rect)
        {
            return (item.Right > rect.Left && item.Left < rect.Right && item.Bottom > rect.Top && item.Top < rect.Bottom);
        }

        public static bool IsItemInsideRectangle(BoardItem item, Rectangle rect)
        {
            return (item.Left > rect.Left && item.Right < rect.Right && item.Top > rect.Top && item.Bottom < rect.Bottom);
        }

        private void GetObjsUnderPointFromList(IMapleList list, Point locationVirtualPos, ref BoardItem itemUnderPoint, ref BoardItem selectedUnderPoint, ref bool selectedItemHigher)
        {
            if (!list.Selectable) return;
            if (list.ListType == ItemTypes.None)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    BoardItem item = (BoardItem)list[i];
                    if ((selectedBoard.EditedTypes & item.Type) != item.Type) continue;
                    if (IsPointInsideRectangle(locationVirtualPos, item.Left, item.Top, item.Right, item.Bottom) && !(item is Mouse) && (selectedBoard.SelectedLayerIndex == -1 || item.CheckIfLayerSelected(selectedBoard.SelectedLayerIndex)) && !item.IsPixelTransparent(locationVirtualPos.X - item.Left, locationVirtualPos.Y - item.Top))
                    {
                        if (item.Selected)
                        {
                            selectedUnderPoint = item;
                            selectedItemHigher = true;
                        }
                        else
                        {
                            itemUnderPoint = item;
                            selectedItemHigher = false;
                        }
                    }
                }
            }
            else if ((selectedBoard.EditedTypes & list.ListType) == list.ListType)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    BoardItem item = (BoardItem)list[i];
                    if (IsPointInsideRectangle(locationVirtualPos, item.Left, item.Top, item.Right, item.Bottom) && !(item is Mouse) && !(item is Mouse) && (selectedBoard.SelectedLayerIndex == -1 || item.CheckIfLayerSelected(selectedBoard.SelectedLayerIndex)) && !item.IsPixelTransparent(locationVirtualPos.X - item.Left, locationVirtualPos.Y - item.Top))
                    {
                        if (item.Selected)
                        {
                            selectedUnderPoint = item;
                            selectedItemHigher = true;
                        }
                        else
                        {
                            itemUnderPoint = item;
                            selectedItemHigher = false;
                        }
                    }
                }
            }
        }

        /*private void GetObjsUnderPointFromBackgroundList(Point locationVirtualPos, ref BoardItem itemUnderPoint, ref BoardItem selectedUnderPoint, ref bool selectedItemHigher, bool front)
        {
            IList list = selectedBoard.BoardItems.Backgrounds;
            for (int i = 0; i < list.Count; i++)
            {
                BackgroundInstance item = (BackgroundInstance)list[i];
                if (item.front == front && IsPointInsideRectangle(locationVirtualPos, item.Left, item.Top, item.Right, item.Bottom) && (selectedBoard.SelectedLayerIndex == -1 || item.CheckIfLayerSelected(selectedBoard.SelectedLayerIndex)) && !item.IsPixelTransparent(locationVirtualPos.X - item.Left, locationVirtualPos.Y - item.Top))
                {
                    if (item.Selected)
                    {
                        selectedUnderPoint = item;
                        selectedItemHigher = true;
                    }
                    else
                    {
                        itemUnderPoint = item;
                        selectedItemHigher = false;
                    }
                }
            }
        }*/

        private BoardItemPair GetObjectsUnderPoint(Point location, out bool selectedItemHigher)
        {
            selectedItemHigher = false; //to stop VS from bitching
            BoardItem itemUnderPoint = null, selectedUnderPoint = null;
            Point locationVirtualPos = new Point(PhysicalToVirtual(location.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, /*item.Origin.X*/0), PhysicalToVirtual(location.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, /*item.Origin.Y*/0));
            /*if ((ApplicationSettings.editedTypes & ItemTypes.Backgrounds) == ItemTypes.Backgrounds)
                GetObjsUnderPointFromBackgroundList(locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if (((ApplicationSettings.editedTypes & ItemTypes.Tiles) == ItemTypes.Tiles) && ((ApplicationSettings.editedTypes & ItemTypes.Objects) == ItemTypes.Objects))
                GetObjsUnderPointFromList(selectedBoard.BoardItems.TileObjs, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            else if (((ApplicationSettings.editedTypes & ItemTypes.Tiles) == ItemTypes.Tiles) || ((ApplicationSettings.editedTypes & ItemTypes.Objects) == ItemTypes.Objects))
                GetObjsUnderPointFromList(selectedBoard.BoardItems.TileObjs, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, true);
            if ((ApplicationSettings.editedTypes & ItemTypes.Mobs) == ItemTypes.Mobs)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.Mobs, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.NPCs) == ItemTypes.NPCs)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.NPCs, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Reactors) == ItemTypes.Reactors)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.Reactors, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Portals) == ItemTypes.Portals)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.Portals, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Backgrounds) == ItemTypes.Backgrounds)
                GetObjsUnderPointFromBackgroundList(locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, true);
            if ((ApplicationSettings.editedTypes & ItemTypes.Footholds) == ItemTypes.Footholds)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.FHAnchors, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Ropes) == ItemTypes.Ropes)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.RopeAnchors, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Chairs) == ItemTypes.Chairs)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.Chairs, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.ToolTips) == ItemTypes.ToolTips)
            {
                GetObjsUnderPointFromList(selectedBoard.BoardItems.CharacterToolTips, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
                GetObjsUnderPointFromList(selectedBoard.BoardItems.ToolTips, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
                GetObjsUnderPointFromList(selectedBoard.BoardItems.ToolTipDots, locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher, false);
            }*/
            for (int i = 0; i < selectedBoard.BoardItems.AllItemLists.Length; i++)
                GetObjsUnderPointFromList(selectedBoard.BoardItems.AllItemLists[i], locationVirtualPos, ref itemUnderPoint, ref selectedUnderPoint, ref selectedItemHigher);
            return new BoardItemPair(itemUnderPoint, selectedUnderPoint);
        }

        private BoardItemPair GetObjectsUnderPoint(Point location)
        {
            bool foo;
            return GetObjectsUnderPoint(location, out foo);
        }

        private BoardItem GetObjectUnderPoint(Point location)
        {
            bool selectedItemHigher;
            BoardItemPair objsUnderPoint = GetObjectsUnderPoint(location, out selectedItemHigher);
            if (objsUnderPoint.SelectedItem == null && objsUnderPoint.NonSelectedItem == null) return null;
            else if (objsUnderPoint.SelectedItem == null) return objsUnderPoint.NonSelectedItem;
            else if (objsUnderPoint.NonSelectedItem == null) return objsUnderPoint.SelectedItem;
            else return selectedItemHigher ? objsUnderPoint.SelectedItem : objsUnderPoint.NonSelectedItem;
            //BoardItem itemUnderPoint = null;
            //Point locationVirtualPos = new Point(PhysicalToVirtual(location.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, /*item.Origin.X*/0), PhysicalToVirtual(location.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, /*item.Origin.Y*/0));
            /*if ((ApplicationSettings.editedTypes & ItemTypes.Backgrounds) == ItemTypes.Backgrounds)
                GetObjUnderPointFromBackgroundList(locationVirtualPos, ref itemUnderPoint, false);
            if (((ApplicationSettings.editedTypes & ItemTypes.Tiles) == ItemTypes.Tiles) && ((ApplicationSettings.editedTypes & ItemTypes.Objects) == ItemTypes.Objects))
                GetObjUnderPointFromList(selectedBoard.BoardItems.TileObjs, locationVirtualPos, ref itemUnderPoint, false);
            else if (((ApplicationSettings.editedTypes & ItemTypes.Tiles) == ItemTypes.Tiles) || ((ApplicationSettings.editedTypes & ItemTypes.Objects) == ItemTypes.Objects))
                GetObjUnderPointFromList(selectedBoard.BoardItems.TileObjs, locationVirtualPos, ref itemUnderPoint, true);
            if((ApplicationSettings.editedTypes & ItemTypes.Mobs) == ItemTypes.Mobs)
                GetObjUnderPointFromList(selectedBoard.BoardItems.Mobs, locationVirtualPos, ref itemUnderPoint, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.NPCs) == ItemTypes.NPCs)
                GetObjUnderPointFromList(selectedBoard.BoardItems.NPCs, locationVirtualPos, ref itemUnderPoint, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Reactors) == ItemTypes.Reactors)
                GetObjUnderPointFromList(selectedBoard.BoardItems.Reactors, locationVirtualPos, ref itemUnderPoint, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Portals) == ItemTypes.Portals)
                GetObjUnderPointFromList(selectedBoard.BoardItems.Portals, locationVirtualPos, ref itemUnderPoint, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Backgrounds) == ItemTypes.Backgrounds)
                GetObjUnderPointFromBackgroundList(locationVirtualPos, ref itemUnderPoint, true);
            if ((ApplicationSettings.editedTypes & ItemTypes.Footholds) == ItemTypes.Footholds)
                GetObjUnderPointFromList(selectedBoard.BoardItems.FHAnchors, locationVirtualPos, ref itemUnderPoint, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Ropes) == ItemTypes.Ropes)
                GetObjUnderPointFromList(selectedBoard.BoardItems.RopeAnchors, locationVirtualPos, ref itemUnderPoint, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.Chairs) == ItemTypes.Chairs)
                GetObjUnderPointFromList(selectedBoard.BoardItems.Chairs, locationVirtualPos, ref itemUnderPoint, false);
            if ((ApplicationSettings.editedTypes & ItemTypes.ToolTips) == ItemTypes.ToolTips)
            {
                GetObjUnderPointFromList(selectedBoard.BoardItems.CharacterToolTips, locationVirtualPos, ref itemUnderPoint, false);
                GetObjUnderPointFromList(selectedBoard.BoardItems.ToolTips, locationVirtualPos, ref itemUnderPoint, false);
                GetObjUnderPointFromList(selectedBoard.BoardItems.ToolTipDots, locationVirtualPos, ref itemUnderPoint, false);
            }
            return itemUnderPoint;*/
        }

        /*private void GetObjUnderPointFromList(IList list, Point locationVirtualPos, ref BoardItem itemUnderPoint, bool recheckTypes)
        {
            if (recheckTypes)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    BoardItem item = (BoardItem)list[i];
                    if ((ApplicationSettings.editedTypes & item.Type) != item.Type) continue;
                    if (IsPointInsideRectangle(locationVirtualPos, item.Left, item.Top, item.Right, item.Bottom) && !(item is Mouse) && !(item is Mouse) && (selectedBoard.SelectedLayerIndex == -1 || item.CheckIfLayerSelected(selectedBoard.SelectedLayerIndex)) && !item.IsPixelTransparent(locationVirtualPos.X - item.Left, locationVirtualPos.Y - item.Top))
                    {
                        itemUnderPoint = item;
                        return;
                    }
                }
            }
            else //recheckTypes is outside the loop for performance purposes
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    BoardItem item = (BoardItem)list[i];
                    if (IsPointInsideRectangle(locationVirtualPos, item.Left, item.Top, item.Right, item.Bottom) && !(item is Mouse) && (selectedBoard.SelectedLayerIndex == -1 || item.CheckIfLayerSelected(selectedBoard.SelectedLayerIndex)) && !item.IsPixelTransparent(locationVirtualPos.X - item.Left, locationVirtualPos.Y - item.Top))
                    {
                        itemUnderPoint = item;
                        return;
                    }
                }
            }
        }

        private void GetObjUnderPointFromBackgroundList(Point locationVirtualPos, ref BoardItem itemUnderPoint, bool front)
        {
            IList list = selectedBoard.BoardItems.Backgrounds;
            for (int i = list.Count - 1; i >= 0; i--)
            {
                BackgroundInstance item = (BackgroundInstance)list[i];
                if (item.front == front && IsPointInsideRectangle(locationVirtualPos, item.Left, item.Top, item.Right, item.Bottom) && !item.IsPixelTransparent(locationVirtualPos.X - item.Left, locationVirtualPos.Y - item.Top))
                {
                    itemUnderPoint = item;
                    return;
                }
            }
        }*/

        public static bool IsPointInsideRectangle(Point point, int left, int top, int right, int bottom)
        {
            if (bottom > point.Y && top < point.Y && left < point.X && right > point.X)
                return true;
            return false;
        }

        /*[DllImport("user32.dll")]
        static extern bool GetAsyncKeyState(Keys vKey);*/

        public delegate void LeftMouseDownDelegate(Board selectedBoard, BoardItem item, BoardItem selectedItem, Point realPosition, Point virtualPosition, bool selectedItemHigher);
        public event LeftMouseDownDelegate LeftMouseDown;

        public delegate void LeftMouseUpDelegate(Board selectedBoard, BoardItem item, BoardItem selectedItem, Point realPosition, Point virtualPosition, bool selectedItemHigher);
        public event LeftMouseUpDelegate LeftMouseUp;

        public delegate void RightMouseClickDelegate(Board selectedBoard, BoardItem target, Point realPosition, Point virtualPosition, MouseState mouseState);
        public event RightMouseClickDelegate RightMouseClick;

        public delegate void MouseDoubleClickDelegate(Board selectedBoard, BoardItem target, Point realPosition, Point virtualPosition);
        public new event MouseDoubleClickDelegate MouseDoubleClick; //"new" is to make VS shut up with it's warnings

        public delegate void ShortcutKeyPressedDelegate(Board selectedBoard, bool ctrl, bool shift, bool alt, Keys key);
        public event ShortcutKeyPressedDelegate ShortcutKeyPressed;

        public delegate void MouseMovedDelegate(Board selectedBoard, Point oldPos, Point newPos, Point currPhysicalPos);
        public event MouseMovedDelegate MouseMoved;

        private void DxContainer_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && RightMouseClick != null)
            {
                Point realPosition = new Point(e.X, e.Y);
                RightMouseClick(selectedBoard, GetObjectUnderPoint(realPosition), realPosition, new Point(PhysicalToVirtual(e.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, 0), PhysicalToVirtual(e.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, 0)), selectedBoard.Mouse.State);
            }
            if (deviceReady)
                RenderFrame();
        }

        private void DxContainer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && MouseDoubleClick != null)
            {
                Point realPosition = new Point(e.X, e.Y);
                MouseDoubleClick(selectedBoard, GetObjectUnderPoint(realPosition), realPosition, new Point(PhysicalToVirtual(e.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, 0), PhysicalToVirtual(e.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, 0)));
            }
            if (deviceReady)
                RenderFrame();
        }

        private void DxContainer_MouseDown(object sender, MouseEventArgs e)
        {
            selectedBoard.Mouse.IsDown = true;
            if (e.Button == MouseButtons.Left && LeftMouseDown != null)
            {
                bool selectedItemHigher;
                Point realPosition = new Point(e.X, e.Y);
                BoardItemPair objsUnderMouse = GetObjectsUnderPoint(realPosition, out selectedItemHigher);
                LeftMouseDown(selectedBoard, objsUnderMouse.NonSelectedItem, objsUnderMouse.SelectedItem, realPosition, new Point(PhysicalToVirtual(e.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, 0), PhysicalToVirtual(e.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, 0)), selectedItemHigher);
            }
            if (deviceReady)
                RenderFrame();
        }

        private void DxContainer_MouseUp(object sender, MouseEventArgs e)
        {
            selectedBoard.Mouse.IsDown = false;
            if (e.Button == MouseButtons.Left && LeftMouseUp != null)
            {
                Point realPosition = new Point(e.X, e.Y);
                bool selectedItemHigher;
                BoardItemPair objsUnderMouse = GetObjectsUnderPoint(realPosition, out selectedItemHigher);
                LeftMouseUp(selectedBoard, objsUnderMouse.NonSelectedItem, objsUnderMouse.SelectedItem, realPosition, new Point(PhysicalToVirtual(e.X, selectedBoard.CenterPoint.X, selectedBoard.hScroll, 0), PhysicalToVirtual(e.Y, selectedBoard.CenterPoint.Y, selectedBoard.vScroll, 0)), selectedItemHigher);
            }
            if (deviceReady)
                RenderFrame();
        }

        private void DxContainer_KeyDown(object sender, KeyEventArgs e)
        {
            if (ShortcutKeyPressed != null)
            {
                bool ctrl = (Control.ModifierKeys & Keys.Control) == Keys.Control;
                bool alt = (Control.ModifierKeys & Keys.Alt) == Keys.Alt;
                bool shift = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;
                Keys filteredKeys = e.KeyData;
                if (ctrl) filteredKeys = filteredKeys ^ Keys.Control;
                if (alt) filteredKeys = filteredKeys ^ Keys.Alt;
                if (shift) filteredKeys = filteredKeys ^ Keys.Shift;
                ShortcutKeyPressed(selectedBoard, ctrl, shift, alt, filteredKeys);
            }
            if (deviceReady)
                RenderFrame();
        }
        #endregion

        #region Events
        public delegate void UndoRedoDelegate();
        public event UndoRedoDelegate OnUndoListChanged;
        public event UndoRedoDelegate OnRedoListChanged;

        public delegate void LayerTSChangedDelegate(Layer layer);
        public event LayerTSChangedDelegate OnLayerTSChanged;

        public delegate void MenuItemClickedDelegate(BoardItem item);
        public event MenuItemClickedDelegate OnEditInstanceClicked;
        public event MenuItemClickedDelegate OnEditBaseClicked;
        public event MenuItemClickedDelegate OnSendToBackClicked;
        public event MenuItemClickedDelegate OnBringToFrontClicked;

        public delegate void ReturnToSelectionStateDelegate();
        public event ReturnToSelectionStateDelegate ReturnToSelectionState;

        public delegate void SelectedItemChangedDelegate(BoardItem selectedItem);
        public event SelectedItemChangedDelegate SelectedItemChanged;

        public void OnSelectedItemChanged(BoardItem selectedItem)
        {
            if (SelectedItemChanged != null) SelectedItemChanged.Invoke(selectedItem);
        }

        public void InvokeReturnToSelectionState()
        {
            if (ReturnToSelectionState != null) ReturnToSelectionState.Invoke();
        }

        public void SendToBackClicked(BoardItem item)
        {
            if (OnSendToBackClicked != null) OnSendToBackClicked.Invoke(item);
        }

        public void BringToFrontClicked(BoardItem item)
        {
            if (OnBringToFrontClicked != null) OnBringToFrontClicked.Invoke(item);
        }

        public void EditInstanceClicked(BoardItem item)
        {
            if (OnEditInstanceClicked != null) OnEditInstanceClicked.Invoke(item);
        }

        public void EditBaseClicked(BoardItem item)
        {
            if (OnEditBaseClicked != null) OnEditBaseClicked.Invoke(item);
        }

        public void LayerTSChanged(Layer layer)
        {
            if (OnLayerTSChanged != null) OnLayerTSChanged.Invoke(layer);
        }

        public void UndoListChanged()
        {
            if (OnUndoListChanged != null) OnUndoListChanged.Invoke();
        }

        public void RedoListChanged()
        {
            if (OnRedoListChanged != null) OnRedoListChanged.Invoke();
        }
        #endregion
    }

    public class BoardItemPair
    {
        public BoardItem NonSelectedItem;
        public BoardItem SelectedItem;

        public BoardItemPair(BoardItem nonselected, BoardItem selected)
        {
            this.NonSelectedItem = nonselected;
            this.SelectedItem = selected;
        }
    }

    public class GraphicsDeviceService : IGraphicsDeviceService
    {
        private GraphicsDevice device;

        public GraphicsDeviceService(GraphicsDevice device)
        {
            this.device = device;
            device.Disposing += new EventHandler<EventArgs>(device_Disposing);
            device.DeviceResetting += new EventHandler<EventArgs>(device_DeviceResetting);
            device.DeviceReset += new EventHandler<EventArgs>(device_DeviceReset);
            if (DeviceCreated != null) DeviceCreated.Invoke(device, new EventArgs());
        }

        void device_DeviceReset(object sender, EventArgs e)
        {
            if (DeviceReset != null) DeviceReset.Invoke(sender, e);
        }

        void device_DeviceResetting(object sender, EventArgs e)
        {
            if (DeviceResetting != null) DeviceResetting.Invoke(sender, e);
        }

        void device_Disposing(object sender, EventArgs e)
        {
            if (DeviceDisposing != null) DeviceDisposing.Invoke(sender, e);
        }

        public GraphicsDevice GraphicsDevice
        {
            get { return device; }
        }

        public event EventHandler<EventArgs> DeviceCreated;
        public event EventHandler<EventArgs> DeviceDisposing;
        public event EventHandler<EventArgs> DeviceReset;
        public event EventHandler<EventArgs> DeviceResetting;
    }
}
