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
		return i == 1;
	}
	public static int IntToPN(int i)
	{
		if(i == 0){return 0;}
		return i / Mathf.Abs(i);
	}
	public static float ClampMax(float value, float max)
	{
		if(value > max){return value;}else{return max;}
	}
	public static float ClampMin(float value, float min)
	{
		if(value < min){return value;}else{return min;}
	}
	public static float ClampDelta(float x, float y, float max, float min)//adds x and y. if that goes beyond the bounds of max and min, returns y minimzied to be closest to those bounds
	{
		float sum = x + y;
		if(sum > max){return ClampMin(y, 0);}
		else if(sum < min){return ClampMax(y, 0);}
		else{return y;}
	}
}