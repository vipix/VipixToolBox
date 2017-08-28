using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using System;
using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.DataStructures;

namespace VipixToolBox.Tiles
{
    public class icepottile : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.Width = 2;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 18 };
          	TileObjectData.newTile.CoordinateWidth = 16;
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.RandomStyleRange = 9;
            TileObjectData.addTile(Type);
            disableSmartCursor = true;

            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Ice Pot");
            AddMapEntry(new Color(255, 0, 255), name);

        }
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType("icepot"));
        }
    }
}
