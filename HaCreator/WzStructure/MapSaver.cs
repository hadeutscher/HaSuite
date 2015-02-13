/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaCreator.MapEditor;

namespace HaCreator.WzStructure
{
    public static class MapSaver
    {
        public static void ConvertToMapleFootholds2(ref MapleList<FootholdLine> oldFootholds, ref MapleList<FootholdAnchor> oldAnchors)
        {
            //Part 1 - copying and filtering out unused anchors
            List<FootholdLine> footholds = new List<FootholdLine>(oldFootholds.Count);
            List<FootholdAnchor> anchors = new List<FootholdAnchor>(oldAnchors.Count);
            foreach (FootholdAnchor oldAnchor in oldAnchors)
            {
                if (oldAnchor.connectedLines.Count == 0) continue;
                //if (oldAnchor.IsMoveHandled) throw new Exception();
                FootholdAnchor anchor = new FootholdAnchor(oldAnchor.Board, oldAnchor.X, oldAnchor.Y, oldAnchor.LayerNumber);
                anchor.connectedLines = new List<MapleLine>(oldAnchor.connectedLines.Count);
                foreach (FootholdLine oldLine in oldAnchor.connectedLines)
                {
                    if (oldLine.cloneLine == null)
                    {
                        FootholdAnchor firstDot = null;
                        FootholdAnchor secondDot = null;
                        if (oldLine.FirstDot.X > oldLine.SecondDot.X)
                        {
                            firstDot = (FootholdAnchor)oldLine.SecondDot;
                            secondDot = (FootholdAnchor)oldLine.FirstDot;
                        }
                        else if (oldLine.FirstDot.X < oldLine.SecondDot.X)
                        {
                            firstDot = (FootholdAnchor)oldLine.FirstDot;
                            secondDot = (FootholdAnchor)oldLine.SecondDot;
                        }
                        else
                        {
                            if (oldLine.FirstDot.Y > oldLine.SecondDot.Y)
                            {
                                firstDot = (FootholdAnchor)oldLine.SecondDot;
                                secondDot = (FootholdAnchor)oldLine.FirstDot;
                            }
                            else
                            {
                                firstDot = (FootholdAnchor)oldLine.FirstDot;
                                secondDot = (FootholdAnchor)oldLine.SecondDot;

                            }
                        }
                        if (anchor.X == firstDot.X && anchor.Y == firstDot.Y) //we are firstdot
                        {
                            firstDot = anchor;
                            secondDot = null;
                        }
                        else //we are seconddot
                        {
                            firstDot = null;
                            secondDot = anchor;
                        }
                        oldLine.cloneLine = FootholdLine.CreateCustomFootholdLine(oldLine.Board, firstDot, secondDot);
                        footholds.Add(oldLine.cloneLine);
                    }
                    else
                    {
                        if (oldLine.cloneLine.FirstDot == null) oldLine.cloneLine.FirstDot = anchor;
                        else oldLine.cloneLine.SecondDot = anchor;
                    }
                    anchor.connectedLines.Add(oldLine.cloneLine);
                }
                anchors.Add(anchor);
            }
            foreach (FootholdLine fhLine in oldFootholds)
                fhLine.cloneLine = null;

            //Part 2 - combining anchors that are on the exact same position
            anchors.Sort(new Comparison<FootholdAnchor>(FootholdAnchor.FHAnchorSorter));
            int groupStart = 0; //inclusive
            int groupEnd = 0; //inclusive
            FootholdAnchor a;
            FootholdAnchor b;
            FootholdAnchor c;
            FootholdAnchor d;
            FootholdLine line;
            for (int i = 0; i < anchors.Count - 1; i++)
            {
                a = anchors[i];
                b = anchors[i + 1];
                if (a.Y != b.Y && a.X != b.X && a.LayerNumber == b.LayerNumber)
                {
                    groupEnd = i;
                    if (groupEnd - groupStart == 1) //there are 2 objects in the group, since groupEnd and groupStart are inclusive
                    {
                        c = anchors[groupStart];
                        d = anchors[groupEnd];
                        if (c.connectedLines.Count == 1 && d.connectedLines.Count == 1)
                        {
                            d.removed = true;
                            line = (FootholdLine)d.connectedLines[0];
                            if (line.FirstDot == d)
                                line.FirstDot = c;
                            else
                                line.SecondDot = c;
                            c.connectedLines.Add(line);
                        }
                    }
                    groupStart = groupEnd + 1;
                }
            }

            groupEnd = anchors.Count - 1;
            if (groupEnd - groupStart == 1) //there are 2 objects in the group, since groupEnd and groupStart are inclusive
            {
                c = anchors[groupStart];
                d = anchors[groupEnd];
                if (c.connectedLines.Count == 1 && d.connectedLines.Count == 1)
                {
                    d.removed = true;
                    line = (FootholdLine)d.connectedLines[0];
                    if (line.FirstDot == d)
                        line.FirstDot = c;
                    else
                        line.SecondDot = c;
                    c.connectedLines.Add(line);
                }
            }

            //Part 3 - connecting footholds to anchors they pass through, selectively 
            //this part is made purely to fix problems in the foothold structure caused
            //by the auto foothold connection feature for tiles
            for (int i = 0; i < footholds.Count; i++)
            {
                line = footholds[i];
                if (line.FirstDot.Y != line.SecondDot.Y) continue;
                foreach (FootholdAnchor anchor in anchors)
                {
                    if (anchor.X == line.FirstDot.Y && ((IContainsLayerInfo)line.FirstDot).LayerNumber == anchor.LayerNumber)
                    {
                        if (anchor.connectedLines.Count == 1)
                            line = (FootholdLine)anchor.connectedLines[0];
                        else
                            line = (FootholdLine)(anchor.connectedLines[0].FirstDot.Y == anchor.connectedLines[0].SecondDot.Y ? anchor.connectedLines[0] : anchor.connectedLines[1]);
                        if (line.FirstDot.Y == line.SecondDot.Y) //blueCave2 tiles, first anchor of sloped tiles
                        {
                            //footholds.RemoveAt(i);
                            line.remove = true;
                            footholds.Add(new FootholdLine(line.Board, line.FirstDot, anchor));
                            footholds.Add(new FootholdLine(line.Board, anchor, line.SecondDot));
                            //i--;
                            break;
                            //anchor.keyAnchor = line.FirstDot.Y == line.SecondDot.Y && IsValidKeyAnchor(anchor);
                        }
                        else //junction anchor for sloped tiles
                        {
                            bool direction = line.FirstDot == anchor; //true = anchor is the left dot, so we need to delete the line going to the right
                            //footholds.RemoveAt(i);
                            line.remove = true;
                            if (direction)
                                footholds.Add(new FootholdLine(line.Board, line.FirstDot, anchor));
                            else
                                footholds.Add(new FootholdLine(line.Board, anchor, line.SecondDot));
                            break;
                        }
                    }
                }
                /*                     if (((line.FirstDot.X == line.SecondDot.X) && (anchor.X == line.FirstDot.X && anchor.Y > line.FirstDot.Y && anchor.Y < line.SecondDot.Y)
                 || ((line.FirstDot.Y == line.SecondDot.Y) && (anchor.Y == line.FirstDot.Y && anchor.X > line.FirstDot.X && anchor.X < line.SecondDot.X))) 
                 && ((IContainsLayerInfo)line.FirstDot).LayerNumber == anchor.LayerNumber)*/
            }

            FootholdLine lineA;
            FootholdLine lineB;
            //Part 4 - removing duplicate footholds (caused by step 3, case 1)
            footholds.Sort(new Comparison<FootholdLine>(FHSorter));
            for (int i = 0; i < footholds.Count - 1; i++)
            {
                lineA = footholds[i];
                lineB = footholds[i + 1];
                if (lineA.FirstDot.X == lineB.FirstDot.X && lineA.FirstDot.Y == lineB.FirstDot.Y &&
                    lineA.SecondDot.X == lineB.SecondDot.X && lineA.SecondDot.Y == lineB.SecondDot.Y)
                {
                    //footholds.RemoveAt(i);
                    lineA.remove = true;
                    i--;
                }
            }

            //Part 5 - executing foothold changes and updating anchors
            foreach (FootholdAnchor anchor in anchors)
                for (int i = 0; i < anchor.connectedLines.Count; i++)
                    if (((FootholdLine)anchor.connectedLines[i]).remove)
                        anchor.connectedLines.RemoveAt(i);
            List<FootholdLine> newFootholds = new List<FootholdLine>(footholds.Count);
            foreach (FootholdLine fh in footholds)
                if (!fh.remove)
                    newFootholds.Add(fh);
            footholds = newFootholds;


            //Part 6 - dealing with 3 way (or more) foothold junctions
            /*foreach (FootholdAnchor anchor in anchors)
            {
                if (anchor.connectedLines.Count > 2)
                {
                    if (anchor.keyAnchor)
                    {
                        int leftLineIndex = -1;
                        int rightLineIndex = -1;
                        int slopedLineIndex = -1;
                        for(int i=0; i<anchor.connectedLines.Count; i++)
                        {
                            FootholdLine line = (FootholdLine)anchor.connectedLines[i];
                            if (line.FirstDot.Y == line.SecondDot.Y)
                            {
                                if (anchor == line.FirstDot) rightLineIndex = i;
                                else if (anchor == line.SecondDot) leftLineIndex = i;
                                else
                                {
                                    MessageBox.Show("Error at foothold parsing"); //TODO
                                }
                            }
                            else if (line.FirstDot.X != line.SecondDot.X) slopedLineIndex = i;
                        }
                        if (leftLineIndex == -1 || rightLineIndex == -1 || slopedLineIndex == -1)
                        {
                            MessageBox.Show("Error at foothold parsing"); //TODO
                        }
                        FootholdLine slopedLine = (FootholdLine)anchor.connectedLines[slopedLineIndex];
                        if (slopedLine.FirstDot.Y < slopedLine.SecondDot.Y)
                        { //eliminate left
                            EliminateFootholdTree(anchor, leftLineIndex, ref footholds);
                        }
                        else
                        {
                            //eliminate right
                            EliminateFootholdTree(anchor, rightLineIndex, ref footholds);
                        }
                    }
                    else
                    {
                        double lowestLength = double.MaxValue;
                        int shortestLineIndex = -1;
                        for (int i = 0; i < anchor.connectedLines.Count; i++)
                        {
                            double length = CalculateFootholdTreeLength(anchor, i);
                            if (length < lowestLength)
                            {
                                lowestLength = length;
                                shortestLineIndex = i;
                            }
                        }
                        EliminateFootholdTree(anchor, shortestLineIndex, ref footholds);
                    }
                }
            }

            //Part 7 - executing foothold changes and updating anchors (again)
            foreach (FootholdAnchor anchor in anchors)
                for (int i = 0; i < anchor.connectedLines.Count; i++)
                    if (((FootholdLine)anchor.connectedLines[i]).remove)
                        anchor.connectedLines.RemoveAt(i);*/

            oldAnchors.Clear();
            foreach (FootholdAnchor anchor in anchors) { /*anchor.keyAnchor = false; */oldAnchors.Add(anchor); }
            oldFootholds.Clear();
            foreach (FootholdLine fh in footholds) { /*anchor.keyAnchor = false; */oldFootholds.Add(fh); }
        }

