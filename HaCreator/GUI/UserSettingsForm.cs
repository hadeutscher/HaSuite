/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HaCreator.GUI
{
    public partial class UserSettingsForm : EditorBase
    {
        public UserSettingsForm()
        {
            InitializeComponent();
            errorsCheckBox.Checked = UserSettings.ShowErrorsMessage;
            linewBox.Value = UserSettings.LineWidth;
            dotwBox.Value = UserSettings.DotWidth;
            inactiveaBox.Value = UserSettings.NonActiveAlpha;
            showMousePosCheckbox.Checked = UserSettings.ShowMousePos;
            xgaResolutionCheckbox.Checked = UserSettings.XGAResolution;
            clipBox.Checked = UserSettings.ClipText;

            tabColorPicker.Color = UserSettings.TabColor;
            dragColorPicker.Color = XNAToSystemColor(UserSettings.SelectSquare);
            dragFillColorPicker.Color = XNAToSystemColor(UserSettings.SelectSquareFill);
            selectedColorPicker.Color = XNAToSystemColor(UserSettings.SelectedColor);
            vrColorPicker.Color = XNAToSystemColor(UserSettings.VRColor);
            fhColorPicker.Color = XNAToSystemColor(UserSettings.FootholdColor);
            rlColorPicker.Color = XNAToSystemColor(UserSettings.RopeColor);
            seatColorPicker.Color = XNAToSystemColor(UserSettings.ChairColor);
            ttColorPicker.Color = XNAToSystemColor(UserSettings.ToolTipColor);
            ttFillColorPicker.Color = XNAToSystemColor(UserSettings.ToolTipFill);
            ttSelectColorPicker.Color = XNAToSystemColor(UserSettings.ToolTipSelectedFill);
            ttcColorPicker.Color = XNAToSystemColor(UserSettings.ToolTipCharFill);
            ttcSelectColorPicker.Color = XNAToSystemColor(UserSettings.ToolTipCharSelectedFill);
            ttLineColorPicker.Color = XNAToSystemColor(UserSettings.ToolTipBindingLine);
            rInput.Value = UserSettings.HiddenLifeR;
            fontName.Text = UserSettings.FontName;
            fontSize.Value = UserSettings.FontSize;

            mobrx0Box.Value = UserSettings.Mobrx0Offset;
            mobrx1Box.Value = UserSettings.Mobrx1Offset;
            npcrx0Box.Value = UserSettings.Npcrx0Offset;
            npcrx1Box.Value = UserSettings.Npcrx1Offset;
            mobtimeBox.Value = UserSettings.defaultMobTime;
            reacttimeBox.Value = UserSettings.defaultReactorTime;
            zShiftBox.Value = UserSettings.zShift;
            snapdistBox.Value = (decimal)UserSettings.SnapDistance;
            scrolldistBox.Value = UserSettings.ScrollDistance;
            scrollbaseBox.Value = (decimal)UserSettings.ScrollBase;
            scrollexpBox.Value = (decimal)UserSettings.ScrollExponentFactor;
            scrollfactBox.Value = (decimal)UserSettings.ScrollFactor;
            movementBox.Value = (decimal)UserSettings.SignificantDistance;
        }

        public static Color XNAToSystemColor(Microsoft.Xna.Framework.Color color)
        {
            return Color.FromArgb((int)color.PackedValue);
        }

        public static Microsoft.Xna.Framework.Color SystemToXNAColor(Color color)
        {
            return new Microsoft.Xna.Framework.Color(color.R, color.G, color.B, color.A);
        }

        protected override void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void okButton_Click(object sender, EventArgs e)
        {

            UserSettings.ShowErrorsMessage = errorsCheckBox.Checked;
            UserSettings.LineWidth = (int)linewBox.Value;
            UserSettings.DotWidth = (int)dotwBox.Value;
            UserSettings.NonActiveAlpha = (int)inactiveaBox.Value;
            UserSettings.ShowMousePos = showMousePosCheckbox.Checked;
            UserSettings.XGAResolution = xgaResolutionCheckbox.Checked;
            UserSettings.ClipText = clipBox.Checked;

            UserSettings.TabColor = tabColorPicker.Color;
            UserSettings.SelectSquare = SystemToXNAColor(dragColorPicker.Color);
            UserSettings.SelectSquareFill = SystemToXNAColor(dragFillColorPicker.Color);
            UserSettings.SelectedColor = SystemToXNAColor(selectedColorPicker.Color);
            UserSettings.VRColor = SystemToXNAColor(vrColorPicker.Color);
            UserSettings.FootholdColor = SystemToXNAColor(fhColorPicker.Color);
            UserSettings.RopeColor = SystemToXNAColor(rlColorPicker.Color);
            UserSettings.ChairColor = SystemToXNAColor(seatColorPicker.Color);
            UserSettings.ToolTipColor = SystemToXNAColor(ttColorPicker.Color);
            UserSettings.ToolTipFill = SystemToXNAColor(ttFillColorPicker.Color);
            UserSettings.ToolTipSelectedFill = SystemToXNAColor(ttSelectColorPicker.Color);
            UserSettings.ToolTipCharFill = SystemToXNAColor(ttcColorPicker.Color);
            UserSettings.ToolTipCharSelectedFill = SystemToXNAColor(ttcSelectColorPicker.Color);
            UserSettings.ToolTipBindingLine = SystemToXNAColor(ttLineColorPicker.Color);
            UserSettings.HiddenLifeR = (int)rInput.Value;

            UserSettings.Mobrx0Offset = (int)mobrx0Box.Value;
            UserSettings.Mobrx1Offset = (int)mobrx1Box.Value;
            UserSettings.Npcrx0Offset = (int)npcrx0Box.Value;
            UserSettings.Npcrx1Offset = (int)npcrx1Box.Value;
            UserSettings.defaultMobTime = (int)mobtimeBox.Value;
            UserSettings.defaultReactorTime = (int)reacttimeBox.Value;
            UserSettings.zShift = (int)zShiftBox.Value;
            UserSettings.SnapDistance = (float)snapdistBox.Value;
            UserSettings.ScrollDistance = (int)scrolldistBox.Value;
            UserSettings.ScrollBase = (double)scrollbaseBox.Value;
            UserSettings.ScrollExponentFactor = (double)scrollexpBox.Value;
            UserSettings.ScrollFactor = (double)scrollfactBox.Value;
            UserSettings.SignificantDistance = (float)movementBox.Value;

            Close();
        }
    }
}
