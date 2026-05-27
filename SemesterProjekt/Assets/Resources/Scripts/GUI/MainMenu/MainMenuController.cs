using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace GUIManagement
{
    public class MainMenuController : MonoBehaviour, IPointerEvent
    {
        [Header("Reference")] [SerializeField] private InputSystemUIInputModule inputModule;

        private List<GameObject> _gameMenusArr;
        private List<Button> _menuButtonsArrList;
        


        private void Start()
        {
            _gameMenusArr = new List<GameObject>(GameObject.FindGameObjectsWithTag("MenuType"));
            _menuButtonsArrList = new List<Button>();
            
            SetStartMenuToDefault();
        }

        /// <summary>
        /// Sets all menus that is not main menu to inactive
        /// </summary>
        private void SetStartMenuToDefault()
        {
            foreach (var menu in _gameMenusArr)
            {
                SetMenuActive(menu, menu.name == "MainMenu");
            }
        }

        /// <summary>
        /// This methods is used to set a menu active. <br />
        /// Used in SetStartMenuToDefault. <br />
        /// It also puts all the button children into the button array.
        /// </summary>
        /// <param name="gobj">The gameobject to perform actions on</param>
        /// <param name="activiity">The activity on witch the gameobject must be</param>
        private void SetMenuActive(GameObject gobj, bool activiity)
        {
            _menuButtonsArrList.Clear();
            if (activiity)
            {
                var sb = new StringBuilder();
                gobj.SetActive(true);
                foreach (Transform child in gobj.transform)
                {
                    if (child.name is "Text" || child.name is "Image")
                    {
                        continue;
                    }

                    _menuButtonsArrList.Add(child.GetComponent<Button>());
                    sb.Append(child.name).Append(" | ");
                }

                Debug.Log(sb.ToString());
                foreach (var buttonInMenu in _menuButtonsArrList)
                {

                    buttonInMenu.onClick.AddListener(delegate { ButtonClicked(buttonInMenu.gameObject); });

                    buttonInMenu.gameObject.AddComponent<GUIAnimationBehaviour>();
                }
            }

            gobj.SetActive(activiity);
        }

        /// <summary>
        /// Checks for the button clicked. <br />
        /// Based on what menu is active.
        /// </summary>
        /// <param name="gobj">Gameobject to look for</param>
        private void ButtonClicked(GameObject gobj)
        {
            foreach (var menu in _gameMenusArr)
            {
                if (menu.activeSelf)
                {
                    switch (menu.name)
                    {
                        case "MainMenu":
                            MainMenuButtons(gobj);
                            break;

                        case "LevelMenu":
                            LevelSelectionButtons(gobj);
                            break;

                        case "OptionsMenu":
                            Debug.Log(gobj.name);
                            break;
                        default:
                            Debug.Log("this is the default");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// The logic on the main menu buttons
        /// </summary>
        /// <param name="gobj">Object to perform action on</param>
        private void MainMenuButtons(Object gobj)
        {
            switch (gobj.name)
            {
                case "PlayButton":
                    SetMenuActive(SearchGameMenuArr("MainMenu"), false);
                    SetMenuActive(SearchGameMenuArr("LevelMenu"), true);


                    break;
                case "OptionButton":
                    SetMenuActive(SearchGameMenuArr("MainMenu"), false);
                    SetMenuActive(SearchGameMenuArr("OptionsMenu"), true);
                    break;

                case "ExitButton":
                    Application.Quit();
                    Debug.Log("Game exited");
                    break;
            }
        }

        /// <summary>
        /// The logic on the Level menu buttons.
        /// based on the button pressed it loads the corresponding scene
        /// </summary>
        /// <param name="gobj">Object to perform action on</param>
        private void LevelSelectionButtons(Object gobj)
        {
            if (gobj.name == "BackButton")
            {
                SetStartMenuToDefault();
            }
            else
            {
                foreach (var button in _menuButtonsArrList.Where(button => button.name == gobj.name))
                {
                    StartCoroutine(GUIManager.Instance.LoadLevel(gobj));
                }
            }
        }

        /// <summary>
        /// Searches the game menu array for the corresponding object.
        /// </summary>
        /// <param name="menuToSeachFor"></param>
        /// <returns>The gameobject matching the menuToSearchFor string</returns>
        private GameObject SearchGameMenuArr(string menuToSeachFor)
        {
            var result = _gameMenusArr.FirstOrDefault(item => item.name == menuToSeachFor);
            return result;
        }


        
        
        


        public int pointerId { get; }
        public string pointerType { get; }
        public bool isPrimary { get; }
        public int button { get; }
        public int pressedButtons { get; }
        public Vector3 position { get; }
        public Vector3 localPosition { get; }
        public Vector3 deltaPosition { get; }
        public float deltaTime { get; }
        public int clickCount { get; }
        public float pressure { get; }
        public float tangentialPressure { get; }
        public float altitudeAngle { get; }
        public float azimuthAngle { get; }
        public float twist { get; }
        public Vector2 radius { get; }
        public Vector2 radiusVariance { get; }
        public EventModifiers modifiers { get; }
        public bool shiftKey { get; }
        public bool ctrlKey { get; }
        public bool commandKey { get; }
        public bool altKey { get; }
        public bool actionKey { get; }
    }
}