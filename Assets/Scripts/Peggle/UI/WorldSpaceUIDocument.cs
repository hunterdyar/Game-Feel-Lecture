using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

namespace Peggle.Peggle.UI
{
	public class WorldSpaceUIDocument : MonoBehaviour
	{
		private const string transparentShader = "Unlit/Transparent";
		private const string textureShader = "Unlit/Texture";
		const string mainTex = "_MainTex";
		static readonly int MainTex = Shader.PropertyToID("_MainTex");

		[SerializeField] private int panelWidth = 1280;
		[SerializeField] private int panelHeight = 720;
		[SerializeField] private float panelScale = 1.0f;
		[SerializeField] private float pixelsPerUnit = 500.0f;

		[SerializeField] private VisualTreeAsset visualTreeAsset;
		[SerializeField] private PanelSettings panelSettingsAsset;
		[SerializeField] private RenderTexture renderTextureAsset;

		private MeshRenderer meshRenderer;
		UIDocument uiDocument;
		private PanelSettings panelSettings;
		RenderTexture renderTexture;
		private Material material;

		public void SetLabelText(string label, string text)
		{
			if (uiDocument.rootVisualElement == null)
			{
				uiDocument.visualTreeAsset = visualTreeAsset;
			}
			
			uiDocument.rootVisualElement.Q<Label>(label).text = label;
		}
		private void Awake()
		{
			InitializeComponents();
			BuildPanel();
		}

		void BuildPanel()
		{
			CreateRenderTexture();
			CreatePanelSettings();
			CreateUIDocument();
			CreateMaterial();
			
			SetMaterialToRenderer();
			SetPanelSize();
		}

		void SetMaterialToRenderer()
		{
			if (meshRenderer != null)
			{
				meshRenderer.sharedMaterial = material;
			}
		}

		void SetPanelSize()
		{
			if (renderTexture != null && (renderTexture.width != panelWidth || renderTexture.height != panelHeight))
			{
				renderTexture.Release();
				renderTexture.width = panelWidth;
				renderTexture.height = panelHeight;
				renderTexture.Create();
				
				uiDocument?.rootVisualElement?.MarkDirtyRepaint();
			}
			transform.localScale = new Vector3(panelWidth/pixelsPerUnit, panelHeight/pixelsPerUnit, 1.0f);
			
		}

		void CreateMaterial()
		{
			string shaderName = panelSettings.colorClearValue.a < 1.0f ? transparentShader : textureShader;
			material = new Material(Shader.Find(shaderName));
			material.SetTexture(MainTex, renderTexture);
			
		}
		void CreateUIDocument()
		{
			uiDocument = gameObject.GetComponent<UIDocument>();
			if (uiDocument == null)
			{
				uiDocument = gameObject.AddComponent<UIDocument>();
			}
			uiDocument.panelSettings = panelSettings;
			uiDocument.visualTreeAsset = visualTreeAsset;
			
		}
		
		void CreatePanelSettings()
		{
			panelSettings = Instantiate(panelSettingsAsset);
			panelSettings.targetTexture = renderTexture;
			panelSettings.clearColor = true;
			panelSettings.scaleMode = PanelScaleMode.ConstantPixelSize;
			panelSettings.scale = panelScale;
			panelSettings.name = $"{name} panel settings";
		}

		void CreateRenderTexture()
		{
			RenderTextureDescriptor desc = renderTextureAsset.descriptor;
			desc.width = panelWidth;
			desc.height = panelHeight;
			renderTexture = new RenderTexture(desc)
			{
				name = $"{name} RenderTexture",
			};
		}
		
		void InitializeComponents()
		{
			var mf = InitializeMeshFilter();
			InitializeMeshRenderer();

			mf.sharedMesh = GetQuadMesh();
			

		}

		MeshFilter InitializeMeshFilter()
		{
			MeshFilter mf = GetComponent<MeshFilter>();
			if (mf == null)
			{
				mf = gameObject.AddComponent<MeshFilter>();
			}

			return mf;
		}
		void InitializeMeshRenderer()
		{
			meshRenderer = GetComponent<MeshRenderer>();
			if (meshRenderer == null)
			{
				meshRenderer = gameObject.AddComponent<MeshRenderer>();
			}
			
			meshRenderer.sharedMaterial = material;
			meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
			meshRenderer.receiveShadows = false;
			meshRenderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
			meshRenderer.lightProbeUsage = LightProbeUsage.Off;
			meshRenderer.reflectionProbeUsage = ReflectionProbeUsage.Off;
			
		}

		static Mesh GetQuadMesh()
		{
			GameObject tempQuad = GameObject.CreatePrimitive(PrimitiveType.Quad);
			Mesh quadMesh = tempQuad.GetComponent<MeshFilter>().sharedMesh;
			Destroy(tempQuad);
			
			return quadMesh;
		}
	}
}