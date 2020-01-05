using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubics_Rubic
{
    public class Cube
    {
        #region static
        static int[] clockwiseMatrix = new int[] { 2, 5, 8, 1, 4, 7, 0, 3, 6 };
        static int[] counterСlockwiseMatrix = new int[] { 6, 3, 0, 7, 4, 1, 8, 5, 2 };
        #endregion

        public Cell[,,] Arr { get; } = new Cell[3, 3, 3];

        public Cube()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                        Arr[x, y, z] = new Cell();

            //white, front side
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    Arr[x, y, 0].z = color.White;
            //red, down side
            for (int x = 0; x < 3; x++)
                for (int z = 0; z < 3; z++)
                    Arr[x, 2, z].y = color.Red;
            //blue, right side
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                    Arr[2, y, z].x = color.Blue;
            //orange, up side
            for (int x = 0; x < 3; x++)
                for (int z = 0; z < 3; z++)
                    Arr[x, 0, z].y = color.Orange;
            //green, left side
            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                    Arr[0, y, z].x = color.Green;
            //yellow, back side
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    Arr[x, y, 2].z = color.Yellow;
        }

        public Cube(Cube original)
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                        Arr[x, y, z] = new Cell(original.Arr[x, y, z]);
        }

        /// <summary>
        /// Front side [clockwise] turn
        /// </summary>
        public Move F(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.Front;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.UnFront;
            }

            Axis axis = Axis.z;
            List<Cell> a = new List<Cell>();

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    a.Add(Arr[x, y, 0]);

            List<Cell> b = new List<Cell>();
            foreach (int i in turnMatrix)
                b.Add(a.ElementAt(i).Turn(axis));

            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                {
                    Arr[x, y, 0] = b.First();
                    b.RemoveAt(0);
                }

            return outMove;
        }

        /// <summary>
        /// Back side [clockwise] turn
        /// </summary>
        public Move B(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.Back;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.UnBack;
            }

            Axis axis = Axis.z;
            List<Cell> a = new List<Cell>();

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    a.Add(Arr[x, y, 2]);

            List<Cell> b = new List<Cell>();
            foreach (int i in turnMatrix)
                b.Add(a.ElementAt(i).Turn(axis));

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                {
                    Arr[x, y, 2] = b.First();
                    b.RemoveAt(0);
                }
            return outMove;
        }

        /// <summary>
        /// Left side [clockwise] turn
        /// </summary>
        public Move L(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.Left;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.UnLeft;
            }

            Axis axis = Axis.x;
            List<Cell> a = new List<Cell>();

            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                    a.Add(Arr[0, y, z]);

            List<Cell> b = new List<Cell>();
            foreach (int i in turnMatrix)
                b.Add(a.ElementAt(i).Turn(axis));

            for (int y = 0; y < 3; y++)
                for (int z = 0; z < 3; z++)
                {
                    Arr[0, y, z] = b.First();
                    b.RemoveAt(0);
                }
            return outMove;
        }

        /// <summary>
        /// Right side [clockwise] turn
        /// </summary>
        public Move R(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.Right;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.UnRight;
            }

            Axis axis = Axis.x;
            List<Cell> a = new List<Cell>();

            for (int z = 0; z < 3; z++)
                for (int y = 0; y < 3; y++)
                    a.Add(Arr[2, y, z]);

            List<Cell> b = new List<Cell>();
            foreach (int i in turnMatrix)
                b.Add(a.ElementAt(i).Turn(axis));

            for (int z = 0; z < 3; z++)
                for (int y = 0; y < 3; y++)
                {
                    Arr[2, y, z] = b.First();
                    b.RemoveAt(0);
                }
            return outMove;
        }

        /// <summary>
        /// Up side [clockwise] turn
        /// </summary>
        public Move U(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.Up;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.UnUp;
            }

            Axis axis = Axis.y;
            List<Cell> a = new List<Cell>();

            for (int z = 0; z < 3; z++)
                for (int x = 0; x < 3; x++)
                    a.Add(Arr[x, 0, z]);

            List<Cell> b = new List<Cell>();
            foreach (int i in turnMatrix)
                b.Add(a.ElementAt(i).Turn(axis));

            for (int z = 0; z < 3; z++)
                for (int x = 0; x < 3; x++)
                {
                    Arr[x, 0, z] = b.First();
                    b.RemoveAt(0);
                }

            return outMove;
        }

        /// <summary>
        /// Down side [clockwise] turn
        /// </summary>
        public Move D(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.Down;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.UnDown;
            }

            Axis axis = Axis.y;
            List<Cell> a = new List<Cell>();

            for (int x = 0; x < 3; x++)
                for (int z = 0; z < 3; z++)
                    a.Add(Arr[x, 2, z]);

            List<Cell> b = new List<Cell>();
            foreach (int i in turnMatrix)
                b.Add(a.ElementAt(i).Turn(axis));

            for (int x = 0; x < 3; x++)
                for (int z = 0; z < 3; z++)
                {
                    Arr[x, 2, z] = b.First();
                    b.RemoveAt(0);
                }
            return outMove;
        }


        /// <summary>
        /// Turn whole cube to left
        /// </summary>
        public Move LeftCubeTurn(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.LeftCubeTurn;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.RightCubeTurn;
            }

            Axis axis = Axis.y;


            for (int k = 0; k < 3; k++)
            {
                List<Cell> a = new List<Cell>();

                for (int z = 0; z < 3; z++)
                    for (int x = 0; x < 3; x++)
                        a.Add(Arr[x, k, z]);

                List<Cell> b = new List<Cell>();
                foreach (int i in turnMatrix)
                    b.Add(a.ElementAt(i).Turn(axis));

                for (int z = 0; z < 3; z++)
                    for (int x = 0; x < 3; x++)
                    {
                        Arr[x, k, z] = b.First();
                        b.RemoveAt(0);
                    }
            }
            return outMove;
        }

        /// <summary>
        /// Turn whole cube to right
        /// </summary>
        public Move RightCubeTurn(TurnDirection direction = TurnDirection.clockwise)
        {
            return LeftCubeTurn(TurnDirection.counterClockwise);
        }

        /// <summary>
        /// Turn whole cube to down
        /// </summary>
        public Move DownCubeTurn(TurnDirection direction = TurnDirection.clockwise)
        {
            int[] turnMatrix;
            Move outMove;
            if (direction == TurnDirection.clockwise)
            {
                turnMatrix = clockwiseMatrix;
                outMove = Move.DownCubeTurn;
            }
            else
            {
                turnMatrix = counterСlockwiseMatrix;
                outMove = Move.UpCubeTurn;
            }

            Axis axis = Axis.x;


            for (int k = 0; k < 3; k++)
            {
                List<Cell> a = new List<Cell>();

                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                        a.Add(Arr[k, y, z]);

                List<Cell> b = new List<Cell>();
                foreach (int i in turnMatrix)
                    b.Add(a.ElementAt(i).Turn(axis));

                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                    {
                        Arr[k, y, z] = b.First();
                        b.RemoveAt(0);
                    }
            }
            return outMove;
        }

        /// <summary>
        /// Turn whole cube to up
        /// </summary>
        public Move UpCubeTurn()
        {
            return DownCubeTurn(TurnDirection.counterClockwise);
        }

        /// <summary>
        /// Perform given turn
        /// </summary>
        public void DoMove(Move move)
        {
            switch (move)
            {
                case Move.Back: B(); break;
                case Move.Down: D(); break;
                case Move.Front: F(); break;
                case Move.Left: L(); break;
                case Move.Right: R(); break;
                case Move.Up: U(); break;
                case Move.UnBack: B(TurnDirection.counterClockwise); break;
                case Move.UnDown: D(TurnDirection.counterClockwise); break;
                case Move.UnFront: F(TurnDirection.counterClockwise); break;
                case Move.UnLeft: L(TurnDirection.counterClockwise); break;
                case Move.UnRight: R(TurnDirection.counterClockwise); break;
                case Move.UnUp: U(TurnDirection.counterClockwise); break;
                case Move.LeftCubeTurn: LeftCubeTurn(); break;
                case Move.RightCubeTurn: RightCubeTurn(); break;
                case Move.UpCubeTurn: UpCubeTurn(); break;
                case Move.DownCubeTurn: DownCubeTurn(); break;
            }
        }

        public XYZ Find(color col1, color col2, color col3 = color.none)
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    for (int z = 0; z < 3; z++)
                    {
                        if (Arr[x, y, z].IsMatches(col1, col2, col3))
                            return new XYZ(x, y, z);
                    }
            return null;
        }

        public List<Move> SwapFrontRightCorners()
        {
            return new List<Move>
            {
                R(TurnDirection.counterClockwise),
                D(TurnDirection.counterClockwise),
                R(),
                D()
            };
        }

        public List<Move> SwapFrontLeftCorners()
        {
            return new List<Move>
            {
                L(),
                D(),
                L(TurnDirection.counterClockwise),
                D(TurnDirection.counterClockwise)
            };
        }

        public List<Move> SwapBackRightCorners()
        {
            return new List<Move>
            {
                R(),
                D(),
                R(TurnDirection.counterClockwise),
                D(TurnDirection.counterClockwise)
            };
        }

        public List<Move> SwapBackLeftCorners()
        {
            return new List<Move>
            {
                L(TurnDirection.counterClockwise),
                D(TurnDirection.counterClockwise),
                L(),
                D()
            };
        }

        /// <summary>
        /// needs to make second top cross
        /// </summary>
        public List<Move> TopCrossFruRuf()
        {
            return new List<Move>
            {
                F(),
                R(),
                U(),
                R(TurnDirection.counterClockwise),
                U(TurnDirection.counterClockwise),
                F(TurnDirection.counterClockwise)
            };
        }
        
        /// <summary>
        /// needs to make second top corners
        /// </summary>
        public List<Move> SecondCorners()
        {
            return new List<Move>
            {
                U(),
                R(),
                U(TurnDirection.counterClockwise),
                L(TurnDirection.counterClockwise),
                U(),
                R(TurnDirection.counterClockwise),
                U(TurnDirection.counterClockwise),
                L()
            };
        }
    }
}