using System;
using System.IO;
using System.Text;

namespace ElGamalApp
{
    // Simple JSON serializer/deserializer — no third-party libs needed
    class SignatureFile
    {
        public string P { get; set; }
        public string G { get; set; }
        public string Y { get; set; }
        public string Message { get; set; }
        public string R { get; set; }
        public string S { get; set; }

        // ── Serialize to JSON manually ───────────────────────────────
        public string ToJson()
        {
            var sb = new StringBuilder();
            sb.AppendLine("{");
            sb.AppendLine(string.Format("  \"p\": \"{0}\",", Escape(P)));
            sb.AppendLine(string.Format("  \"g\": \"{0}\",", Escape(G)));
            sb.AppendLine(string.Format("  \"y\": \"{0}\",", Escape(Y)));
            sb.AppendLine(string.Format("  \"message\": \"{0}\",", Escape(Message)));
            sb.AppendLine(string.Format("  \"r\": \"{0}\",", Escape(R)));
            sb.AppendLine(string.Format("  \"s\": \"{0}\"", Escape(S)));
            sb.AppendLine("}");
            return sb.ToString();
        }

        // ── Deserialize from JSON manually ───────────────────────────
        public static SignatureFile FromJson(string json)
        {
            var file = new SignatureFile();
            file.P = ExtractValue(json, "p");
            file.G = ExtractValue(json, "g");
            file.Y = ExtractValue(json, "y");
            file.Message = ExtractValue(json, "message");
            file.R = ExtractValue(json, "r");
            file.S = ExtractValue(json, "s");

            if (string.IsNullOrEmpty(file.P) || string.IsNullOrEmpty(file.G) ||
                string.IsNullOrEmpty(file.Y) || string.IsNullOrEmpty(file.Message) ||
                string.IsNullOrEmpty(file.R) || string.IsNullOrEmpty(file.S))
            {
                throw new FormatException("Invalid or incomplete .elg file.");
            }

            return file;
        }

        public void SaveToFile(string path)
        {
            File.WriteAllText(path, ToJson(), Encoding.UTF8);
        }

        public static SignatureFile LoadFromFile(string path)
        {
            string json = File.ReadAllText(path, Encoding.UTF8);
            return FromJson(json);
        }

        // ── Helpers ──────────────────────────────────────────────────

        // Extracts value from: "key": "value"
        private static string ExtractValue(string json, string key)
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

        // Escapes special characters for JSON strings
        private static string Escape(string s)
        {
            if (s == null) return "";
            return s.Replace("\\", "\\\\")
                    .Replace("\"", "\\\"")
                    .Replace("\n", "\\n")
                    .Replace("\r", "\\r")
                    .Replace("\t", "\\t");
        }

        // Unescapes JSON string values
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