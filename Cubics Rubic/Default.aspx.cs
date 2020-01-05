using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace Cubics_Rubic
{
    public partial class _Default : Page
    {
        static Cube cube;
        static List<Move> movesToSolve = new List<Move>();
        static bool timerEnabled = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (cube == null)
                if (Session["Cube"] != null)
                    cube = new Cube((Cube)Session["Cube"]);
                else
                {
                    cube = new Cube();
                    Session["Cube"] = new Cube(cube);
                }
            if (movesToSolve.Count == 0)
                if (Session["Moves"] != null)
                    movesToSolve = (List<Move>)Session["Moves"];
            UI.Draw(cube);
            if (timerEnabled)
                Timer1.Enabled = true;
        }

        protected void ShuffleBtn_Click(object sender, EventArgs e)
        {
            var random = new Random();
            var stepCount = random.Next(50, 100);

            for (int i = 0; i < stepCount; i++)
            {
                var turn = (Move)random.Next(0, 5);
                cube.DoMove(turn);
                ListBox1.Items.Add($"{i} {turn.ToString()}");
            }

            Session["Cube"] = new Cube(cube);
            UI.Draw(cube);
            Timer1.Enabled = false;
        }

        protected void SolveBtn_Click(object sender, EventArgs e)
        {
            movesToSolve = AI.Solve(cube).ToList();
            Session["Moves"] = movesToSolve;

            ListBox1.Items.Clear();
            for (int i = 0; i < movesToSolve.Count; i++)
            {
                ListBox1.Items.Add($"{i} {movesToSolve.ElementAt(i).ToString()}");
            }

            Timer1.Enabled = true;
        }

        protected void MoveBtn_Click(object sender, EventArgs e)
        {
            if (movesToSolve.Count>0)
            {
                cube.DoMove(movesToSolve.First());
                movesToSolve.RemoveAt(0);
                Session["Cube"] = new Cube(cube);
                Session["Moves"] = movesToSolve;

                ListBox1.Items.Clear();
                for (int i = 0; i < movesToSolve.Count; i++)
                {
                    ListBox1.Items.Add($"{i} {movesToSolve.ElementAt(i).ToString()}");
                }

                UI.Draw(cube);
            }
            else
                Timer1.Enabled = false;
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (movesToSolve.Count > 0)
            {
                cube.DoMove(movesToSolve.First());
                movesToSolve.RemoveAt(0);
                Session["Cube"] = new Cube(cube);
                Session["Moves"] = movesToSolve;

                ListBox1.Items.Clear();
                for (int i = 0; i < movesToSolve.Count; i++)
                {
                    ListBox1.Items.Add($"{i} {movesToSolve.ElementAt(i).ToString()}");
                }

                UI.Draw(cube);
            }
            else
                Timer1.Enabled = false;
        }
    }
}