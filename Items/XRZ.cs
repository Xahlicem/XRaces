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
    public class XRZ : GlobalItem {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips) {
            XRPlayer player = Main.player[item.owner].GetModPlayer<XRPlayer>();
            if (!(item.damage > 0)) return;
            for (int i = 0; i < tooltips.Count; i++)
                if (tooltips[i].Name.Equals("Damage")) {
                    string[] text = tooltips[i].text.Split(' ');
                    Item baseItem = new Item();
                    baseItem.CloneDefaults(item.type);
                    int damage = int.Parse(text[0]);
                    damage -= baseItem.damage;
                    if (damage == 0) return;
                    tooltips[i].text = text[0] + "(" + ((damage > 0) ? "+" : "-") + Math.Abs(damage) + ")";
                    for (int j = 1; j < text.Length; j++)
                        tooltips[i].text += " " + text[j];
                }
        }
    }
}