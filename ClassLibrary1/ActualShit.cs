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

        public void OnGUI()
        {
            //renderer
            GUIStyle style = new GUIStyle
            {
                fontSize = 20
            };

            GUI.Label(new Rect(50, 100, 1000, 1000), "Rimurus Menu: Muck Edition", style);
            GUI.Label(new Rect(50, 500, 1000, 1000), "Controls:\nP: Spawn Cow\nG: Max Health\nS: Spawn Random Boss", style);

            foreach (Mob mob1 in FindObjectsOfType(typeof(Mob)) as Mob[])
            {
                Vector3 pos = Camera.current.WorldToScreenPoint(mob1.transform.position);

                if (pos.z > 0)
                {
                    GUI.Label(new Rect(pos.x, (float)Screen.height - pos.y, 100, 100), mob1.name.Replace("(Clone)", "").Replace("StoneTitan", "Big Chunk"));
                    mob1.mobType.speed = 1000;
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

            if (Input.GetKeyDown(KeyCode.S))
            {
               var rand = new System.Random();
               int num = rand.Next(0, 3);

                GameLoop.Instance.bosses[num].behaviour = MobType.MobBehaviour.Neutral;
                GameLoop.Instance.StartBoss(GameLoop.Instance.bosses[num]);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                PlayerStatus.Instance.stamina = 9999;
                PlayerStatus.Instance.hunger = 9999;
                PlayerStatus.Instance.maxHp = 9999;
                PlayerStatus.Instance.hp = PlayerStatus.Instance.maxHp;
            }       

            if (Input.GetKeyDown(KeyCode.P))          
                MobSpawner.Instance.SpawnMob(PlayerStatus.Instance.transform.position, 0, 0, 1, 1);        
        }
    }
}
