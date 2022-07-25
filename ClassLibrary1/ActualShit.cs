using UnityEngine;
using UnityMenuUI;
using System.Reflection;

namespace ClassLibrary1
{
    class ActualShit : MonoBehaviour
    {
        public static Texture2D lineTex;

        Menu playerOptions = new Menu(10, 10, "Player Options");
        Menu miscOptions = new Menu(240, 10, "Misc Options");

        bool GodMode = false;
        bool noHunger = false;
        bool infiniteSprint = false;
        bool alwaysDay = false;
        bool esp = false;

        float playerSpeed = 1;
        FieldInfo movespeed = typeof(PlayerMovement).GetField("maxRunSpeed", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
        {
            Matrix4x4 matrix = GUI.matrix;
            if (!lineTex)
            {
                lineTex = new Texture2D(1, 1);
            }
            Color color2 = GUI.color;
            GUI.color = color;
            float num = Vector3.Angle(pointB - pointA, Vector2.right);
            if (pointA.y > pointB.y)
            {
                num = -num;
            }
            GUIUtility.ScaleAroundPivot(new Vector2((pointB - pointA).magnitude, width), new Vector2(pointA.x, pointA.y + 0.5f));
            GUIUtility.RotateAroundPivot(num, pointA);
            GUI.DrawTexture(new Rect(pointA.x, pointA.y, 1f, 1f), lineTex);
            GUI.matrix = matrix;
            GUI.color = color2;
        }

        void Start()
        {
            playerOptions.m_controls.bottom = 20;
            playerOptions.StartX = 5;
            playerOptions.StartY = 15;

            miscOptions.m_controls.bottom = 20;
            miscOptions.StartX = 5 + playerOptions.Width / 2;
            miscOptions.StartY = 15;
        }

        public void OnGUI()
        {
            GUI.Label(new Rect(Screen.width + Screen.width / 2 + 50, Screen.height / 10, 1000, 1000), "Rimurus Menu: Muck Edition");

            foreach (Mob mob1 in FindObjectsOfType(typeof(Mob)) as Mob[])
            {
                Vector3 pos = Camera.current.WorldToScreenPoint(mob1.transform.position);
                if (pos.z > 0)
                {

                    Vector2 screen;
                    screen.x = (float)Screen.width / 2;
                    screen.y = Screen.height;

                    Vector2 enemypos;
                    enemypos.x = pos.x;
                    enemypos.y = Screen.height - pos.y;

                    var blue = new Color(52.0f / 255.0f, 155.0f / 255.0f, 235.0f / 255.0f, 1.0f); ;
                    if (esp)
                    {
                        GUI.Label(new Rect(pos.x, Screen.height - pos.y, 100, 100), mob1.name.Replace("(Clone)", "").Replace("StoneTitan", "Big Chunk"));
                        DrawLine(screen, enemypos, blue, 1.5f);
                    }
                }
            }

            playerOptions.Start();
            {
                GodMode = playerOptions.Toggle("GodMode", GodMode);
                noHunger = playerOptions.Toggle("No Hunger", noHunger);
                infiniteSprint = playerOptions.Toggle("Inf Sprint", infiniteSprint);
                playerSpeed = playerOptions.Slider_Float("Run Speed", playerSpeed, 100, 30, 0, 100);

            }

            miscOptions.Start();
            {
                alwaysDay = miscOptions.Toggle("Freeze Time", alwaysDay);
                esp = miscOptions.Toggle("Esp", esp);
            }

            if (GodMode)
            {
                PlayerStatus.Instance.maxHp = 9999;
                PlayerStatus.Instance.hp = PlayerStatus.Instance.maxHp;
            }

            if (noHunger)
                PlayerStatus.Instance.hunger = PlayerStatus.Instance.maxHunger;

            if (infiniteSprint)
                PlayerStatus.Instance.stamina = PlayerStatus.Instance.maxStamina;

            movespeed.SetValue(PlayerMovement.Instance, 3500 * playerSpeed);
            
            if (alwaysDay)
                DayCycle.dayDuration = int.MaxValue;

            //trying to figure out free chests
            FieldInfo chest = typeof(ChestManager).GetField("chestId", BindingFlags.Instance | BindingFlags.NonPublic);
            int chestId = (int)chest.GetValue(ChestManager.Instance);
            ChestManager.Instance.UseChest(chestId, false);
        }
    }
}
