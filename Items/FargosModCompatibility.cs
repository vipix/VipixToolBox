using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using VipixToolBox.UI;

namespace VipixToolBox.Items
{
    public class FargosModCompatibility : GlobalItem
    {
        public override void UpdateAccessory(Item item, Player player, bool hideVisual)
        {
            //https://github.com/Fargowilta/FargowiltasSouls/blob/master/Items/Accessories/Souls/WorldShaperSoul.cs
            if (item.modItem?.GetType().Name == "WorldShaperSoul")
            {
                VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
                myPlayer.fargoRange = 54;//range of normal tools with fargo mod
            }
        }
    }
}
