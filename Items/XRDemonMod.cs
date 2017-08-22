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
    public class XRDemonMod : GlobalItem {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            XRPlayer player = Main.player[item.owner].GetModPlayer<XRPlayer>();
            if (player.race != Race.Demon) return;
            int damageIndex = -1, manaCostIndex = -1, tooltipIndex = -1, modIndex = -1;
            for (int i = 0; i < tooltips.Count; i++) {
                if (tooltips[i].Name.Contains("Tooltip")) tooltipIndex = i;
                if (tooltips[i].Name.Equals("PrefixDamage")) damageIndex = i;
                if (tooltips[i].Name.Equals("PrefixUseMana")) manaCostIndex = i;
                if (tooltips[i].mod.Equals("Terraria")) modIndex = i;
            }
            if (tooltipIndex == -1) tooltipIndex = tooltips.Count - 1;
            TooltipLine line;

            if (item.ranged) {
                bool bad = false;
                if (damageIndex != -1) {
                    int damage = int.Parse(tooltips[damageIndex].text.Substring(1, tooltips[damageIndex].text.IndexOf("%") - 1));
                    if (tooltips[damageIndex].text[0] == '-') damage *= -1;
                    damage -= 25;
                    bad = (damage < 0);

                    tooltips[damageIndex].text = ((!bad) ? "+" : "") + damage + "% damage";
                    tooltips[damageIndex].isModifierBad = bad;
                    tooltips[damageIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "DemonMagic", "+25% damage");
                    line.isModifier = !bad;
                    line.isModifierBad = bad;
                    tooltips.Insert(++tooltipIndex, line);
                    damageIndex = tooltipIndex;
                }
            }

            if (item.magic) {
                bool bad = false;
                if (damageIndex != -1) {
                    //int damage = int.Parse(tooltips[damageIndex].text.Substring(1, tooltips[damageIndex].text.IndexOf("%") - 1));
                    //if (tooltips[damageIndex].text[0] == '-') damage *= -1;
                    //damage += 25;
                    //bad = (damage < 0);

                    //tooltips[damageIndex].text = ((!bad) ? "+" : "") + damage + "% damage";
                    //tooltips[damageIndex].isModifierBad = bad;
                    //tooltips[damageIndex].isModifier = true;
                } else {
                    //line = new TooltipLine(mod, "DemonMagic", "+25% damage");
                    //line.isModifier = !bad;
                    //line.isModifierBad = bad;
                    //tooltips.Insert(++tooltipIndex, line);
                    damageIndex = tooltipIndex;
                }

                int cost = (int)((0.50f - (player.player.position.Y / Main.bottomWorld)) * 100f);
                bad = (cost > 0);
                if (manaCostIndex != -1) {
                    int manaCost = int.Parse(tooltips[manaCostIndex].text.Substring(1, tooltips[manaCostIndex].text.IndexOf("%") - 1));
                    if (tooltips[manaCostIndex].text[0] == '-') manaCost *= -1;
                    manaCost += cost;
                    bad = (manaCost > 0);

                    tooltips[manaCostIndex].text = ((bad) ? "+" : "") + manaCost + "% mana cost";
                    tooltips[manaCostIndex].isModifierBad = bad;
                    tooltips[manaCostIndex].isModifier = true;
                } else {
                    line = new TooltipLine(mod, "DemonMagic", ((bad) ? "+" : "") + cost + "% mana cost");
                    line.isModifier = true;
                    line.isModifierBad = bad;
                    tooltips.Insert(damageIndex + 1, line);
                }
            }

            int index = player.player.FindBuffIndex(mod.BuffType<Buffs.Bloodlust>());
            if (index != -1 && item.damage >= 1) {
                int damage = (int)((player.player.buffTime[index] / 120f) * 0.01f * 100f);
                line = new TooltipLine(mod, "BloodlustMod", "Bloodlust: +" + damage + "% damage");
                line.isModifier = true;
                if (damageIndex != -1) tooltips.Insert(damageIndex, line);
                else if (modIndex != -1) tooltips.Insert(tooltips.Count - 1, line);
                else tooltips.Add(line);
            }
        }
    }
}