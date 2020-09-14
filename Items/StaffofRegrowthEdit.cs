using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using VipixToolBox.UI;
using System.Collections.Generic;

namespace VipixToolBox.Items
{
    public class StaffofRegrowthEdit : GlobalItem
    {
        public override bool AltFunctionUse(Item item, Player player)
        {
            if (item.type == ItemID.StaffofRegrowth) return true;
            else return base.AltFunctionUse(item, player);
        }
        public override bool CanUseItem(Item item, Player player)
        {
            if (item.type == ItemID.StaffofRegrowth &&
                ServerConfig.Instance.StaffofRegrowthEdit)
            {
                bool moddedStaff = false;
                VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
                if (Main.netMode != NetmodeID.Server && myPlayer.CursorReady)
                {
                    List<int> mossToolList = new List<int>
                    {
                        -1,//no TileID is -1
                        TileID.GreenMoss,
                        TileID.BrownMoss,
                        TileID.RedMoss,
                        TileID.BlueMoss,
                        TileID.PurpleMoss,
                        TileID.LavaMoss
                    };
                    float maxReach = 5.5f;//blocks. With testing I find 6 longer than vanilla reach and 5 shorter that vanilla reach
                    if (player.altFunctionUse == 2)
                    {
                        myPlayer.tbMouseX = Main.mouseX;
                        myPlayer.tbMouseY = Main.mouseY;
                        MossUI.visible = true;
                        moddedStaff = true;
                    }
                    else if (myPlayer.mossTool != 0 &&
                    myPlayer.pointedTile.type != (ushort)mossToolList[myPlayer.mossTool] &&
                    Vector2.Distance(player.Center, myPlayer.pointerCoord) < maxReach * 16 &&
                    (myPlayer.pointedTile.type == TileID.Stone || mossToolList.Contains(myPlayer.pointedTile.type)))
                    {
                        //if tool is custom moss and not default AND
                        //if block to change isn't already the correct moss type AND
                        //if block to change is closer than 5 blocks AND
                        //if block to change is either stone or moss
                        myPlayer.pointedTile.type = (ushort)mossToolList[myPlayer.mossTool];
                        WorldGen.SquareTileFrame(myPlayer.pointedTileX, myPlayer.pointedTileY, true);
                        if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendTileSquare(-1, myPlayer.pointedTileX, myPlayer.pointedTileY, 1);
                        moddedStaff = true;
                    }
                }
                if (moddedStaff) return false;
            }
            return base.CanUseItem(item, player);
        }
    }
}
