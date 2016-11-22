using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace Annie_O_Matic
{
    class AnnieDraws : Annie
    {
        internal static void OnDraw(EventArgs args)
        {
            var draw = Config.Item("drawMenu.drawOn").GetValue<bool>();
            var useq = Config.Item("drawMenu.drawQ").GetValue<bool>();
            var usew = Config.Item("drawMenu.drawW").GetValue<bool>();
            var user = Config.Item("drawMenu.drawR").GetValue<bool>();
            
            

            if (!draw)
                return;

            if (useq && Q.IsReady() && Q.Level > 0)
            {
                Render.Circle.DrawCircle(Player.Position, Q.Range, System.Drawing.Color.Fuchsia,3);
            }

            if (usew && W.IsReady() && W.Level > 0)
            {
                Render.Circle.DrawCircle(Player.Position, W.Range, System.Drawing.Color.Goldenrod, 3);
                
            }

            if (user && R.IsReady() && R.Level > 0)
            {
                
                Render.Circle.DrawCircle(Player.Position, R.Range, System.Drawing.Color.Red, 3);
            }

            
                var pos = ObjectManager.Player.HPBarPosition;

                Drawing.DrawText(Drawing.Width * 0.1f, Drawing.Height * 0.5f, System.Drawing.Color.Red, "Passive ON!" );
            
        }
    }
}