        private static FootholdAnchor GetOtherAnchor(FootholdLine line, FootholdAnchor currAnchor)
        {
            return (FootholdAnchor)(line.FirstDot == currAnchor ? line.SecondDot : line.FirstDot);
        }

        private static FootholdLine GetOtherLine(FootholdLine line, FootholdAnchor currAnchor)
        {
            foreach (FootholdLine currLine in currAnchor.connectedLines)
                if (line != currLine)
                    return currLine;
            return null;
        }

        /*private static double CalculateFootholdTreeLength(FootholdAnchor anchor, int lineIndex)
        {
            double length = 0;
            FootholdLine line = (FootholdLine)anchor.connectedLines[lineIndex];
            int dx = line.FirstDot.X - line.SecondDot.X;
            int dy = line.FirstDot.Y - line.SecondDot.Y;
            length += Math.Sqrt(dx * dx + dy * dy);
            FootholdAnchor otherAnchor = GetOtherAnchor(line, anchor);
            for (int i = 0; i < otherAnchor.connectedLines.Count; i++)
                if (otherAnchor.connectedLines[i] != line) 
                    length += CalculateFootholdTreeLength(anchor, i);
            return length;
        }*/

        private static bool FootholdTreeLengthExceeds(FootholdAnchor anchor, int lineIndex, int max) //max is inclusive
        {
            FootholdLine line = (FootholdLine)anchor.connectedLines[0];
            int length = 1;
            while (length <= max)
            {
                anchor = GetOtherAnchor(line, anchor);
                line = GetOtherLine(line, anchor);
                if (line == null) return false;
            }
            return true;
        }

