using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace VipixToolBox
{
	public class VipixToolBoxPlayer : ModPlayer
	{
		public List<int> treeList;
		public int centerUI = 1;//could be static but would apply to everyone in multiplayer ? 0 for fixed, 1 for follow mouse, 2 for free
		//periodic update (for UI and tool needs)
		public byte colorByte = 1;//for paint tool, 1 is red (not deep red)
		public int paintStatus = 0;
		public bool spotlight = false;
		public float tbMouseX;
		public float tbMouseY;
		public int tileX;//for hammer tool
		public int tileY;
		public int blockTool;
		public int mossTool;
		public List<int> tempTileX;
		public List<int> tempTileY;
		public int fargoRange;
		//continuous update (see PreUpdate())
		public Vector2 pointerCoord;//coordinates of mouse pointer relatively to the world (top left of the world at 0,0)
		public Tile pointedTile;
		public int pointedTileX;
		public int pointedTileY;
		public Tile pointedTileAbove;//the tile just above the pointed one, potentially useful

		public override void PreUpdate()
		{
			pointerCoord.X = (float)Main.mouseX + Main.screenPosition.X;
			if (player.gravDir == 1f) pointerCoord.Y = (float)Main.mouseY + Main.screenPosition.Y;
			else pointerCoord.Y = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY;//handles reversed gravity, yay
			pointedTileX = (int)(pointerCoord.X / 16f);
			pointedTileY = (int)(pointerCoord.Y / 16f);
			pointedTile = Main.tile[pointedTileX, pointedTileY];
			pointedTileAbove = Main.tile[pointedTileX, pointedTileY - 1];
		}
		public override TagCompound Save()
		{
			return new TagCompound {
				{"centerUI", centerUI}
			};
		}
		public override void Load(TagCompound tag)
		{
			treeList = new List<int>();
			treeList.Add(TileID.Trees);
			treeList.Add(TileID.MushroomTrees);
			treeList.Add(TileID.ChristmasTree);
			treeList.Add(TileID.PalmTree);
			centerUI = tag.GetInt("centerUI");
		}
		public override void ResetEffects()
		{
			fargoRange = 0;//resetting the UpdateAccessory of FargosModCompatibility
		}
	}
}
