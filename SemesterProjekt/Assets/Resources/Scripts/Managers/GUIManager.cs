using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace GUIManagement
{
    public class GUIManager
    {
        private Animator Transition;
        
        private GUIManager()
        {
            CurrentScene = SceneManager.GetActiveScene();
        }

        private static GUIManager _instance;
        // private AsyncOperation _async;

        public static GUIManager Instance => _instance ?? (_instance = new GUIManager());

        private Scene CurrentScene { get; set; }

        /// <summary>
        /// Checks if its the first scene in the SceneManager
        /// </summary>
        /// <returns>Bool</returns>
        public bool CheckIfStartScreen()
        {
            return CurrentScene.buildIndex==0;
        }

        public void setAnimator(Animator transition)
        {
            Transition = transition;
        }

        public void ChangeScene(Object gameObject)
        {
            SceneManager.LoadScene(gameObject.name);
        }
        public void ChangeScene(int i)
        {
            SceneManager.LoadScene(i);
        }

        public IEnumerator LoadLevel(Object gameObject)
        {
            Transition.SetTrigger("AnimationStart");

            yield return new WaitForSeconds(2);
            
            ChangeScene(gameObject);

        }public IEnumerator LoadLevel(int i)
        {
            Transition.SetTrigger("AnimationStart");

            yield return new WaitForSeconds(2);
            
            ChangeScene(i);

        }



    }
}