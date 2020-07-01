using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using VipixToolBox.UI;

namespace VipixToolBox.Items
{
  public class ClearUIs : GlobalItem
  {
    public override void HoldItem(Item item, Player player) //the only ways to make the UI disappear: click on a button or switch item (with this hook)
    {
      if (item.type != ModContent.ItemType<ColorPalette>()) ColorUI.visible = false;
      if (item.type != ModContent.ItemType<AutoHammer>()) HammerUI.visible = false;
      if (item.type != ModContent.ItemType<BlockWand>()) BlockUI.visible = false;
      if (item.type != ItemID.StaffofRegrowth) MossUI.visible = false;
    }
  }
}
