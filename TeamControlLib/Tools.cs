using System;
using System.Collections.Generic;

namespace TeamControlLib
{
    public class Tools
    {
        public static string GetBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }
        public static string ReadLine(string Default)
        {
            int pos = Console.CursorLeft;
            Console.Write(Default);
            ConsoleKeyInfo info;
            List<char> chars = new List<char>();
            if (string.IsNullOrEmpty(Default) == false)
            {
                chars.AddRange(Default.ToCharArray());
            }

            while (true)
            {
                info = Console.ReadKey(true);
                if (info.Key == ConsoleKey.Backspace && Console.CursorLeft > pos)
                {
                    chars.RemoveAt(chars.Count - 1);
                    Console.CursorLeft -= 1;
                    Console.Write(' ');
                    Console.CursorLeft -= 1;

                }
                else if (info.Key == ConsoleKey.Enter) { Console.Write(Environment.NewLine); break; }
                else if (info.Key == ConsoleKey.Spacebar) { chars.Insert(chars.Count, ' '); Console.Write(' '); }
                else if (info.Key == ConsoleKey.LeftArrow) { Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop); }
                else if (info.Key == ConsoleKey.RightArrow) { Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop); }
                else if (char.IsLetterOrDigit(info.KeyChar))
                {
                    Console.Write(info.KeyChar);
                    chars.Add(info.KeyChar);
                }
            }
            return new string(chars.ToArray());
        }
    }
}
