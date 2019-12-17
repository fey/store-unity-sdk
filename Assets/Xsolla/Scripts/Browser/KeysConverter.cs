using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using UnityEngine;

public static class KeysConverter
{
	static Dictionary<KeyCode, string> dictionary;

	public static string Convert(KeyCode key)
	{
		Dictionary<KeyCode, string> keys = GetDictionary();
		string result = "";
		if (keys.ContainsKey(key))
			result = keys[key];
		else
			result = key.ToString();
		return result;
	}

	static Dictionary<KeyCode, string> GetDictionary()
	{
		if(dictionary == null) {
			dictionary = new Dictionary<KeyCode, string> {
				{ KeyCode.None, Keys.Null },
				{ KeyCode.CapsLock, Keys.Cancel },
				{ KeyCode.Help, Keys.Help },
				{ KeyCode.Backspace, Keys.Backspace },
				{ KeyCode.Tab, Keys.Tab },
				{ KeyCode.Clear, Keys.Clear },
				{ KeyCode.Return, Keys.Return },
				{ KeyCode.KeypadEnter, Keys.Enter },
				{ KeyCode.LeftShift, Keys.Shift },
				{ KeyCode.RightShift, Keys.Shift },
				{ KeyCode.LeftControl, Keys.Control },
				{ KeyCode.RightControl, Keys.Control },
				{ KeyCode.LeftAlt, Keys.Alt },
				{ KeyCode.RightAlt, Keys.Alt },
				{ KeyCode.Pause, Keys.Pause },
				{ KeyCode.Escape, Keys.Escape },
				{ KeyCode.Space, Keys.Space },
				{ KeyCode.PageUp, Keys.PageUp },
				{ KeyCode.PageDown, Keys.PageDown },
				{ KeyCode.End, Keys.End },
				{ KeyCode.Home, Keys.Home },
				{ KeyCode.LeftArrow, Keys.Left },
				{ KeyCode.UpArrow, Keys.Up },
				{ KeyCode.RightArrow, Keys.Right },
				{ KeyCode.DownArrow, Keys.Down },
				{ KeyCode.Insert, Keys.Insert },
				{ KeyCode.Delete, Keys.Delete },
				{ KeyCode.Semicolon, Keys.Semicolon },
				{ KeyCode.Equals, Keys.Equal },
				{ KeyCode.KeypadEquals, Keys.Equal },
				{ KeyCode.Keypad0, Keys.NumberPad0 },
				{ KeyCode.Keypad1, Keys.NumberPad1 },
				{ KeyCode.Keypad2, Keys.NumberPad2 },
				{ KeyCode.Keypad3, Keys.NumberPad3 },
				{ KeyCode.Keypad4, Keys.NumberPad4 },
				{ KeyCode.Keypad5, Keys.NumberPad5 },
				{ KeyCode.Keypad6, Keys.NumberPad6 },
				{ KeyCode.Keypad7, Keys.NumberPad7 },
				{ KeyCode.Keypad8, Keys.NumberPad8 },
				{ KeyCode.Keypad9, Keys.NumberPad9 },
				{ KeyCode.KeypadMultiply, Keys.Multiply },
				{ KeyCode.Plus, Keys.Add },
				{ KeyCode.KeypadPlus, Keys.Add },
				{ KeyCode.Comma, Keys.Separator },
				{ KeyCode.Minus, Keys.Subtract },
				{ KeyCode.KeypadMinus, Keys.Subtract },
				{ KeyCode.KeypadPeriod, Keys.Decimal },
				{ KeyCode.KeypadDivide, Keys.Divide },
				{ KeyCode.F1, Keys.F1 },
				{ KeyCode.F2, Keys.F2 },
				{ KeyCode.F3, Keys.F3 },
				{ KeyCode.F4, Keys.F4 },
				{ KeyCode.F5, Keys.F5 },
				{ KeyCode.F6, Keys.F6 },
				{ KeyCode.F7, Keys.F7 },
				{ KeyCode.F8, Keys.F8 },
				{ KeyCode.F9, Keys.F9 },
				{ KeyCode.F10, Keys.F10 },
				{ KeyCode.F11, Keys.F11 },
				{ KeyCode.F12, Keys.F12 },
				{ KeyCode.Tilde, Keys.Meta },
				{ KeyCode.LeftCommand, Keys.Command },
				{ KeyCode.RightCommand, Keys.Command }
			};
		}
		return dictionary;
	}
}
