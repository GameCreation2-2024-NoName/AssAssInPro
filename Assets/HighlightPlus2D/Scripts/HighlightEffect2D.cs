//#define USE_SPRITE_SKIN

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

#if USE_SPRITE_SKIN
using UnityEngine.U2D.Animation;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HighlightPlus2D {

	public delegate void OnObjectHighlightStartEvent(GameObject obj, ref bool cancelHighlight);
	public delegate void OnObjectHighlightEndEvent(GameObject obj);


	public enum SeeThroughMode {
		WhenHighlighted = 0,
		AlwaysWhenOccluded = 1,
		Never = 2
	}

	public enum QualityLevel {
		Simple = 0,
		Medium = 1,
		High = 2
	}

	public enum TargetOptions {
		Children,
		OnlyThisObject,
		RootToChildren,
		LayerInScene,
		LayerInChildren
	}

	[ExecuteInEditMode]
	[HelpURL("https://kronnect.com/support")]
	[DefaultExecutionOrder(150)]
	public partial class HighlightEffect2D : MonoBehaviour {

		[Serializable]
		public struct GlowPassData {
			public float offset;
			public float alpha;
			public Color color;
		}

		struct ModelMaterials {
			public Transform transform;
			public bool currentRenderIsVisible;
			public Renderer renderer;
			public Material fxMatMask, fxMatMaskExclusive, fxMatDepthWrite, fxMatGlow, fxMatOutline, fxMatUnMask;
			public Material fxMatSeeThrough, fxMatOverlay, fxMatShadow;
			public Matrix4x4 maskMatrix, effectsMatrix;
			public Vector2 center, pivotPos;
			public float aspectRatio;
			public float rectWidth, rectHeight;
			public Mesh quadMesh;
			public bool valid;
			public MeshFilter mf;
#if USE_SPRITE_SKIN
            public SpriteSkin spriteSkin;
#endif
		}

		public bool previewInEditor = true;

		/// <summary>
		/// The layer used to render the effects. If 0, it will default to the gameobject layer
		/// </summary>
		[Tooltip("The layer used to render the effects.  If set to default, effects will be rendered using the gameobject layer.")]
		public int renderingLayer;

		[Tooltip("Draw effects on top of any occluder or opaque object.")]
		public bool alwaysOnTop;

		/// <summary>
		/// Specifies which objects are affected by this effect.
		/// </summary>
		[Tooltip("Specifies which objects are affected by this effect")]
		public TargetOptions effectGroup = TargetOptions.Children;

		/// <summary>
		/// The layer that contains the affected objects by this effect when effectGroup is set to LayerMask.
		/// </summary>
		[Tooltip("The layer that contains the affected objects by this effect when effectGroup is set to LayerMask.")]
		public LayerMask effectGroupLayer = -1;


		[NonSerialized]
		public Transform target;

		public bool occluder;

		[SerializeField]
		bool _highlighted;

		public bool highlighted {
			get {
				return _highlighted;
			}
			set {
				if (_highlighted != value) {
					SetHighlighted(value);
				}
			}
		}

		[Tooltip("Ignore highlighting on this object.")]
		public bool ignore;
		[Tooltip("Enable if sprite packing is polygon or SVG.")]
		public bool polygonPacking;
		[Tooltip("For mesh-based sprites, preserve its original mesh instead of using a single quad internally to render effects.")]
		public bool preserveMesh;
		[Tooltip("Do not combine outline or glow with other highlighted sprites")]
		public bool outlineExclusive;
		public float fadeInDuration;
		public float fadeOutDuration;

		[Range(0, 1)]
		public float overlay = 0.5f;
		public Color overlayColor = Color.yellow;
		public float overlayAnimationSpeed = 1f;
		[Range(0, 1)]
		public float overlayMinIntensity = 0.5f;
		[Range(0, 1)]
		public float overlayBlending = 1.0f;
		public int overlayRenderQueue = 3001;

		[Range(0, 1)]
		public float outline = 1f;
		public Color outlineColor = Color.black;
		public float outlineWidth = 0.5f;
		[Tooltip("Uses additional passes to create a better outline.")]
		public QualityLevel outlineQuality = QualityLevel.Simple;
		[Tooltip("Forces bilinear sampling to smooth edges when rendering outline effect.")]
		public bool outlineSmooth;

		[Range(0, 5)]
		public float glow;
		public float glowWidth = 1.5f;
		public bool glowDithering = true;
		public float glowMagicNumber1 = 0.75f;
		public float glowMagicNumber2 = 0.5f;
		public float glowAnimationSpeed = 1f;
		[Tooltip("Forces bilinear sampling to smooth edges when rendering glow effect.")]
		public bool glowSmooth;
		[Tooltip("Uses additional passes to create a better glow effect.")]
		public QualityLevel glowQuality = QualityLevel.Simple;

#if UNITY_2020_2_OR_NEWER
		[NonReorderable]
#endif
		[Tooltip("Renders effect on top of other sprites in the same sorting layer.")]
		public GlowPassData[] glowPasses;

		[Range(0.1f, 3f)]
		[Tooltip("Scales the sprite while highlighted.")]
		public float zoomScale = 1f;

		[Range(0, 1f)]
		public float shadowIntensity;
		public Color shadowColor = new Color(0, 0, 0, 0.2f);
		public Vector2 shadowOffset = new Vector2(0.1f, -0.1f);
		public bool shadow3D;

		public event OnObjectHighlightStartEvent OnObjectHighlightStart;
		public event OnObjectHighlightEndEvent OnObjectHighlightEnd;

		public SeeThroughMode seeThrough = SeeThroughMode.Never;
		[Range(0, 5f)]
		public float seeThroughIntensity = 0.8f;
		[Range(0, 1)]
		public float seeThroughTintAlpha = 0.5f;
		public Color seeThroughTintColor = Color.red;
		[Range(0, 1)]
		public float seeThroughNoise = 1;

		[Tooltip("Snap sprite renderers to a grid in world space at render-time.")]
		public bool pixelSnap;
		[Range(0, 1)]
		public float alphaCutOff = 0.05f;
		[Tooltip("Automatically computes the sprite center based on texture colors.")]
		public bool autoSize = true;
		public Vector2 center;
		public Vector2 scale = Vector2.one;
		public float aspectRatio = 1f;

		// This is informative.
		public Vector2 pivotPos;

		static Mesh _quadMesh;
		public static Mesh GetQuadMesh() {
			if (_quadMesh == null) {
				_quadMesh = new Mesh {
					vertices = new[] {
						new Vector3(-0.5f, -0.5f, 0),
						new Vector3(-0.5f, +0.5f, 0),
						new Vector3(+0.5f, +0.5f, 0),
						new Vector3(+0.5f, -0.5f, 0),
					},
					normals = new[] {
						Vector3.forward,
						Vector3.forward,
						Vector3.forward,
						Vector3.forward,
					},
					triangles = new[] { 0, 1, 2, 2, 3, 0 },

					uv = new[] {
						new Vector2(0, 0),
						new Vector2(0, 1),
						new Vector2(1, 1),
						new Vector2(1, 0),
					}
				};
			}
			return _quadMesh;

		}


		[SerializeField, HideInInspector]
		ModelMaterials[] rms;
		[SerializeField, HideInInspector]
		int rmsCount;

		static readonly Vector2[] offsetsHQ = {
				new Vector2 (0, 1),
				new Vector2 (1, 1),
				new Vector2 (1, 0),
				new Vector2 (1, -1),
				new Vector2 (0, -1),
				new Vector2 (-1, -1),
				new Vector2 (-1, 0),
				new Vector2 (-1, 1)
			};
		static readonly Vector2[] offsetsMQ = {
				new Vector2(1, 1),
				new Vector2(1, -1),
				new Vector2(-1, -1),
				new Vector2(-1, 1)
			};

		static Material fxMatSpriteMask, fxMatSpriteUnMask, fxMatSpriteDepthWrite, fxMatSpriteSeeThrough, fxMatSpriteGlow;
		static Material fxMatSpriteOutline, fxMatSpriteOverlay, fxMatSpriteShadow, fxMatSpriteShadow3D;
		static Material dummyMaterial;
		List<Vector3> vertices;
		List<int> indices;

		MaterialPropertyBlock outlineProps, glowProps;

		[SerializeField]
		Vector3 scaleBeforeZoom, scaleAfterZoom;

		readonly static Dictionary<Sprite, Mesh> cachedMeshes = new Dictionary<Sprite, Mesh>();
		readonly static Dictionary<Texture, Texture> cachedTextures = new Dictionary<Texture, Texture>();

		bool useGPUInstancing;

		readonly List<Matrix4x4> propMatrices = new List<Matrix4x4>();
		readonly List<Vector4> propPivots = new List<Vector4>();
		readonly List<Vector4> propGlowData = new List<Vector4>();
		readonly List<Vector4> propGlowColor = new List<Vector4>();

		bool requiresUpdateMaterial;
#if USE_SPRITE_SKIN
        readonly List<Vector3> animatedVertices = new List<Vector3>();
#endif
		int enabledTimeFrame;

		enum FadingState {
			FadingOut = -1,
			NoFading = 0,
			FadingIn = 1
		}
		float fadeStartTime;
		FadingState fading = FadingState.NoFading;



		void OnEnable() {
			if (target == null) {
				target = transform;
			}
			if (glowPasses == null || glowPasses.Length == 0) {
				glowPasses = new GlowPassData[4];
				glowPasses[0] = new GlowPassData() { offset = 4, alpha = 0.1f, color = new Color(0.64f, 1f, 0f, 1f) };
				glowPasses[1] = new GlowPassData() { offset = 3, alpha = 0.2f, color = new Color(0.64f, 1f, 0f, 1f) };
				glowPasses[2] = new GlowPassData() { offset = 2, alpha = 0.3f, color = new Color(0.64f, 1f, 0f, 1f) };
				glowPasses[3] = new GlowPassData() { offset = 1, alpha = 0.4f, color = new Color(0.64f, 1f, 0f, 1f) };
			}

			outlineProps = new MaterialPropertyBlock();
			glowProps = new MaterialPropertyBlock();
			enabledTimeFrame = Time.frameCount - 1;

			CheckSpriteSupportDependencies();
		}

		private void OnValidate() {
			outlineWidth = Mathf.Max(0, outlineWidth);
			glowWidth = Mathf.Max(0, glowWidth);
		}

		void DestroyMaterial(Material mat) {
			if (mat != null && mat != dummyMaterial) DestroyImmediate(mat);
		}

		void OnDestroy() {
			if (rms != null) {
				for (int k = 0; k < rms.Length; k++) {
					DestroyMaterial(rms[k].fxMatDepthWrite);
					DestroyMaterial(rms[k].fxMatGlow);
					DestroyMaterial(rms[k].fxMatMask);
					DestroyMaterial(rms[k].fxMatMaskExclusive);
					DestroyMaterial(rms[k].fxMatOutline);
					DestroyMaterial(rms[k].fxMatOverlay);
					DestroyMaterial(rms[k].fxMatSeeThrough);
					DestroyMaterial(rms[k].fxMatShadow);
					DestroyMaterial(rms[k].fxMatUnMask);
				}
			}
		}

		void Reset() {
			Refresh();
		}


		public void Refresh() {
			if (!gameObject.activeInHierarchy) return;
			if (!enabled) {
				enabled = true;
			} else {
				SetupMaterial();
			}
		}

		void LateUpdate() {
#if UNITY_EDITOR
            if (!previewInEditor && !Application.isPlaying && !occluder)
                return;
#endif
			if (gameObject == null) return;

			bool seeThroughReal = seeThroughIntensity > 0 && (this.seeThrough == SeeThroughMode.AlwaysWhenOccluded || (this.seeThrough == SeeThroughMode.WhenHighlighted && _highlighted));
			if (!_highlighted && !seeThroughReal && shadowIntensity <= 0 && !occluder && !hitActive) {
				return;
			}

			if (rms == null) {
				SetupMaterial(); // delayed setup
				if (rmsCount == 0) return;
			}

			// Ensure renderers are valid and visible (in case LODgroup has changed active renderer)
			for (int k = 0; k < rmsCount; k++) {
				if (rms[k].renderer != null && rms[k].renderer.isVisible != rms[k].currentRenderIsVisible) {
					SetupMaterial();
					break;
				}
			}

			if (requiresUpdateMaterial) {
				UpdateMaterialPropertiesNow();
			}

			// Compute tweening
			float fadeGroup = 1f;
			if (fading != FadingState.NoFading) {
				if (fading == FadingState.FadingIn) {
					if (fadeInDuration > 0) {
						fadeGroup = (Time.time - fadeStartTime) / fadeInDuration;
						if (fadeGroup > 1f) {
							fadeGroup = 1f;
							fading = FadingState.NoFading;
						}
					}
				} else if (fadeOutDuration > 0) {
					fadeGroup = 1f - (Time.time - fadeStartTime) / fadeOutDuration;
					if (fadeGroup < 0f) {
						fadeGroup = 0f;
						fading = FadingState.NoFading;
						_highlighted = false;
						if (OnObjectHighlightEnd != null) {
							OnObjectHighlightEnd(gameObject);
						}
						SendMessage("HighlightEnd", null, SendMessageOptions.DontRequireReceiver);
					}
				}
			}

			// Apply effect
			float glowReal = this._highlighted ? this.glow : 0;
			int layer = renderingLayer != 0 ? renderingLayer : gameObject.layer;
			Mesh defaultQuadMesh = GetQuadMesh();
			outlineProps.Clear();

			// First create masks
			float viewportAspectRatio = (float)Screen.height / Screen.width;
			for (int k = 0; k < rmsCount; k++) {
				rms[k].valid = false;
				Transform t = rms[k].transform;
				if (t == null || rms[k].fxMatMask == null)
					continue;
				Vector3 lossyScale;
				Vector3 position = t.position;
				Renderer renderer = rms[k].renderer;
				if (renderer == null)
					continue;

				lossyScale = t.lossyScale;

				Vector2 pivot, flipVector;
				Vector4 uv = Vector4.zero;
				Texture spriteTexture = null;

				if (renderer is SpriteRenderer) {
					SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
					Sprite sprite = spriteRenderer.sprite;
					if (sprite == null)
						continue;

					float rectWidth = sprite.rect.width;
					float rectHeight = sprite.rect.height;
					if (rectWidth == 0 || rectHeight == 0)
						continue;
					rms[k].rectWidth = rectWidth;
					rms[k].rectHeight = rectHeight;

					// pass pivot position to shaders
					pivotPos = new Vector2(sprite.pivot.x / rectWidth, sprite.pivot.y / rectHeight);
					if (polygonPacking) {
						pivotPos.x = pivotPos.y = 0.5f;
						rms[k].quadMesh = SpriteToMesh(sprite);
#if USE_SPRITE_SKIN
                        SpriteSkin spriteSkin = rms[k].spriteSkin;
                        if (spriteSkin != null && spriteSkin.enabled) {
                            UpdateAnimatedVerticesPositions(rms[k].quadMesh, spriteSkin);
                            Bounds meshBounds = rms[k].quadMesh.bounds;
                            rms[k].center = meshBounds.center;
                            rms[k].aspectRatio = meshBounds.size.x / meshBounds.size.y;
                        }
#endif
					} else {
						rms[k].quadMesh = defaultQuadMesh;
					}

					pivot = rms[k].pivotPos = new Vector2(pivotPos.x - 0.5f, pivotPos.y - 0.5f);

					// adjust scale
					spriteTexture = sprite.texture;
					if (!polygonPacking && spriteTexture != null) {
						lossyScale.x *= rectWidth / sprite.pixelsPerUnit;
						lossyScale.y *= rectHeight / sprite.pixelsPerUnit;
						uv = new Vector4(sprite.rect.xMin / spriteTexture.width, sprite.rect.yMin / spriteTexture.height, sprite.rect.xMax / spriteTexture.width, sprite.rect.yMax / spriteTexture.height);
					}

					// inverted sprite?
					flipVector = new Vector2(spriteRenderer.flipX ? -1 : 1, spriteRenderer.flipY ? -1 : 1);

					// external alpha texture?
					Texture2D alphaTex = sprite.associatedAlphaSplitTexture;
					if (alphaTex != null) {
						rms[k].fxMatMask.SetTexture(ShaderParams.AlphaTex, alphaTex);
						rms[k].fxMatMask.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						rms[k].fxMatUnMask.SetTexture(ShaderParams.AlphaTex, alphaTex);
						rms[k].fxMatUnMask.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						rms[k].fxMatMaskExclusive.SetTexture(ShaderParams.AlphaTex, alphaTex);
						rms[k].fxMatMaskExclusive.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);

						if (outline > 0) {
							rms[k].fxMatOutline.SetTexture(ShaderParams.AlphaTex, alphaTex);
							rms[k].fxMatOutline.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						}
						if (glow > 0) {
							rms[k].fxMatGlow.SetTexture(ShaderParams.AlphaTex, alphaTex);
							rms[k].fxMatGlow.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						}
						if (overlay > 0) {
							rms[k].fxMatOverlay.SetTexture(ShaderParams.AlphaTex, alphaTex);
							rms[k].fxMatOverlay.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						}
						if (seeThrough != SeeThroughMode.Never) {
							rms[k].fxMatSeeThrough.SetTexture(ShaderParams.AlphaTex, alphaTex);
							rms[k].fxMatSeeThrough.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						}

						if (occluder) {
							rms[k].fxMatDepthWrite.SetTexture(ShaderParams.AlphaTex, alphaTex);
							rms[k].fxMatDepthWrite.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						}
						if (shadowIntensity > 0) {
							rms[k].fxMatShadow.SetTexture(ShaderParams.AlphaTex, alphaTex);
							rms[k].fxMatShadow.EnableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
						}
					}
				} else if (renderer is MeshRenderer) {
					pivot = Vector2.zero;
					uv = new Vector4(0, 0, 1, 1);
					flipVector = Vector2.one;
					rms[k].rectWidth = 100f;
					rms[k].rectHeight = 100f;
					Mesh mesh = defaultQuadMesh;
					if (preserveMesh && rms[k].mf != null && rms[k].mf.sharedMesh != null) {
						mesh = rms[k].mf.sharedMesh;
					}
					rms[k].quadMesh = mesh;
					rms[k].fxMatMask.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					rms[k].fxMatUnMask.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					rms[k].fxMatMaskExclusive.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					if (glow > 0) {
						rms[k].fxMatGlow.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					}
					if (outline > 0) {
						rms[k].fxMatOutline.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					}
					if (overlay > 0) {
						rms[k].fxMatOverlay.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					}
					if (seeThrough != SeeThroughMode.Never) {
						rms[k].fxMatSeeThrough.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					}
					if (occluder) {
						rms[k].fxMatDepthWrite.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					}
					if (shadowIntensity > 0) {
						rms[k].fxMatShadow.DisableKeyword(ShaderParams.SKW_ETC1_EXTERNAL_ALPHA);
					}

					if (renderer.sharedMaterial != null) {
						spriteTexture = renderer.sharedMaterial.mainTexture;
					}
				} else {
					continue;
				}

				// Assign realtime sprite properties to shaders
				rms[k].fxMatMask.SetVector(ShaderParams.Pivot, pivot);
				rms[k].fxMatUnMask.SetVector(ShaderParams.Pivot, pivot);
				rms[k].fxMatMaskExclusive.SetVector(ShaderParams.Pivot, pivot);

				Vector3 geom = new Vector3(rms[k].center.x, rms[k].center.y, rms[k].aspectRatio * viewportAspectRatio);

				if (glow > 0) {
					rms[k].fxMatGlow.SetVector(ShaderParams.Geom, geom);
					rms[k].fxMatGlow.mainTexture = glowSmooth ? TextureWithBilinearSampling(spriteTexture) : spriteTexture;
					rms[k].fxMatGlow.SetVector(ShaderParams.UV, uv);
					rms[k].fxMatGlow.SetVector(ShaderParams.Flip, flipVector);
					rms[k].fxMatGlow.SetFloat(ShaderParams.Fade, fadeGroup);
				}

				if (outline > 0) {
					rms[k].fxMatOutline.SetVector(ShaderParams.Geom, geom);
					rms[k].fxMatOutline.mainTexture = outlineSmooth ? TextureWithBilinearSampling(spriteTexture) : spriteTexture;
					rms[k].fxMatOutline.SetVector(ShaderParams.UV, uv);
					rms[k].fxMatOutline.SetVector(ShaderParams.Flip, flipVector);
					rms[k].fxMatOutline.SetFloat(ShaderParams.Fade, fadeGroup);
				}

				if (seeThrough != SeeThroughMode.Never) {
					rms[k].fxMatSeeThrough.mainTexture = spriteTexture;
					rms[k].fxMatSeeThrough.SetVector(ShaderParams.Pivot, pivot);
					rms[k].fxMatSeeThrough.SetVector(ShaderParams.UV, uv);
					rms[k].fxMatSeeThrough.SetVector(ShaderParams.Flip, flipVector);
					rms[k].fxMatSeeThrough.SetFloat(ShaderParams.Fade, fadeGroup);
				}

				if (overlay > 0) {
					rms[k].fxMatOverlay.mainTexture = spriteTexture;
					rms[k].fxMatOverlay.SetVector(ShaderParams.Pivot, pivot);
					rms[k].fxMatOverlay.SetVector(ShaderParams.UV, uv);
					rms[k].fxMatOverlay.SetVector(ShaderParams.Flip, flipVector);
					rms[k].fxMatOverlay.SetFloat(ShaderParams.Fade, fadeGroup);
				}

				if (shadowIntensity > 0) {
					rms[k].fxMatShadow.SetVector(ShaderParams.Pivot, shadow3D ? pivot : pivot - shadowOffset);
					rms[k].fxMatShadow.mainTexture = spriteTexture;
					rms[k].fxMatShadow.SetVector(ShaderParams.UV, uv);
					rms[k].fxMatShadow.SetVector(ShaderParams.Flip, flipVector);
					rms[k].fxMatShadow.SetFloat(ShaderParams.Fade, fadeGroup);
				}

				// Assign textures
				rms[k].fxMatMask.mainTexture = spriteTexture;
				rms[k].fxMatUnMask.mainTexture = spriteTexture;
				rms[k].fxMatMaskExclusive.mainTexture = spriteTexture;

				// UV mapping with atlas support
				rms[k].fxMatMask.SetVector(ShaderParams.UV, uv);
				rms[k].fxMatUnMask.SetVector(ShaderParams.UV, uv);
				rms[k].fxMatMaskExclusive.SetVector(ShaderParams.UV, uv);

				// Flip option
				rms[k].fxMatMask.SetVector(ShaderParams.Flip, flipVector);
				rms[k].fxMatUnMask.SetVector(ShaderParams.Flip, flipVector);
				rms[k].fxMatMaskExclusive.SetVector(ShaderParams.Flip, flipVector);

				Matrix4x4 matrix = Matrix4x4.TRS(position, t.rotation, lossyScale);
				rms[k].maskMatrix = matrix;
				if (!autoSize) {
					lossyScale.x *= scale.x;
					lossyScale.y *= scale.y;
					rms[k].effectsMatrix = Matrix4x4.TRS(position, t.rotation, lossyScale);
				} else {
					rms[k].effectsMatrix = matrix;
				}

				rms[k].valid = true;
				Mesh quadMesh = rms[k].quadMesh;
				if (occluder) {
					Material fxMat = rms[k].fxMatDepthWrite;
					fxMat.SetVector(ShaderParams.Pivot, pivot);
					fxMat.mainTexture = spriteTexture;
					fxMat.SetVector(ShaderParams.UV, uv);
					fxMat.SetVector(ShaderParams.Flip, flipVector);
					Graphics.DrawMesh(quadMesh, matrix, fxMat, layer);
				} else {
					if (outlineExclusive) {
						DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatMaskExclusive, layer, outlineProps);
					}
					DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatMask, layer, outlineProps);
					DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatUnMask, layer, outlineProps);
				}
			}

			// Add shadow
			if (shadowIntensity > 0) {
				for (int k = 0; k < rmsCount; k++) {
					if (rms[k].valid) {
						Matrix4x4 matrix = rms[k].maskMatrix;
						Mesh quadMesh = rms[k].quadMesh;
						DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatShadow, layer, outlineProps);
					}
				}
			}

			if (occluder)
				return;


			// Add see-through
			if (seeThroughReal) {
				for (int k = 0; k < rmsCount; k++) {
					if (rms[k].valid) {
						Matrix4x4 matrix = rms[k].maskMatrix;
						Mesh quadMesh = rms[k].quadMesh;
						DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatSeeThrough, layer, outlineProps);
					}
				}
			}

			// Highlight effects
			if (_highlighted) {
				// Add Glow
				if (glow > 0) {
					for (int k = 0; k < rms.Length; k++) {
						if (!rms[k].valid) continue;
						Matrix4x4 matrix = rms[k].effectsMatrix;
						Vector2 originalPivotPos = rms[k].pivotPos;
						Mesh quadMesh = rms[k].quadMesh;

						if (useGPUInstancing) {
							// GPU instancing glow
							propMatrices.Clear();
							propPivots.Clear();
							propGlowData.Clear();
							propGlowColor.Clear();

							Vector2[] offsets = glowQuality == QualityLevel.High ? offsetsHQ : offsetsMQ;
							for (int j = 0; j < glowPasses.Length; j++) {
								if (glowQuality != QualityLevel.Simple) {
									Vector4 glowData = new Vector4(glowReal * glowPasses[j].alpha, glowWidth / 100f, glowMagicNumber1, glowMagicNumber2);
									float mult = glowPasses[j].offset * glowWidth;
									Vector2 disp = new Vector2(mult / rms[k].rectWidth, mult / rms[k].rectHeight);
									for (int z = 0; z < offsets.Length; z++) {
										propPivots.Add(new Vector4(originalPivotPos.x + disp.x * offsets[z].x, originalPivotPos.y + disp.y * offsets[z].y, 0, 0));
										propGlowColor.Add(glowPasses[j].color);
										propGlowData.Add(glowData);
										propMatrices.Add(matrix);
									}
								} else {
									propPivots.Add(rms[k].pivotPos);
									propGlowColor.Add(glowPasses[j].color);
									propGlowData.Add(new Vector4(glowReal * glowPasses[j].alpha, glowPasses[j].offset * glowWidth / 100f, glowMagicNumber1, glowMagicNumber2));
									propMatrices.Add(matrix);
								}
							}
							glowProps.SetVectorArray(ShaderParams.PivotArray, propPivots);
							glowProps.SetVectorArray(ShaderParams.GlowArray, propGlowData);
							glowProps.SetVectorArray(ShaderParams.GlowColorArray, propGlowColor);
							DrawSubMeshesInstanced(quadMesh, rms[k].renderer, rms[k].fxMatGlow, propMatrices, layer, glowProps);
						} else {
							// Non instanced glow
							for (int j = 0; j < glowPasses.Length; j++) {
								glowProps.SetColor(ShaderParams.GlowColor, glowPasses[j].color);
								if (glowQuality != QualityLevel.Simple) {
									Vector2[] offsets = glowQuality == QualityLevel.High ? offsetsHQ : offsetsMQ;
									glowProps.SetVector(ShaderParams.Glow, new Vector4(glowReal * glowPasses[j].alpha, glowWidth / 100f, glowMagicNumber1, glowMagicNumber2));
									float mult = glowPasses[j].offset * glowWidth;
									Vector2 disp = new Vector2(mult / rms[k].rectWidth, mult / rms[k].rectHeight);
									for (int z = 0; z < offsets.Length; z++) {
										Vector2 newPivot = new Vector2(originalPivotPos.x + disp.x * offsets[z].x, originalPivotPos.y + disp.y * offsets[z].y);
										glowProps.SetVector(ShaderParams.Pivot, newPivot);
										DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatGlow, layer, glowProps);
									}
								} else {
									glowProps.SetVector(ShaderParams.Pivot, rms[k].pivotPos);
									glowProps.SetVector(ShaderParams.Glow, new Vector4(glowReal * glowPasses[j].alpha, glowPasses[j].offset * glowWidth / 100f, glowMagicNumber1, glowMagicNumber2));
									DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatGlow, layer, glowProps);
								}
							}
						}
					}
				}

				// Add Outline
				if (outline > 0) {
					Vector2 disp = new Vector2(outlineWidth / 100f, outlineWidth / 100f);
					for (int k = 0; k < rms.Length; k++) {
						if (!rms[k].valid) continue;
						Matrix4x4 matrix = rms[k].effectsMatrix;
						Mesh quadMesh = rms[k].quadMesh;
						Vector2 originalPivotPos = rms[k].pivotPos;

						if (outlineQuality != QualityLevel.Simple) {
							Vector2[] offsets = outlineQuality == QualityLevel.High ? offsetsHQ : offsetsMQ;
							rms[k].fxMatOutline.SetFloat(ShaderParams.OutlineWidth, 0f);

							if (useGPUInstancing) {
								// GPU instancing outline
								propMatrices.Clear();
								propPivots.Clear();
								for (int z = 0; z < offsets.Length; z++) {
									propPivots.Add(new Vector4(originalPivotPos.x + disp.x * offsets[z].x, originalPivotPos.y + disp.y * offsets[z].y, 0, 0));
									propMatrices.Add(matrix);
								}
								outlineProps.SetVectorArray(ShaderParams.PivotArray, propPivots);
								DrawSubMeshesInstanced(quadMesh, rms[k].renderer, rms[k].fxMatOutline, propMatrices, layer, outlineProps);
							} else {
								for (int z = 0; z < offsets.Length; z++) {
									Vector2 newPivot = new Vector2(originalPivotPos.x + disp.x * offsets[z].x, originalPivotPos.y + disp.y * offsets[z].y);
									outlineProps.SetVector(ShaderParams.Pivot, newPivot);
									//                                Graphics.DrawMesh(quadMesh, matrix, rms[k].fxMatOutline, layer, null, 0, outlineProps);
									DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatOutline, layer, outlineProps);
								}
							}
						} else {
							outlineProps.SetVector(ShaderParams.Pivot, originalPivotPos);
							DrawSubMeshes(quadMesh, rms[k].renderer, matrix, rms[k].fxMatOutline, layer, outlineProps);
						}
					}
				}
			}

			if (_highlighted || hitActive) {
				Color overlayColor = this.overlayColor;
				bool updateOverlayProps = false;
				float overlayMinIntensity = this.overlayMinIntensity;
				float overlayBlending = this.overlayBlending;
				if (hitActive) {
					updateOverlayProps = true;
					float t = hitFadeOutDuration > 0 ? (Time.time - hitStartTime) / hitFadeOutDuration : 1f;
					if (t >= 1f) {
						hitActive = false;
						overlayColor.a = _highlighted ? overlay : 0;
					} else {
						bool lerpToCurrentOverlay = _highlighted && overlay > 0;
						overlayColor = lerpToCurrentOverlay ? Color.Lerp(hitColor, overlayColor, t) : hitColor;
						overlayColor.a = lerpToCurrentOverlay ? Mathf.Lerp(1f - t, overlay, t) : 1f - t;
						overlayColor.a *= hitInitialIntensity;
						overlayMinIntensity = 1f;
						overlayBlending = 0;
					}
				} else {
					overlayColor.a *= overlay;
				}

				// Add Overlay
				if (overlayColor.a > 0) {
					for (int k = 0; k < rms.Length; k++) {
						if (!rms[k].valid) continue;
						Matrix4x4 matrix = rms[k].maskMatrix;
						Material fxMat = rms[k].fxMatOverlay;
						Mesh quadMesh = rms[k].quadMesh;

						if (updateOverlayProps) {
							fxMat.color = overlayColor;
							fxMat.SetVector(ShaderParams.OverlayData, new Vector3(overlayAnimationSpeed, overlayMinIntensity, overlayBlending));
						}
						//Graphics.DrawMesh(quadMesh, matrix, fxMat, layer);
						DrawSubMeshes(quadMesh, rms[k].renderer, matrix, fxMat, layer, outlineProps);
					}
				}
			}

		}


		void DrawSubMeshes(Mesh mesh, Renderer renderer, Matrix4x4 matrix, Material mat, int layer, MaterialPropertyBlock props) {
			int subMeshCount = mesh.subMeshCount;
			if (subMeshCount == 1) {
				Graphics.DrawMesh(mesh, matrix, mat, layer, null, 0, props);
				return;
			}

			Material[] materials = renderer.sharedMaterials;
			int maxMaterialIndex = materials.Length;
			for (int k = 0; k < subMeshCount; k++) {
				if (k < maxMaterialIndex) {
					props.SetTexture(ShaderParams.MainTex, materials[k].mainTexture);
				}
				Graphics.DrawMesh(mesh, matrix, mat, layer, null, k, props);
			}
		}

		void DrawSubMeshesInstanced(Mesh mesh, Renderer renderer, Material mat, List<Matrix4x4> matrices, int layer, MaterialPropertyBlock props) {
			int subMeshCount = mesh.subMeshCount;
			if (subMeshCount == 1) {
				Graphics.DrawMeshInstanced(mesh, 0, mat, matrices, props, UnityEngine.Rendering.ShadowCastingMode.Off, false, layer);
				return;
			}

			Material[] materials = renderer.sharedMaterials;
			int maxMaterialIndex = materials.Length;
			for (int k = 0; k < subMeshCount; k++) {
				if (k < maxMaterialIndex) {
					props.SetTexture(ShaderParams.MainTex, materials[k].mainTexture);
				}
				Graphics.DrawMeshInstanced(mesh, k, mat, matrices, props, UnityEngine.Rendering.ShadowCastingMode.Off, false, layer);
			}
		}

		void InitMaterial(ref Material material, string shaderName) {
			if (material == null) {
				Shader shaderFX = Shader.Find(shaderName);
				if (shaderFX == null) {
					Debug.LogError("Shader " + shaderName + " not found.");
					return;
				}
				material = new Material(shaderFX);
			}
		}

		public void SetTarget(Transform transform) {
			if (transform == null || transform == target)
				return;

			if (_highlighted) {
				ImmediateFadeOut();
			}

			target = transform;
			SetupMaterial();
		}

		public void SetHighlighted(bool state) {
			if (ignore)
				return;
			bool cancelHighlight = false;

			float now = Time.time;

			if (fading == FadingState.NoFading) {
				fadeStartTime = now;
			}

			if (state) {
				if (OnObjectHighlightStart != null) {
					OnObjectHighlightStart(gameObject, ref cancelHighlight);
					if (cancelHighlight) {
						return;
					}
				}
				if (fadeInDuration > 0) {
					if (fading == FadingState.FadingOut) {
						float remaining = fadeOutDuration - (now - fadeStartTime);
						fadeStartTime = now - remaining;
						fadeStartTime = Mathf.Min(fadeStartTime, now);
					}
					fading = FadingState.FadingIn;
				} else {
					fading = FadingState.NoFading;
				}
				_highlighted = true;
				SendMessage("HighlightStart", null, SendMessageOptions.DontRequireReceiver);
			} else if (_highlighted) {
				if (fadeOutDuration > 0) {
					if (fading == FadingState.FadingIn) {
						float elapsed = now - fadeStartTime;
						fadeStartTime = now + elapsed - fadeInDuration;
						fadeStartTime = Mathf.Min(fadeStartTime, now);
					}
					fading = FadingState.FadingOut; // when fade out ends, highlighted will be set to false in OnRenderObject
				} else {
					fading = FadingState.NoFading;
					ImmediateFadeOut();
				}
			}

			Refresh();
		}


		void ImmediateFadeOut() {
			fading = FadingState.NoFading;
			_highlighted = false;
			if (OnObjectHighlightEnd != null) {
				OnObjectHighlightEnd(gameObject);
			}
			SendMessage("HighlightEnd", null, SendMessageOptions.DontRequireReceiver);
		}
		void SetupMaterial() {

			rmsCount = 0;
			if (target == null)
				return;

			Renderer[] rr = null;

			switch (effectGroup) {
				case TargetOptions.OnlyThisObject:
					Renderer renderer = target.GetComponent<Renderer>();
					if (renderer != null) {
						rr = new Renderer[1];
						rr[0] = renderer;
					}
					break;
				case TargetOptions.RootToChildren:
					Transform root = target;
					while (root.parent != null) {
						root = root.parent;
					}
					rr = root.GetComponentsInChildren<Renderer>();
					break;
				case TargetOptions.LayerInScene: {
						HighlightEffect2D eg = this;
						if (target != transform) {
							HighlightEffect2D targetEffect = target.GetComponent<HighlightEffect2D>();
							if (targetEffect != null) {
								eg = targetEffect;
							}
						}
						rr = FindRenderersWithLayerInScene(eg.effectGroupLayer);
					}
					break;
				case TargetOptions.LayerInChildren: {
						HighlightEffect2D eg = this;
						if (target != transform) {
							HighlightEffect2D targetEffect = target.GetComponent<HighlightEffect2D>();
							if (targetEffect != null) {
								eg = targetEffect;
							}
						}
						rr = FindRenderersWithLayerInChildren(eg.effectGroupLayer);
					}
					break;
				case TargetOptions.Children:
					rr = target.GetComponentsInChildren<Renderer>();
					break;
			}

			SetupMaterial(rr);
		}

		List<Renderer> tempRR;

		Renderer[] FindRenderersWithLayerInScene(LayerMask layer) {
			Renderer[] rr = Misc.FindObjectsOfType<Renderer>();
			if (tempRR == null) {
				tempRR = new List<Renderer>();
			} else {
				tempRR.Clear();
			}
			for (var i = 0; i < rr.Length; i++) {
				Renderer r = rr[i];
				if (((1 << r.gameObject.layer) & layer) != 0) {
					tempRR.Add(r);
				}
			}
			return tempRR.ToArray();
		}

		Renderer[] FindRenderersWithLayerInChildren(LayerMask layer) {
			Renderer[] rr = target.GetComponentsInChildren<Renderer>();
			if (tempRR == null) {
				tempRR = new List<Renderer>();
			} else {
				tempRR.Clear();
			}
			for (var i = 0; i < rr.Length; i++) {
				Renderer r = rr[i];
				if (((1 << r.gameObject.layer) & layer) != 0) {
					tempRR.Add(r);
				}
			}
			return tempRR.ToArray();
		}

		/// <summary>
		/// Sets the color for all glow passes
		/// </summary>
		public void SetGlowColor(Color color) {
			if (glowPasses == null) return;
			for (int k = 0; k < glowPasses.Length; k++) {
				glowPasses[k].color = color;
			}
			UpdateMaterialProperties();
		}

		void SetupMaterial(Renderer[] rr) {
			if (rr == null) {
				rr = new Renderer[0];
			}
			if (rms == null) {
				rms = new ModelMaterials[rr.Length];
			} else {
				Array.Resize(ref rms, rr.Length);
			}
			if (aspectRatio < 0.01f) {
				aspectRatio = 0.01f;
			}
			glowProps.Clear();
			for (int k = 0; k < rr.Length; k++) {
				Renderer renderer = rr[k];
				if (renderer == null) continue;
				rms[rmsCount].mf = renderer.GetComponent<MeshFilter>();
				rms[rmsCount].renderer = renderer;
				rms[rmsCount].currentRenderIsVisible = renderer.isVisible || Time.frameCount <= enabledTimeFrame;

				if (!rms[rmsCount].currentRenderIsVisible) {
					rmsCount++;
					continue;
				}

				if (renderer.transform != target && renderer.GetComponent<HighlightEffect2D>() != null)
					continue; // independent subobject

#if USE_SPRITE_SKIN
                rms[rmsCount].spriteSkin = renderer.GetComponent<SpriteSkin>();
#endif

				rms[rmsCount].center = center;
				rms[rmsCount].aspectRatio = aspectRatio;
				if (autoSize && renderer is SpriteRenderer) {
					SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
					ComputeSpriteCenter(rmsCount, spriteRenderer.sprite);
				}
				rms[rmsCount].transform = renderer.transform;
				if (rms[rmsCount].fxMatMask == null) rms[rmsCount].fxMatMask = Instantiate(fxMatSpriteMask);
				if (rms[rmsCount].fxMatMaskExclusive == null) rms[rmsCount].fxMatMaskExclusive = Instantiate(fxMatSpriteMask);
				if (rms[rmsCount].fxMatUnMask == null) rms[rmsCount].fxMatUnMask = Instantiate(fxMatSpriteUnMask);
				if (rms[rmsCount].fxMatDepthWrite == null) rms[rmsCount].fxMatDepthWrite = dummyMaterial;
				if (rms[rmsCount].fxMatGlow == null) rms[rmsCount].fxMatGlow = dummyMaterial;
				if (rms[rmsCount].fxMatOutline == null) rms[rmsCount].fxMatOutline = dummyMaterial;
				if (rms[rmsCount].fxMatSeeThrough == null) rms[rmsCount].fxMatSeeThrough = dummyMaterial;
				if (rms[rmsCount].fxMatShadow == null) rms[rmsCount].fxMatShadow = dummyMaterial;
				if (rms[rmsCount].fxMatOverlay == null) rms[rmsCount].fxMatOverlay = dummyMaterial;
				rmsCount++;
			}

			group = GetComponent<HighlightGroup2D>();

			UpdateMaterialProperties();
		}

		void CheckSpriteSupportDependencies() {
			InitMaterial(ref fxMatSpriteMask, "HighlightPlus2D/Sprite/Mask");
			if (dummyMaterial == null) dummyMaterial = Instantiate(fxMatSpriteMask);
			InitMaterial(ref fxMatSpriteUnMask, "HighlightPlus2D/Sprite/UnMask");
			InitMaterial(ref fxMatSpriteDepthWrite, "HighlightPlus2D/Sprite/JustDepth");
			useGPUInstancing = SystemInfo.supportsInstancing;
			InitMaterial(ref fxMatSpriteGlow, useGPUInstancing ? "HighlightPlus2D/Sprite/GlowInstanced" : "HighlightPlus2D/Sprite/Glow");
			fxMatSpriteGlow.enableInstancing = useGPUInstancing;
			InitMaterial(ref fxMatSpriteOutline, useGPUInstancing ? "HighlightPlus2D/Sprite/OutlineInstanced" : "HighlightPlus2D/Sprite/Outline");
			fxMatSpriteOutline.enableInstancing = useGPUInstancing;
			InitMaterial(ref fxMatSpriteOverlay, "HighlightPlus2D/Sprite/Overlay");
			InitMaterial(ref fxMatSpriteSeeThrough, "HighlightPlus2D/Sprite/SeeThrough");
			InitMaterial(ref fxMatSpriteShadow, "HighlightPlus2D/Sprite/Shadow");
			InitMaterial(ref fxMatSpriteShadow3D, "HighlightPlus2D/Sprite/Shadow3D");
		}

		Material GetMaterial(ref Material rmsMat, Material templateMat) {
			if (rmsMat == null || rmsMat == dummyMaterial) {
				rmsMat = Instantiate(templateMat);
			}
			return rmsMat;
		}

		HighlightGroup2D group;

		public void UpdateMaterialProperties() {
			requiresUpdateMaterial = true;
		}

		void UpdateMaterialPropertiesNow() {

			requiresUpdateMaterial = false;

			if (rms == null) {
				SetupMaterial();
				if (rmsCount == 0) return;
			}

			if (ignore)
				_highlighted = false;

			Color outlineColor = this.outlineColor;
			outlineColor.a = outline;
			Color overlayColor = this.overlayColor;
			overlayColor.a = overlay;
			Color seeThroughTintColor = this.seeThroughTintColor;
			seeThroughTintColor.a = this.seeThroughTintAlpha;

			if (outlineWidth < 0.01f) {
				outlineWidth = 0.01f;
			}
			if (glowWidth < 0) {
				glowWidth = 0;
			}
			if (overlayMinIntensity > overlay) {
				overlayMinIntensity = overlay;
			}

			int zTest = alwaysOnTop ? (int)CompareFunction.Always : (int)CompareFunction.LessEqual;

			for (int k = 0; k < rmsCount; k++) {
				// Setup materials
				if (rms[k].transform == null)
					continue;

				ToggleZoom(rms[k].transform);

				// Sprite related
				rms[k].fxMatMask.SetFloat(ShaderParams.CutOff, alphaCutOff);
				if (pixelSnap) {
					rms[k].fxMatMask.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					rms[k].fxMatUnMask.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					rms[k].fxMatMaskExclusive.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					rms[k].fxMatShadow.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
				} else {
					rms[k].fxMatMask.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					rms[k].fxMatUnMask.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					rms[k].fxMatMaskExclusive.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					rms[k].fxMatShadow.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
				}
				if (polygonPacking) {
					rms[k].fxMatMask.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					rms[k].fxMatUnMask.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					rms[k].fxMatMaskExclusive.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					rms[k].fxMatShadow.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
				} else {
					rms[k].fxMatMask.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					rms[k].fxMatUnMask.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					rms[k].fxMatMaskExclusive.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					rms[k].fxMatShadow.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
				}

				rms[k].fxMatMask.SetInt(ShaderParams.ZTest, zTest);
				rms[k].fxMatUnMask.SetInt(ShaderParams.ZTest, zTest);
				rms[k].fxMatMaskExclusive.SetInt(ShaderParams.ZTest, zTest);
				if (!shadow3D) rms[k].fxMatShadow.SetInt(ShaderParams.ZTest, zTest);

				if (occluder) {
					Material fxMat = GetMaterial(ref rms[k].fxMatDepthWrite, fxMatSpriteDepthWrite);
					if (fxMat != null) {
						fxMat.SetFloat(ShaderParams.CutOff, alphaCutOff);
						if (pixelSnap) {
							fxMat.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
						} else {
							fxMat.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
						}
						if (polygonPacking) {
							fxMat.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
						} else {
							fxMat.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
						}
					}
				}


				// Glow
				if (glow > 0) {
					Material fxMat = GetMaterial(ref rms[k].fxMatGlow, fxMatSpriteGlow);
					if (fxMat != null) {
						if (useGPUInstancing) fxMat.enableInstancing = true;
						fxMat.SetVector(ShaderParams.Glow2, new Vector3(0f, glowAnimationSpeed, glowDithering ? 0 : 1));
						fxMat.SetFloat(ShaderParams.CutOff, alphaCutOff);
						if (pixelSnap) {
							fxMat.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
						} else {
							fxMat.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
						}
						if (polygonPacking) {
							fxMat.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
						} else {
							fxMat.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
						}
						fxMat.SetInt(ShaderParams.ZTest, zTest);
					}
				}

				// Outline
				if (outline > 0) {
					Material fxMat = GetMaterial(ref rms[k].fxMatOutline, fxMatSpriteOutline);
					if (fxMat != null) {
						if (useGPUInstancing) fxMat.enableInstancing = true;
						Color outlineColorAdj = outlineColor;
						if (outlineSmooth) {
							if (outlineQuality == QualityLevel.Simple) {
								outlineColorAdj.a *= 4f;
							} else if (outlineQuality == QualityLevel.Medium) {
								outlineColorAdj.a *= 2f;
							}
							fxMat.EnableKeyword(ShaderParams.SKW_SMOOTH_EDGES);
						} else {
							fxMat.DisableKeyword(ShaderParams.SKW_SMOOTH_EDGES);
						}

						fxMat.SetColor(ShaderParams.OutlineColor, outlineColorAdj);
						fxMat.SetFloat(ShaderParams.OutlineWidth, outlineWidth / 100f);

						fxMat.SetFloat(ShaderParams.CutOff, alphaCutOff);
						if (pixelSnap) {
							fxMat.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
						} else {
							fxMat.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
						}
						if (polygonPacking) {
							fxMat.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
						} else {
							fxMat.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
						}
						fxMat.SetInt(ShaderParams.ZTest, zTest);
					}
				}

				Renderer renderer = rms[k].renderer;
				Material mat = renderer.sharedMaterial;
				Texture texture = null;
				if (renderer != null && mat != null) {
					if (renderer is SpriteRenderer) {
						SpriteRenderer spriteRenderer = (SpriteRenderer)renderer;
						if (spriteRenderer.sprite != null) {
							texture = spriteRenderer.sprite.texture;
						}
					} else {
						texture = mat.mainTexture;
					}
				}

				// See-through
				if (seeThrough != SeeThroughMode.Never) {
					Material fxMat = GetMaterial(ref rms[k].fxMatSeeThrough, fxMatSpriteSeeThrough);
					if (mat.HasProperty("_MainTex")) {
						fxMat.mainTexture = texture;
						fxMat.mainTextureOffset = mat.mainTextureOffset;
						fxMat.mainTextureScale = mat.mainTextureScale;
					}
					fxMat.SetFloat(ShaderParams.SeeThrough, seeThroughIntensity);
					fxMat.SetColor(ShaderParams.SeeThroughTintColor, seeThroughTintColor);
					fxMat.SetFloat(ShaderParams.CutOff, alphaCutOff);
					fxMat.SetFloat(ShaderParams.SeeThroughNoise, seeThroughNoise);
					if (pixelSnap) {
						fxMat.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					} else {
						fxMat.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					}
					if (polygonPacking) {
						fxMat.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					} else {
						fxMat.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					}

				}

				// Overlay
				if (overlay > 0) {
					Material fxMat = GetMaterial(ref rms[k].fxMatOverlay, fxMatSpriteOverlay);
					if (mat != null) {
						if (mat.HasProperty(ShaderParams.MainTex)) {
							fxMat.mainTexture = texture;
							fxMat.mainTextureOffset = mat.mainTextureOffset;
							fxMat.mainTextureScale = mat.mainTextureScale;
						}
						if (mat.HasProperty(ShaderParams.Color)) {
							fxMat.SetColor(ShaderParams.OverlayBackColor, mat.GetColor(ShaderParams.Color));
						}
					}
					fxMat.SetFloat(ShaderParams.CutOff, alphaCutOff);
					fxMat.color = overlayColor;
					fxMat.SetVector(ShaderParams.OverlayData, new Vector3(overlayAnimationSpeed, overlayMinIntensity, overlayBlending));
					if (pixelSnap) {
						fxMat.EnableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					} else {
						fxMat.DisableKeyword(ShaderParams.SKW_PIXELSNAP_ON);
					}
					if (polygonPacking) {
						fxMat.EnableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					} else {
						fxMat.DisableKeyword(ShaderParams.SKW_POLYGON_PACKING);
					}
					fxMat.renderQueue = overlayRenderQueue;
					fxMat.SetInt(ShaderParams.ZTest, zTest);
				}

				// Shadow
				if (shadowIntensity > 0) {
					Material fxMat = GetMaterial(ref rms[k].fxMatShadow, shadow3D ? fxMatSpriteShadow3D : fxMatSpriteShadow);
					if (mat != null) {
						if (mat.HasProperty("_MainTex")) {
							fxMat.mainTexture = texture;
							fxMat.mainTextureOffset = mat.mainTextureOffset;
							fxMat.mainTextureScale = mat.mainTextureScale;
						}
					}
					if (shadow3D) {
						shadowIntensity = 1f;
					}
					Color shadowColor = this.shadowColor;
					shadowColor.a *= shadowIntensity;
					fxMat.color = shadowColor;
				}

				// Render queues
				int renderQueueOffset = 0;
				int groupQueueOffset = 0;
				if (outlineExclusive) {
					renderQueueOffset += 1000;
				}
				if (group != null) {
					groupQueueOffset += HighlightGroup2D.GROUP_OFFSET * group.groupNumber;
				}
				renderQueueOffset += groupQueueOffset;
				rms[k].fxMatMask.renderQueue = fxMatSpriteMask.renderQueue + groupQueueOffset;
				rms[k].fxMatShadow.renderQueue = fxMatSpriteShadow.renderQueue + groupQueueOffset;
				rms[k].fxMatUnMask.renderQueue = fxMatSpriteUnMask.renderQueue + renderQueueOffset;
				rms[k].fxMatOutline.renderQueue = fxMatSpriteOutline.renderQueue + renderQueueOffset;
				rms[k].fxMatGlow.renderQueue = fxMatSpriteGlow.renderQueue + renderQueueOffset;
				rms[k].fxMatSeeThrough.renderQueue = fxMatSpriteSeeThrough.renderQueue + renderQueueOffset;
				rms[k].fxMatOverlay.renderQueue = fxMatSpriteOverlay.renderQueue + renderQueueOffset;
				rms[k].fxMatMaskExclusive.renderQueue = fxMatSpriteGlow.renderQueue + renderQueueOffset - 1;
			}
		}



		void ComputeSpriteCenter(int index, Sprite sprite) {
			if (sprite == null) return;
			Rect texRect = sprite.textureRect.width != 0 ? sprite.textureRect : sprite.rect;
			texRect.x -= sprite.rect.xMin;
			texRect.y -= sprite.rect.yMin;
			float xMin = texRect.xMin;
			float xMax = texRect.xMax;
			float yMin = texRect.yMin;
			float yMax = texRect.yMax;
			float xMid = (xMax + xMin) / 2;
			float yMid = (yMax + yMin) / 2;
			// substract pivot
			xMid -= sprite.pivot.x;
			yMid -= sprite.pivot.y;
			// normalize 0-1
			xMid = xMid / sprite.rect.width;
			yMid = yMid / sprite.rect.height;
			rms[index].center = new Vector2(xMid, yMid);
			// also set aspect ratio
			rms[index].aspectRatio = (float)(xMax - xMin) / (yMax - yMin);
		}

		void ToggleZoom(Transform target) {
			if (target == null)
				return;

			if (target.transform.localScale == scaleAfterZoom) {
				target.transform.localScale = scaleBeforeZoom;
			}

			if (_highlighted) {
				Vector3 scale = target.transform.localScale;
				scaleBeforeZoom = scale;
				scaleAfterZoom = new Vector3(scale.x * zoomScale, scale.y * zoomScale, 1f);
				target.transform.localScale = scaleAfterZoom;
			}
		}


		Mesh SpriteToMesh(Sprite sprite) {
			Mesh mesh;
			if (!cachedMeshes.TryGetValue(sprite, out mesh) || mesh == null) {
				mesh = new Mesh();
				Vector2[] spriteVertices = sprite.vertices;
				int vertexCount = spriteVertices.Length;
				if (vertices == null) {
					vertices = new List<Vector3>(vertexCount);
				} else {
					vertices.Clear();
				}
				for (int x = 0; x < vertexCount; x++) {
					vertices.Add(spriteVertices[x]);
				}
				ushort[] triangles = sprite.triangles;
				int indexCount = triangles.Length;
				if (indices == null) {
					indices = new List<int>(indexCount);
				} else {
					indices.Clear();
				}
				for (int x = 0; x < indexCount; x++) {
					indices.Add(triangles[x]);
				}
				mesh.SetVertices(vertices);
				mesh.SetTriangles(indices, 0);
				mesh.uv = sprite.uv;
				cachedMeshes[sprite] = mesh;
			}
			return mesh;
		}

#if USE_SPRITE_SKIN
        void UpdateAnimatedVerticesPositions(Mesh mesh, SpriteSkin spriteSkin) {
            animatedVertices.Clear();
            animatedVertices.AddRange(spriteSkin.GetDeformedVertexPositionData());
            mesh.SetVertices(animatedVertices);
            mesh.RecalculateBounds();
        }
#endif


		Texture TextureWithBilinearSampling(Texture tex) {
			if (tex == null || tex.filterMode != FilterMode.Point) {
				return tex;
			}
			Texture linTex;
			if (!cachedTextures.TryGetValue(tex, out linTex)) {
#if UNITY_EDITOR
                // Ensure texture is readable
                if (!Application.isPlaying) {
                    string path = AssetDatabase.GetAssetPath(tex);
                    if (!string.IsNullOrEmpty(path)) {
                        TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
                        if (ti != null) {
                            if (!ti.isReadable) {
                                ti.isReadable = true;
                                ti.SaveAndReimport();
                            }
                        }
                    }
                }
#endif
				linTex = Instantiate<Texture>(tex);
				linTex.filterMode = FilterMode.Bilinear;
				cachedTextures[tex] = linTex;
			}
			return linTex;
		}

	}
}

