using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HighlightPlus2D {
    [CustomEditor(typeof(HighlightEffect2D))]
    [CanEditMultipleObjects]
    public class HighlightEffectEditor : Editor {

        SerializedProperty previewInEditor, renderingLayer, alwaysOnTop, ignore, highlighted, occluder, effectGroup, effectGroupLayer;
        SerializedProperty pixelSnap, alphaCutOff, pivotPos, preserveMesh, polygonPacking, autoSize, center, scale, aspectRatio;
        SerializedProperty overlay, overlayColor, overlayAnimationSpeed, overlayMinIntensity, overlayBlending, overlayRenderQueue;
        SerializedProperty outline, outlineColor, outlineWidth, outlineQuality, outlineSmooth, outlineExclusive;
        SerializedProperty fadeInDuration, fadeOutDuration;
        SerializedProperty glow, glowWidth, glowDithering, glowMagicNumber1, glowMagicNumber2, glowAnimationSpeed, glowSmooth, glowQuality, glowPasses;
        SerializedProperty zoomScale;
        SerializedProperty shadowIntensity, shadowColor, shadowOffset, shadow3D;
        SerializedProperty seeThrough, seeThroughIntensity, seeThroughTintAlpha, seeThroughTintColor, seeThroughNoise;
        Color hitColor = Color.white;
        float hitDuration = 1f;
        float hitMinIntensity = 1f;

        void OnEnable() {
            ignore = serializedObject.FindProperty("ignore");
            previewInEditor = serializedObject.FindProperty("previewInEditor");
			renderingLayer = serializedObject.FindProperty("renderingLayer");
			alwaysOnTop = serializedObject.FindProperty("alwaysOnTop");
			preserveMesh = serializedObject.FindProperty("preserveMesh");
            polygonPacking = serializedObject.FindProperty("polygonPacking");
            highlighted = serializedObject.FindProperty("_highlighted");
            occluder = serializedObject.FindProperty("occluder");
            effectGroup = serializedObject.FindProperty("effectGroup");
            effectGroupLayer = serializedObject.FindProperty("effectGroupLayer");
            overlay = serializedObject.FindProperty("overlay");
            overlayColor = serializedObject.FindProperty("overlayColor");
            overlayAnimationSpeed = serializedObject.FindProperty("overlayAnimationSpeed");
            overlayMinIntensity = serializedObject.FindProperty("overlayMinIntensity");
            overlayBlending = serializedObject.FindProperty("overlayBlending");
            overlayRenderQueue = serializedObject.FindProperty("overlayRenderQueue");
            outline = serializedObject.FindProperty("outline");
            outlineColor = serializedObject.FindProperty("outlineColor");
            outlineWidth = serializedObject.FindProperty("outlineWidth");
            outlineSmooth = serializedObject.FindProperty("outlineSmooth");
            outlineQuality = serializedObject.FindProperty("outlineQuality");
            outlineExclusive = serializedObject.FindProperty("outlineExclusive");
			fadeInDuration = serializedObject.FindProperty("fadeInDuration");
			fadeOutDuration = serializedObject.FindProperty("fadeOutDuration");
			glow = serializedObject.FindProperty("glow");
            glowWidth = serializedObject.FindProperty("glowWidth");
            glowAnimationSpeed = serializedObject.FindProperty("glowAnimationSpeed");
            glowDithering = serializedObject.FindProperty("glowDithering");
            glowMagicNumber1 = serializedObject.FindProperty("glowMagicNumber1");
            glowMagicNumber2 = serializedObject.FindProperty("glowMagicNumber2");
            glowSmooth = serializedObject.FindProperty("glowSmooth");
            glowQuality = serializedObject.FindProperty("glowQuality");
            glowPasses = serializedObject.FindProperty("glowPasses");
            seeThrough = serializedObject.FindProperty("seeThrough");
            seeThroughIntensity = serializedObject.FindProperty("seeThroughIntensity");
            seeThroughTintAlpha = serializedObject.FindProperty("seeThroughTintAlpha");
            seeThroughTintColor = serializedObject.FindProperty("seeThroughTintColor");
            seeThroughNoise = serializedObject.FindProperty("seeThroughNoise");
			pixelSnap = serializedObject.FindProperty("pixelSnap");
            alphaCutOff = serializedObject.FindProperty("alphaCutOff");
            pivotPos = serializedObject.FindProperty("pivotPos");
            autoSize = serializedObject.FindProperty("autoSize");
            center = serializedObject.FindProperty("center");
            scale = serializedObject.FindProperty("scale");
            aspectRatio = serializedObject.FindProperty("aspectRatio");
            zoomScale = serializedObject.FindProperty("zoomScale");
            shadowIntensity = serializedObject.FindProperty("shadowIntensity");
            shadowColor = serializedObject.FindProperty("shadowColor");
            shadowOffset = serializedObject.FindProperty("shadowOffset");
            shadow3D = serializedObject.FindProperty("shadow3D");
        }

        public override void OnInspectorGUI() {
            HighlightEffect2D thisEffect = (HighlightEffect2D)target;
            bool isManager = thisEffect.GetComponent<HighlightManager2D>() != null;
            EditorGUILayout.Separator();
            serializedObject.Update();
            if (isManager) {
                EditorGUILayout.HelpBox("These are default settings for highlighted objects. If the highlighted object already has a Highlight Effect component, those properties will be used.", MessageType.Info);
            } else {
                EditorGUILayout.PropertyField(occluder, new GUIContent("Occluder", "Add depth compatibility to this object making it an occluder to other sprites so see-through works properly. Only needed in this object does not write to z-buffer."));
                if (occluder.boolValue) {
                    EditorGUILayout.HelpBox("Make sure this sprite is in front of other sprites in the Z axis (adjust transform's position Z value).", MessageType.Info);
                } else {
                    EditorGUILayout.PropertyField(previewInEditor, new GUIContent("Preview In Edit Mode"));
                }
			}

			renderingLayer.intValue = EditorGUILayout.LayerField(new GUIContent("Rendering Layer", "The layer used to render the effects. If set to default, effects will be rendered using the gameobject layer."), renderingLayer.intValue);

            if (!occluder.boolValue) {
                EditorGUILayout.PropertyField(alwaysOnTop);
            }

			EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Sprite Options", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(pixelSnap);
            EditorGUILayout.PropertyField(alphaCutOff);
            EditorGUILayout.PropertyField(preserveMesh);
            if (!preserveMesh.boolValue) {
                EditorGUILayout.PropertyField(polygonPacking, new GUIContent("Polygon/SVG"));
                if (!polygonPacking.boolValue) {
                    EditorGUILayout.LabelField("Sprite Pivot", pivotPos.vector2Value.ToString("F4"));
                    EditorGUILayout.PropertyField(autoSize);
                    GUI.enabled = !autoSize.boolValue;
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(scale);
                    EditorGUILayout.PropertyField(aspectRatio);
                    EditorGUILayout.PropertyField(center);
                    EditorGUI.indentLevel--;
                    GUI.enabled = true;
                }
            }
            if (!occluder.boolValue) {
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Highlight Options", EditorStyles.boldLabel);
                EditorGUI.BeginChangeCheck();
                if (!isManager) {
                    if (!occluder.boolValue) {
                        EditorGUILayout.PropertyField(ignore, new GUIContent("Ignore", "This object won't be highlighted."));
                        if (!ignore.boolValue) {
                            EditorGUILayout.PropertyField(highlighted);
                        }
                    }
                }
                if (!ignore.boolValue) {
                    EditorGUILayout.PropertyField(effectGroup, new GUIContent("Include", "Which objects to highlight."));
                    if (effectGroup.intValue == (int)TargetOptions.LayerInScene || effectGroup.intValue == (int)TargetOptions.LayerInChildren) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(effectGroupLayer, new GUIContent("Layer"));
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.PropertyField(outlineExclusive, new GUIContent("Exclusive"));

                    EditorGUILayout.PropertyField(fadeInDuration);
                    EditorGUILayout.PropertyField(fadeOutDuration);

                    EditorGUILayout.Separator();
                    EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.PropertyField(overlay);
                    if (overlay.floatValue > 0) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(overlayColor, new GUIContent("Color"));
                        EditorGUILayout.PropertyField(overlayBlending, new GUIContent("Blending"));
                        EditorGUILayout.PropertyField(overlayMinIntensity, new GUIContent("Min Intensity"));
                        EditorGUILayout.PropertyField(overlayAnimationSpeed, new GUIContent("Animation Speed"));
                        EditorGUILayout.PropertyField(overlayRenderQueue, new GUIContent("Render Queue"));
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.PropertyField(outline);
                    if (outline.floatValue > 0) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(outlineWidth, new GUIContent("Width"));
                        EditorGUILayout.PropertyField(outlineColor, new GUIContent("Color"));
                        EditorGUILayout.PropertyField(outlineSmooth, new GUIContent("Smooth Edges"));
                        EditorGUILayout.PropertyField(outlineQuality, new GUIContent("Quality"));
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.PropertyField(glow);
                    if (glow.floatValue > 0) {
                        EditorGUI.indentLevel++;
                        EditorGUILayout.PropertyField(glowWidth, new GUIContent("Width"));
                        EditorGUILayout.PropertyField(glowAnimationSpeed, new GUIContent("Animation Speed"));
                        EditorGUILayout.PropertyField(glowSmooth, new GUIContent("Smooth Edges"));
                        EditorGUILayout.PropertyField(glowQuality, new GUIContent("Quality"));
                        EditorGUILayout.PropertyField(glowDithering, new GUIContent("Dithering"));
                        if (glowDithering.boolValue) {
                            EditorGUILayout.PropertyField(glowMagicNumber1, new GUIContent("Magic Number 1"));
                            EditorGUILayout.PropertyField(glowMagicNumber2, new GUIContent("Magic Number 2"));
                        }
                        EditorGUILayout.PropertyField(glowPasses, true);
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.PropertyField(zoomScale);
                    EditorGUILayout.EndVertical();
                }

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("See-Through Options", EditorStyles.boldLabel);
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.PropertyField(seeThrough);
                if (seeThrough.intValue != (int)SeeThroughMode.Never) {
                    EditorGUI.indentLevel++;
                    if (isManager && seeThrough.intValue == (int)SeeThroughMode.AlwaysWhenOccluded) {
                        EditorGUILayout.HelpBox("This option is not valid in Manager.\nTo make a sprite always visible add a Highlight Effect component to the sprite and enable this option on the component.", MessageType.Error);
                    }
                    EditorGUILayout.PropertyField(seeThroughIntensity, new GUIContent("Intensity"));
                    EditorGUILayout.PropertyField(seeThroughTintAlpha, new GUIContent("Alpha"));
                    EditorGUILayout.PropertyField(seeThroughTintColor, new GUIContent("Color"));
                    EditorGUILayout.PropertyField(seeThroughNoise, new GUIContent("Noise"));
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndVertical();

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Shadow Options", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(GUI.skin.box);
                EditorGUILayout.PropertyField(shadow3D, new GUIContent("3D Shadow"));
                if (!shadow3D.boolValue) {
                    EditorGUILayout.PropertyField(shadowIntensity, new GUIContent("Intensity"));
                    EditorGUILayout.PropertyField(shadowColor, new GUIContent("Color"));
                    EditorGUILayout.PropertyField(shadowOffset, new GUIContent("Offset"));
                }
                EditorGUILayout.EndVertical();
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Hit FX Sample", EditorStyles.boldLabel);
            if (!Application.isPlaying) {
                EditorGUILayout.HelpBox("Enter Play Mode to test this feature. In your code, call effect.HitFX() method to execute this hit effect.", MessageType.Info);
            } else {
                EditorGUI.indentLevel++;
                hitColor = EditorGUILayout.ColorField(new GUIContent("Color"), hitColor);
                hitDuration = EditorGUILayout.FloatField(new GUIContent("Duration"), hitDuration);
                hitMinIntensity = EditorGUILayout.FloatField(new GUIContent("Min Intensity"), hitMinIntensity);
                if (GUILayout.Button("Execute Hit")) {
                    thisEffect.HitFX(hitColor, hitDuration, hitMinIntensity);
                }
                EditorGUI.indentLevel--;
            }

            if (serializedObject.ApplyModifiedProperties()) {
                foreach (HighlightEffect2D effect in targets) {
                    effect.Refresh();
                }
            }
        }


        [MenuItem("GameObject/Effects/Highlight Plus 2D/Create Manager", false, 10)]
        static void CreateManager(MenuCommand menuCommand) {
            HighlightManager2D manager = Misc.FindObjectOfType<HighlightManager2D>();
            if (manager == null) {
                GameObject managerGO = new GameObject("HighlightPlus2DManager");
                manager = managerGO.AddComponent<HighlightManager2D>();
                // Register root object for undo.
                Undo.RegisterCreatedObjectUndo(manager, "Create Highlight Plus 2D Manager");
            }
            Selection.activeObject = manager;
        }

    }

}