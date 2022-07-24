
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        public string[] paths3 = new string[10000000];
        public string[] paths2 = new string[10000000];
        public string[] paths = new string[100000000];
        public string[] names_arr = new string[100000000];
        int num = 0;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.Text = "Form1";

            names.Location = new System.Drawing.Point(100, 100);
            names.Size = new System.Drawing.Size(400, 400);
            Controls.Add(names);

            del.Location = new System.Drawing.Point(350, 550);
            del.Size = new System.Drawing.Size(150, 50);
            del.Text = "Delete";
            Controls.Add(del);

            start.Click += Start_Click;
            start.Location = new System.Drawing.Point(100, 550);
            start.Size = new System.Drawing.Size(150, 50);
            start.Text = "Start";
            Controls.Add(start);

            list.Location = new System.Drawing.Point(100, 100);
            Controls.Add(list);
        }

        private void Start_Click(object sender, System.EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select a folder";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    watcher_(dlg.SelectedPath);
                }
            }
        }

        public void watcher_(string path)
        {
            paths2 = Directory.GetFiles(path);
            paths3 = Directory.GetDirectories(path);
            for(; num < paths2.Length + paths3.Length; num++)
            {
                if(num < paths2.Length)
                {
                    paths[num] = paths2[num];
                    list.Items.Add(new FileInfo(paths[num]).Name);
                }
                else
                {
                    paths[num] = paths3[num];
                    list.Items.Add(new FileInfo(paths[num]).Name);
                }
            }
            FileSystemWatcher[] watcher = new FileSystemWatcher[100000];
            int s = 0;
            int x = 0, x2 = 0;
            while (true)
            {
                try
                {
                    ++s;
                    add_arr(s);
                }
                catch
                {
                    break;
                }
            }



            while (true)
            {

                try
                {
                    if (System.IO.Directory.Exists(paths[x]))
                    {
                        watcher[x2] = new FileSystemWatcher(paths[x]);

                        watcher[x2].NotifyFilter = NotifyFilters.Attributes
                                             | NotifyFilters.CreationTime
                                             | NotifyFilters.DirectoryName
                                             | NotifyFilters.FileName
                                             | NotifyFilters.LastAccess
                                             | NotifyFilters.LastWrite
                                             | NotifyFilters.Security
                                             | NotifyFilters.Size;

                        watcher[x2].Created += OnChanged;
                        watcher[x2].Deleted += Form1_Deleted;

                        watcher[x2].IncludeSubdirectories = true;
                        watcher[x2].EnableRaisingEvents = true;
                    }
                    x++;
                }
                catch
                {
                    break;
                }
            }
            info();
        }

        private void Form1_Deleted(object sender, FileSystemEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void add_watcher()
        {
            
        }

        public void add_arr(int i)
        {
            int count = list.Items.Count;
            if (System.IO.Directory.Exists(paths[i]))
            {
                paths2 = Directory.GetFiles(paths[i]);
                paths3 = Directory.GetDirectories(paths[i]);
                for (int j = count; j < count + paths2.Length + paths3.Length; j++)
                {
                    if (j < count + paths2.Length)
                    {
                        paths[j] = paths2[j - count];
                        list.Items.Add(new FileInfo(paths[j]).Name);
                    }
                    else
                    {
                        paths[j] = paths3[j - count - paths2.Length];
                        list.Items.Add(new FileInfo(paths[j]).Name);
                    }
                    if(j == count + paths2.Length + paths3.Length - 1)
                    {
                        num = j;
                    }
                }
            }

        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            list.Invoke((MethodInvoker)delegate {
                list.Items.Add(e.Name);
            });

            paths[num++] = e.FullPath;
        }

        public void info()
        {
            int count = list.Items.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if(list.Items[i] == list.Items[j + 1] && j< count - 1)
                    {
                        names.Items.Add(list.Items[i]);
                    }
                }
            }
        }

        ListBox names = new ListBox();
        ListBox list = new ListBox();

        Button start = new Button();
        Button del = new Button();
        #endregion
    }
}

