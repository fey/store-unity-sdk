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
using UnityEngine.Profiling;
using System.Threading.Tasks;
using System.Text;

public class XsollaBrowser : MonoBehaviour
{
	[SerializeField]
	private string url = "https://unity3d.com";
	public string Url {
		get {
			return url;
		}
		set {
			if (url != value) {
				url = value;
				instance?.ChangeUrlTo(url);
			}
		}
	}

	public SpriteRenderer spriteRenderer;
	public Image image;

	public event Action<byte[], Size> RedrawComplete;
	public byte[] Data { get; private set; }
	private Size size;

	XsollaBrowserInstance instance;
	

	bool isProccess = true;
	private void Awake()
	{
		instance = new XsollaBrowserInstance();
		instance.RedrawComplete += Instance_RedrawComplete;
		instance.LogEvent += Instance_LogEvent;

		XsollaBrowserRender render = gameObject.AddComponent<XsollaBrowserRender>();
		render.image = image;
		render.spriteRenderer = spriteRenderer;
	}

	private void Instance_LogEvent(string obj)
	{
		obj = "Browser instance: " + obj;
		logs += obj + "\r\n";
		Debug.Log(obj);
	}

	private void Instance_RedrawComplete(byte[] arg1, Size arg2)
	{
		Data = arg1;
		size = arg2;
	}

	private void Start()
	{
		if (string.IsNullOrEmpty(Url)) {
			url = "https://google.com";
		}
		instance.ChangeUrlTo(url);
		StartCoroutine(RedrawCoroutine());
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
		instance?.Dispose();
	}

	public void GoTo(string url)
	{
		this.Url = url;
		instance.ChangeUrlTo(url);
	}

	IEnumerator RedrawCoroutine()
	{
		while (true) {
			isProccess = true;
			instance.Redraw(() => { isProccess = false; });
			yield return new WaitWhile(() => isProccess);
			RedrawComplete?.Invoke(Data, size);
		}
	}

	static string logs = "";
	static bool needLogs = true;
	bool thisLogs = false;
	Vector2 scrollPosition = Vector2.zero;
	private void OnGUI()
	{
		if(needLogs) {
			needLogs = false;
			thisLogs = true;
		}
		if (thisLogs) {
			scrollPosition = GUILayout.BeginScrollView(scrollPosition, true, true, GUILayout.Width(Screen.width / 2.0F), GUILayout.Height(Screen.height / 2.0F));
			GUILayout.TextArea(logs);
			GUILayout.EndScrollView();
		}
	}

	void Update()
	{
		foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) {
			//if (Input.GetKeyDown(kcode)) {
			//	Debug.Log("KeyCode down: " + kcode);
			//	string key = KeysConverter.Convert(kcode);
			//	instance?.KeyDown(key);
			//}
				
			if (Input.GetKeyUp(kcode)) {
				Debug.Log("KeyCode up: " + kcode);
				string key = KeysConverter.Convert(kcode);
				instance?.InputKeys(key);
			}
		}
	}
}
