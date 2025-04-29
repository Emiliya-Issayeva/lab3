using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace lab1
{
    public class Token
    {
        public string Code { get; set; }
        public string Type { get; set; }
        public string Lexeme { get; set; }
        public string Position { get; set; }
    }

    public class Lexer
    {
        public List<Token> Analyze(string input, out int errorCount)
        {
            var tokens = new List<Token>();
            var errors = new List<string>();

            var lines = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);

            bool insideComment = false;
            string commentType = "";
            int startLine = 0, startCol = 0;
            string commentContent = "";

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                int j = 0;
                while (j < line.Length)
                {
                    // Проверка на случай, если уже внутри комментария
                    if (insideComment)
                    {
                        if (commentType == "{}")
                        {
                            if (line[j] == '}')
                            {
                                // Добавляем тело комментария
                                tokens.Add(new Token
                                {
                                    Code = "2",
                                    Type = "Тело комментария",
                                    Lexeme = commentContent,
                                    Position = $"Строка {startLine}, символ {startCol + 1}"
                                });

                                // Добавляем конец комментария
                                tokens.Add(new Token
                                {
                                    Code = "3",
                                    Type = "Конец комментария",
                                    Lexeme = "}",
                                    Position = $"Строка {i + 1}, символ {j + 1}"
                                });

                                // Сброс состояния
                                insideComment = false;
                                commentContent = "";
                                j++;
                                continue;
                            }
                            else
                            {
                                commentContent += line[j];
                                j++;
                                continue;
                            }
                        }
                        else if (commentType == "(* *)")
                        {
                            if (j < line.Length - 1 && line[j] == '*' && line[j + 1] == ')')
                            {
                                // Добавляем тело комментария
                                tokens.Add(new Token
                                {
                                    Code = "2",
                                    Type = "Тело комментария",
                                    Lexeme = commentContent,
                                    Position = $"Строка {startLine}, символ {startCol + 2}"
                                });

                                // Добавляем конец комментария
                                tokens.Add(new Token
                                {
                                    Code = "3",
                                    Type = "Конец комментария",
                                    Lexeme = "*)",
                                    Position = $"Строка {i + 1}, символ {j + 1}"
                                });

                                // Сброс состояния
                                insideComment = false;
                                commentContent = "";
                                j += 2;
                                continue;
                            }
                            else
                            {
                                commentContent += line[j];
                                j++;
                                continue;
                            }
                        }
                    }

                    // Обработка вне комментария
                    if (line[j] == '{')
                    {
                        insideComment = true;
                        commentType = "{}";
                        startLine = i + 1;
                        startCol = j + 1;
                        tokens.Add(new Token
                        {
                            Code = "1",
                            Type = "Начало комментария",
                            Lexeme = "{",
                            Position = $"Строка {startLine}, символ {startCol}"
                        });
                        j++;
                    }
                    else if (j < line.Length - 1 && line[j] == '(' && line[j + 1] == '*')
                    {
                        insideComment = true;
                        commentType = "(* *)";
                        startLine = i + 1;
                        startCol = j + 1;
                        tokens.Add(new Token
                        {
                            Code = "1",
                            Type = "Начало комментария",
                            Lexeme = "(*",
                            Position = $"Строка {startLine}, символ {startCol}"
                        });
                        j += 2;
                    }
                    else if (line[j] == '}')
                    {
                        // Ошибка: закрывающая скобка без открытия
                        errors.Add($"Ошибка: найден символ '}}' без соответствующего начала комментария на строке {i + 1}, символ {j + 1}");
                        j++;
                    }
                    else if (j < line.Length - 1 && line[j] == '*' && line[j + 1] == ')')
                    {
                        // Ошибка: закрывающая скобка *) без открытия
                        errors.Add($"Ошибка: найдено '*)' без соответствующего начала комментария на строке {i + 1}, символ {j + 1}");
                        j += 2;
                    }
                    else
                    {
                        j++;
                    }
                }

                // Если строка закончилась, а комментарий ещё не закрыт
                if (insideComment && j >= line.Length)
                {
                    // Продолжаем ждать закрытия на следующих строках
                }
            }

            // После окончания всех строк: проверяем, есть ли незакрытый комментарий
            if (insideComment)
            {
                errors.Add($"Ошибка: комментарий начат на строке {startLine}, символ {startCol}, но не закрыт.");
            }

            // Добавляем ошибки как токены типа "Ошибка"
            foreach (var error in errors)
            {
                tokens.Add(new Token
                {
                    Code = "4",
                    Type = "Ошибка",
                    Lexeme = "",
                    Position = error
                });
            }

            errorCount = errors.Count;
            return tokens;
        }
    }
}