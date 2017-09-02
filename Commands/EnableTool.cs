using VipixToolBox;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace VipixToolBox.Commands
{
  public class EnableTool : ModCommand
  {
    public override CommandType Type
    {
      get { return CommandType.Chat; }
    }

    public override string Command
    {
      get { return "tool"; }
    }
    public override string Usage
    {
      get { return "\n/tool toolName <enable|disable>\n"+
      "/tool toolName"; }
    }
    public override string Description
    {
      get { return "enables or disables VipixToolBox tools"; }
    }

    public override void Action(CommandCaller caller, string input, string[] args)
    {
      Mod myMod = ModLoader.GetMod("VipixToolBox");
      bool allTools = false;
      var keys = new List<string>(VipixToolBox.toolEnabled.Keys);
      string displaykeys = "";
      foreach(string i in keys) displaykeys += i + ", ";

      if (keys.Contains(args[0]))
      {
        //you can put anything after a valid command
        if (args.Length >= 2)
        {
          if (args[1] == "enable")
          {
            VipixToolBox.toolEnabled[args[0]] = true;
            Main.NewText(args[0].ToString()+" has been enabled",0,150,0);
          }
          else if (args[1] == "disable")
          {
            VipixToolBox.toolEnabled[args[0]] = false;
            Main.NewText(args[0].ToString()+" has been disabled",200,0,0);
          }
          else
          {
            throw new UsageException(args[1] + " is not valid, please specify either enable|disable");
          }
        }
        else
        {
          VipixToolBox.toolEnabled[args[0]] = !VipixToolBox.toolEnabled[args[0]];
          if (VipixToolBox.toolEnabled[args[0]]) Main.NewText(args[0].ToString()+" has been enabled",0,150,0);
          else Main.NewText(args[0].ToString()+" has been disabled",200,0,0);
        }
        if (args[0] == "all")
        {
          bool temp = VipixToolBox.toolEnabled["all"];
          for (int i = 0; i < keys.Count; i++)
          {
            VipixToolBox.toolEnabled[keys[i]] = temp;
          }
        }
      }
      else
      {
        throw new UsageException(args[0] + " does not exist\navailable tools are "+displaykeys);
      }
    }
  }
}
