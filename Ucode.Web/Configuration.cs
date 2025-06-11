using MudBlazor.Utilities;
using MudBlazor;

namespace Ucode.Web
{
    public static class Configuration
    {
        public const string HttpClientName = "ucode";
        public static string BackendUrl { get; set; } = "https://localhost:7209";


        public static MudTheme Theme = new()
        {
            
            Typography = new Typography
            {
                Default = new Default
                {
                    FontFamily = ["Raleway", "sans-serif"]
                }
            },
            Palette = new PaletteLight
            {
                
                Primary = new MudColor("#0D47A1"), // Azul escuro
                PrimaryContrastText = Colors.Shades.White, // Bom contraste com azul escuro
                Secondary = Colors.BlueGrey.Default, // Azul acinzentado para suavizar
                Background = Colors.Grey.Lighten5, // Fundo bem claro
                AppbarBackground = new MudColor("#0D47A1"), // Mesma cor da primária
                AppbarText = Colors.Shades.White,
                TextPrimary = Colors.Grey.Darken3, // Texto escuro, mas não preto
                DrawerText = Colors.Shades.White,
                DrawerBackground = "#0D47A1",
                
            },
            
           PaletteDark = new PaletteDark
            {
                
                Primary = Colors.Blue.Lighten2, // Azul claro para se destacar no escuro
                PrimaryContrastText = Colors.Shades.White, // Texto branco sobre azul claro
                Secondary = Colors.BlueGrey.Default, // Azul acinzentado como secundária
                Background = Colors.Grey.Darken4, // Fundo escuro padrão
                Surface = Colors.Grey.Darken3, // Cartões, modais, etc
                DrawerBackground = Colors.BlueGrey.Darken4, // Lateral com tom frio
                DrawerText = Colors.Shades.White,
                AppbarBackground = Colors.Blue.Darken3, // AppBar azul escuro
                AppbarText = Colors.Shades.White,
                TextPrimary = Colors.Shades.White,
                TextSecondary = Colors.Grey.Lighten1
                
            }
            
        };
            
        }
}