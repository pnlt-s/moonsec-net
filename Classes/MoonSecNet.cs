using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace MoonSec
{
    class MoonSecNet
    {
        string endpoint = "https://api.f3d.at/v1/obfuscate.php";

        string Platform = "Lua";
        string Bytecode = "1";
        string Options = "";
        public string EndFile = "deobfuscated.lua";

        string[] AvOpt = new string[] {
            "StringEncryption",
            "ConstantEncryption",
            "AntiDump",
            "SmallOutput"
        };

        public void UpdateOptions(bool[] options = null)
        {
            if (options == null)
            {
                options = new bool[] { true, true, true, false };
            }
            else
            {
                List<bool> boob = new List<bool>() { false, false, false, false };
                for (int i = 0; i < options.Length; i++)
                {
                    if (options[i])
                    {
                        boob[i] = true;
                    }
                }
            }

            List<string> list = new List<string>();

            Options = "";
            for (int i = 0; i < options.Length; i++)
            {
                if (options[i])
                {
                    list.Add(AvOpt[i]);
                }
            }

            Options = string.Join('+', list.ToArray());
        }

        public void UpdateBytecode(Bytecode bc = MoonSec.Bytecode.Symbols1)
        {
            Bytecode = ((int)bc).ToString();
        }
        public void UpdatePlatform(Platform pl = MoonSec.Platform.Lua)
        {
            Platform = PlatformCollection.GetPlatform(pl).type;
        }
        public void UpdateTarget(string file)
        {
            var f = file.Split('.');
            f[0] = f[0] + "-obfuscated";
            EndFile = string.Join('.', f);
        }

        public async void Obfuscate(string code)
        {
            var settings = new List<string>();

            settings.Add(endpoint);
            settings.Add(string.Format("options={0}", Options));
            settings.Add(string.Format("platform={0}", Platform));
            settings.Add(string.Format("bytecode={0}", Bytecode));

            var req = WebRequest.CreateHttp(string.Join("&", settings.ToArray()));
            req.Method = "POST";
            req.ContentType = "text/html charset=utf-8;";

            using (StreamWriter sw = new StreamWriter(req.GetRequestStream()))
            {
                sw.Write(code);
            }

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Lua files (*.lua)|*.lua|Text Files|*.txt|All Files|*.*";
                sfd.FileName = "";
                sfd.Title = "Select file to save result to";
                if (sfd.ShowDialog() == true)
                {
                    EndFile = sfd.FileName;
                }
                var resp = await req.GetResponseAsync();
                using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                {
                    using (StreamWriter sw = new StreamWriter(EndFile))
                    {
                        sw.WriteLine(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception e)
            {
                StreamWriter sw = new StreamWriter("errors.log", true);
                sw.WriteLine(string.Format("{0} in {1}", e.Message, e.StackTrace));
                sw.Close();
            }   
        }

        public MoonSecNet(string api)
        {
            endpoint += "?key=" + api;

            UpdateBytecode();
            UpdateOptions();
            UpdatePlatform();
        }
    }
}
