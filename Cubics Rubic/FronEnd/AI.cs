using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubics_Rubic
{
    public static class AI
    {
        static IEnumerable<Move> moves;
        public static IEnumerable<Move> Solve(Cube cube) 
        {
            moves = new List<Move>();

            var cubeClone = new Cube(cube);

            SimpleCross(cubeClone);
            Cross(cubeClone);
            TopCorners(cubeClone);
            Middle(cubeClone);
            SimpleSecondCross(cubeClone);
            OrientSecondCross(cubeClone);
            SecondCorners(cubeClone);
            TurnLastCorners(cubeClone);
            return moves;
        }

        /// <summary>
        /// Makes cross with pieces that can be oriented improperly
        /// </summary>
        /// <param name="cube"></param>
        public static void SimpleCross(Cube cube)
        {
            for(int i = 0; i < 4; i++)
            {
                var topColor = cube.Arr[1, 0, 1].y;
                var frontColor = cube.Arr[1, 1, 0].z;

                var cellXYZ = cube.Find(topColor, frontColor);

                //on the left side top. need to move to front
                if (cellXYZ.x == 0 && cellXYZ.y == 0 && cellXYZ.z == 1)
                {
                    moves = moves.Append(cube.L());
                    cellXYZ = cube.Find(topColor, frontColor);
                }
                //on the right side top. need to move to front
                if (cellXYZ.x == 2 && cellXYZ.y == 0 && cellXYZ.z == 1)
                {
                    moves = moves.Append(cube.R(TurnDirection.counterClockwise));
                    cellXYZ = cube.Find(topColor, frontColor);
                }
                //on the back side top. need to move to bottom
                if (cellXYZ.z == 2 && cellXYZ.y == 0)
                {
                    moves = moves.Append(cube.B());
                    moves = moves.Append(cube.B());
                    cellXYZ = cube.Find(topColor, frontColor);
                }
                //on the back side, left or right. need to move to front
                if (cellXYZ.z == 2 && cellXYZ.y == 1)
                {
                    moves = moves.Append(cube.U());
                    moves = moves.Append(cube.U());
                    while (cellXYZ.z == 2 && cellXYZ.y != 0)
                    {
                        moves = moves.Append(cube.B());
                        cellXYZ = cube.Find(topColor, frontColor);
                    }
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                }
                //on the bottom. need to move to front
                while (cellXYZ.z != 0)
                {
                    moves = moves.Append(cube.D());
                    cellXYZ = cube.Find(topColor, frontColor);
                }
                //on the front. need to move to top
                while (cellXYZ.y != 0)
                {
                    moves = moves.Append(cube.F());
                    cellXYZ = cube.Find(topColor, frontColor);
                }

                moves = moves.Append(cube.RightCubeTurn());
            }
        }


        /// <summary>
        /// Makes cross with pieces that oriented properly
        /// </summary>
        /// <param name="cube"></param>
        public static void Cross(Cube cube)
        {
            for (int i = 0; i < 4; i++)
            {
                var topColor = cube.Arr[1, 0, 1].y;

                if (cube.Arr[1, 0, 0].y != topColor)
                {
                    moves = moves.Append(cube.L(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.F(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.L());
                    moves = moves.Append(cube.D());
                    moves = moves.Append(cube.F());
                    moves = moves.Append(cube.F());
                }

                moves = moves.Append(cube.RightCubeTurn());
            }
        }

        /// <summary>
        /// Puts correct corners at the top of the cube
        /// </summary>
        /// <param name="cube"></param>
        public static void TopCorners(Cube cube)
        {
            for (int i = 0; i < 4; i++)
            {
                var topColor = cube.Arr[1, 0, 1].y;
                var frontColor = cube.Arr[1, 1, 0].z;
                var rightColor = cube.Arr[2, 1, 1].x;

                var cellXYZ = cube.Find(topColor, frontColor, rightColor);

                //if needed courner already on top, but in wrong place. Puts it down
                if (cellXYZ.y == 0 && (cellXYZ.x != 2 || cellXYZ.z != 0))
                {
                    if (cellXYZ.x == 0 && cellXYZ.z == 0)
                        moves = moves.Concat(cube.SwapFrontLeftCorners());
                    else if (cellXYZ.x == 0 && cellXYZ.z == 2)
                        moves = moves.Concat(cube.SwapBackLeftCorners());
                    else if (cellXYZ.x == 2 && cellXYZ.z == 2)
                        moves = moves.Concat(cube.SwapBackRightCorners());

                    cellXYZ = cube.Find(topColor, frontColor, rightColor);
                }

                //needed corcer at the bottom. Need to move it to front right bottom
                while (cellXYZ.y == 2 && (cellXYZ.x != 2 || cellXYZ.z != 0))
                {
                    moves = moves.Append(cube.D());
                    cellXYZ = cube.Find(topColor, frontColor, rightColor);
                }

                //swap corners until it'll be correctly oriended
                while (cube.Arr[2, 0, 0].x != rightColor || cube.Arr[2, 0, 0].y != topColor || cube.Arr[2, 0, 0].z != frontColor
                    || cellXYZ.y != 0)
                {
                    moves = moves.Concat(cube.SwapFrontRightCorners());
                    cellXYZ = cube.Find(topColor, frontColor, rightColor);
                }

                moves = moves.Append(cube.RightCubeTurn());
            }

            moves = moves.Append(cube.UpCubeTurn());
            moves = moves.Append(cube.UpCubeTurn());
        }


        /// <summary>
        /// Makes middle section
        /// </summary>
        /// <param name="cube"></param>
        public static void Middle(Cube cube)
        {
            for (int i = 0; i < 4; i++)
            {
                var frontColor = cube.Arr[1, 1, 0].z;
                var rightColor = cube.Arr[2, 1, 1].x;
                color tempColor;

                var cellXYZ = cube.Find(rightColor, frontColor);

                //if our cube allready in middle, but we need to get it from there
                if (cellXYZ.y == 1)
                {
                    if (cellXYZ.x == 2 && cellXYZ.z == 0 && cube.Arr[2, 1, 0].z == frontColor)
                    {
                        moves = moves.Append(cube.RightCubeTurn());
                        continue;
                    }
                        
                    while (cellXYZ.x != 2 || cellXYZ.z != 0)
                    {
                        moves = moves.Append(cube.RightCubeTurn());
                        cellXYZ = cube.Find(rightColor, frontColor);
                    }
                    moves = moves.Append(cube.U());
                    moves = moves.Append(cube.R());
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.R(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.F(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.U());
                    moves = moves.Append(cube.F());
                    cellXYZ = cube.Find(rightColor, frontColor);
                }

                //put it at its place
                while (cellXYZ.x != 1 || cellXYZ.z != 0)
                {
                    moves = moves.Append(cube.RightCubeTurn());
                    cellXYZ = cube.Find(rightColor, frontColor);
                }
                tempColor = cube.Arr[cellXYZ.x, cellXYZ.y, cellXYZ.z].z;
                while (cube.Arr[1, 1, 0].z != tempColor)
                    moves = moves.Append(cube.RightCubeTurn()); 
                cellXYZ = cube.Find(rightColor, frontColor);
                while (cellXYZ.x != 1 || cellXYZ.z != 0)
                {
                    moves = moves.Append(cube.U());
                    cellXYZ = cube.Find(rightColor, frontColor);
                }
                if (cube.Arr[cellXYZ.x, cellXYZ.y, cellXYZ.z].y == rightColor)//move to right-down
                {
                    moves = moves.Append(cube.U());
                    moves = moves.Append(cube.R());
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.R(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.F(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.U());
                    moves = moves.Append(cube.F());
                }
                else
                {
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.L(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.U());
                    moves = moves.Append(cube.L());
                    moves = moves.Append(cube.U());
                    moves = moves.Append(cube.F());
                    moves = moves.Append(cube.U(TurnDirection.counterClockwise));
                    moves = moves.Append(cube.F(TurnDirection.counterClockwise));
                }
                while (cube.Arr[1, 1, 0].z != frontColor)
                    moves = moves.Append(cube.RightCubeTurn());

                moves = moves.Append(cube.RightCubeTurn());
            }
        }


        /// <summary>
        /// Makes top cross with not oriented pieces
        /// </summary>
        /// <param name="cube"></param>
        public static void SimpleSecondCross(Cube cube)
        {
            var topColor = cube.Arr[1, 0, 1].y;

            //if there are no right cubes at the top
            if ( !(cube.Arr[1, 0, 0].y == topColor && cube.Arr[1, 0, 2].y == topColor ||
                   cube.Arr[0, 0, 1].y == topColor && cube.Arr[2, 0, 1].y == topColor) &&
                !(cube.Arr[1, 0, 0].y == topColor && cube.Arr[0, 0, 1].y == topColor ||
                  cube.Arr[1, 0, 0].y == topColor && cube.Arr[2, 0, 1].y == topColor ||
                  cube.Arr[2, 0, 1].y == topColor && cube.Arr[1, 0, 2].y == topColor ||
                  cube.Arr[1, 0, 2].y == topColor && cube.Arr[0, 0, 1].y == topColor))
            {
                moves = moves.Concat(cube.TopCrossFruRuf());
            }

            //if we have the corner
            if (!(cube.Arr[1, 0, 0].y == topColor && cube.Arr[1, 0, 2].y == topColor ||
                   cube.Arr[0, 0, 1].y == topColor && cube.Arr[2, 0, 1].y == topColor))
            {
                while (!(cube.Arr[0, 0, 1].y == topColor && cube.Arr[1, 0, 2].y == topColor))
                    moves = moves.Append(cube.RightCubeTurn());
                moves = moves.Concat(cube.TopCrossFruRuf());
            }

            //if we have the line
            if (!(cube.Arr[1, 0, 0].y == topColor && cube.Arr[1, 0, 2].y == topColor &&
                   cube.Arr[0, 0, 1].y == topColor && cube.Arr[2, 0, 1].y == topColor))
            {
                while (!(cube.Arr[0, 0, 1].y == topColor && cube.Arr[2, 0, 1].y == topColor))
                    moves = moves.Append(cube.RightCubeTurn());
                moves = moves.Concat(cube.TopCrossFruRuf());
            }
        }

        /// <summary>
        /// Makes top cross with pieces that oriented properly
        /// </summary>
        /// <param name="cube"></param>
        public static void OrientSecondCross(Cube cube)
        {
            while (!(cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                   !(cube.Arr[1, 0, 0].z == cube.Arr[1, 1, 0].z && cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z) &&
                !(cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                !(cube.Arr[1, 0, 0].z == cube.Arr[1, 1, 0].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                !(cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x) &&
                !(cube.Arr[1, 0, 0].z == cube.Arr[1, 1, 0].z && cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x))
                moves = moves.Append(cube.U());

            while (!(cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                !(cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x))
                moves = moves.Append(cube.RightCubeTurn());

            //if only left & right sides fits right
            if ((cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                !(cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x))
            {
                moves = moves.Append(cube.R());
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.R(TurnDirection.counterClockwise));
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.R());
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.R(TurnDirection.counterClockwise));
                while (!(cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                   !(cube.Arr[1, 0, 0].z == cube.Arr[1, 1, 0].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                   !(cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x) &&
                   !(cube.Arr[1, 0, 0].z == cube.Arr[1, 1, 0].z && cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x))
                    moves = moves.Append(cube.U());
                while (!(cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x))
                    moves = moves.Append(cube.RightCubeTurn());
            }

            //if top & right sides fits right
            if (!(cube.Arr[0, 0, 1].x == cube.Arr[0, 1, 1].x && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x) &&
                (cube.Arr[1, 0, 2].z == cube.Arr[1, 1, 2].z && cube.Arr[2, 0, 1].x == cube.Arr[2, 1, 1].x))
            {
                moves = moves.Append(cube.R());
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.R(TurnDirection.counterClockwise));
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.R());
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.U());
                moves = moves.Append(cube.R(TurnDirection.counterClockwise));
                moves = moves.Append(cube.U());
            }
        }

        /// <summary>
        /// Puts last corners at proper places, but not orient them
        /// </summary>
        /// <param name="cube"></param>
        public static void SecondCorners(Cube cube)
        {
            var topCol = cube.Arr[1, 0, 1].y;
            var frontCol = cube.Arr[1, 1, 0].z;
            var rightCol = cube.Arr[2, 1, 1].x;
            var leftCol = cube.Arr[0, 1, 1].x;
            var backCol = cube.Arr[1, 1, 2].z;

            var lFCell = cube.Find(leftCol, frontCol, topCol);
            var rFCell = cube.Find(rightCol, frontCol, topCol);
            var lBCell = cube.Find(leftCol, backCol, topCol);
            var rBCell = cube.Find(rightCol, backCol, topCol);

            //trying to find one right placed corner
            bool isFound = false;
            for (int i = 0; i < 4; i++)
            {
                if (lFCell.x == 0 && lFCell.y == 0 && lFCell.z == 0 ||
                    rFCell.x == 2 && rFCell.y == 0 && rFCell.z == 0 ||
                    lBCell.x == 0 && lBCell.y == 0 && lBCell.z == 2 ||
                    rBCell.x == 2 && rBCell.y == 0 && rBCell.z == 2)
                {
                    isFound = true;
                    break;
                }
                moves = moves.Append(cube.U());
            }
            if (!isFound)
            {
                moves = moves.Concat(cube.SecondCorners());
                lFCell = cube.Find(leftCol, frontCol, topCol);
                rFCell = cube.Find(rightCol, frontCol, topCol);
                lBCell = cube.Find(leftCol, backCol, topCol);
                rBCell = cube.Find(rightCol, backCol, topCol);
                for (int i = 0; i < 4; i++)
                {
                    if (lFCell.x == 0 && lFCell.y == 0 && lFCell.z == 0 ||
                    rFCell.x == 2 && rFCell.y == 0 && rFCell.z == 0 ||
                    lBCell.x == 0 && lBCell.y == 0 && lBCell.z == 2 ||
                    rBCell.x == 2 && rBCell.y == 0 && rBCell.z == 2)
                    {
                        break;
                    }
                    moves = moves.Append(cube.U());
                }
            }

            while (!(rFCell.x == 2 && rFCell.y == 0 && rFCell.z == 0))
            {
                moves = moves.Append(cube.RightCubeTurn());
                frontCol = cube.Arr[1, 1, 0].z;
                rightCol = cube.Arr[2, 1, 1].x;
                rFCell = cube.Find(rightCol, frontCol, topCol);  
            }

            leftCol = cube.Arr[0, 1, 1].x;
            backCol = cube.Arr[1, 1, 2].z;
            lFCell = cube.Find(leftCol, frontCol, topCol);
            lBCell = cube.Find(leftCol, backCol, topCol);
            rBCell = cube.Find(rightCol, backCol, topCol);
            while (!(lFCell.x == 0 && lFCell.y == 0 && lFCell.z == 0 &&
                   rFCell.x == 2 && rFCell.y == 0 && rFCell.z == 0 &&
                   lBCell.x == 0 && lBCell.y == 0 && lBCell.z == 2 &&
                   rBCell.x == 2 && rBCell.y == 0 && rBCell.z == 2))
            {
                moves = moves.Concat(cube.SecondCorners());
                lFCell = cube.Find(leftCol, frontCol, topCol);
                rFCell = cube.Find(rightCol, frontCol, topCol);
                lBCell = cube.Find(leftCol, backCol, topCol);
                rBCell = cube.Find(rightCol, backCol, topCol);
            }
        }


        /// <summary>
        /// Turnes all last corners properly
        /// </summary>
        /// <param name="cube"></param>
        public static void TurnLastCorners(Cube cube)
        {
            var topColor = cube.Arr[1, 0, 1].y;
            for (int i = 0; i < 4; i++)
            {
                while (cube.Arr[2, 0, 0].y != topColor)
                {
                    moves = moves.Concat(cube.SwapFrontRightCorners());
                }

                moves = moves.Append(cube.U());
            }
        }
    }
}
