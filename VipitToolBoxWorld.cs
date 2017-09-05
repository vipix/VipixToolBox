using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System;
using System.Collections.Generic;

namespace VipixToolBox
{
	class VipixToolBoxWorld : ModWorld
	{
    public static Dictionary<string, bool> toolEnabled;

    public override void Initialize()
    {
      toolEnabled = new Dictionary<string, bool>();
      toolEnabled.Add("all",false);
      toolEnabled.Add("AutoHammer",true);
      toolEnabled.Add("BlockWand",true);
      toolEnabled.Add("ColorPalette",true);
      toolEnabled.Add("LevitationWand",true);
      toolEnabled.Add("RattlesnakeWand",true);
      toolEnabled.Add("StaffofRegrowthEdit",true);
      toolEnabled.Add("WallHammer",true);
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
}
