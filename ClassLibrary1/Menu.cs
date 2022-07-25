using System;
using UnityEngine;

namespace UnityMenuUI
{
    public struct Margin
    {
        public int left;
        public int top;
        public int bottom;
    }

    public class Menu
    {
        public int x, y;
        private int width = 0, height = 0;
        public bool Visible = true;

        string Name;
        public Margin m_controls = new Margin();


        private int next_controlY = 0;

        public int StartX = 0;
        public int StartY = 0;

        public int Width { get => width; }
        public int Height { get => height; }

        public Menu(int x, int y, string name)
        {
            this.x = x;
            this.y = y;
            this.Name = name;
        }

        public void Start()
        {
            height = next_controlY;
            next_controlY = 0;
            GUI.Box(new Rect(x, y, width + StartX, height + StartY), Name);
        }

        public bool Button(string bt_name, int bt_width, int bt_height)
        {
            if (!Visible)
                return false;

            bool result = GUI.Button(new Rect(StartX + x, StartY + next_controlY + m_controls.top + y, bt_width, bt_height), bt_name);

            next_controlY += bt_height + m_controls.bottom;

            if (width < bt_width)
            {
                width = bt_width;
            }

            return result;
        }

        public void Label(string l_name)
        {

            if (!Visible)
                return;
            GUI.Label(new Rect(StartX + x, StartY + next_controlY + m_controls.top + y, 200, 30), l_name);

            next_controlY += 9 + m_controls.bottom;

            if (width < 200)
            {
                width = 200;
            }
        }


        public bool Toggle(string cb_name, bool value)
        {

            if (!Visible)
                return value;
            bool reusult = GUI.Toggle(new Rect(StartX + x, StartY + next_controlY + m_controls.top + y, 200, 30), value, cb_name);

            next_controlY += 13 + m_controls.bottom;

            if (width < 200)
            {
                width = 200;
            }

            return reusult;
        }

        public float Slider_Float(string name, float value, int s_witdh, int s_height, float min, float max)
        {

            if (!Visible)
                return value;

            string l_text = name + " : " + Math.Round(value, 2);
            GUI.Label(new Rect(StartX + x + m_controls.left, StartY + next_controlY + m_controls.top + y, s_witdh + 20, s_height), l_text);

            float result = GUI.HorizontalSlider(new Rect(StartX + x + m_controls.left, StartY + next_controlY + m_controls.top + 16 + y, s_witdh, s_height), value, min, max);

            next_controlY += 12 + m_controls.bottom + 16;


            if (width < s_witdh)
            {
                width = s_witdh;
            }

            return result;
        }

        public int NumericUpDown(string name, int value, int n_width, int n_height)
        {
            int button_size = 20;

            GUI.Label(new Rect(StartX + x + m_controls.left, StartY + next_controlY + m_controls.top + y, n_width, n_height), name);

            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width, StartY + next_controlY + m_controls.top + y, button_size, button_size), "+"))
            {
                value++;
            }

            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width + button_size, StartY + next_controlY + m_controls.top + y, button_size, button_size), "-"))
            {
                value--;
            }


            next_controlY += 12 + m_controls.bottom;

            return value;
        }

        public int NumericUpDown2(string name, int value, int n_width, int n_height) //have double button
        {
            int button_size = 20;

            GUI.Label(new Rect(StartX + x + m_controls.left, StartY + next_controlY + m_controls.top + y, n_width, n_height), name);

            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width, StartY + next_controlY + m_controls.top + y, button_size, button_size), "+"))
            {
                value++;
            }

            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width + button_size, StartY + next_controlY + m_controls.top + y, button_size, button_size), "-"))
            {
                value--;
            }

            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width + button_size * 2, StartY + next_controlY + m_controls.top + y, 25, button_size), "2x"))
            {
                value *= 2;
            }
            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width + button_size * 2 + 25, StartY + next_controlY + m_controls.top + y, 25, button_size), "2/"))
            {
                value /= 2;
            }



            next_controlY += 12 + m_controls.bottom;


            if (width < n_width + (button_size * 2) + (25 * 2) + 5)
            {
                width = n_width + (button_size * 2) + (25 * 2) + 5;
            }

            return value;
        }


        public bool NumericUpDown3(string name, ref int value, int n_width, int n_height) //have double button
        {
            int button_size = 20;

            GUI.Label(new Rect(StartX + x + m_controls.left, StartY + next_controlY + m_controls.top + y, n_width, n_height), name);


            if (width < n_width + (button_size * 2) + (25 * 2) + 5)
            {
                width = n_width + (button_size * 2) + (25 * 2) + 5;
            }



            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width, StartY + next_controlY + m_controls.top + y, button_size, button_size), "+"))
            {
                value++;
                next_controlY += 12 + m_controls.bottom;
                return true;
            }

            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width + button_size, StartY + next_controlY + m_controls.top + y, button_size, button_size), "-"))
            {
                value--;
                next_controlY += 12 + m_controls.bottom;
                return true;
            }

            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width + button_size * 2, StartY + next_controlY + m_controls.top + y, 25, button_size), "2x"))
            {
                value *= 2;
                next_controlY += 12 + m_controls.bottom;
                return true;
            }
            if (GUI.Button(new Rect(StartX + x + m_controls.left + n_width + button_size * 2 + 25, StartY + next_controlY + m_controls.top + y, 25, button_size), "2/"))
            {
                value /= 2;
                next_controlY += 12 + m_controls.bottom;
                return true;
            }
            next_controlY += 12 + m_controls.bottom;
            return false;
        }


    }
}
