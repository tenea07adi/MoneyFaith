using Abstractions.DTOs;

namespace Abstractions.Console
{
    public interface IPrettyConsole
    {
        public void NextLine();
        public void Write(string message, bool centered = false, PrettyColorsEnum color = PrettyColorsEnum.SimpleText);
        public void WriteTable(List<CalculationMonthlyResultDTO> monthlyValues, 
            PrettyColorsEnum tableColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText);
        public void WriteTitle(string title, PrettyColorsEnum color = PrettyColorsEnum.Title);
        public void WriteAsPannel(string[] rows, string? title = null, 
            PrettyColorsEnum panelColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText);

        public T ReadData<T>(string message, PrettyColorsEnum color = PrettyColorsEnum.SimpleText);
        public T AskData<T>(string message, T[] choises, PrettyColorsEnum color = PrettyColorsEnum.SimpleText);

        public T SelectData<T>(string message, T[] choises,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum selectedTextColor = PrettyColorsEnum.SimpleText);
        public int SelectDataByPosition<T>(string message, T[] choises,
            PrettyColorsEnum textColor = PrettyColorsEnum.SimpleText,
            PrettyColorsEnum selectedTextColor = PrettyColorsEnum.SimpleText);

        public void Clean();
    }
}