using UnityEngine;
using System;
using System.Collections;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;


public partial class XsollaBrowserInstance : IDisposable
{
	private ChromeDriver driver;
	private Thread driverThread;
	private bool needToBeClosed;

	void DriverThread(object driverPath)
	{
		Initialization((string)driverPath);

		while (!needToBeClosed) {
			try {
				ProccessCommandForDriver();
			} catch(Exception e) {
				Log("Driver: Error: " + e.Message);
			}
		}

		Destruction();
	}

	void Initialization(string driverPath)
	{
		Log("Driver: Initialization. Platform: " + Platform.CurrentPlatform + "; OS: " + Environment.OSVersion + "; OS platformID: " + Environment.OSVersion.Platform);
		string chromePath = driverPath + "/Selenium/lib/Selenium.WebDriver.ChromeDriver.78.0.3904.10500/driver/mac64";
		//ChromeOptions options = null;
		//try {
		//	options = new ChromeOptions();
		//} catch (Exception e) {
		//	Log("Driver: Error: " + e.Message);
		//}
		//try {
		//	options.AddArgument("--headless");
		//	//options.AddArgument("window-size=800x600");
		//	options.AddArguments("--proxy-server='direct://'");
		//	options.AddArguments("--proxy-bypass-list=*");
		//	options.AddArguments("disable-gpu=false");
		//} catch (Exception e) {
		//	Log("Driver: Error: " + e.Message);
		//}
		//try {
		//	//driver = new ChromeDriver(chromePath, options);
		//	driver = new ChromeDriver(chromePath);
		//} catch (Exception e) {
		//	Log("Driver: Error: " + e.Message + "; Stack trace: " + e.StackTrace);
		//}
		//try {
		//	ChangeSizeTo(800, 600);
		//} catch (Exception e) {
		//	Log("Driver: Error: " + e.Message);
		//}
		try {
			ChromeOptions options = new ChromeOptions();
			options.AddArgument("--headless");
			//options.AddArgument("window-size=800x600");
			options.AddArguments("--proxy-server='direct://'");
			options.AddArguments("--proxy-bypass-list=*");
			options.AddArguments("disable-gpu=false");
			options.PageLoadStrategy = PageLoadStrategy.Eager;
			driver = new ChromeDriver(chromePath, options);
		} catch (Exception e) {
			Log("Driver: Error: " + e.Message);
		}
		ChangeSizeTo(800, 600);

	}

	void Destruction()
	{
		commands.GetConsumingEnumerable();
		commands.Dispose();
		commands = null;
		driver.Quit();
		driver.Dispose();
		driver = null;
		driverThread = null;
	}

	CommandFactory.ICommand ProccessCommandForDriver()
	{
		commands.TryTake(out CommandFactory.ICommand command, 10);
		command?.Invoke();
		return command;
	}

	public void Dispose()
	{
		Log("Driver: Destructor invoked!!!");
		needToBeClosed = true;
		driverThread?.Join();
	}
}
