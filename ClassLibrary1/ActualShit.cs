using System.Runtime.InteropServices;
using UnityEngine;

namespace ClassLibrary1
{
    class ActualShit : MonoBehaviour
    {
        public static Texture2D lineTex;
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

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        public void Start()
        {
            AllocConsole();
        }

        bool boss = false;
        public void OnGUI()
        {
            //renderer
            GUIStyle style = new GUIStyle
            {
                fontSize = 25
            };

            GUI.Label(new Rect(100, 100, 1000, 1000), "Rimuru", style);

            foreach (Mob mob1 in Object.FindObjectsOfType(typeof(Mob)) as Mob[])
            {
                Vector3 pos = Camera.current.WorldToScreenPoint(mob1.transform.position);

                if (pos.z > 0)
                {
                    GUI.Label(new Rect(pos.x, (float)Screen.height - pos.y, 100, 100), mob1.name.Replace("(Clone)", "").Replace("StoneTitan", "Big Chunk"));

                    Vector2 screen;
                    screen.x = (float)Screen.width / 2;
                    screen.y = (float)Screen.height;

                    Vector2 enemypos;
                    enemypos.x = pos.x;
                    enemypos.y = (float)Screen.height - pos.y;

                    var blue = new Color(52.0f / 255.0f, 155.0f / 255.0f, 235.0f / 255.0f, 1.0f); ;
                    DrawLine(screen, enemypos, blue, 1.5f);
                }
            }

            PlayerStatus.Instance.stamina = 9999;
            PlayerStatus.Instance.hunger = 9999;
            PlayerStatus.Instance.maxHp = 9999;
            PlayerStatus.Instance.hp = PlayerStatus.Instance.maxHp;

            foreach (InventoryItem chest in ItemManager.Instance.allItems.Values)
            {
                System.Console.WriteLine(chest.name);
            }

            if (!boss)
            {
                GameLoop.Instance.bosses[0].behaviour = MobType.MobBehaviour.Neutral;
                GameLoop.Instance.bosses[1].behaviour = MobType.MobBehaviour.Neutral;
                GameLoop.Instance.bosses[2].behaviour = MobType.MobBehaviour.Neutral;
                GameLoop.Instance.bosses[3].behaviour = MobType.MobBehaviour.Neutral;
                //GameLoop.Instance.bosses[4].behaviour = MobType.MobBehaviour.Neutral;
                GameLoop.Instance.StartBoss(GameLoop.Instance.bosses[0]);
                GameLoop.Instance.StartBoss(GameLoop.Instance.bosses[1]);
                GameLoop.Instance.StartBoss(GameLoop.Instance.bosses[2]);
                GameLoop.Instance.StartBoss(GameLoop.Instance.bosses[3]);
                //GameLoop.Instance.StartBoss(GameLoop.Instance.bosses[4]);

                boss = true;
            }
          
            if (Input.GetKeyDown(KeyCode.P))
                MobSpawner.Instance.SpawnMob(PlayerStatus.Instance.transform.position, 0, 0, 1, 1);
        }
    }
}
