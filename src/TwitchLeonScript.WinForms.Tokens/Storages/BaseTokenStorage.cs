using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TwitchLeonScript.WinForms.Tokens.Storages
{
    public abstract class BaseTokenStorage<T>
    {
        protected string? filePath;

        public void Save(T tokens)
        {
            if (string.IsNullOrEmpty(this.filePath))
            {
                throw new ArgumentNullException(nameof(this.filePath));
            }

            var json = JsonSerializer.Serialize(tokens);
            var data = Encoding.UTF8.GetBytes(json);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            var directoryPath = Path.GetDirectoryName(this.filePath);

            if (directoryPath != null)
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(this.filePath, encrypted);
        }

        public T? Load()
        {
            if (string.IsNullOrEmpty(this.filePath))
            {
                throw new ArgumentNullException(nameof(this.filePath));
            }

            if (!File.Exists(this.filePath))
            {
                return default(T);
            }

            try
            {
                var encrypted = File.ReadAllBytes(this.filePath);
                var decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);
                var json = Encoding.UTF8.GetString(decrypted);

                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return default(T);
            }
        }

        public void Clear()
        {
            if (string.IsNullOrEmpty(this.filePath))
            {
                throw new ArgumentNullException(nameof(this.filePath));
            }

            if (File.Exists(this.filePath))
            {
                File.Delete(this.filePath);
            }
        }
    }
}
