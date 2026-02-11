using System.Collections.Generic;
using System.IO;
using System.Text;

public static class CSVWriter
{
    private const char Separator = ',';

    public static void Write(string filePath, IList<Dictionary<string, object>> rows, IList<string> headers)
    {
        if (string.IsNullOrEmpty(filePath))
            return;

        var builder = new StringBuilder();

        if (headers != null && headers.Count > 0)
        {
            builder.AppendLine(BuildLine(headers));
        }
        else if (rows != null && rows.Count > 0)
        {
            builder.AppendLine(BuildLine(rows[0].Keys));
        }
        else
        {
            return;
        }

        if (rows != null)
        {
            for (int i = 0; i < rows.Count; i++)
            {
                builder.AppendLine(BuildLine(rows[i], headers));
            }
        }

        File.WriteAllText(filePath, builder.ToString(), new UTF8Encoding(false));
    }

    private static string BuildLine(IEnumerable<string> values)
    {
        var builder = new StringBuilder();
        bool first = true;

        foreach (string value in values)
        {
            if (!first)
                builder.Append(Separator);

            builder.Append(Escape(value));
            first = false;
        }

        return builder.ToString();
    }

    private static string BuildLine(Dictionary<string, object> row, IList<string> headers)
    {
        if (row == null)
            return string.Empty;

        var builder = new StringBuilder();
        bool first = true;

        if (headers != null && headers.Count > 0)
        {
            for (int i = 0; i < headers.Count; i++)
            {
                if (!first)
                    builder.Append(Separator);

                row.TryGetValue(headers[i], out object value);
                builder.Append(Escape(value?.ToString()));
                first = false;
            }
        }
        else
        {
            foreach (var pair in row)
            {
                if (!first)
                    builder.Append(Separator);

                builder.Append(Escape(pair.Value?.ToString()));
                first = false;
            }
        }

        return builder.ToString();
    }

    private static string Escape(string value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        bool mustQuote = value.IndexOfAny(new[] { ',', '"', '\n', '\r' }) >= 0;
        string escaped = value.Replace("\"", "\"\"");
        return mustQuote ? $"\"{escaped}\"" : escaped;
    }
}
