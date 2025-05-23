using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace TwitchLeonScript.UI.Tokens.Storages
{
    public abstract class BaseTokenStorage<T>
    {
        protected string? FilePath;

        public void Save(T tokens)
        {
            if (string.IsNullOrEmpty(this.FilePath))
            {
                throw new ArgumentNullException(nameof(this.FilePath));
            }

            var json = JsonSerializer.Serialize(tokens);
            var data = Encoding.UTF8.GetBytes(json);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            var directoryPath = Path.GetDirectoryName(this.FilePath);

            if (directoryPath != null)
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(this.FilePath, encrypted);
        }

        public T? Load()
        {
            if (string.IsNullOrEmpty(this.FilePath))
            {
                throw new ArgumentNullException(nameof(this.FilePath));
            }

            if (!File.Exists(this.FilePath))
            {
                return default(T);
            }

            try
            {
                var encrypted = File.ReadAllBytes(this.FilePath);
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
            if (string.IsNullOrEmpty(this.FilePath))
            {
                throw new ArgumentNullException(nameof(this.FilePath));
            }

            if (File.Exists(this.FilePath))
            {
                File.Delete(this.FilePath);
            }
        }
    }
}
