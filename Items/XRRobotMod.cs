using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace XRaces.Items {
    public class XRRobotMod : GlobalItem {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            XRPlayer player = Main.player[item.owner].GetModPlayer<XRPlayer>();
            if (player.race != Race.Robot) return;
            int damageIndex = -1, speedIndex = -1, tooltipIndex = -1, modIndex = -1;
            for (int i = 0; i < tooltips.Count; i++) {
                if (tooltips[i].Name.Contains("Tooltip")) tooltipIndex = i;
                if (tooltips[i].Name.Equals("PrefixDamage")) damageIndex = i;
                if (tooltips[i].Name.Equals("PrefixSpeed")) speedIndex = i;
                if (tooltips[i].mod.Equals("Terraria")) modIndex = i;
            }
            if (tooltipIndex == -1) tooltipIndex = tooltips.Count - 1;
            TooltipLine line;

            if (item.summon) {
                bool bad = true;
                if (damageIndex != -1) {
                    int damage = int.Parse(tooltips[damageIndex].text.Substring(1, tooltips[damageIndex].text.IndexOf("%") - 1));
                    if (tooltips[damageIndex].text[0] == '-') damage *= -1;
                    damage -= 50;
                    bad = (damage < 0);

                    tooltips[damageIndex].text = ((!bad) ? "+" : "") + damage + "% damage";
                    tooltips[damageIndex].isModifierBad = bad;
                    tooltips[damageIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "Robot", "-50% damage");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(++tooltipIndex, line);
                    damageIndex = tooltipIndex;
                }
            }

            if (!item.magic && !item.summon && item.damage > 0) {
                int dam = (int)(((0.75f + (player.manaMaxMul * 0.50f)) - 1) * 100f);
                bool bad = dam < 0;
                if (damageIndex != -1) {
                    int damage = int.Parse(tooltips[damageIndex].text.Substring(1, tooltips[damageIndex].text.IndexOf("%") - 1));
                    if (tooltips[damageIndex].text[0] == '-') damage *= -1;
                    damage += dam;
                    bad = (damage < 0);

                    tooltips[damageIndex].text = ((!bad) ? "+" : "") + damage + "% damage";
                    tooltips[damageIndex].isModifierBad = bad;
                    tooltips[damageIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "Robot", ((!bad) ? "+" : "") + dam + "% damage");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(++tooltipIndex, line);
                    damageIndex = tooltipIndex;
                    if (speedIndex != -1) speedIndex++;
                }

                if (item.ranged) return;
                int speed = (int)((0.50f + (player.manaMaxMul * 0.75f) - 1) / 1f * 100f);
                bad = (speed < 0);
                if (speedIndex != -1) {
                    int manaCost = int.Parse(tooltips[speedIndex].text.Substring(1, tooltips[speedIndex].text.IndexOf("%") - 1));
                    if (tooltips[speedIndex].text[0] == '-') manaCost *= -1;
                    manaCost += speed;
                    bad = (manaCost < 0);

                    tooltips[speedIndex].text = ((!bad) ? "+" : "") + manaCost + "% speed";
                    tooltips[speedIndex].isModifierBad = bad;
                    tooltips[speedIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "Robot", ((!bad) ? "+" : "") + speed + "% speed");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(damageIndex + 1, line);
                }
            }
        }
    }
}