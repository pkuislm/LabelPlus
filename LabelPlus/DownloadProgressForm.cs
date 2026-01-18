using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace LabelPlus
{
    public partial class DownloadProgressForm : Form
    {
        public Func<IProgress<DownloadProgress>, Task> Work { get; set; }

        public DownloadProgressForm()
        {
            InitializeComponent();
            Shown += async (_, __) => await RunAsync();
        }

        private async Task RunAsync()
        {
            try
            {
                progressBar.Minimum = 0;
                progressBar.Value = 0;

                var progress = new Progress<DownloadProgress>(p =>
                {
                    progressBar.Maximum = p.Total;
                    progressBar.Value = Math.Min(p.Current, p.Total);
                    label1.Text = p.Message;
                });

                await Work(progress);

                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "失败",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DialogResult = DialogResult.Abort;
            }
            finally
            {
                Close();
            }
        }


    }
    public struct DownloadProgress
    {
        public int Current;
        public int Total;
        public string Message;

        public DownloadProgress(int current, int total, string msg)
        {
            Current = current;
            Total = total;
            Message = msg;
        }
    }
}
