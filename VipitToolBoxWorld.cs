using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace VipixToolBox
{
    /*
    class VipixToolBoxWorld : ModWorld
    {
        public static Dictionary<string, bool> toolEnabled;

        public override void Initialize()
        {
            toolEnabled = new Dictionary<string, bool>
            {
                { "all", false },
                { "AutoHammer", true },
                { "BlockWand", true },
                { "ColorPalette", true },
                { "LevitationWand", true },
                { "RattlesnakeWand", true },
                { "StaffofRegrowthEdit", true },
                { "WallHammer", true }
            };
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte bits = new BitsByte();

            int i = 0;
            bits[i++] = toolEnabled["all"];
            bits[i++] = toolEnabled["AutoHammer"];
            bits[i++] = toolEnabled["BlockWand"];
            bits[i++] = toolEnabled["ColorPalette"];
            bits[i++] = toolEnabled["LevitationWand"];
            bits[i++] = toolEnabled["RattlesnakeWand"];
            bits[i++] = toolEnabled["StaffofRegrowthEdit"];
            bits[i++] = toolEnabled["WallHammer"];

            writer.Write((byte)bits);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte bits = reader.ReadByte();
            int i = 0;
            toolEnabled["all"] = bits[i++];
            toolEnabled["AutoHammer"] = bits[i++];
            toolEnabled["BlockWand"] = bits[i++];
            toolEnabled["ColorPalette"] = bits[i++];
            toolEnabled["LevitationWand"] = bits[i++];
            toolEnabled["RattlesnakeWand"] = bits[i++];
            toolEnabled["StaffofRegrowthEdit"] = bits[i++];
            toolEnabled["WallHammer"] = bits[i++];
        }

        public override TagCompound Save()
        {
            return new TagCompound {
                {"AutoHammer", toolEnabled["AutoHammer"]},
                {"BlockWand", toolEnabled["BlockWand"]},
                {"ColorPalette", toolEnabled["ColorPalette"]},
                {"LevitationWand", toolEnabled["LevitationWand"]},
                {"RattlesnakeWand", toolEnabled["RattlesnakeWand"]},
                {"StaffofRegrowthEdit", toolEnabled["StaffofRegrowthEdit"]},
                {"WallHammer", toolEnabled["WallHammer"]}
            };
        }

        public override void Load(TagCompound tag)
        {
            toolEnabled["AutoHammer"] = tag.GetBool("AutoHammer");
            toolEnabled["BlockWand"] = tag.GetBool("BlockWand");
            toolEnabled["ColorPalette"] = tag.GetBool("ColorPalette");
            toolEnabled["LevitationWand"] = tag.GetBool("LevitationWand");
            toolEnabled["RattlesnakeWand"] = tag.GetBool("RattlesnakeWand");
            toolEnabled["StaffofRegrowthEdit"] = tag.GetBool("StaffofRegrowthEdit");
            toolEnabled["WallHammer"] = tag.GetBool("WallHammer");
        }
    }
    */
}
