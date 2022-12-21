using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CommonLibrary.StringDecryptor
{
    public static class StringDecryptor
    {
        public static string GetDecryptedString(this string inputString)
        {
            var decryptor = new Decryptor(EpsKeys.Key01);

            var textConverter = new ASCIIEncoding();

            return textConverter
                .GetString(
                    decryptor
                        .Decrypt(
                            DecodeUtil
                                .HexDecode(inputString)
                        )
                );
        }

        public static string GetEncodedString(this string inputString)
        {
            var encryptor = new Encryptor(EpsKeys.Key01);

            var textConverter = new ASCIIEncoding();

            return EncodeUtil
                .HexEncode(
                    encryptor
                        .Encrypt(
                            textConverter
                                .GetBytes(inputString)
                        )
                );
        }

        public class EpsKeys
        {
            public static byte[] Key01
            {
                get
                {
                    var textConverter = new ASCIIEncoding();

                    return textConverter.GetBytes("@(Qf0jp#W$7eY)*Nr%OzHLd%^8-0qWcu");
                }
            }
        }

        public class DecodeUtil
        {
            /// <summary>
            ///     Decode a hex string.
            /// </summary>
            /// <param name="input">hex string to decode.</param>
            /// <returns>decoded byte array.</returns>
            public static byte[] HexDecode(string input)
            {
                if (input.Length % 2 != 0)
                    throw new ArgumentException(
                        "Invalid hex string length, length=" + input.Length, input);

                var output = new byte[input.Length / 2];

                for (var i = 0; i < output.Length; i++)
                    output[i] = Convert.ToByte(input.Substring(i * 2, 2), 16);

                return output;
            }
        }

        public sealed class Decryptor
        {
            private readonly byte[] _encryptionKey;

            public Decryptor(byte[] encryptionKey)
            {
                this._encryptionKey = encryptionKey;
            }

            /// <summary>
            ///     Decrypt data.
            /// </summary>
            /// <param name="input">Data to decrypt.</param>
            /// <returns>Decrypted data.</returns>
            public byte[] Decrypt(byte[] input)
            {
                // retrieve encrypted IV, encrypted IV has 128 bits
                var encryptedIv = new byte[16];

                var encryptedValue = new byte[input.Length - encryptedIv.Length];

                Array.Copy(input, 0, encryptedIv, 0, encryptedIv.Length);

                Array.Copy(input, encryptedIv.Length, encryptedValue, 0, encryptedValue.Length);

                var decryptedIv = DecryptIv(encryptedIv);

                // decrypt the rest using decrypted IV
                var rij = new RijndaelManaged
                {
                    BlockSize = 128,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.None
                };

                var decryptor = rij.CreateDecryptor(_encryptionKey, decryptedIv);

                var decryptedStream = new MemoryStream();

                var cryptoStream = new CryptoStream(
                    decryptedStream, decryptor, CryptoStreamMode.Write);

                cryptoStream.Write(encryptedValue, 0, encryptedValue.Length);

                cryptoStream.Close();

                decryptedStream.Close();

                return PadUtil.UnPad(decryptedStream.ToArray(), 0);
            }

            /// <summary>
            ///     Decrypt IV.
            /// </summary>
            /// <param name="encryptedIv">IV to encrypt.</param>
            /// <returns>Decrypted IV.</returns>
            private byte[] DecryptIv(byte[] encryptedIv)
            {
                var rij = new RijndaelManaged
                {
                    BlockSize = 128,
                    Mode = CipherMode.ECB, // Electronic Codebook mode does not need IV
                    Padding = PaddingMode.None
                };

                var decryptor = rij.CreateDecryptor(_encryptionKey, null);

                var decryptedStream = new MemoryStream(encryptedIv.Length * 2);

                var cryptoStream = new CryptoStream(
                    decryptedStream, decryptor, CryptoStreamMode.Write);

                cryptoStream.Write(encryptedIv, 0, encryptedIv.Length);

                cryptoStream.Close();

                decryptedStream.Close();

                return decryptedStream.ToArray();
            }
        }

        public class EncodeUtil
        {
            /// <summary>
            ///     Hex encode.
            /// </summary>
            /// <param name="input">Byte array data to be encoded into hexidecimal string.</param>
            /// <returns>Hexidecimal string.</returns>
            public static string HexEncode(byte[] input)
            {
                return BitConverter.ToString(input).Replace("-", string.Empty);
            }
        }

        public sealed class Encryptor
        {
            private readonly byte[] _encryptionKey;

            public Encryptor(byte[] encryptionKey)
            {
                _encryptionKey = encryptionKey;
            }

            /// <summary>
            ///     Encrypt data.
            /// </summary>
            /// <param name="input">data to encrypt.</param>
            /// <returns>encrypted data.</returns>
            public byte[] Encrypt(byte[] input)
            {
                var rij = new RijndaelManaged
                {
                    BlockSize = 128,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.None
                };

                rij.GenerateIV();

                var paddedInput = PadUtil.Pad(input, 16, 0);

                var paddedIv = PadUtil.Pad(rij.IV, 16, 0);

                var encryptor = rij.CreateEncryptor(_encryptionKey, rij.IV);

                var encryptedStream = new MemoryStream(paddedInput.Length * 2);

                var cryptoStream = new CryptoStream(
                    encryptedStream, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(paddedInput, 0, paddedInput.Length);

                cryptoStream.Close();

                encryptedStream.Close();

                var encryptedValue = encryptedStream.ToArray();

                var encryptedIv = EncryptIv(paddedIv);

                var output = new byte[encryptedIv.Length + encryptedValue.Length];

                Array.Copy(encryptedIv, 0, output, 0, encryptedIv.Length);

                Array.Copy(encryptedValue, 0, output, encryptedIv.Length, encryptedValue.Length);

                return output;
            }

            /// <summary>
            ///     Encrypt the IV.
            /// </summary>
            /// <param name="iv">IV to encrypt.</param>
            /// <returns>encrypted IV.</returns>
            private byte[] EncryptIv(byte[] iv)
            {
                var rij = new RijndaelManaged
                {
                    BlockSize = 128,
                    Mode = CipherMode.ECB, // Electronic Codebook does not need IV
                    Padding = PaddingMode.None
                };

                var encryptor = rij.CreateEncryptor(_encryptionKey, null);

                var encryptedIv = new MemoryStream(iv.Length * 2);

                var cryptoStream = new CryptoStream(
                    encryptedIv, encryptor, CryptoStreamMode.Write);

                cryptoStream.Write(iv, 0, iv.Length);

                cryptoStream.Close();

                encryptedIv.Close();

                return encryptedIv.ToArray();
            }
        }

        public class PadUtil
        {
            /// <summary>
            ///     Pad a byte array up to the boundary size with given padding value.
            /// </summary>
            /// <param name="input">Byte array to padd.</param>
            /// <param name="boundary">Boundary size.</param>
            /// <param name="padValue">Padding value.</param>
            /// <returns>Padded byte array.</returns>
            public static byte[] Pad(byte[] input, int boundary, byte padValue)
            {
                var padSize = input.Length % boundary;

                if (padSize == 0) return input;

                padSize = boundary - padSize;

                var output = new byte[input.Length + padSize];

                Array.Copy(input, 0, output, 0, input.Length);

                for (var i = input.Length; i < input.Length + padSize; i++) output[i] = padValue;

                return output;
            }

            /// <summary>
            ///     Unpad a byte array.
            /// </summary>
            /// <param name="input">Byte array to unpad.</param>
            /// <param name="padValue">Padding value.</param>
            /// <returns>Unpadded byte array.</returns>
            public static byte[] UnPad(byte[] input, byte padValue)
            {
                var realLength = input.Length;

                for (var i = input.Length - 1; i >= 0 && input[i] == padValue; i--)
                    realLength = i;

                var output = new byte[realLength];

                Array.Copy(input, 0, output, 0, output.Length);

                return output;
            }
        }
    }
}