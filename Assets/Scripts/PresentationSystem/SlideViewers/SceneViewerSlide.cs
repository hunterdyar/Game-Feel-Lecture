using System.Collections;
using PresentationSystem.Viewer.SlideViewers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class SceneViewerSlide : SlideBase
{
	public string sceneName;
	public string OverlayText;
	public bool DisplayOverlay = true;
	private Label _label;
	private Camera _camera;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	protected override void Awake()
	{
		base.Awake();
		_camera = FindFirstObjectByType<Camera>();
	}

	void OnEnable()
	{
		SetBackgroundTransparent();
		_label = _uiDocument.rootVisualElement.Q<Label>("Header");
		_label.text = OverlayText;
		_label.style.color = Settings.DefaultTextColor;
		_label.parent.visible = DisplayOverlay;
	}
	public override IEnumerator EnterSlide()
	{
		_camera.gameObject.SetActive(false);
		_label.parent.visible = DisplayOverlay;
		SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
		
		yield break;
	}

	public override IEnumerator ExitSlide()
	{
		var unload = SceneManager.UnloadSceneAsync(sceneName);
		while (unload.isDone == false)
		{
			yield return null;
		}
		_camera.gameObject.SetActive(true);
	}
}