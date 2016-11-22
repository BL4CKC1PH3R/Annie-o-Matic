using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LeagueSharp.Common;
using LeagueSharp;
using SharpDX;

namespace Annie_O_Matic
{
    class Program
    {
        static void Main(string[] args)
        {
            
            CustomEvents.Game.OnGameLoad += Annie.Load;

        }
    }
}