        private static void EliminateFootholdTree(FootholdAnchor anchor, int lineIndex, ref List<FootholdLine> footholds)
        {
            FootholdLine line = (FootholdLine)anchor.connectedLines[lineIndex];
            footholds.Remove(line);
            line.remove = true;
            FootholdAnchor otherAnchor = GetOtherAnchor(line, anchor);
            for (int i = 0; i < otherAnchor.connectedLines.Count; i++)
                if (otherAnchor.connectedLines[i] != line) EliminateFootholdTree(otherAnchor, i, ref footholds);
        }

        /*private static bool IsValidKeyAnchor(FootholdAnchor anchor)
        {
            int horizontalLines = 0;
            int nonHorizontalLines = 0;
            foreach (FootholdLine line in anchor.connectedLines)
            {
                if (line.FirstDot.Y == line.SecondDot.Y) horizontalLines++;
                else if (line.FirstDot.X != line.SecondDot.X) nonHorizontalLines++;
            }
            return horizontalLines == 1 && nonHorizontalLines == 1;
        }*/

        private static int FHSorter(FootholdLine a, FootholdLine b)
        {
            if (a.FirstDot.X > b.FirstDot.X)
                return 1;
            else if (a.FirstDot.X < b.FirstDot.X)
                return -1;
            else
            {
                if (a.FirstDot.Y > b.FirstDot.Y)
                    return 1;
                else if (a.FirstDot.Y < b.FirstDot.Y)
                    return -1;
                else
                {
                    if (a.SecondDot.X > b.SecondDot.X)
                        return 1;
                    else if (a.SecondDot.X < b.SecondDot.X)
                        return -1;
                    else
                    {
                        if (a.SecondDot.Y > b.SecondDot.Y)
                            return 1;
                        else if (a.SecondDot.Y < b.SecondDot.Y)
                            return -1;
                        else
                            return 0;
                    }
                }
            }
        }

        private static int FHAnchorSorter(FootholdAnchor a, FootholdAnchor b)
        {
            if (a.X > b.X)
                return 1;
            else if (a.X < b.X)
                return -1;
            else
            {
                if (a.Y > b.Y)
                    return 1;
                else if (a.Y < b.Y)
                    return -1;
                else return 0;
            }
        }
    }
}
