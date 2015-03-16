/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using HaCreator.MapEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaCreator.WzStructure
{
    public class FootholdEnumerator : IEnumerable, IEnumerator
    {
        bool started = false;
        private FootholdLine firstLine;
        private FootholdAnchor firstAnchor;
        private FootholdLine currLine;
        private FootholdAnchor currAnchor;
        private HashSet<FootholdLine> visited = new HashSet<FootholdLine>();
        private Stack<Tuple<FootholdLine, FootholdAnchor>> stashedLines = new Stack<Tuple<FootholdLine, FootholdAnchor>>();
        
        public FootholdEnumerator(FootholdLine startLine, FootholdAnchor startAnchor)
        {
            firstLine = startLine;
            firstAnchor = startAnchor;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (!started)
            {
                // First MoveNext should return the starting element
                currLine = firstLine;
                currAnchor = firstAnchor;
                visited.Add(currLine);
                started = true;
                return true;
            }
            FootholdAnchor nextAnchor = currLine.GetOtherAnchor(currAnchor);
            List<FootholdLine> nextLineOpts = nextAnchor.connectedLines.Where(x => !visited.Contains(x)).Cast<FootholdLine>().ToList();
            if (nextLineOpts.Count == 0)
            {
                if (stashedLines.Count == 0)
                {
                    // Enumeration finished
                    return false;
                }
                else
                {
                    // This path finished, pop a new path from the stack
                    var item = stashedLines.Pop();
                    currLine = item.Item1;
                    nextAnchor = item.Item2;
                }
            }
            else if (nextLineOpts.Count == 1)
            {
                currLine = nextLineOpts[0];
                visited.Add(currLine);
            }
            else // more than 1 option, we need to stash
            {
                // Stash all options
                foreach (FootholdLine line in nextLineOpts)
                {
                    visited.Add(line);
                    stashedLines.Push(new Tuple<FootholdLine, FootholdAnchor>(line, nextAnchor));
                }
                // Pull the last one
                var item = stashedLines.Pop();
                currLine = item.Item1;
            }
            currAnchor = nextAnchor;
            return true;
        }

        public void Reset()
        {
            started = false;
            visited.Clear();
            stashedLines.Clear();
        }

        public void Dispose()
        {
        }

        public object Current
        {
            get { return currLine; }
        }

        public FootholdLine CurrentLine
        {
            get { return currLine; }
        }

        public FootholdAnchor CurrentAnchor
        {
            get { return currAnchor; }
        }
    }

    public class AnchorEnumerator : IEnumerable, IEnumerator
    {
        private bool started = false;
        private FootholdAnchor first;
        private FootholdAnchor curr;
        private HashSet<FootholdAnchor> visited = new HashSet<FootholdAnchor>();
        private Stack<FootholdAnchor> toVisit = new Stack<FootholdAnchor>();


        public AnchorEnumerator(FootholdAnchor start)
        {
            first = start;
        }

        public IEnumerator GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (!started)
            {
                curr = first;
                started = true;
                return true;
            }
            List<FootholdAnchor> anchors = new List<FootholdAnchor>();
            foreach (FootholdLine line in curr.connectedLines)
            {
                FootholdAnchor anchor = line.GetOtherAnchor(curr);
                if (!visited.Contains(anchor))
                {
                    anchors.Add(anchor);
                }
            }
            if (anchors.Count == 0)
            {
                if (toVisit.Count == 0)
                {
                    return false;
                }
                else
                {
                    curr = toVisit.Pop();
                }
            }
            else if (anchors.Count == 1)
            {
                curr = anchors[0];
                visited.Add(curr);
            }
            else
            {
                foreach (FootholdAnchor anchor in anchors)
                {
                    visited.Add(anchor);
                    toVisit.Push(anchor);
                }
                curr = toVisit.Pop();
            }
            return true;
        }

        public void Reset()
        {
            started = false;
            visited.Clear();
            toVisit.Clear();
        }

        public void Dispose()
        {
        }

        public object Current
        {
            get { return curr; }
        }
    }
}
