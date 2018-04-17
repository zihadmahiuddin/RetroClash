using System;
using System.Linq;

namespace RetroClash.Crypto
{
    internal class Rc4
    {
        internal Rc4(byte[] key)
        {
            Key = Ksa(key);
        }

        internal Rc4(string key)
        {
            Key = Ksa(StringToByteArray(key));
        }

        internal byte[] Key { get; set; }

        internal byte i { get; set; }

        internal byte j { get; set; }

        internal byte Prga()
        {
            i = (byte) ((i + 1) % 256);
            j = (byte) ((j + Key[i]) % 256);

            var temp = Key[i];
            Key[i] = Key[j];
            Key[j] = temp;

            return Key[(Key[i] + Key[j]) % 256];
        }

        internal static byte[] Ksa(byte[] key)
        {
            var keyLength = key.Length;
            var s = new byte[256];

            for (var i = 0; i != 256; i++)
                s[i] = (byte) i;

            byte j = 0;

            for (var i = 0; i != 256; i++)
            {
                j = (byte) ((j + s[i] + key[i % keyLength]) % 256);

                var temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }
            return s;
        }

        internal static byte[] StringToByteArray(string str)
        {
            var bytes = new byte[str.Length];
            for (var i = 0; i < str.Length; i++)
                bytes[i] = (byte) str[i];
            return bytes;
        }
    }

    public class Rc4Core
    {
        internal string InitialKey = Resources.Configuration.EncryptionKey;
        internal const string InitialNonce = "nonce";

        internal Rc4Core()
        {
            InitializeCiphers(InitialKey + InitialNonce);
        }

        internal Rc4Core(string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            InitializeCiphers(key);
        }

        internal Rc4 Encryptor { get; set; }

        internal Rc4 Decryptor { get; set; }

        internal static byte[] GenerateNonce
        {
            get
            {
                var random = new Random();
                var buffer = new byte[random.Next(15, 25)];
                random.NextBytes(buffer);
                return buffer;
            }
        }

        internal void Encrypt(ref byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            for (var k = 0; k < data.Length; k++)
                data[k] ^= Encryptor.Prga();
        }

        internal void Decrypt(ref byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            for (var k = 0; k < data.Length; k++)
                data[k] ^= Decryptor.Prga();
        }

        public void UpdateCiphers(uint clientSeed, byte[] serverNonce)
        {
            if (serverNonce == null)
                throw new ArgumentNullException(nameof(serverNonce));

            var newNonce = ScrambleNonce(clientSeed, serverNonce);
            var key = InitialKey + newNonce;

            InitializeCiphers(key);
        }

        internal void InitializeCiphers(string key)
        {
            Encryptor = new Rc4(key);
            Decryptor = new Rc4(key);

            for (var k = 0; k < key.Length; k++)
            {
                Encryptor.Prga();
                Decryptor.Prga();
            }
        }

        internal static string ScrambleNonce(ulong clientSeed, byte[] serverNonce)
        {
            var scrambler = new Scrambler(clientSeed);
            var byte100 = 0;
            for (var i = 0; i < 100; i++)
                byte100 = scrambler.GetByte;
            return serverNonce.Aggregate(string.Empty,
                (current, t) => current + (char) (t ^ (scrambler.GetByte & byte100)));
        }
    }
}