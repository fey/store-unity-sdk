using UnityEngine;
using System;
using System.Collections;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.Extensions;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;
using OpenQA.Selenium.Interactions;

public partial class XsollaBrowserInstance : IDisposable
{
	public Size Size { get; protected set; }

	public void Redraw(Action callback = null)
	{
		Log("Display: Redraw method");
		AddCommand(RedrawCommand, callback);
	}

	void RedrawCommand()
	{
		try {
			Screenshot screenshot = driver.GetScreenshot();
			if (screenshot == null)
				throw new Exception("Screenshot is null!!!");
			RedrawComplete?.Invoke(screenshot.AsByteArray, Size);
		} catch(Exception e) {
			Log("Display: Taking screenshot error: " + e.Message);
		}
	}

	public void ChangeSizeTo(uint width, uint height, Action callback = null)
	{
		AddCommand(ChangeWindowSize, new Size((int)width, (int)height), callback);
	}

	void ChangeWindowSize(Size size)
	{
		this.Size = size;
		driver.Manage().Window.Size = this.Size;
		driver.Manage().Window.Maximize();
	}
}
