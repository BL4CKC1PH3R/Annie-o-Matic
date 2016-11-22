using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;



namespace Annie_O_Matic 
{
    internal class Annie : AnnieMenu
    {
        public static Obj_AI_Hero Player { get { return ObjectManager.Player; } }

        #region Spells 
        //Spells
        public static Spell Q = new Spell(SpellSlot.Q, 625);
        public static Spell W = new Spell(SpellSlot.W, 550);
        public static Spell E = new Spell(SpellSlot.E);
        public static Spell R = new Spell(SpellSlot.R, 600);  
        #endregion

        #region Passive
        //Thx Hoes
        public static int GetPassiveBuff
        {
            get
            {
                var data = ObjectManager.Player.Buffs.FirstOrDefault(b => b.DisplayName == "Pyromania");
                return data != null ? data.Count : 0;
            }
        }
        #endregion
        public static void Load(EventArgs args)
        {

            if (Player.ChampionName != "Annie")
            {
                return;
            }

            CreateMenu();
            W.SetSkillshot(0.5f, 250f, float.MaxValue, false, SkillshotType.SkillshotCone);
            R.SetSkillshot(0.2f, 250f, float.MaxValue, false, SkillshotType.SkillshotCircle);
            Game.OnUpdate += OnGameUpdate;
            Drawing.OnDraw += AnnieDraws.OnDraw;
        }

        private static void LastHit()
        {
            var canQ = Config.Item("farmMenu.lastHitMenu.useqlast").GetValue<bool>();
            var saveBuff = Config.Item("farmMenu.laneClearMenu.saveBuff").GetValue<bool>();
            var buffStatus = ObjectManager.Player.HasBuff("pyromania_particle");

            if (saveBuff && buffStatus)
                return;

            var minionNumber =
               MinionManager.GetMinions(Player.Position, Q.Range, MinionTypes.All,
                   MinionTeam.NotAlly).FirstOrDefault();

            if (minionNumber == null)
                return;

            var minion = minionNumber;
            var minionhp = minion.Health;

            if (minionhp <= Q.GetDamage(minion) && canQ && Q.IsReady())
            {
                Q.Cast(minion);
            }
        }

        private static void LaneClear()
        {
            var canQ = Config.Item("farmMenu.laneClearMenu.canQ").GetValue<bool>();
            var canW = Config.Item("farmMenu.laneClearMenu.canW").GetValue<bool>();
            var mambo = Config.Item("farmMenu.laneClearMenu.mambo").GetValue<bool>();
            var minMana = Config.Item("farmMenu.laneClearMenu.manalimit").GetValue<Slider>().Value;
            var minW = Config.Item("farmMenu.laneClearMenu.usewslider").GetValue<Slider>().Value;
            var saveBuff = Config.Item("farmMenu.laneClearMenu.saveBuff").GetValue<bool>();
            var buffStatus = ObjectManager.Player.HasBuff("pyromania_particle");

            if (saveBuff && buffStatus)
                return;


            #region Q Usage
            var minionNumber =
                 MinionManager.GetMinions(Player.Position, Q.Range,
                 MinionTypes.All, MinionTeam.NotAlly).FirstOrDefault();

            if (minionNumber == null)
                return;

            var minion = minionNumber;

            var minionhp = minion.Health;

           

            if (canQ && Q.IsReady() && minion.IsValidTarget(Q.Range) && minionhp <= Q.GetDamage(minion) && minionhp > Player.GetAutoAttackDamage(minion) && Player.ManaPercent >= minMana)
            {
                Q.Cast(minion);
            }
            
            #endregion

            #region W Usage
            
            var wMinionNumber =
                MinionManager.GetMinions(Player.Position,
                W.Range, MinionTypes.All, MinionTeam.NotAlly);

            if (wMinionNumber == null)
                return;

            var wPredict = W.GetLineFarmLocation(wMinionNumber);

            if (canW && W.IsReady() && minion.IsValidTarget(W.Range) && minionhp <= W.GetDamage(minion) && Player.ManaPercent >= minMana && wPredict.MinionsHit >= 3)
            {
                W.Cast(minion);
            } 
            #endregion


        }

        private static void Combo()
        {
            var target = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Magical);
            if (target == null)
                return;

            var canQ = Config.Item("comboMenu.canQ").GetValue<bool>();
            var canW = Config.Item("comboMenu.canW").GetValue<bool>();
            var canR = Config.Item("comboMenu.canR").GetValue<bool>();

            

            if (Q.IsReady() && canQ && target.IsValidTarget(Q.Range))
            {
                if (!R.Cast())
                {
                    Q.Cast(target);
                }

                else
                {
                    R.Cast(target.Position);

                    Q.Cast(target);
                }

            }

            

           

            if (W.IsReady() && canW && target.IsValidTarget(W.Range))
            {
                W.Cast(target.Position);
            }


        }

        private static void OnGameUpdate(EventArgs args)
        {
            Orbwalker.SetAttack(true);

            switch (Orbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.LastHit:
                    LastHit();
                    break;

                case Orbwalking.OrbwalkingMode.LaneClear:
                    LaneClear();
                    break;

                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;
            }
            autoStack();
        }

        private static void DamageCalc()
        {
            var target = TargetSelector.GetTarget(Player.CastRange, TargetSelector.DamageType.Magical);

            var qDamage = Q.GetDamage(target);
            var wDamage = W.GetDamage(target);
            var rDamage = R.GetDamage(target);

            var totalDamage = qDamage + wDamage + rDamage;
            string totalmana = Q.ManaCost.ToString();

            Game.PrintChat(totalmana);
        }

        private static void autoStack()
        {
            var canStack = Config.Item("miscMenu.stackPassive").GetValue<bool>();
            if (ObjectManager.Player.HasBuff("pyromania_particle") == false)
            {
                if (canStack)
                {
                    E.Cast();
                    if (E.Cooldown > 0)
                    {
                        W.Cast(Player.Position);
                    }
                }
            }

        }
    }
}
