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

		//PreUpdate runs before tileTargetX/Y is set
		public bool CursorReady => Player.tileTargetX != 0 && Player.tileTargetY != 0;

		public override void PreUpdate()
		{
			if (Main.netMode != NetmodeID.Server && CursorReady)
			{
				pointerCoord = Main.MouseWorld;
				//Point point = Main.MouseWorld.ToTileCoordinates();
				//pointedTileX = point.X;
				//pointedTileY = point.Y;

				//SmartCursor support
				Point point = new Point(Player.tileTargetX, Player.tileTargetY);
				pointedTileX = point.X;
				pointedTileY = point.Y;

				pointedTile = Framing.GetTileSafely(point);
				//Main.NewText("calc: " + point);
				//Main.NewText("targ: " + new Point(Player.tileTargetX, Player.tileTargetY));
				pointedTileAbove = Framing.GetTileSafely(point.X, point.Y - 1);
			}
		}

		public override TagCompound Save()
		{
			return new TagCompound {
				{"centerUI", centerUI}
			};
		}

		public override void Load(TagCompound tag)
		{
			centerUI = tag.GetInt("centerUI");
		}
		public override void ResetEffects()
		{
			fargoRange = 0;//resetting the UpdateAccessory of FargosModCompatibility
		}
	}
}
