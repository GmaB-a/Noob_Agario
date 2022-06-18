using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Noob_Agario
{
    class SavingSystem
    {
        string path;
        string projectName = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public SavingSystem(string iniPath = null)
        {
            path = new FileInfo(iniPath ?? projectName + ".ini").FullName;
        }

        public string Read(string key, string section = null)
        {
            var stringBuilder = new StringBuilder(255);
            GetPrivateProfileString(section ?? projectName, key, "", stringBuilder, 255, path);
            return stringBuilder.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? projectName, Key, Value, path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? projectName);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? projectName);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }
    }
}