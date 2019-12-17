using System;
using OpenQA.Selenium.Interactions;

public partial class XsollaBrowserInstance : IDisposable
{
	public void InputKeys(string keys, Action callback = null)
	{
		LogEvent?.Invoke("Input keys method");
		AddCommand(InputKeysCommand, keys, callback);
	}

	void InputKeysCommand(string keys)
	{
		Actions actions = new Actions(driver);
		actions.SendKeys(keys);
		actions.Perform();
	}

	public void KeyDown(string key, Action callback = null)
	{
		LogEvent?.Invoke("KeyDown method");
		AddCommand(KeyDownCommand, key, callback);
	}

	void KeyDownCommand(string key)
	{
		Actions actions = new Actions(driver);
		actions.KeyDown(key);
		
		actions.Perform();
	}

	public void KeyUp(string key, Action callback = null)
	{
		LogEvent?.Invoke("KeyUp method");
		AddCommand(KeyUpCommand, key, callback);
	}

	void KeyUpCommand(string key)
	{
		Actions actions = new Actions(driver);
		actions.KeyUp(key);
		actions.Perform();
	}
}
