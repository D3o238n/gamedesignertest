//Скрипт для загрузки сцен
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Sych_scripts
{
    [AddComponentMenu("Sych scripts / General / Load scene")]
    [DisallowMultipleComponent]
    public class Load_scene : MonoBehaviour
    {

        #region Переменные
        enum Type_level_loading
        {
            [InspectorName(displayName: ("Моментальная загрузка"))]
            Instant,
            [InspectorName(displayName: ("Через показ загрузочного меню"))]
            Progressbar
        }

        [Header("Тип загрузки")]
        [SerializeField]
        private Type_level_loading Type = Type_level_loading.Instant;

        [Header("Номер сцены которую нужно загрузить")]
        int Scene_number = 0;

        [Header("Имя сцены которую нужно загрузить")]
        [Tooltip("Если пусто, то загрузка через номер")]
        [SerializeField]
        string Scene_name = null;

        [Tooltip("Чёрный фон (затемнение перед загрузкой игры) (не обязательно)")]
        [SerializeField]
        Image Black_fon = null;

        [Tooltip("Плавность затемнения (или наоборот)")]
        [SerializeField]
        float Speed_blackout = 1f;

        [Tooltip("Убрать затемнение при старте сцены?")]
        [SerializeField]
        bool Start_reverse_blackout = true;

        Coroutine Blackout_coroutine = null;

        #endregion


        #region Методы Callback
        private void Start()
        {
            if(Start_reverse_blackout)
            if (Blackout_coroutine == null)
                Blackout_coroutine = StartCoroutine(Coroutine_Blackout(false));
        }
        #endregion


        #region Методы
        void Load()
        {
            //Мгновенный вариант загрузки
            if (Type == Type_level_loading.Instant)
            {
                if (Scene_name == "")
                {
                    if (SceneManager.sceneCountInBuildSettings > Scene_number)
                        SceneManager.LoadScene(Scene_number);
                    else
                        Debug.LogError("Сцены для загрузки под номером ( " + Scene_number + " ) не существует!");
                }
                else
                {
                    SceneManager.LoadScene(Scene_name);

                    /* //Проверка не работает, нужно подумать и придумать рабочую проверку по имени сцены
                    bool result = false;
                    
                    for (int x = 0; x < SceneManager.sceneCountInBuildSettings; x++)
                    {
                        print(SceneManager.GetSceneByBuildIndex(x).path);
                        if (SceneManager.GetSceneByBuildIndex(x).name == Scene_name)
                        {
                            
                            result = true;
                            break;
                        }

                    }

                    if (result)
                        SceneManager.LoadScene(Scene_name);
                    else
                        Debug.LogError("Сцены для загрузки под именем ( " + Scene_name + " ) не существует!");
                    */
                }

            }

            //Вариант загрузки через загрузочное меню
            else if (Type == Type_level_loading.Progressbar)
            {
                if (Scene_name == "")
                {
                    PlayerPrefs.SetInt("Load_scene_ID", Scene_number);
                    PlayerPrefs.SetString("Load_scene_name", "");
                }
                else
                    PlayerPrefs.SetString("Load_scene_name", Scene_name);

                PlayerPrefs.Save();

                SceneManager.LoadScene("Loading");
            }
        }

        /// <summary>
        /// Затемнение экрана
        /// </summary>
        /// <param name="_normal">Обычный режим затемнение, в противномслучае будет убирать затемнение</param>
        /// <returns></returns>
        IEnumerator Coroutine_Blackout(bool _normal)
        {
            bool active_bool = true;

            Color color_default = Black_fon.color;

            float Color_alpha = 0;

            if(!_normal)
                Color_alpha = 1;

            while (active_bool)
            {
                if (_normal)
                {
                    Color_alpha += Speed_blackout * Time.fixedDeltaTime;

                    if (Color_alpha >= 1)
                    {
                        active_bool = false;
                        Load();
                    }
                }
                else
                {
                    Color_alpha -= Speed_blackout * Time.fixedDeltaTime;

                    if (Color_alpha <= 0)
                    {
                        active_bool = false;
                    }
                }


                Black_fon.color = new Color(color_default.r, color_default.g, color_default.b, Color_alpha);

                yield return null;
            }

            Blackout_coroutine = null;
        }
        #endregion


        #region Публичные методы

        /// <summary>
        /// Перезагрузить сцену (уровень)
        /// </summary>
        public void Restart_scene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Загрузить
        /// </summary>
        public void Load_start()
        {
            if (Black_fon != null)
            {
                if (Blackout_coroutine == null)
                Blackout_coroutine = StartCoroutine(Coroutine_Blackout(true));
            }
            else
            {
                Load();
            }
        }

        public void Load_start(int _id_scene)
        {
            Scene_number = _id_scene;

            Load_start();
        }

        public void Load_start(string _name_scene)
        {
            Scene_name = _name_scene;

            Load_start();
        }

        /// <summary>
        /// Загрузить следующую сцену
        /// </summary>
        public void Next_scene()
        {
            int id_scene = SceneManager.GetActiveScene().buildIndex + 1;

            if (SceneManager.sceneCountInBuildSettings > id_scene)
            {
                Load_start(id_scene);
            }
            else
            {
                //Debug.Log("Сцены закончились, возвращаемся в главное меню.");
                //Load_main_menu();
                Load_start(0);//Начинаем с нулевой сценой
            }

        }

        /// <summary>
        /// Загрузить предыдущую сцену
        /// </summary>
        public void Back_scene()
        {
            int id_scene = SceneManager.GetActiveScene().buildIndex - 1;

            if (0 < id_scene)
            {
                Load_start(id_scene);
            }
            else
            {
                //Debug.Log("Сцены закончились, возвращаемся в главное меню.");
                //Load_main_menu();
                Load_start(0);//Начинаем с нулевой сценой
            }

        }


        public void Exit_game()
        {
            Application.Quit();
        }
        #endregion

    }
}