using Abstractions.Console;
using Abstractions.DTOs;
using Spectre.Console;

namespace ConsoleTools
{
    public class PrettyConsole : IPrettyConsole
    {
        #region Display data
        public void NextLine()
        {
            AnsiConsole.WriteLine();
        }

        public void Write(string message, bool centered = false, PrettyColorsEnum color = PrettyColorsEnum.SimpleText)
        {
            var displayText = new Markup(message, GetStyle(color));

            if (centered)
            {
                displayText.Centered();
            }

            AnsiConsole.Write(displayText);
        }

        public void WriteAsPannel(string[] rows, string? title = null,
            PrettyColorsEnum panelColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText)
        {
            var displayText = string.Empty;

            for(int i = 0; i < rows.Length; i++)
            {
                if(displayText != string.Empty)
                {
                    displayText += "\n";
                }

                displayText += rows[i];
            }

            var markupText = new Markup(displayText, GetStyle(textColor));

            Panel panel = new Panel(markupText);

            if(title != null)
            {
                panel.Header(title);
            }

            panel.Border = BoxBorder.Rounded;
            panel.BorderColor(MapColors(panelColor));

            AnsiConsole.Write(panel);
        }

        public void WriteTable(List<CalculationMonthlyResultDTO> monthlyValues, 
            PrettyColorsEnum tableColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText)
        {
            var colorName = MapColors(textColor).ToString().ToLower();

            Table table = new();

            table.BorderColor(MapColors(tableColor));

            table.AddColumn($"[{colorName}]Month[/]");
            table.AddColumn($"[{colorName}]Value[/]");

            for (int  i = 0; i < monthlyValues.Count; i++)
            {
                var value = monthlyValues[i];

                table.AddRow(
                    new Markup(value.Month.ToString(), GetStyle(textColor)),
                    new Markup(value.Value.ToString(), GetStyle(textColor))
                    );
            }

            AnsiConsole.Write(table);
        }

        public void WriteTitle(string title, PrettyColorsEnum color = PrettyColorsEnum.Title)
        {
            var ft =(new FigletText(title));

            ft.Centered();
            ft.Color = MapColors(color);

            AnsiConsole.Write(ft);
        }

        public void Clean()
        {
            AnsiConsole.Clear();
        }
        #endregion

        #region Data retrival
        public T AskData<T>(string message, T[] choises, PrettyColorsEnum color = PrettyColorsEnum.SimpleText)
        {
            var colorName = MapColors(color).ToString().ToLower();
            var styledMessage = $"[{colorName}]{message}[/]";

            var textProp = new TextPrompt<T>(styledMessage);

            textProp.PromptStyle = GetStyle(color);
            textProp.ChoicesStyle = GetStyle(color);

            for (int i =0; i< choises.Length; i++)
            {
                textProp.AddChoice(choises[i]);
            }

            return AnsiConsole.Prompt(textProp);
        }

        public T ReadData<T>(string message, PrettyColorsEnum color = PrettyColorsEnum.SimpleText)
        {
            var colorName = MapColors(color).ToString().ToLower();
            var styledMessage = $"[{colorName}]{message}[/]";

            var textProp = new TextPrompt<T>(styledMessage);
            textProp.PromptStyle = GetStyle(color);

            return AnsiConsole.Prompt(textProp);
        }

        public T SelectData<T>(string message, T[] choises,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum selectedTextColor = PrettyColorsEnum.SimpleText)
        {
            var colorName = MapColors(textColor).ToString().ToLower();

            var prompt = new SelectionPrompt<T>()
                    .Title($"[{colorName}]{message}[/]")
                    .AddChoices(choises);

            prompt.DisabledStyle = GetStyle(textColor);
            prompt.HighlightStyle = GetStyle(selectedTextColor);

            var selectedElement = AnsiConsole.Prompt(prompt);

            return selectedElement;
        }

        public int SelectDataByPosition<T>(string message, T[] choises,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum selectedTextColor = PrettyColorsEnum.SimpleText)
        {
            var initialResult = SelectData(message, choises, textColor, selectedTextColor);

            for(int i = 0; i < choises.Length; i++)
            {
                if (choises[i].Equals(initialResult))
                {
                    return i;
                }
            }

            return -1;
        }
        #endregion

        #region Utility
        private Style GetStyle(PrettyColorsEnum color)
        {
            var style = new Style(
                foreground: MapColors(color));

            return style;
        }

        private Color MapColors(PrettyColorsEnum color)
        {
            var result = Color.White;

            switch (color)
            {
                case PrettyColorsEnum.Title: 
                    result = Color.Blue; 
                    break;
                case PrettyColorsEnum.ImportantText:
                    result = Color.White;
                    break;
                case PrettyColorsEnum.Info:
                    result = Color.Blue;
                    break;
                case PrettyColorsEnum.SimpleText:
                    result = Color.Gray70;
                    break;
            }

            return result;
        }
        #endregion
    }
}