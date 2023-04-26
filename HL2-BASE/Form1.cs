using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Runtime.InteropServices;
using Memory;
using HL2_BASE;

namespace HL2_BASE
{
    
    public partial class Form1 : MaterialForm
    {
        [DllImport("User32.dll")]
        public static extern bool GetAsyncKeyState(int key);
        public Form1()
        {
            InitializeComponent();
        }

        public async Task KeyStrokesManager()
        {
            await Task.Run(() =>
            {
                while (true)
                {
                    if (GetAsyncKeyState(0x57))
                    {
                        ResetKeys("W");
                    }
                    if (GetAsyncKeyState(0x53))
                    {
                        ResetKeys("S");
                    }
                    if (GetAsyncKeyState(0x41))
                    {
                        ResetKeys("A");
                    }
                    if (GetAsyncKeyState(0x44))
                    {
                        ResetKeys("D");
                    }
                    if (GetAsyncKeyState(0x20))
                    {
                        ResetKeys("Space");
                    }
                }
            });
        }

        public void ResetKeys(string Key)
        {
            if(Key=="W")
            {
                label1.ForeColor = Color.Red;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
            }
            if (Key == "S")
            {
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.Red;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
            }
            if (Key == "A")
            {
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.Red;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.White;
            }
            if (Key == "D")
            {
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.Red;
                label5.ForeColor = Color.White;
            }
            if (Key == "Space")
            {
                label1.ForeColor = Color.White;
                label2.ForeColor = Color.White;
                label3.ForeColor = Color.White;
                label4.ForeColor = Color.White;
                label5.ForeColor = Color.Red;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            Hacks.Init();
            Hacks.autobh();
            KeyStrokesManager();

            MaterialSkinManager materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;

            // Configure color schema
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Grey600, Primary.Green600,
                Primary.Blue500, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            config.BHOP = !config.BHOP;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}


class config
{
    public static bool BHOP = false;
}

class Hacks
{
    static Mem _mem = new Mem();
    public static void Init() { int id = _mem.GetProcIdFromName("hl2.exe"); if (id < 0) { MessageBox.Show("Half-Life 2 Isn't running"); } else { _mem.OpenProcess(id); } }
    [DllImport("User32.dll")]
    public static extern bool GetAsyncKeyState(int key);
    public static async Task autobh() { 
        await Task.Run(() => 
        { 
            while (true) 
            { 
                if (config.BHOP) 
                { 
                    if (GetAsyncKeyState(0x20)) 
                    { 
                        _mem.WriteMemory(offsets.DwForceJump, "int", "5"); 
                        _mem.WriteMemory(offsets.DwForceJump, "int", "4"); 
                    } 
                } 
            } 
        }); 
    }
    
}

class offsets
{
    public static string DwForceJump = "client.dll+0x04B4D24";
    public static string DwForceAttack = "client.dll+0x04B4D30";
    public static string DwForceAttack2 = "client.dll+0x04B4D3C";
    public static string DwForceAttack3 = "client.dll+0x04B4DCC";
    public static string DwForceDuck = "client.dll+0x04B4D60";
    public static string WallHack = "client.dll+0x0483890";
}
