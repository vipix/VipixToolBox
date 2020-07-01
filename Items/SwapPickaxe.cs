using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using VipixToolBox;
using VipixToolBox.Items;

namespace VipixToolBox.Items
{
    public class SwapPickaxe : ModItem
    {
        public int baseRange = 12;
        public int toolRange;
        public bool operationAllowed;
        public List<int> validBlocks;
        public List<int> validItems;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Swap Pickaxe");
            Tooltip.SetDefault("Nothing is lost, nothing is created\n100% equivalent pickaxe power");
        }

        public override void SetDefaults()
        {
            Item moltenPickaxe = new Item(); //defaults on copper pickaxe
            moltenPickaxe.CloneDefaults(ItemID.MoltenPickaxe);
            item.damage = moltenPickaxe.damage;
            item.knockBack = moltenPickaxe.knockBack;
            item.useStyle = 1;
            item.useAnimation = 16;
            item.useTime = 16;
            item.width = 32;
            item.height = 32;
            item.rare = moltenPickaxe.rare + +1;
            item.UseSound = SoundID.Item1;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.autoReuse = true;
            validBlocks = new List<int>{
                0,1,6,7,8,9,38,39,40,45,46,47,53,54,57,59,75,76,188,112,116,118,119,120,
                121,122,123,130,131,137,140,145,146,147,148,150,151,152,153,154,155,156,
                160,161,166,167,168,169,175,176,177,189,193,195,196,194,197,198,202,163,
                164,200,206,224,229,230,137,137,137,137,234,272,248,249,250,170,262,263,
                264,265,266,267,268,273,274,284,311,312,313,315,325,326,327,328,329,336,
                340,341,342,343,344,345,346,347,348,350,351,357,367,368,369,370,371,379,
                385,396,397,398,399,400,401,402,403,404,407,408,409,415,416,417,418,
                30,157,159,208,251,252,253,321,322,2,70,179,180,181,182,183,184

            };//IDs of blocks that need 1 hit from a pickaxe with >100 power except the grass

            validItems = new List<int>{
                2,3,11,12,13,14,129,131,133,141,143,145,169,170,172,176,192,214,276,370,
                408,412,413,414,415,416,424,511,512,539,577,586,591,593,594,604,607,609,
                611,612,613,614,662,664,699,700,701,702,717,718,719,751,762,763,765,766,
                767,775,824,833,834,835,883,1103,1125,1127,1146,1147,1148,1149,1246,1344,
                1589,1591,1593,1872,1970,1971,1972,1973,1974,1975,1976,2119,2120,2173,
                2260,2261,2262,2435,2692,2693,2694,2695,2697,2701,2751,2752,2753,2754,
                2755,2787,2792,2793,2794,2860,2868,3066,3081,3086,3087,3100,3113,3214,
                3234,3271,3272,3274,3275,3276,3277,3338,3339,3347,3380,3460,3461,3573,
                3574,3575,3576,
                9,619,621,911,1725,1727,1729,2503,2504,2,176,1,1,1,1,1,1
            };//IDs of corresponding items
        }

        public override bool AltFunctionUse(Player player)
        {
            return false;
        }

        public override void HoldItem(Player player)
        {
            //this method determines if the pointed block is buildable and in range of the player
            //it shows the item icon if true
            //and it allows the actions in CanUseItem
            VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
            toolRange = Math.Max(baseRange, myPlayer.fargoRange);//blocks
            //Main.NewText(validBlocks.Contains(myPlayer.pointedTile.type).ToString());
            if (Vector2.Distance(player.position, myPlayer.pointerCoord) < toolRange * 16 &&
            myPlayer.pointedTile.active() &&
            Main.tileSolid[myPlayer.pointedTile.type])
            {
                operationAllowed = true;
                player.showItemIcon = true;
            }
            else
            {
                operationAllowed = false;
                player.showItemIcon = false;
            }
        }

        public override bool CanUseItem(Player player)
        {
            VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();

            return true;
        }

        public override bool UseItem(Player player)
        {
            VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();

            if (operationAllowed && validBlocks.Contains(myPlayer.pointedTile.type))
            {
                int index = -1;
                int iindex = -1;

                for (int i = 0; i < player.inventory.Length; i++)
                {
                    if (validItems.Contains(player.inventory[i].type))
                    {
                        iindex = i;
                        index = validItems.FindIndex(a => a == player.inventory[i].type);
                        break;
                    }
                }
                //need to check for resource first
                //player.inventory[boneIndex].stack--;
                if (index != -1)
                {
                    bool halfBrick = myPlayer.pointedTile.halfBrick();
                    byte slope = myPlayer.pointedTile.slope();
                    bool inActive = myPlayer.pointedTile.inActive();
                    byte color = myPlayer.pointedTile.color();

                    WorldGen.KillTile(myPlayer.pointedTileX, myPlayer.pointedTileY);
                    WorldGen.PlaceTile(myPlayer.pointedTileX, myPlayer.pointedTileY, validBlocks[index]);
                    player.inventory[iindex].stack--;

                    myPlayer.pointedTile.halfBrick(halfBrick);
                    myPlayer.pointedTile.slope(slope);
                    myPlayer.pointedTile.inActive(inActive);
                    myPlayer.pointedTile.color(color);

                    if (Main.netMode == 1) NetMessage.SendTileSquare(-1, myPlayer.pointedTileX, myPlayer.pointedTileY, 1);//player, X, Y, square "diameter". -1 as the player equals all ?
                    Main.PlaySound(SoundID.Dig);
                }
            }
            return true;
        }
    }
}