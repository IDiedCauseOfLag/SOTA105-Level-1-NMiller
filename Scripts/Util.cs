using Godot;
using System;

public static class Util
{
	public static int BoolToInt(bool b)
	{
		if (b){return 1;}
		else{return 0;}
	}
	public static bool IntToBool(int i)
	{
		return i == 0;
	}
	public static int IntToPN(int i)
	{
		return i / Mathf.Abs(i);
	}
}
