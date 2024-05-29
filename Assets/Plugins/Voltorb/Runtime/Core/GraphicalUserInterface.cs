using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Voltorb
{
	public abstract class GraphicalUserInterface : MonoBehaviour
	{
        protected static readonly Dictionary<string, Menu> s_SceneGraphicReferences = new Dictionary<string, Menu>();

        private void Awake()
        {
            BindSceneGraphicReferences();
        }

        protected abstract void BindSceneGraphicReferences();

        protected void Add<T>(T entry, string name = null) where T : Menu
        {
            string key = name ?? typeof(T).Name;

            s_SceneGraphicReferences.Add(key, entry);
        }

        protected void Remove<T>(string name = null) where T : Menu
        {
            string key = name ?? typeof(T).Name;

            s_SceneGraphicReferences.Remove(key);
        }

        public static void Show<T>(string name = null) where T : Menu
        {
            string key = name ?? typeof(T).Name;

            s_SceneGraphicReferences[key].gameObject.SetActive(true);
        }

        public static IEnumerator ShowAsync<T>(string name = null) where T : Menu
        {
            string key = name ?? typeof(T).Name;

            yield return s_SceneGraphicReferences[key].Show();
        }

        public static void ShowAll()
        {
            foreach (var menu in s_SceneGraphicReferences.Values)
            {
                menu.gameObject.SetActive(true);
            }
        }

        public static void Hide<T>(string name = null) where T : Menu
        {
            string key = name ?? typeof(T).Name;

            s_SceneGraphicReferences[key].gameObject.SetActive(false);
        }

        public static IEnumerator HideAsync<T>(string name = null) where T : Menu
        {
            string key = name ?? typeof(T).Name;

            yield return s_SceneGraphicReferences[key].Hide();
        }

        public static void HideAll()
        {
            foreach (var menu in s_SceneGraphicReferences.Values)
            {
                menu.gameObject.SetActive(false);
            }
        }

        public static void SetProperties<T>(string key, T properties) where T : struct
        {
            ((Menu<T>)s_SceneGraphicReferences[key]).SetProperties(properties);
        }

        private void OnDestroy()
        {
            s_SceneGraphicReferences.Clear();
        }
    }
}
