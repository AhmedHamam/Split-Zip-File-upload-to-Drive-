using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadFiles
{
    public static class GetPath
    {
        public static string? GetUploadFilePath()
        {
            string filter = "";
            string title = "Select  File";
            string message = "Please Select File";
            return GetFilePath(title, filter, message);
        }
        public static string? GetFilePath(string title, string? filters, string message)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = filters;
            ofd.Title = title;
            if (ofd.ShowDialog() == DialogResult.OK)
            { return ofd.FileName; }
            else
            {
                MessageBox.Show(message);
                return null;
            }
        }
    }
}
