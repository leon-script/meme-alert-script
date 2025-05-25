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
            ArgumentNullException.ThrowIfNull(FilePath);

            var json = JsonSerializer.Serialize(tokens);
            var data = Encoding.UTF8.GetBytes(json);
            var encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            var directoryPath = Path.GetDirectoryName(FilePath);

            if (directoryPath != null)
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllBytes(FilePath, encrypted);
        }

        public T? Load()
        {
            ArgumentNullException.ThrowIfNull(FilePath);

            if (!File.Exists(FilePath))
            {
                return default;
            }

            try
            {
                var encrypted = File.ReadAllBytes(FilePath);
                var decrypted = ProtectedData.Unprotect(encrypted, null, DataProtectionScope.CurrentUser);
                var json = Encoding.UTF8.GetString(decrypted);

                return JsonSerializer.Deserialize<T>(json);
            }
            catch
            {
                return default;
            }
        }

        public void Clear()
        {
            ArgumentNullException.ThrowIfNull(FilePath);

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
        }
    }
}
