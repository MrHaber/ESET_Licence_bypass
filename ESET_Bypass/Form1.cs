using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESET_Bypass
{
    public partial class Form1 : Form
    {
        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        public Form1()
        {
            if (!IsAdministrator())
            {
                MessageBox.Show(
"Can't access to another files",
"Run program as administrator",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
                Environment.Exit(0);
            }
            InitializeComponent();
        }
        public void copyLF()
        {
            string[] lfDestination = Directory.GetFiles(@"C:\ProgramData\ESET\ESET Security\License", "*.lf", SearchOption.AllDirectories);
            foreach (String file in lfDestination)
            {
                if (Path.GetFileName(file).Equals("license.lf"))
                {
                    try
                    {
                        File.SetAttributes(file, FileAttributes.Normal);
                        File.Delete(file);
                        isLicenced = true;
                    }
                    catch (UnauthorizedAccessException ex)
                    {
                        MessageBox.Show(
       "Disable self-defense module in ESET settings",
       "Unable to delete file",
       MessageBoxButtons.OK,
       MessageBoxIcon.Error,
       MessageBoxDefaultButton.Button1,
       MessageBoxOptions.DefaultDesktopOnly);
                        Environment.Exit(0);
                    }
                }

            }
            try
            {
                File.Copy(@"Resources\license.lf", @"C:\ProgramData\ESET\ESET Security\License\license.lf");
            }
            catch (IOException ex)
            {
                MessageBox.Show(
"Disable ESET file defense",
"File is already exists",
MessageBoxButtons.OK,
MessageBoxIcon.Warning,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
                Environment.Exit(0);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(
"Disable self-defense module in ESET settings",
"Unable to delete file",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
                Environment.Exit(0);
            }
        }
        private bool isLocalDb = false;
        private bool isVersion = false;
        private bool isLicenced = false;
        private void removeDbFile(string[] dbfile)
        {
            foreach (String file in dbfile)
            {
                if (Path.GetFileName(file).Equals("local.db"))
                {
                    try
                    {
                        File.Delete(file);
                        isLocalDb = true;
                    }
                    catch (UnauthorizedAccessException  ex)
                    {
                        MessageBox.Show(
       "Disable self-defense module in ESET settings",
       "Unable to delete file",
       MessageBoxButtons.OK,
       MessageBoxIcon.Error,
       MessageBoxDefaultButton.Button1,
       MessageBoxOptions.DefaultDesktopOnly);
                        Environment.Exit(0);
                    }
                }

            }
        }
        private void error()
        {
            MessageBox.Show(
"ESET Security Module not found, please reinstall ESET Security",
"ESET Module not found",
MessageBoxButtons.OK,
MessageBoxIcon.Error,
MessageBoxDefaultButton.Button1,
MessageBoxOptions.DefaultDesktopOnly);
            Environment.Exit(0);
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(@"C:\ProgramData\ESET\ESET Security", "*.csv", SearchOption.AllDirectories);
            string[] dbfile = Directory.GetFiles(@"C:\ProgramData\ESET\ESET Security", "*.db", SearchOption.AllDirectories);
            if (dbfile.Length >= 1)
            {
               removeDbFile(dbfile);
            }
            if (files.Length >= 1)
            {
                foreach (String file in files)
                {
                    if (Path.GetFileName(file).Equals("versions.csv"))
                    {
                        try
                        {
                            File.SetAttributes(file, FileAttributes.Normal);
                            File.Delete(file);
                            isVersion = true;
                        }
                        catch (UnauthorizedAccessException ex)
                        {

                            MessageBox.Show(
           "Disable self-defense module in ESET settings",
           "Unable to delete file",
           MessageBoxButtons.OK,
           MessageBoxIcon.Error,
           MessageBoxDefaultButton.Button1,
           MessageBoxOptions.DefaultDesktopOnly);
                            Environment.Exit(0);
                        }
                    }

                }
            }
            copyLF();
            if (isLocalDb)
            {
               
                new Exit().Show();
                this.Hide();
            }

        }
    }
}
