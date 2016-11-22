using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;

namespace Annie_O_Matic
{
    internal class AnnieMenu
    {
        public static Menu Config;
        public static Orbwalking.Orbwalker Orbwalker;
        

        public static void CreateMenu()
        {
            
            Config = new Menu("Annie-O-Matic", "Annie-O-Matic", true);
            Config.AddSubMenu(new Menu("Orbwalking", "Orbwalking"));

            var targetSelectorMenu = new Menu("Target Selector", "Target Selector");
            TargetSelector.AddToMenu(targetSelectorMenu);
            Config.AddSubMenu(targetSelectorMenu);

            Orbwalker = new Orbwalking.Orbwalker(Config.SubMenu("Orbwalking"));

            #region Draws

            var drawMenu = new Menu("Drawing-o-Matic Settings", "Drawing-o-Matic Settings");
            {
                drawMenu.AddItem(new MenuItem("drawMenu.drawOn", "Drawings")).SetValue(true);
                drawMenu.AddItem(new MenuItem("drawMenu.drawQ", "Draw - Q -")).SetValue(true);
                drawMenu.AddItem(new MenuItem("drawMenu.drawW", "Draw - W -")).SetValue(true);
                drawMenu.AddItem(new MenuItem("drawMenu.drawR", "Draw - R -")).SetValue(true);
            }
            Config.AddSubMenu(drawMenu);
            #endregion

            #region WomboCombo
            var comboMenu = new Menu("Combo-O-Matic Settings", "Combo-O-Matic Settings");
            {
                comboMenu.AddItem(new MenuItem("comboMenu.comboOrder", "Combo Type").SetValue(new StringList(new[] { "R - W - Q", "R - Q - W", "Q - W - R" })));
                comboMenu.AddItem(new MenuItem("comboMenu.canQ", "Use - Q -")).SetValue(false);
                comboMenu.AddItem(new MenuItem("comboMenu.canW", "Use - W - To Last Hit")).SetValue(true);
                comboMenu.AddItem(new MenuItem("comboMenu.canR", "Use - R -")).SetValue(false);
                comboMenu.AddItem(new MenuItem("comboMenu.useRifHit", "Use - R -  If Hits X Enemies"))
                       .SetValue(new Slider(2, 1, 5));

                Config.AddSubMenu(comboMenu);   
            }

            #endregion

            #region Farming

            var farmMenu = new Menu("Farming Settings", "Farming Settings");
            {
                var laneClearMenu = new Menu("Lane-o-Clear Settings", "Lane-o-Clear Settings");
                {
                    laneClearMenu.AddItem(new MenuItem("farmMenu.laneClearMenu.canQ", "Use - Q -")).SetValue(false);
                    laneClearMenu.AddItem(new MenuItem("farmMenu.laneClearMenu.useQlast", "Use - Q - To Last Hit")).SetValue(true);
                    laneClearMenu.AddItem(new MenuItem("farmMenu.laneClearMenu.canW", "Use - W -")).SetValue(false);
                    laneClearMenu.AddItem(new MenuItem("farmMenu.laneClearMenu.manalimit", "Mana X Limit (%)"))
                        .SetValue(new Slider(60, 1, 100));
                    laneClearMenu.AddItem(new MenuItem("farmMenu.laneClearMenu.usewslider", "Use [W] If Hits X Enemies"))
                        .SetValue(new Slider(3, 1, 5));
                    laneClearMenu.AddItem(new MenuItem("farmMenu.laneClearMenu.saveBuff", "Save passive?"))
                        .SetValue(false);
                    laneClearMenu.AddItem(new MenuItem("farmMenu.laneClearMenu.mambo",
                        "MAMBO LIKE A CRAZY  [! DANGER LOW MANA RISK !] ")).SetValue(false);
                    farmMenu.AddSubMenu(laneClearMenu);
                }

                var lastHitMenu = new Menu("Last-o-Hit Settings", "Last-o-Hit Settings");
                {

                    lastHitMenu.AddItem(new MenuItem("farmMenu.lastHitMenu.useqlast", "Use [Q] To Last Hit")).SetValue(true);
                    lastHitMenu.AddItem(new MenuItem("farmMenu.lastHitMenu.saveBuff", "Save passive?")).SetValue(true);
                    farmMenu.AddSubMenu(lastHitMenu);
                }
                Config.AddSubMenu(farmMenu);
            } 
            #endregion

            #region AutoStack

            var miscMenu = new Menu("Stack-O-Matic", "Stack-O-Matic");
            {
                miscMenu.AddItem(new MenuItem("miscMenu.stackPassive", "Stack Passive ?")).SetValue(true);
            }
            Config.AddSubMenu(miscMenu);
            #endregion
            Config.AddToMainMenu();
        }
    }
}
           