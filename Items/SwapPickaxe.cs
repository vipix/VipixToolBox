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
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 16;
            item.useTime = 16;
            item.width = 32;
            item.height = 32;
            item.rare = moltenPickaxe.rare + +1;
            item.UseSound = SoundID.Item1;
            item.value = Item.buyPrice(0, 5, 0, 0);
            item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return false;
        }

        public override void HoldItem(Player player)
        {
            VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();
            if (Main.netMode != NetmodeID.Server && myPlayer.CursorReady)
            {
                //this method determines if the pointed block is buildable and in range of the player
                //it shows the item icon if true
                //and it allows the actions in CanUseItem
                toolRange = Math.Max(baseRange, myPlayer.fargoRange);//blocks
                                                                     //Main.NewText(validBlocks.Contains(myPlayer.pointedTile.type).ToString());
                if (Vector2.Distance(player.Center, myPlayer.pointerCoord) < toolRange * 16 &&
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
        }

        public override bool CanUseItem(Player player)
        {
            return true;
        }

        public override bool UseItem(Player player)
        {
            VipixToolBoxPlayer myPlayer = player.GetModPlayer<VipixToolBoxPlayer>();

            if (operationAllowed && VipixToolBox.validBlocks.Contains(myPlayer.pointedTile.type))
            {
                int index = -1;
                int iindex = -1;

                for (int i = 0; i < Main.maxInventory; i++)
                {
                    Item item1 = player.inventory[i];
                    if (!item1.IsAir && item1.stack > 0 && VipixToolBox.validItems.Contains(item1.type))
                    {
                        iindex = i;
                        index = VipixToolBox.validItems.FindIndex(a => a == item1.type);
                        break;
                    }
                }
                //need to check for resource first
                //player.inventory[boneIndex].stack--;
                if (index != -1)
                {
                    int newType = VipixToolBox.validBlocks[index];

                    int i = myPlayer.pointedTileX;
                    int j = myPlayer.pointedTileY;
                    Tile tile = Framing.GetTileSafely(i, j);
                    if (tile.type != newType && WorldGen.CanKillTile(i, j))
                    {
                        Tile tileAbove = Framing.GetTileSafely(i, j - 1);
                        int tileAboveType = tileAbove.type;
                        bool cont = true;
                        if (tileAbove.active() && (TileID.Sets.BasicChest[tileAboveType] || TileID.Sets.BasicChestFake[tileAboveType] || TileLoader.IsDresser(tileAboveType)))
                        {
                            //Chests/containers in general above the tile should not be swappable
                            cont = false;
                        }
                        if (cont)
                        {
                            bool halfBrick = myPlayer.pointedTile.halfBrick();
                            byte slope = myPlayer.pointedTile.slope();
                            bool inActive = myPlayer.pointedTile.inActive();
                            byte color = myPlayer.pointedTile.color();
                            WorldGen.KillTile(i, j);
                            NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, i, j, 1f, 0, 0, 0);
                            bool success = WorldGen.PlaceTile(i, j, newType);
                            if (success)
                            {
                                player.inventory[iindex].stack--;

                                myPlayer.pointedTile.halfBrick(halfBrick);
                                myPlayer.pointedTile.slope(slope);
                                myPlayer.pointedTile.inActive(inActive);
                                myPlayer.pointedTile.color(color);
                                NetMessage.SendData(MessageID.TileChange, -1, -1, null, 0, i, j, 1f, 0, 0, 0);
                                //if (Main.netMode == NetmodeID.MultiplayerClient) NetMessage.SendTileSquare(-1, i, j, 2);//player, X, Y, square "diameter". -1 as the player equals all ?
                                Main.PlaySound(SoundID.Dig);
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}