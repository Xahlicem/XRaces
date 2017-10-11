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
    public class XRZombieMod : GlobalItem {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            XRPlayer player = Main.player[item.owner].GetModPlayer<XRPlayer>();
            if (player.race != Race.Zombie) return;
            int damageIndex = -1, speedIndex = -1, tooltipIndex = -1, modIndex = -1;
            for (int i = 0; i < tooltips.Count; i++) {
                if (tooltips[i].Name.Contains("Tooltip")) tooltipIndex = i;
                if (tooltips[i].Name.Equals("PrefixDamage")) damageIndex = i;
                if (tooltips[i].Name.Equals("PrefixSpeed")) speedIndex = i;
                if (tooltips[i].mod.Equals("Terraria")) modIndex = i;
            }
            if (tooltipIndex == -1) tooltipIndex = tooltips.Count - 1;
            TooltipLine line;

            if (item.ranged) {
                bool bad = true;
                if (damageIndex != -1) {
                    int damage = int.Parse(tooltips[damageIndex].text.Substring(1, tooltips[damageIndex].text.IndexOf("%") - 1));
                    if (tooltips[damageIndex].text[0] == '-') damage *= -1;
                    damage -= 75;
                    bad = (damage < 0);

                    tooltips[damageIndex].text = ((!bad) ? "+" : "") + damage + "% damage";
                    tooltips[damageIndex].isModifierBad = bad;
                    tooltips[damageIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "Zombie", "-50% damage");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(++tooltipIndex, line);
                    damageIndex = tooltipIndex;
                }
            } else if (item.thrown) {
                bool bad = true;
                if (damageIndex != -1) {
                    int damage = int.Parse(tooltips[damageIndex].text.Substring(1, tooltips[damageIndex].text.IndexOf("%") - 1));
                    if (tooltips[damageIndex].text[0] == '-') damage *= -1;
                    damage -= 25;
                    bad = (damage < 0);

                    tooltips[damageIndex].text = ((!bad) ? "+" : "") + damage + "% damage";
                    tooltips[damageIndex].isModifierBad = bad;
                    tooltips[damageIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "Zombie", "-50% damage");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(++tooltipIndex, line);
                    damageIndex = tooltipIndex;
                }
            }

            if (item.melee && item.damage > 0) {
                bool bad = false;
                if (damageIndex != -1) {
                    int damage = int.Parse(tooltips[damageIndex].text.Substring(1, tooltips[damageIndex].text.IndexOf("%") - 1));
                    if (tooltips[damageIndex].text[0] == '-') damage *= -1;
                    damage += 75;
                    bad = (damage < 0);

                    tooltips[damageIndex].text = ((!bad) ? "+" : "") + damage + "% damage";
                    tooltips[damageIndex].isModifierBad = bad;
                    tooltips[damageIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "Zombie", ((!bad) ? "+" : "") + 75 + "% damage");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(++tooltipIndex, line);
                    damageIndex = tooltipIndex;
                }

                bad = true;
                if (speedIndex != -1) {
                    int speed = int.Parse(tooltips[speedIndex].text.Substring(1, tooltips[speedIndex].text.IndexOf("%") - 1));
                    if (tooltips[speedIndex].text[0] == '-') speed *= -1;
                    speed += -25;
                    bad = (speed < 0);

                    tooltips[speedIndex].text = ((!bad) ? "+" : "") + speed + "% speed";
                    tooltips[speedIndex].isModifierBad = bad;
                    tooltips[speedIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "Zombie", ((!bad) ? "+" : "-") + 25 + "% speed");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(damageIndex + 1, line);
                }
            }
        }
    }
}