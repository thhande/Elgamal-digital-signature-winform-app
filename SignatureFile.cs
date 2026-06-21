using System;
using System.IO;
using System.Text;

namespace ElGamalApp
{
    // ── Public Key file (.pub) — shared with verifier ───────────────────
    // Contains: p, g, y
    class PublicKeyFile
    {
        public string P { get; set; }
        public string G { get; set; }
        public string Y { get; set; }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine(string.Format("  \"p\": \"{0}\",", Json.Escape(P)));
            sb.AppendLine(string.Format("  \"g\": \"{0}\",", Json.Escape(G)));
            sb.AppendLine(string.Format("  \"y\": \"{0}\"", Json.Escape(Y)));
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static PublicKeyFile FromJson(string json)
        {
            var f = new PublicKeyFile
            {
                P = Json.ExtractValue(json, "p"),
                G = Json.ExtractValue(json, "g"),
                Y = Json.ExtractValue(json, "y")
            };

            if (string.IsNullOrEmpty(f.P) || string.IsNullOrEmpty(f.G) || string.IsNullOrEmpty(f.Y))
                throw new FormatException("Invalid or incomplete public key file.");

            return f;
        }

        public void SaveToFile(string path) { File.WriteAllText(path, ToJson(), Encoding.UTF8); }
        public static PublicKeyFile LoadFromFile(string path) { return FromJson(File.ReadAllText(path, Encoding.UTF8)); }
    }

    // ── Private Key file (.key) — kept secret by signer ─────────────────
    // Contains: p, g, x
    class PrivateKeyFile
    {
        public string P { get; set; }
        public string G { get; set; }
        public string X { get; set; }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine(string.Format("  \"p\": \"{0}\",", Json.Escape(P)));
            sb.AppendLine(string.Format("  \"g\": \"{0}\",", Json.Escape(G)));
            sb.AppendLine(string.Format("  \"x\": \"{0}\"", Json.Escape(X)));
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static PrivateKeyFile FromJson(string json)
        {
            var f = new PrivateKeyFile
            {
                P = Json.ExtractValue(json, "p"),
                G = Json.ExtractValue(json, "g"),
                X = Json.ExtractValue(json, "x")
            };

            if (string.IsNullOrEmpty(f.P) || string.IsNullOrEmpty(f.G) || string.IsNullOrEmpty(f.X))
                throw new FormatException("Invalid or incomplete private key file.");

            return f;
        }

        public void SaveToFile(string path) { File.WriteAllText(path, ToJson(), Encoding.UTF8); }
        public static PrivateKeyFile LoadFromFile(string path) { return FromJson(File.ReadAllText(path, Encoding.UTF8)); }
    }

    // ── Message file (.msg) — the plain message text ────────────────────
    class MessageFile
    {
        public static void SaveToFile(string path, string message)
        {
            File.WriteAllText(path, message, Encoding.UTF8);
        }

        public static string LoadFromFile(string path)
        {
            return File.ReadAllText(path, Encoding.UTF8);
        }
    }

    // ── Signature file (.sig) — R and S only ────────────────────────────
    class SignatureOnlyFile
    {
        public string R { get; set; }
        public string S { get; set; }

        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine(string.Format("  \"r\": \"{0}\",", Json.Escape(R)));
            sb.AppendLine(string.Format("  \"s\": \"{0}\"", Json.Escape(S)));
            sb.AppendLine("}");
            return sb.ToString();
        }

        public static SignatureOnlyFile FromJson(string json)
        {
            var f = new SignatureOnlyFile
            {
                R = Json.ExtractValue(json, "r"),
                S = Json.ExtractValue(json, "s")
            };

            if (string.IsNullOrEmpty(f.R) || string.IsNullOrEmpty(f.S))
                throw new FormatException("Invalid or incomplete signature file.");

            return f;
        }

        public void SaveToFile(string path) { File.WriteAllText(path, ToJson(), Encoding.UTF8); }
        public static SignatureOnlyFile LoadFromFile(string path) { return FromJson(File.ReadAllText(path, Encoding.UTF8)); }
    }

    // ── Shared minimal JSON helper ───────────────────────────────────────
    static class Json
    {
        public static string ExtractValue(string json, string key)
        {
            string search = string.Format("\"{0}\"", key);
            int keyIndex = json.IndexOf(search, StringComparison.OrdinalIgnoreCase);
            if (keyIndex < 0) return null;

            int colonIndex = json.IndexOf(':', keyIndex + search.Length);
            if (colonIndex < 0) return null;

            int openQuote = json.IndexOf('"', colonIndex + 1);
            if (openQuote < 0) return null;

            int closeQuote = openQuote + 1;
            while (closeQuote < json.Length)
            {
                if (json[closeQuote] == '\\') { closeQuote += 2; continue; }
                if (json[closeQuote] == '"') break;
                closeQuote++;
            }

            if (closeQuote >= json.Length) return null;

            return Unescape(json.Substring(openQuote + 1, closeQuote - openQuote - 1));
        }

        public static string Escape(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n")
                    .Replace("\r", "\\r")
                    .Replace("\t", "\\t");
        }

        private static string Unescape(string s)
        {
            if (s == null) return "";
            return s.Replace("\\\"", "\"")
                    .Replace("\\\\", "\\")
                    .Replace("\\n", "\n")
                    .Replace("\\r", "\r")
                    .Replace("\\t", "\t");
        }
    }
}