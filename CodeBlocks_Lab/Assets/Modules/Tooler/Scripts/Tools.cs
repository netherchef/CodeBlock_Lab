using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Tool Types _____________________________________________________________

public enum ToolType { Gardening, Cooking, Food }

public struct Tool
{
	public ToolType type;
	public int strength;
	public int durability;
	public int healRate;
	public int duration;
}

#endregion

public class Tools
{
	// Gardening

	public static Tool shovel = new Tool { type = ToolType.Gardening, strength = 1, durability = 5 };

	// Cooking

	public static Tool spoon = new Tool { type = ToolType.Cooking, durability = 10 };

	// Food

	public static Tool sukiyaki = new Tool { type = ToolType.Food, healRate = 10, duration = 10 };
}
