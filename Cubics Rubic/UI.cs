using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Cubics_Rubic
{
    public static class UI
    {
        static string contentPath;
        static System.Drawing.Image redCell;
        static System.Drawing.Image whiteCell;
        static System.Drawing.Image greenCell;
        static System.Drawing.Image blueCell;
        static System.Drawing.Image yellowCell;
        static System.Drawing.Image orangeCell;
        static System.Drawing.Image redTopCell;
        static System.Drawing.Image whiteTopCell;
        static System.Drawing.Image greenTopCell;
        static System.Drawing.Image blueTopCell;
        static System.Drawing.Image yellowTopCell;
        static System.Drawing.Image orangeTopCell;
        static System.Drawing.Image redRightCell;
        static System.Drawing.Image whiteRightCell;
        static System.Drawing.Image greenRightCell;
        static System.Drawing.Image blueRightCell;
        static System.Drawing.Image yellowRightCell;
        static System.Drawing.Image orangeRightCell;

        static Point[,] frontXY = new Point[,]
        {
            { new Point(10, 190), new Point(10, 290), new Point(10, 390) },
            { new Point(110, 190), new Point(110, 290), new Point(110, 390) },
            { new Point(210, 190), new Point(210, 290), new Point(210, 390) },
        };
        static Point[,] topXY = new Point[,]
        {
            { new Point(10, 130), new Point(110, 130), new Point(210, 130) },
            { new Point(70, 70), new Point(170, 70), new Point(270, 70) },
            { new Point(130, 10), new Point(230, 10), new Point(330, 10) },
        };
        static Point[,] rightXY = new Point[,]
        {
            { new Point(310, 130), new Point(310, 230), new Point(310, 330) },
            { new Point(370, 70), new Point(370, 170), new Point(370, 270) },
            { new Point(430, 10), new Point(430, 110), new Point(430, 210) },
        };

        static UI()
        {
            HttpContext context = HttpContext.Current;
            contentPath = context.Server.MapPath("Content\\");

            redCell = System.Drawing.Image.FromFile(contentPath + "resources\\red.png");
            whiteCell = System.Drawing.Image.FromFile(contentPath + "resources\\white.png");
            greenCell = System.Drawing.Image.FromFile(contentPath + "resources\\green.png");
            blueCell = System.Drawing.Image.FromFile(contentPath + "resources\\blue.png");
            yellowCell = System.Drawing.Image.FromFile(contentPath + "resources\\yellow.png");
            orangeCell = System.Drawing.Image.FromFile(contentPath + "resources\\orange.png");
            redTopCell = System.Drawing.Image.FromFile(contentPath + "resources\\redT.png");
            whiteTopCell = System.Drawing.Image.FromFile(contentPath + "resources\\whiteT.png");
            greenTopCell = System.Drawing.Image.FromFile(contentPath + "resources\\greenT.png");
            blueTopCell = System.Drawing.Image.FromFile(contentPath + "resources\\blueT.png");
            yellowTopCell = System.Drawing.Image.FromFile(contentPath + "resources\\yellowT.png");
            orangeTopCell = System.Drawing.Image.FromFile(contentPath + "resources\\orangeT.png");
            redRightCell = System.Drawing.Image.FromFile(contentPath + "resources\\redR.png");
            whiteRightCell = System.Drawing.Image.FromFile(contentPath + "resources\\whiteR.png");
            greenRightCell = System.Drawing.Image.FromFile(contentPath + "resources\\greenR.png");
            blueRightCell = System.Drawing.Image.FromFile(contentPath + "resources\\blueR.png");
            yellowRightCell = System.Drawing.Image.FromFile(contentPath + "resources\\yellowR.png");
            orangeRightCell = System.Drawing.Image.FromFile(contentPath + "resources\\orangeR.png");
        }

        private static System.Drawing.Image color2png(color col, side side)
        {
            return side switch
            {
                side.front => col switch
                {
                    color.White => whiteCell,
                    color.Blue => blueCell,
                    color.Orange => orangeCell,
                    color.Red => redCell,
                    color.Yellow => yellowCell,
                    color.Green => greenCell,
                    _ => null
                },
                side.top => col switch
                {
                    color.White => whiteTopCell,
                    color.Blue => blueTopCell,
                    color.Orange => orangeTopCell,
                    color.Red => redTopCell,
                    color.Yellow => yellowTopCell,
                    color.Green => greenTopCell,
                    _ => null
                },
                side.right => col switch
                {
                    color.White => whiteRightCell,
                    color.Blue => blueRightCell,
                    color.Orange => orangeRightCell,
                    color.Red => redRightCell,
                    color.Yellow => yellowRightCell,
                    color.Green => greenRightCell,
                    _ => null
                },
                _ => null,
            };
        }


        public static void Draw(Cube cube)
        {
            System.Drawing.Image i = System.Drawing.Image.FromFile(contentPath + "white.png");

            Graphics g = Graphics.FromImage(i);
            //g.Clear(Color.White);

            //front side
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    g.DrawImage(color2png(cube.Arr[x, y, 0].z, side.front), frontXY[x, y].X, frontXY[x, y].Y);

            //top side
            for (int z = 0; z < 3; z++)
                for (int x = 0; x < 3; x++)
                    g.DrawImage(color2png(cube.Arr[x, 0, z].y, side.top), topXY[z, x].X, topXY[z, x].Y);


            //right side
            for (int z = 0; z < 3; z++)
                for (int y = 0; y < 3; y++)
                    g.DrawImage(color2png(cube.Arr[2, y, z].x, side.right), rightXY[z, y].X, rightXY[z, y].Y);

            //var picName = $"{contentPath}temp {DateTime.Now.Ticks}.png";
            i.Save(contentPath + "temp.png");

            g.Dispose();
            i.Dispose();
        }


        enum side { front, right, top };
    }
}