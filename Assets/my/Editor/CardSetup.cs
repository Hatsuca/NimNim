using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CardSetup : EditorWindow {

	public GameObject cardStorage;
	public Material frontMat_7;
	public Material frontMat_5;
	public Material frontMat_3;
	public Material frontMat_2;
	public Material frontMat_1;
	private Color textColor_7 = new Color(1.0f, 1.0f, 1.0f);
	private Color textColor_5 = new Color(1.0f, 1.0f, 1.0f);
	private Color textColor_3 = new Color(1.0f, 1.0f, 1.0f);
	private Color textColor_2 = new Color(1.0f, 1.0f, 1.0f);
	private Color textColor_1 = new Color(1.0f, 1.0f, 1.0f);

	[MenuItem("Editor/CardSetup")]
	static void Open() {
		GetWindow<CardSetup> ("CardSetup");
	}

	void OnGUI() {
		
		EditorGUILayout.Space ();

		using (new EditorGUILayout.HorizontalScope ()) {
			EditorGUILayout.LabelField ("CardStorage");
			cardStorage = (GameObject)EditorGUILayout.ObjectField (cardStorage, typeof(GameObject), true);
		}

		EditorGUILayout.Space ();

		EditorGUILayout.LabelField ("FrontMaterials & Color");
		EditorGUI.indentLevel++;
		using (new EditorGUILayout.HorizontalScope ()) {
			EditorGUILayout.LabelField ("7Cows");
			frontMat_7 = (Material)EditorGUILayout.ObjectField (frontMat_7, typeof(Material), false);
			textColor_7 = EditorGUILayout.ColorField(textColor_7);
		}

		using (new EditorGUILayout.HorizontalScope ()) {
			EditorGUILayout.LabelField ("5Cows");
			frontMat_5 = (Material)EditorGUILayout.ObjectField (frontMat_5, typeof(Material), false);
			textColor_5 = EditorGUILayout.ColorField (textColor_5);
		}

		using (new EditorGUILayout.HorizontalScope ()) {
			EditorGUILayout.LabelField ("3Cows");
			frontMat_3 = (Material)EditorGUILayout.ObjectField (frontMat_3, typeof(Material), false);
			textColor_3 = EditorGUILayout.ColorField (textColor_3);
		}

		using (new EditorGUILayout.HorizontalScope ()) {
			EditorGUILayout.LabelField ("2Cows");
			frontMat_2 = (Material)EditorGUILayout.ObjectField (frontMat_2, typeof(Material), false);
			textColor_2 = EditorGUILayout.ColorField (textColor_2);
		}

		using (new EditorGUILayout.HorizontalScope ()) {
			EditorGUILayout.LabelField ("1Cows");
			frontMat_1 = (Material)EditorGUILayout.ObjectField (frontMat_1, typeof(Material), false);
			textColor_1 = EditorGUILayout.ColorField (textColor_1);
		}
		EditorGUI.indentLevel--;

		EditorGUILayout.Space ();

		if (GUILayout.Button ("処理開始")) {
			if (cardStorage != null) {
				Processing();
			}else {
				Debug.Log("error");
			}
		}
	}

	void Processing() {
		for (int i = 0; i < cardStorage.transform.childCount; i++) {
			
			var card = cardStorage.transform.GetChild (i);

			Material frontMat;
			Color textCol;

			var num = i + 1;

			if (num == 55) {
				frontMat = frontMat_7;
				textCol = textColor_7;
			} else if (num % 11 == 0) {
				frontMat = frontMat_5;
				textCol = textColor_5;
			} else if (num % 10 == 0) {
				frontMat = frontMat_3;
				textCol = textColor_3;
			} else if (num % 5 == 0) {
				frontMat = frontMat_2;
				textCol = textColor_2;
			} else {
				frontMat = frontMat_1;
				textCol = textColor_1;
			}

			//Position
			var s_transform = new SerializedObject(card);
			s_transform.Update ();
			s_transform.FindProperty ("m_LocalPosition").vector3Value = new Vector3(i, 0, 0);
			s_transform.ApplyModifiedProperties ();

			//Text
			var s_text = new SerializedObject(card.GetComponentInChildren<Text>());
			s_text.Update ();
			s_text.FindProperty ("m_Text").stringValue = num.ToString();
			s_text.FindProperty ("m_Color").colorValue = textCol;
			s_text.ApplyModifiedProperties ();


			//Material
			MeshRenderer meshRenderer;
			foreach (MeshRenderer m in card.GetComponentsInChildren<MeshRenderer>()) {
				if (m.name == "Front") {
					var s_mat = new SerializedObject(m);
					s_mat.Update ();
					s_mat.FindProperty ("m_Materials").GetArrayElementAtIndex (0).objectReferenceValue = frontMat;
					s_mat.ApplyModifiedProperties ();
					break;
				}
			}

		}

		Debug.Log ("処理完了");
	}
}
