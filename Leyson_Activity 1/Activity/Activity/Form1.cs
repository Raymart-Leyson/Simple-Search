using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Activity
{
    public partial class Form1 : Form
    {
        private int start = -1;
        private int end = -1;
        private int[,] maze = new int[11,11];
        public Stack<int> stack = new Stack<int>();
        public List<int> check = new List<int>();
        public String path;
        public Form1()
        {
            InitializeComponent();
            Start();

        }
        void Start()
        {
            int count = 0;
            int pos = 0;
            for (int i = 0; i < 100; i++)
            {
                flowLayoutPanel1.Controls.Add(btn(i));
                if(count <= 9)
                {
                    maze[count, pos] = 0;
                    count++;
                }
                else
                {
                    count -= 10;
                    pos++;
                    maze[count, pos] = 0;  
                }
            }
            try
            {
                string name = "0";
                foreach (var item in flowLayoutPanel1.Controls.OfType<Button>())
                {
                    if (item.Name == name)
                    {
                        item.BackColor = Color.Green;
                    }
                    if(item.Name == "9" || item.Name == "19" || item.Name == "29" || item.Name == "39" || item.Name == "49" ||
                        item.Name == "59" || item.Name == "69" || item.Name == "79" || item.Name == "89" || item.Name == "99")
                    {
                        item.Visible = false;
                    }
                }
            }
            catch (Exception ex) { }
        }

        Button btn(int i)
        {
            Button b = new Button();
            b.Name = i.ToString();
            b.Padding = new Padding(10, 10, 10, 10);
            b.Width = 30;
            b.Height = 30;
            b.Click += b_Click;
            return b;
        }

        void b_Click(Object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (end == -1)
            {
                end = Int32.Parse(b.Name.ToString());
                maze[getPosition(end)[0], getPosition(end)[1]] = 10;
                b.BackColor = Color.Red;
            }
        }
        public void ChangeColor(string name)
        {
            try
            {
                foreach (var item in flowLayoutPanel1.Controls.OfType<Button>())
                {
                    if (item.Name == name && item.Name != "0")
                    {
                        item.BackColor = Color.YellowGreen;
                    }
                }
            }
            catch (Exception ex) { }
        }

        public void Search()
        {
            Add(start);
            int[] pos;
            while (true)
            {
               int pop = stack.Pop();
                pos = getPosition(pop);
                path += getButtonName(pos) + " => ";
                MessageBox.Show(path);
               if (maze[getPosition(pop)[0], getPosition(pop)[1]] == 0)
               {
                   Add(pop);
                   ChangeColor(getButtonName(getPosition(pop)));
               }
               if (maze[getPosition(pop)[0], getPosition(pop)[1]] == 10)
               {
                  break;
               }
            }
            MessageBox.Show("The Path is Found");
        }

        public void Add(int i)
        {
            int[] pos = getPosition(i);
            int x = pos[0];
            int y = pos[1];

            // Up and Right
            if (pos[0] == 0 && pos[1] == 0)
            {
                pos[1] = y + 1;
                Push(pos);
                pos[0] = x + 1;
                Push(pos);
            }
            // Down and left
            else if (pos[0] == 9 && pos[1] == 9)
            {
                pos[1] = y - 1;
                Push(pos);
                pos[0] = x - 1;
                Push(pos);
            }
            // Up, Dpwn, Right
            else if (pos[0] == 0)
            {
                pos[1] = y - 1;
                Push(pos);
                pos[1] = y + 1;
                Push(pos);
                pos[0] = x + 1;
                Push(pos);
            }
            //  Down, Up , Left
            else if (pos[0] == 9)
            {
                pos[1] = y - 1;
                Push(pos);
                pos[1] = y + 1;
                Push(pos);
                pos[0] = x - 1;
                Push(pos);
            }
            // Up, Left, Right
            else if (pos[1] == 0)
            {
                pos[1] = y + 1;
                Push(pos);
                pos[0] = x - 1;
                Push(pos);
                pos[0] = x + 1;
                Push(pos);
            }
            // Down, Left, Right
            else if (pos[1] == 9)
            {
                pos[1] = y - 1;
                Push(pos);
                pos[0] = x - 1;
                Push(pos);
                pos[0] = x + 1;
                Push(pos);
            }
            // Up, Down, Left, Right
            else
            {
                pos[1] = y - 1;
                Push(pos);
                pos[1] = y + 1;
                Push(pos);
                pos[0] = x - 1;
                Push(pos);
                pos[0] = x + 1;
                Push(pos);
            }
        }
        
        public void Push(int[] i)
        {
            int name = Int32.Parse(getButtonName(i));
            if (check.Contains(name) == false)
            {
                stack.Push(name);
                check.Add(name);
            }
        }

        public int[] getPosition(int i)
        {
            int[] x = new int[2];
            int posY = 0;
            while (true)
            {
                if(i >= 10)
                {
                    i -= 10;
                    posY++;
                }
                if (i <= 9)
                {
                    x[0] = i;
                    x[1] = posY;
                    break;
                }
            }
            return x;
        }

        public string getButtonName(int[] i)
        {
            int name = 0; 
            while (i[1] > 0)
            {
                name += 10;
                i[1]--;
            }
            name += i[0];
            return name.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(end == -1)
            {
                MessageBox.Show("Pls Input End Point");
            }
            else
            {
                Search();
            }
        }
    }
}
