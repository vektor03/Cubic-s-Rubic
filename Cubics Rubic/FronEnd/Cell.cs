using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubics_Rubic
{
    public enum color { White, Blue, Orange, Green, Yellow, Red, none }
    public enum Side { Front, Down, Right, Left, Up, Back }
    public enum Move { Front, Down, Right, Left, Up, Back, UnFront, UnDown, UnRight, UnLeft, 
        UnUp, UnBack, RightCubeTurn, UpCubeTurn, LeftCubeTurn, DownCubeTurn }
    public enum Axis { x, y, z }
    public enum TurnDirection { clockwise, counterClockwise }

    public class XYZ
    {
        public int x = -1;
        public int y = -1;
        public int z = -1;

        public XYZ(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public XYZ(XYZ original)
        {
            x = original.x;
            y = original.y;
            z = original.z;
        }
    }
        public class Cell
    {
        public color x;
        public color y;
        public color z;

        public Cell(color x, color y, color z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Cell()
        {
            x = color.none;
            y = color.none;
            z = color.none;
        }

        public Cell(Cell original)
        {
            x = original.x;
            y = original.y;
            z = original.z;
        }

        public Cell Turn(Axis axis)
        {
            color a;
            switch (axis)
            {
                case Axis.x:
                    a = y;  y = z;  z = a;
                    break;
                case Axis.y:
                    a = x;  x = z;  z = a;
                    break;
                case Axis.z:
                    a = x;  x = y;  y = a;
                    break;
            }
            return this;
        }

        public bool IsMatches(color col1, color col2, color col3)
        {
            var colors = new List<color> { col1, col2, col3 };
            foreach (var col in colors)
            {
                if (x != col && y != col && z != col)
                    return false;
            }
            return true;
        }
    }
}
