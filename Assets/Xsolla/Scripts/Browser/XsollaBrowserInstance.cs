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
using System.Runtime.CompilerServices;

public partial class XsollaBrowserInstance : IDisposable
{
	#region Commands
	class CommandFactory
	{
		public interface ICommand
		{
			void Invoke();
		}
		class Command : ICommand
		{
			Action action;
			Action callback;

			public Command(Action action, Action callback = null)
			{
				this.action = action;
				this.callback = callback;
			}

			public void Invoke()
			{
				action?.Invoke();
				callback?.Invoke();
			}
		}
		class Command<T> : ICommand
		{
			Action<T> action;
			T args;
			Action callback;

			public Command(Action<T> action, T args, Action callback = null)
			{
				this.action = action;
				this.callback = callback;
				this.args = args;
			}

			public void Invoke()
			{
				action?.Invoke(args);
				callback?.Invoke();
			}
		}

		public static ICommand Create(Action action, Action callback = null)
		{
			return new Command(action, callback);
		}

		public static ICommand Create<T>(Action<T> action, T args, Action callback = null)
		{
			return new Command<T>(action, args, callback);
		}
	}

	private BlockingCollection<CommandFactory.ICommand> commands;

	void AddCommand(Action action, Action callback = null)
	{
		CommandFactory.ICommand command = CommandFactory.Create(action, callback);
		commands.Add(command);
	}
	void AddCommand<T>(Action<T> action, T args, Action callback = null)
	{
		CommandFactory.ICommand command = CommandFactory.Create<T>(action, args, callback);
		commands.Add(command);
	}
	#endregion

	public event Action<byte[], Size> RedrawComplete;
	public event Action<string> LogEvent;

	private readonly string DRIVER_PATH = Application.streamingAssetsPath;// Application.dataPath + "/Xsolla/Plugins";

	public XsollaBrowserInstance()
	{
		commands = new BlockingCollection<CommandFactory.ICommand>(new ConcurrentQueue<CommandFactory.ICommand>(), 1024);
		Log("Ctor");

		driverThread = new Thread(DriverThread);
		driverThread.Start(DRIVER_PATH);
	}

	public void ChangeUrlTo(string newUrl, Action callback = null)
	{
		Log("Change url method");
		AddCommand(UrlTask, newUrl, callback);
	}
	
	void UrlTask(string url)
	{
		Log("Url is changing");
		driver.Navigate().GoToUrl(url);
		driver.Manage().Window.Maximize();
	}

	void Log(string message, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
	{
		LogEvent?.Invoke(message + " at " + lineNumber + " (" + caller + ")");
	}
}
