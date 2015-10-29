using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Sdl.StudioServer.Api.Core;

namespace CsvAuthenticationProvider
{
    [CustomAuthenticationProviderExtention(CanValidateUserExistance = true)]
    public class CsvAuthenticationProvider: ICustomAuthenticationProvider
    {
        private readonly Dictionary<string, string> _users;

        public CsvAuthenticationProvider()
        {
            _users =  new Dictionary<string, string>();
            this.LoadUsers();
        }

        private void LoadUsers()
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream("CsvAuthenticationProvider.Resources.users.csv"))
                if (stream != null)
                    using (var reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = reader.ReadLine();
                            if (line == null) continue;
                            var strArrays = line.Split(',');
                            if (_users.ContainsKey(strArrays[0]))
                            {
                                continue;
                            }
                            _users.Add(strArrays[0].ToLower(), strArrays[1]);
                        }
                    }
        }

        public bool UserExists(string userName)
        {
            return _users.ContainsKey(userName.ToLower());
        }

        public bool ValidateCredentials(string userName, string password)
        {
            if (!_users.ContainsKey(userName))
            {
                return false;
            }
            string savedPassword;
            _users.TryGetValue(userName.ToLower(), out savedPassword);
            return savedPassword == password;
        }
    }
}
