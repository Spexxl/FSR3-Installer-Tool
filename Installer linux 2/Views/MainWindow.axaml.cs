using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using LibModAuth;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace Installer_linux_2.Views;

public partial class MainWindow : Window
{


    private Avalonia.Point? _startPosition;
    public List<string> PastaNomes { get; set; }
    //Cria a pasta com os arquivos
    private string diretorioOrigem;


    //  icones de erro e sucesso MsBOX Avalonia



    public MainWindow()
    {

        InitializeComponent();
        LerCacheIdioma();
        LerCacheTemas();
        PreencherComboBox();


        diretorioOrigem = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files");

        Gamedirect.Text = "";
        OpMode.SelectedIndex = 0;
        ModVersionComboBox.SelectedIndex = 0;
        gamecombobox.SelectionChanged += GameComboBox_SelectionChanged;
        ModVersionComboBox.SelectionChanged += DisableFuctions;

        tema1.Checked += RadioButton1_Checked;
        tema2.Checked += RadioButton2_Checked;


        IdiomaIngles.Checked += Ingles_Checked;
        IdiomaPortugues.Checked += Portugues_Checked;



        // Evita que a janela comece em tela cheia


        // Impede o redimensionamento da janela
        this.CanResize = false;

        WebClient webClient = new WebClient();
        var client = new WebClient();
        string currentDirectory = Directory.GetCurrentDirectory();




        // ///////////////////////////////////////////// ADICIONA ITEMS A COMBOBOX DE GAMES ///////////////////////////////////////////////


        gamecombobox.Items.Add("Common Fsr 2.0");
        gamecombobox.Items.Add("Common Fsr 2.1");
        gamecombobox.Items.Add("Common Fsr 2.2");
        gamecombobox.Items.Add("Common Fsr SDK");
        gamecombobox.Items.Add("Alan Wake 2");
        gamecombobox.Items.Add("A Plague Tale Requiem");
        gamecombobox.Items.Add("Assassin's Creed Mirage");
        gamecombobox.Items.Add("Atomic Heart");
        gamecombobox.Items.Add("Banishers: Ghost of New Eden");
        gamecombobox.Items.Add("Cyberpunk 2077");
        gamecombobox.Items.Add("Dead Island 2");
        gamecombobox.Items.Add("Dead Space");
        gamecombobox.Items.Add("Dakar Desert Rally");
        gamecombobox.Items.Add("Dying Light 2");
        gamecombobox.Items.Add("Dragons Dogma 2");
        gamecombobox.Items.Add("Ghostwire");
        gamecombobox.Items.Add("Gotham Knights");
        gamecombobox.Items.Add("Hellblade II");
        gamecombobox.Items.Add("Hogwarts Legacy");
        gamecombobox.Items.Add("Horizon Zero Dawn");
        gamecombobox.Items.Add("Horizon Forbidden West");
        gamecombobox.Items.Add("Lies of P");
        gamecombobox.Items.Add("Lords of The fallen");
        gamecombobox.Items.Add("Marvel Guardians of The Galaxy");
        gamecombobox.Items.Add("Marvel's Spider-Man: Miles Morales");
        gamecombobox.Items.Add("Marvel's Spider-Man Remastered");
        gamecombobox.Items.Add("Metro Exodus Enhanced");
        gamecombobox.Items.Add("PalWord");
        gamecombobox.Items.Add("Pacific Drive");
        gamecombobox.Items.Add("Ratchet & Clank");
        gamecombobox.Items.Add("Ready or Not");
        gamecombobox.Items.Add("Red Dead Redemption 2");
        gamecombobox.Items.Add("Remnant II");
        gamecombobox.Items.Add("RoboCop: Rogue City");
        gamecombobox.Items.Add("Satisfactory");
        gamecombobox.Items.Add("Star Wars Jedi: Survivor");
        gamecombobox.Items.Add("Starfield");
        gamecombobox.Items.Add("The Invincibles");
        gamecombobox.Items.Add("The Last of Us Part I");
        gamecombobox.Items.Add("The Witcher 3");
        gamecombobox.Items.Add("The Medium");
        gamecombobox.Items.Add("UNCHARTED");



        // ///////////////////////////////////////////// ADICIONA ITEMS A COMBOBOX OPMODE///////////////////////////////////////////////





        //Se a key ja existe ela sera inserida no combobox versions

        string KeyLocal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config\\Config.adx");

        // Verificar se o arquivo existe antes de tentar lê-lo

        if (File.Exists(KeyLocal))
        {
            string ChaveSalva = File.ReadAllText(KeyLocal);
            KeyDoUser.Text = ChaveSalva;
        }



        DisableFuctions_INCIAL();
    }

    private Window parentWindow;



    // ////////////////////////Perfil de cada jogo///////////////////////////////////////////////




    private void GameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (gamecombobox.SelectedItem != null)
        {
            string selectedItem = gamecombobox.SelectedItem.ToString();

            // Atualiza o estado do ToggleSwitch com base no item selecionado
            switch (selectedItem)
            {
                case "Alan Wake 2":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;
                    break;

                case "A Plague Tale Requiem":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;


                case "Assassin's Creed Mirage":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;
                    DxgiSwitch.IsChecked = true;

                    break;
                case "Atomic Heart":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;

                    break;
                case "Banishers: Ghost of New Eden":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Cyberpunk 2077":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;
                    OpMode.SelectedIndex = 2;

                    break;
                case "Dead Island 2":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Dead Space":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Dragons Dogma 2":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Dying Light 2":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;
                    DxgiSwitch.IsChecked = true;

                    break;

                case "Dakar Desert Rally":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;

                    break;

                case "Ghostwire":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Gotham Knights":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;

                case "Hogwarts Legacy":
                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;
                    break;

                case "Hellblade II":
                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;
                    NvapiSwitch.IsChecked = true;
                    DxgiSwitch.IsChecked = true;
                    NVNGX_Stub.IsChecked = true;
                    break;

                case "Horizon Zero Dawn":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Horizon Forbidden West":

                    FakeGpuSwitch.IsChecked = true;
                    break;
                case "Lies of P":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Lords of The fallen":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;

                    break;
                case "Marvel Guardians of The Galaxy":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Marvel's Spider-Man: Miles Morales":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Marvel's Spider-Man Remastered":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Metro Exodus Enhanced":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "PalWord":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;
                    break;



                case "Pacific Drive":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;

                    break;


                case "Ratchet & Clank":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Ready or Not":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Red Dead Redemption 2":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "Remnant II":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "RoboCop: Rogue City":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;


                    break;
                case "Star Wars Jedi: Survivor":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = true;

                    break;
                case "Starfield":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;

                case "Satisfactory":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;

                case "The Invincibles":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "The Last of Us Part I":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                case "The Witcher 3":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;
                    break;

                case "The Medium":

                    FakeGpuSwitch.IsChecked = true;
                    UnrealAmdSwitch.IsChecked = false;

                    break;

                case "UNCHARTED":

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
                default:
                    // Caso padrão, caso nenhum item seja selecionado

                    FakeGpuSwitch.IsChecked = false;
                    UnrealAmdSwitch.IsChecked = false;

                    break;
            }
        }
    }





    //Alterna a visibilidade do SubMenu

    private void DropDown_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var submenuPANEL = this.FindControl<Canvas>("submenuPANEL");

        submenuPANEL.IsVisible = !submenuPANEL.IsVisible;

    }



    // FSR Quality Valores de Upscaling!!!


    //Ultra quality
    private void UltraQualityMinusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(UltraQualityNumeric.Text);
        if (value > 33)
        {
            value -= 1;
            UltraQualityNumeric.Text = value.ToString();
        }
    }

    private void UltraQualityPlusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(UltraQualityNumeric.Text);
        if (value < 100)
        {
            value += 1;
            UltraQualityNumeric.Text = value.ToString();
        }
    }


    //Quality
    private void QualityMinusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(QualityNumeric.Text);
        if (value > 33)
        {
            value -= 1;
            QualityNumeric.Text = value.ToString();
        }
    }

    private void QualityPlusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(QualityNumeric.Text);
        if (value < 100)
        {
            value += 1;
            QualityNumeric.Text = value.ToString();
        }
    }

    //Balanced
    private void BalancedMinusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(BalancedNumeric.Text);
        if (value > 33)
        {
            value -= 1;
            BalancedNumeric.Text = value.ToString();
        }
    }

    private void BalancedPlusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(BalancedNumeric.Text);
        if (value < 100)
        {
            value += 1;
            BalancedNumeric.Text = value.ToString();
        }
    }

    //Performance
    private void PerformanceMinusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(PerformanceNumeric.Text);
        if (value > 33)
        {
            value -= 1;
            PerformanceNumeric.Text = value.ToString();
        }
    }

    private void PerformancePlusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(PerformanceNumeric.Text);
        if (value < 100)
        {
            value += 1;
            PerformanceNumeric.Text = value.ToString();
        }
    }


    //Ultra Performance
    private void UltraPerformanceMinusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(UltraPerformanceNumeric.Text);
        if (value > 33)
        {
            value -= 1;
            UltraPerformanceNumeric.Text = value.ToString();
        }
    }

    private void UltraPerformancePlusButton_Click(object sender, RoutedEventArgs e)
    {
        double value = double.Parse(UltraPerformanceNumeric.Text);
        if (value < 100)
        {
            value += 1;
            UltraPerformanceNumeric.Text = value.ToString();
        }
    }



    //Abertura do Config Panel

    private void ConfigButton_Click(object sender, RoutedEventArgs e)
    {
        if (ConfigPanel.IsPaneOpen == false)
        {
            ConfigPanel.IsPaneOpen = true;
            ConfigButton.Background = new SolidColorBrush(Color.FromArgb(255, 46, 175, 225));
            //Desativa Outros Panel

            AboutPanel.IsPaneOpen = false;
            HomeButton.Background = Brushes.Transparent;
            AboutButton.Background = Brushes.Transparent;
        }

        else
        {
            ConfigPanel.IsPaneOpen = false;
            ConfigButton.Background = Brushes.Transparent;
            HomeButton.Background = new SolidColorBrush(Color.FromArgb(255, 46, 175, 225));
            //Desativa Outros Panel

            AboutPanel.IsPaneOpen = false;
        }

    }


    //Abertura do About Panel

    private void AboutButton_Click(object sender, RoutedEventArgs e)
    {
        if (AboutPanel.IsPaneOpen == false)
        {
            AboutPanel.IsPaneOpen = true;
            AboutButton.Background = new SolidColorBrush(Color.FromArgb(255, 46, 175, 225));

            //Desativa Outros Panel
            ConfigPanel.IsPaneOpen = false;

            //troca a cor de outros buttons
            ConfigButton.Background =  Brushes.Transparent;
            HomeButton.Background = Brushes.Transparent;
            
        }

        else
        {
            AboutPanel.IsPaneOpen = false;
            AboutButton.Background = Brushes.Transparent;
            HomeButton.Background = new SolidColorBrush(Color.FromArgb(255, 46, 175, 225));
            //Desativa Outros Panel

            ConfigPanel.IsPaneOpen = false;

        }

    }

    // Abertura Idioma



    // Abertura Tema
    private void TemaButton_Click(object sender, RoutedEventArgs e)
    {
        if (TemaPanel.IsPaneOpen == false)
        {
            TemaPanel.IsPaneOpen = true;
        }

        else
        {
            TemaPanel.IsPaneOpen = false;
        }

    }


    //Fechamento dos panel HOME

    private void HomeButton_Click(object sender, RoutedEventArgs e)
    {
        ConfigPanel.IsPaneOpen = false;

        AboutPanel.IsPaneOpen = false;

        TemaPanel.IsPaneOpen = false;


        // Colors
        HomeButton.Background = new SolidColorBrush(Color.FromArgb(255, 46, 175, 225));
        ConfigButton.Background = Brushes.Transparent;
        AboutButton.Background = Brushes.Transparent;

    }


    //Expander SubMenu Button

    private void Expander_SubMenu_Click(object sender, RoutedEventArgs e)
    {
        Split_SubMenu.IsPaneOpen =! Split_SubMenu.IsPaneOpen;

        if (Split_SubMenu.IsPaneOpen == true)
        {
            CanvaSubMenu.ZIndex = 1;
        }
        else
        {
            CanvaSubMenu.ZIndex = -1;
        }

    }

    //Browse Button

    private async void BrowseButton_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFolderDialog();
        var selectedDirectory = await dialog.ShowAsync(this);

        if (!string.IsNullOrEmpty(selectedDirectory))
        {
            Gamedirect.Text = selectedDirectory;
        }
    }

    //Desisntalar Button

    private async void UninstallButton_Click(object sender, RoutedEventArgs e)
    {
        string gameDirectory = Gamedirect.Text;

        if (!string.IsNullOrEmpty(gameDirectory))
        {
            try
            {
                string[] fileNames = { "dxgi.dll", "nvngx.dll", "lfz.sl.dlss.dll", "winmm.dll", "winmm.ini", "version.dll", "FSR2FSR3.asi", "fsr2fsr3.config.toml", "_nvngx.dll", "fsr2fsr3.log",
                                     "Uniscaler.asi", "uniscaler.config.toml", "default-fsr2fsr3.config.toml" };

                string[] DirectoryNames = { "uniscaler", "optional_nvngx" };

                foreach (string fileName in fileNames)
                {
                    string filePath = Path.Combine(gameDirectory, fileName);

                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
                foreach (string Directorys in DirectoryNames)
                {
                    string DirectoryPath = Path.Combine(gameDirectory, Directorys);

                    if (Directory.Exists(DirectoryPath))
                    {
                        Directory.Delete(DirectoryPath, true);
                    }
                }

                var box = MessageBoxManager.GetMessageBoxStandard("Sucessfully", "Uninstallation Completed Successfully!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
                await box.ShowAsync();
            }
            catch (Exception ex)
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Error", $"No Files Found: {ex.Message}", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await box.ShowAsync();
            }

        }
        else
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Select Game Directory!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();

        }
    }




    //Aplica o mod

    private async void ApplyButton_Click(object sender, RoutedEventArgs e)
    {
        // Obtenha a opção selecionada
        string opcaoSelecionada = gamecombobox.SelectedItem?.ToString();

        // Obtenha a opção selecionada
        string VersaoSelecionada = ModVersionComboBox.SelectedItem?.ToString();

        // operation mode
        string Sopmode = OpMode.SelectedItem?.ToString();
        string Selectopmode = "";
        string NomeTOML = "";




        // Verifica se uma mod version foi selecionada

        if (ModVersionComboBox.SelectedItem != null && !string.IsNullOrEmpty(ModVersionComboBox.SelectedItem.ToString()))
        {

        }
        else
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Download Mod Version!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();

            ConfigButton_Click(ConfigButton, new RoutedEventArgs());
            return;
        }


        // Verifica se a opção selecionada é nula ou vazia
        if (string.IsNullOrEmpty(opcaoSelecionada))
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Select A Game !", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();
            return;
        }



        // Chame o método CopiarArquivos passando a opção selecionada
        if (Gamedirect.Text != "")
        {
            if (Directory.Exists(Gamedirect.Text))
            {
                CopiarArquivos(opcaoSelecionada, VersaoSelecionada);

                if (NVNGX_Stub.IsChecked == true)
                {
                    NVNGX_Click(InstallNvngx, new RoutedEventArgs ());
                }

                if (DxgiSwitch.IsChecked == true)
                {
                    dxgi();
                }

            }
            else
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Error", "Select Valid Directory!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await box.ShowAsync();
            }
        }
        else
        {

            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Select Game Directory!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();

        }

        if(gamecombobox.SelectedItem.ToString() == "Hellblade II")
        {
            H2Fix();
        }


        // Obtém o caminho do diretório selecionado
        string caminhoDiretorio = Gamedirect.Text;

        // Verifica se o diretório não está vazio
        if (!string.IsNullOrEmpty(caminhoDiretorio))
        {
            // Cria o conteúdo com base nos valores dos botões de alternância
            var UltraQualityValue = Convert.ToDecimal(UltraQualityNumeric.Text, CultureInfo.InvariantCulture) / 100;
            var QualityValue = Convert.ToDecimal(QualityNumeric.Text, CultureInfo.InvariantCulture) / 100;
            var BalancedValue = Convert.ToDecimal(BalancedNumeric.Text, CultureInfo.InvariantCulture) / 100;
            var PerformanceValue = Convert.ToDecimal(PerformanceNumeric.Text, CultureInfo.InvariantCulture) / 100;
            var UltraPerformanceValue = Convert.ToDecimal(UltraPerformanceNumeric.Text, CultureInfo.InvariantCulture) / 100;


            //shapenesss
            CultureInfo culture = CultureInfo.InvariantCulture;

            double valorSlider = Convert.ToDouble(SharpnessSlider.Value == 0 ? -1.0 : SharpnessSlider.Value, culture);

            string conteudo = "";

            string ModVersion = ModVersionComboBox.SelectedItem?.ToString();


            // TOML do FSR2FSR3

            if (ModVersion == "0.9.0" || ModVersion == "0.10.0" || ModVersion == "0.10.1h1" ||
               ModVersion == "0.10.2h1" || ModVersion == "0.10.3" || ModVersion == "0.10.4")
            {
                //Recebe o opmode do fsr2fsr3

                switch (Sopmode)
                {
                    case "Default":
                        Selectopmode = "\"default\"";
                        break;

                    case "Disable FG":
                        Selectopmode = "\"enable_upscaling_only\"";
                        break;

                    case "Replace Dlss FG":
                        Selectopmode = "\"replace_dlss_fg\"";
                        break;

                    case "Use Game Upscaling":
                        Selectopmode = "\"use_game_upscaling\"";
                        break;
                }

                NomeTOML = "fsr2fsr3.config.toml";

                conteudo =
                $"fake_nvidia_gpu = {FakeGpuSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n\n" +
                "[general]\n" +
                "#Changes how the mod operates. Allowed values: default, enable_upscaling_only, use_game_upscaling, replace_dlss_fg \n" +
                (
                    ModVersionComboBox.SelectedItem?.ToString() != "0.8.0" ||
                    ModVersionComboBox.SelectedItem?.ToString() != "0.9.0" ?
                    $"Mode = {Selectopmode}  \n" : ""
                ) +

                $"sharpness_override = {valorSlider.ToString("F1", CultureInfo.InvariantCulture)} \n\n" +
                "[resolution_override] \n" +
                $"native = 1.0 \n" +
                $"quality = {QualityValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"balanced = {BalancedValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"performance = {PerformanceValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"ultra_performance = {UltraPerformanceValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"ultra_quality = {UltraQualityValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +

                (
                    ModVersionComboBox.SelectedItem?.ToString() == "0.8.0" || // Adicione todas as opções desejadas aqui
                    ModVersionComboBox.SelectedItem?.ToString() == "0.9.0" ?
                    $"enable_upscaling_only = {DisableFgSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n\n" : ""
                ) +

                "[compatibility]\n" +
                $"fake_nvapi_results = {NvapiSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n" +
                $"macos_crossover_support = {MacCrossoverSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n" +
                $"amd_unreal_engine_dlss_workaround = {UnrealAmdSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n\n" +
                "[debug]\n" +
                $"enable_debug_view = {DebuggerSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n" +
                $"enable_debug_tear_lines = {DebuggerSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}";
            }

            // TOML do Uniscaler
            else
            {
                // Opmode/upscaler do Uniscaler

                switch (Sopmode)
                {
                    case "Fsr3":
                        Selectopmode = "\"fsr3\"";
                        break;

                    case "Dlss":
                        Selectopmode = "\"dlss\"";
                        break;

                    case "Xess":
                        Selectopmode = "\"xess\"";
                        break;
                }

                NomeTOML = "uniscaler.config.toml";

                conteudo =
                $"config_version = 4 \n\n" +

                $"[general]\n" +
                $"upscaler = {Selectopmode}\n" +
                $"disable_frame_generation = {DisableFgSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()} \n" +
                $"sharpness_override = {valorSlider.ToString("F1", CultureInfo.InvariantCulture)} \n\n" +

                $"[resolution_override]\n" +
                $"native = 1.0 \n" +
                $"quality = {QualityValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"balanced = {BalancedValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"performance = {PerformanceValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"ultra_performance = {UltraPerformanceValue.ToString("F2", CultureInfo.InvariantCulture)}\n" +
                $"ultra_quality = {UltraQualityValue.ToString("F2", CultureInfo.InvariantCulture)}\n\n" +

                $"[compatibility]\n" +
                $"fake_nvidia_gpu = {FakeGpuSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n" +
                $"fake_nvapi_results = {NvapiSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n" +
                $"amd_unreal_engine_dlss_workaround = {UnrealAmdSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n" +
                $"macos_crossover_support = {MacCrossoverSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n\n" +

                $"[logging] \n" +
                $"disable_console = false \n\n" +

                $"[debug] \n" +
                $"enable_debug_view = {DebuggerSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}\n" +
                $"enable_debug_tear_lines = {DebuggerSwitch.IsChecked.GetValueOrDefault().ToString().ToLower()}";

            }




            // Combina o caminho do diretório com o nome do arquivo
            string caminhoCompleto = Path.Combine(caminhoDiretorio, NomeTOML);

            try
            {
                // Escreve o conteúdo no arquivo de texto
                File.WriteAllText(caminhoCompleto, conteudo);
                var box = MessageBoxManager.GetMessageBoxStandard("Sucessfully!", "Installation completed!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
                await box.ShowAsync();
                // Não exibe mensagem de sucesso
            }
            catch (Exception ex)
            {
                // Não exibe mensagem de erro
            }
        }
    }

    private void ConfigButton_Click1(object? sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }



    // /////////////////////////////////                               CONTROLE CENTRAL                    ////////////////////////////////////////////////
    // ////////////////////////////////                                       DA                          ////////////////////////////////////////////////
    // ///////////////////////////////                                 INSTALAÇAO DO MOD                  ////////////////////////////////////////////////



    private async void CopiarArquivos(string opcaoSelecionada, string VersaoSelecionada)
    {
        //Multiplas versoes



        string CommonFiles = ("");
        string fsr20 = ("");
        string fsr21 = ("");
        string fsr22 = ("");
        string fsrSDK = ("");
        string red2 = ("");
        string loft = ("");
        string exefile = ("");

        {



            // Verifica se a opção selecionada é nula ou vazia
            if (string.IsNullOrEmpty(VersaoSelecionada))
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Error", "Download some version of the mod!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await box.ShowAsync();
                ConfigButton.Click += ConfigButton_Click;
            }



            // Define a lista de pastas de origem com base na opção selecionada
            List<string> pastasOrigem = new List<string>();

            // Adicione as pastas de origem conforme necessário

            string ModVersion = ModVersionComboBox.SelectedItem?.ToString();

            string firstdirect = Gamedirect.Text;



            CommonFiles = ($"Fsr3Files\\{ModVersion}\\CommonFiles");
            fsr20 = ($"Fsr3Files\\{ModVersion}\\Fsr200");
            fsr21 = ($"Fsr3Files\\{ModVersion}\\Fsr210");
            fsr22 = ($"Fsr3Files\\{ModVersion}\\Fsr220");
            fsrSDK = ($"Fsr3Files\\{ModVersion}\\FsrSdk");
            red2 = ($"Fsr3Files\\{ModVersion}\\FsrRdr2");
            loft = ($"Loft");
            string ScriptHookR2D = ($"CommonFiles\\ScriptHookR2D");


            // Verifica Qual o MOD a ser usado

            if (ModVersion != "0.9.0" && ModVersion != "0.10.0" && ModVersion != "0.10.1h1" &&
                ModVersion != "0.10.2h1" && ModVersion != "0.10.3" && ModVersion != "0.10.4")

            {
                fsr20 = ($"Fsr3Files\\{ModVersion}\\Universal");
                fsr21 = ($"Fsr3Files\\{ModVersion}\\Universal");
                fsr22 = ($"Fsr3Files\\{ModVersion}\\Universal");
                fsrSDK = ($"Fsr3Files\\{ModVersion}\\Universal");
                red2 = ($"Fsr3Files\\{ModVersion}\\Universal");
            }

            try
            {
                string gameselected = (gamecombobox.SelectedItem.ToString());


                switch (gameselected)
                {

                    case "Common Fsr 2.0":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr20));
                        // Adicione mais pastas, se necessário
                        break;

                    case "Common Fsr 2.1":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        // Adicione mais pastas, se necessário
                        break;

                    case "Common Fsr 2.2":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        break;

                    case "Common Fsr SDK":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsrSDK));
                        break;

                    case "Red Dead Redemption 2":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, red2));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, ScriptHookR2D));
                        exefile = "RDR2.exe";
                        break;

                    case "Alan Wake 2":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "AlanWake2.exe";
                        break;

                    case "Cyberpunk 2077":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        exefile = "Cyberpunk2077.exe";
                        break;

                    case "Hogwarts Legacy":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        Gamedirect.Text = Path.Combine(Gamedirect.Text, "Phoenix\\Binaries\\Win64");
                        if (!Directory.Exists(Gamedirect.Text))
                        {
                            Gamedirect.Text = firstdirect;
                        }

                        break;

                    case "Ratchet & Clank":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsrSDK));
                        exefile = "RiftApart.exe";
                        break;

                    case "Remnant II":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "Remnant2-WinGDK-Shipping.exe";
                        break;


                    case "Starfield":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "Starfield.exe";
                        break;

                    case "The Last of Us Part I":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        exefile = "tlou-i.exe";
                        break;

                    case "The Witcher 3":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr20));
                        Gamedirect.Text = Path.Combine(Gamedirect.Text, "bin\\x64_dx12");
                        if (!Directory.Exists(Gamedirect.Text))
                        {
                            Gamedirect.Text = firstdirect;
                        }

                        break;

                    case "Dead Space":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        exefile = "Dead Space.exe";
                        break;

                    case "Hellblade II":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "Hellblade2-Win64-Shipping.exe";
                        break;

                    case "Horizon Zero Dawn":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "HorizonZeroDawn.exe";
                        break;

                    case "Horizon Forbidden West":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "HorizonForbiddenWest.exe";
                        break;

                    case "Ghostwire":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        Gamedirect.Text = Path.Combine(Gamedirect.Text, "Snowfall\\Binaries\\Win64");
                        if (!Directory.Exists(Gamedirect.Text))
                        {
                            Gamedirect.Text = firstdirect;
                        }

                        break;

                    case "Lies of P":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "LOP-Win64-Shipping.exe";
                        break;

                    case "Star Wars Jedi: Survivor":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        break;

                    case "A Plague Tale Requiem":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "APlagueTaleRequiem_x64.exe";
                        break;

                    case "Marvel's Spider-Man: Miles Morales":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "MilesMorales.exe";
                        break;

                    case "Marvel's Spider-Man Remastered":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "Spider-Man.exe";
                        break;

                    case "Assassin's Creed Mirage":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "ACMirage.exe";

                        break;

                    case "Atomic Heart":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "AtomicHeart-Win64-Shipping.exe";
                        break;

                    case "Dying Light 2":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr20));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "DyingLightGame_x64_nwdi.exe";
                        break;

                    case "UNCHARTED":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "u4.exe";
                        break;

                    case "RoboCop: Rogue City":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "RoboCop-Win64-Shipping.exe";
                        break;

                    case "Metro Exodus Enhanced":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "MetroExodus.exe";
                        break;

                    case "The Invincibles":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "TheInvincible-Win64-Shipping.exe";
                        break;

                    case "Gotham Knights":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        Gamedirect.Text = Path.Combine(Gamedirect.Text, "Mercury\\Binaries\\Win64");
                        if (!Directory.Exists(Gamedirect.Text))
                        {
                            Gamedirect.Text = firstdirect;
                        }

                        break;

                    case "Marvel Guardians of The Galaxy":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr20));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "gotg.exe";
                        break;

                    case "Dead Island 2":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "DeadIsland-Win64-Shipping.exe";
                        break;

                    case "Ready or Not":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr21));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "ReadyOrNot-Win64-Shipping.exe";
                        break;

                    case "Lords of The fallen":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, loft));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        exefile = "LOTF2-Win64-Shipping.exe";
                        break;

                    case "PalWord":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsrSDK));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "Palword-Win64-Shipping.exe";
                        break;

                    case "Banishers: Ghost of New Eden":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "Banishers-Win64-Shipping.exe";
                        break;

                    case "Pacific Drive":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "PenDriverPro-Win64-Shipping.exe";
                        break;


                    case "Dakar Desert Rally":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "Dakar2Game-Win64-Shipping.exe";
                        break;

                    case "Satisfactory":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr20));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "FactoryGame-Win64-Shipping.exe";
                        break;

                    case "The Medium":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "Medium-Win64-Shipping.exe";
                        break;

                    case "Dragons Dogma 2":
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, fsr22));
                        pastasOrigem.Add(Path.Combine(diretorioOrigem, CommonFiles));
                        exefile = "DD2.exe";
                        break;

                    // Adicione mais pastas, se necessário

                    default:
                        var box = MessageBoxManager.GetMessageBoxStandard("Error", $"Invalid Option", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                        await box.ShowAsync();
                        return;
                }



                string diretorioInicial = Gamedirect.Text;
                string nomeArquivoProcurado = exefile;

                // Chame um método para buscar o arquivo, passando também a substring desejada
                string caminhoArquivoEncontrado = BuscarArquivo(diretorioInicial, nomeArquivoProcurado, "-Shipping");

                if (!string.IsNullOrEmpty(caminhoArquivoEncontrado))
                {
                    string diretorioDoArquivo = Path.GetDirectoryName(caminhoArquivoEncontrado);
                    Gamedirect.Text = diretorioDoArquivo;

                }



                // Itera sobre a lista de pastas de origem
                string completo = null;

                foreach (string pastaOrigem in pastasOrigem)
                {
                    // Verifica se a pasta de origem existe
                    if (!Directory.Exists(pastaOrigem))
                    {
                        var box = MessageBoxManager.GetMessageBoxStandard("Error", $"Folder{pastaOrigem} Not Found", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                        await box.ShowAsync();
                        return;
                    }

                    // Obtém a lista de arquivos na pasta de origem
                    string[] nomesArquivos = Directory.GetFiles(pastaOrigem);
                    string[] nomesPastas = Directory.GetDirectories(pastaOrigem);

                    List<string> todosNomes = new List<string>();
                    todosNomes.AddRange(nomesArquivos);
                    todosNomes.AddRange(nomesPastas);

                    // Itera sobre a lista de nomes de arquivos
                    foreach (string nomeArquivoOrigem in todosNomes)
                    {
                        // Verifica se o nome do arquivo não está vazio ou nulo
                        if (string.IsNullOrEmpty(nomeArquivoOrigem))
                        {
                            var box = MessageBoxManager.GetMessageBoxStandard("Error", "File Name Invalid !", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                            await box.ShowAsync();
                            return;
                        }

                        // Cria o caminho completo do arquivo na pasta de destino
                        string caminhoDestino = Path.Combine(Gamedirect.Text, Path.GetFileName(nomeArquivoOrigem));

                        try
                        {
                            if (Directory.Exists(nomeArquivoOrigem)) // Verifica se a pasta de origem existe
                            {
                                // Cria a pasta de destino se ainda não existir
                                if (!Directory.Exists(caminhoDestino))
                                {
                                    Directory.CreateDirectory(caminhoDestino);
                                }

                                // Copia a pasta de origem para a pasta de destino
                                CopiarPastaRecursiva(nomeArquivoOrigem, caminhoDestino);
                            }
                            else if (File.Exists(nomeArquivoOrigem)) // Verifica se o arquivo de origem existe
                            {
                                // Copia o arquivo de origem para o destino
                                File.Copy(nomeArquivoOrigem, caminhoDestino, true);
                            }
                            else // Se nem a pasta nem o arquivo forem encontrados
                            {
                                var box = MessageBoxManager.GetMessageBoxStandard("Error", "Not Found in Internal Folder !", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                                await box.ShowAsync();
                                return; // Se um arquivo não for encontrado, interrompe imediatamente
                            }
                        }
                        catch (Exception ex)
                        {
                            // Se ocorrer um erro durante o processo de cópia, exiba a mensagem de erro
                            var box = MessageBoxManager.GetMessageBoxStandard("Error", $"An error occurred while copying files: {ex.Message}", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                            await box.ShowAsync();
                        }

                    }


                }
            }
            catch { }
        }

    }


    private static void CopiarPastaRecursiva(string origem, string destino)
    {
        // Cria a pasta de destino se ainda não existir
        if (!Directory.Exists(destino))
        {
            Directory.CreateDirectory(destino);
        }

        // Obtém todos os arquivos na pasta de origem e copia-os para a pasta de destino
        string[] arquivos = Directory.GetFiles(origem);
        foreach (string arquivo in arquivos)
        {
            string nomeArquivo = Path.GetFileName(arquivo);
            string caminhoDestinoArquivo = Path.Combine(destino, nomeArquivo);
            File.Copy(arquivo, caminhoDestinoArquivo, true);
        }

        // Obtém todas as subpastas na pasta de origem e copia-as para a pasta de destino
        string[] subpastas = Directory.GetDirectories(origem);
        foreach (string subpasta in subpastas)
        {
            string nomeSubpasta = new DirectoryInfo(subpasta).Name;
            string caminhoDestinoSubpasta = Path.Combine(destino, nomeSubpasta);
            CopiarPastaRecursiva(subpasta, caminhoDestinoSubpasta);
        }
    }


    // Scanner De SubPastas

    private string BuscarArquivo(string diretorio, string nomeArquivo, string substringAdicional)
    {
        try
        {
            // Tenta encontrar o arquivo exatamente correspondente
            var arquivoEncontrado = Directory.GetFiles(diretorio, $"{nomeArquivo}", SearchOption.AllDirectories).FirstOrDefault();

            // Se não encontrar, tenta encontrar um arquivo com a substring adicional
            if (string.IsNullOrEmpty(arquivoEncontrado))
            {
                var arquivosComSubstring = Directory.GetFiles(diretorio, $"*{substringAdicional}*", SearchOption.AllDirectories);

                // Verifica se algum arquivo com a substring foi encontrado
                if (arquivosComSubstring.Length > 0)
                {
                    // Retorna o primeiro arquivo encontrado com a substring
                    return arquivosComSubstring[0];
                }
                else
                {
                    return null;
                }
            }

            return arquivoEncontrado;
        }
        catch (Exception ex)
        {

            return null;
        }
    }



    // /////////////////////////////////                                                             ////////////////////////////////////////////////
    // ////////////////////////////////                                 FIM                          ////////////////////////////////////////////////
    // ///////////////////////////////                                                                  ////////////////////////////////////////////////



    //Disabilita o Overlay da EpicGames

    private async void EpicGamesOverlayButton_Click(object sender, RoutedEventArgs e)
    {
        string OverlayPath = "C:\\Program Files (x86)\\Epic Games\\Launcher\\Portal\\Extras\\Overlay";

        if (!Directory.Exists(OverlayPath))
        {
            // Cria uma janela de diálogo para selecionar o diretório
            OpenFolderDialog folderDialog = new OpenFolderDialog();
            folderDialog.Title = "Selecione o diretório para excluir os arquivos.";

            // Mostra o diálogo e espera pelo resultado
            var result = await folderDialog.ShowAsync(this);

            // Verifica se o usuário selecionou um diretório
            if (!string.IsNullOrWhiteSpace(result))
            {
                OverlayPath = Path.Combine(result, "Overlay");
            }
            else
            {
                // Usuário cancelou a seleção do diretório
                return;
            }
        }

        try
        {
            ExcluirArquivo(Path.Combine(OverlayPath, "EOSOverlayRenderer-Win32-Shipping.exe"));
            ExcluirArquivo(Path.Combine(OverlayPath, "EOSOverlayRenderer-Win64-Shipping.exe"));

            var box = MessageBoxManager.GetMessageBoxStandard("Succesfully!", "Epic Games Overlay Disabled !", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
            await box.ShowAsync();
        }
        catch (Exception ex)
        {

            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Error When Disabling Overlay", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();

        }
    }

    private void ExcluirArquivo(string caminhooverlay)
    {
        if (File.Exists(caminhooverlay))
        {
            File.Delete(caminhooverlay);
        }
    }

    private void ExcluirTodosArquivos(string diretoriooverlay)
    {
        if (Directory.Exists(diretoriooverlay))
        {
            DirectoryInfo di = new DirectoryInfo(diretoriooverlay);

            foreach (FileInfo arquivo in di.GetFiles())
            {
                arquivo.Delete();
            }

            foreach (DirectoryInfo suboverlay in di.GetDirectories())
            {
                suboverlay.Delete(true);
            }
        }
    }






    //Instala o NVNGX

    private async void NVNGX_Click(object sender, RoutedEventArgs e)
    {
        if (Gamedirect.Text != "")
        {
            if (Directory.Exists(Gamedirect.Text))
            {
                // Obtém o diretório do executável
                string exelocate = AppDomain.CurrentDomain.BaseDirectory;

                //Instala o regedit do NVNGX
                string caminhoArquivoReg = Path.Combine(exelocate, "Fsr3Files", "reg", "EnableSignatureOverride.reg");


                try
                {


                    // Caminho da pasta de origem
                    string data = Path.Combine(exelocate, "Fsr3Files", "NVNGX");

                    string[] arquivos = Directory.GetFiles(data);

                    foreach (string arquivo in arquivos)
                    {
                        string nomeArquivo = Path.GetFileName(arquivo);
                        string caminhoDestino = Path.Combine(Gamedirect.Text, nomeArquivo);

                        File.Copy(arquivo, caminhoDestino, true);
                    }

                    Process.Start("regedit.exe", $@"/s ""{caminhoArquivoReg}""");

                    var box = MessageBoxManager.GetMessageBoxStandard("Succesfully!", "NVNGX Installed !", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
                    await box.ShowAsync();
                }
                catch
                {
                    var box = MessageBoxManager.GetMessageBoxStandard("Error", "Run The Program As Administrator!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                    await box.ShowAsync();

                }
            }
            else
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Error", "Select Valid Directory!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await box.ShowAsync();
            }
        }
        else
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Select Game Directory!", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();
        }

    }

    public void H2Fix()
    {
        string Origin_Directory = Path.Combine(Directory.GetCurrentDirectory(), "Fsr3Files", "H2Fix", "Engine.ini");
        string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        string configDirectoryPath = Path.Combine(localAppDataPath, "Hellblade2", "Saved", "Config", "Windows");

        if(Directory.Exists(configDirectoryPath))
        {
            string destinationFilePath = Path.Combine(configDirectoryPath, "Engine.ini");

            try
            {
                File.Copy(Origin_Directory, destinationFilePath);
            }
            catch { }
        }

    }


  



    // ////////////////////////////// Sistema de Controle Da key API /////////////////////////////////////////////



    private async void LoginModVersions_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            // Valor da string a ser armazenada no arquivo
            string KeyASerGravada = KeyDoUser.Text;
            SaveVersionsToFileAsync();
            FillComboBoxWithVersionsAsync();
            ComboBoxModVersionDownload.SelectedIndex = 0;




            // Caminho do arquivo
            string CaminhoKeyaSerGravada = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config");

            if (!Directory.Exists(CaminhoKeyaSerGravada))
            {
                Directory.CreateDirectory(CaminhoKeyaSerGravada);

                using (StreamWriter writer = File.CreateText(Path.Combine(CaminhoKeyaSerGravada, "Config.adx")))
                {
                    writer.Write(KeyASerGravada);
                }
            }
            else
            {
                using (StreamWriter writer = File.CreateText(Path.Combine(CaminhoKeyaSerGravada, "Config.adx")))
                {
                    writer.Write(KeyASerGravada);
                }
            }

        }

        catch
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Invalid Api Key", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();

        }


    }

    // Recebe As versoes Disponiveis Do Mod em um .txt


    public async Task SaveVersionsToFileAsync()
    {
        try
        {
            // Obtém as versões disponíveis do mod
            using var api = new ModApi();

            await api.LoginAsync(KeyDoUser.Text);

            var ModTypes = new List<ModType>
                {
                   ModType.Uniscaler,
                   ModType.FSR2FSR3
                };


            var box = MessageBoxManager.GetMessageBoxStandard("Succesfully!", "Login Successful", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
            await box.ShowAsync();

            DawloadModVersionButton.IsEnabled = true;
            ComboBoxModVersionDownload.IsEnabled = true;
            ComboBoxModVersionDownload.SelectedIndex = 0;

            // Cria um StreamWriter para escrever no arquivo
            String ModVersionsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config\\ModVersions.txt");
            using (StreamWriter writer = File.CreateText(ModVersionsDirectory))
            {
                foreach (ModType TiposMod in ModTypes)
                {
                    var mod_versions = await api.GetVersionsAsync(TiposMod);


                    foreach (ModInfo item in mod_versions)
                    {
                        if (TiposMod == LibModAuth.ModType.Uniscaler)
                        {

                            // Escreve cada versão no arquivo
                            await writer.WriteLineAsync($"Uniscaler  {item.Version}");

                        }

                        else if (TiposMod == LibModAuth.ModType.FSR2FSR3)
                        {
                            // Escreve cada versão no arquivo
                            await writer.WriteLineAsync($"FSR2FSR3  {item.Version}");
                        }

                    }
                }
            }

            FillComboBoxWithVersionsAsync();
            ComboBoxModVersionDownload.SelectedIndex = 0;
        }
        catch
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Failed to connect to API", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();

        }
    }


    // Combobox Recebe as versoes Disponiveis

    public async Task FillComboBoxWithVersionsAsync()
    {
        try
        {
            String ModVersionsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config\\ModVersions.txt");
            // Verifica se o arquivo existe


            // Limpa a ComboBox antes de adicionar as novas versões
            ComboBoxModVersionDownload.Items.Clear();

            // Lê todas as linhas do arquivo
            string[] lines = await File.ReadAllLinesAsync(ModVersionsDirectory);

            // Adiciona cada linha (que representa uma versão) à ComboBox
            foreach (string line in lines)
            {
                ComboBoxModVersionDownload.Items.Add(line);
            }
            ComboBoxModVersionDownload.SelectedIndex = 0;
        }
        catch (Exception ex)
        {

        }
    }



    private async void DawloadModVersionButton_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            PopUpDownload.IsVisible = true;

            DawloadModVersionButton.IsEnabled = false;
            HomeButton.IsEnabled = false;
            ConfigButton.IsEnabled = false;
            AboutButton.IsEnabled = false;
            TemaButton.IsEnabled = false;


            // Lê o arquivo txt para logar
            string keyLocal = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config\\Config.adx");
            string chaveSalva = File.ReadAllText(keyLocal);

            using (var api = new ModApi())
            {
                await api.LoginAsync(chaveSalva);

                // Obtém a versão selecionada na combobox
                string selectedVersion = ComboBoxModVersionDownload.SelectedItem?.ToString();

                string selectedVersionComboBox = selectedVersion;



                if (selectedVersion != null)
                {

                    // Declara a lista de variantes
                    var modVariants = new List<ModVariant> { };

                    if (selectedVersionComboBox.IndexOf("FSR2FSR3", StringComparison.OrdinalIgnoreCase) >= 0)
                    {

                        // Lista de variantes do mod que você deseja baixar FSR2FSR3
                        modVariants.AddRange(new List<ModVariant>
                            {
                            ModVariant.Fsr200,
                            ModVariant.Fsr210,
                            ModVariant.Fsr220,
                            ModVariant.FsrSdk,
                            ModVariant.FsrRdr2,
                            ModVariant.CommonFiles
                            });

                        selectedVersion = selectedVersion.Replace("Uniscaler  ", "").Replace("FSR2FSR3  ", "");

                    }
                    else if (selectedVersionComboBox.IndexOf("Uniscaler", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        // Lista de variantes do mod que você deseja baixar Uniscaler
                        modVariants.AddRange(new List<ModVariant>
                            {
                            ModVariant.CommonFiles,
                            ModVariant.Universal
                            });

                        selectedVersion = selectedVersion.Replace("Uniscaler  ", "").Replace("FSR2FSR3  ", "");
                    }

                    // Diretório de destino para o download
                    string destinationDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"Fsr3Files\\Fsr3Files\\{selectedVersion}");

                    if (Directory.Exists(destinationDirectory))
                    {
                        Directory.Delete(destinationDirectory, true);
                    }

                    // Cria o diretório de destino, se necessário
                    Directory.CreateDirectory(destinationDirectory);

                    //Baixa o Mod dependendo do ModType

                    if (selectedVersionComboBox.IndexOf("Uniscaler", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var TipoDeMod = LibModAuth.ModType.Uniscaler;

                        // Download e extração do mod para cada variante
                        foreach (var variant in modVariants)
                        {
                            await DownloadAndExtractModAsync(api, selectedVersion, variant, destinationDirectory, TipoDeMod);
                        }

                    }
                    else if (selectedVersionComboBox.IndexOf("FSR2FSR3", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var TipoDeMod = LibModAuth.ModType.FSR2FSR3;

                        // Download e extração do mod para cada variante
                        foreach (var variant in modVariants)
                        {
                            await DownloadAndExtractModAsync(api, selectedVersion, variant, destinationDirectory, TipoDeMod);
                        }
                    }


                    var box = MessageBoxManager.GetMessageBoxStandard("Succesfully!", "Mod Download Completed", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Success);
                    await box.ShowAsync();

                    PreencherComboBox();
                    ModVersionComboBox.SelectedIndex = 0;
                    //Adiciona as versoes no MOdversion

                    PastaNomes = new List<string>();

                    // Diretório que contém as pastas
                    string diretorio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Fsr3Files");

                    // Verifica se o diretório existe
                    if (Directory.Exists(diretorio))
                    {
                        try
                        {
                            // Obtém os nomes de todas as pastas dentro do diretório
                            string[] nomesPastas = Directory.GetDirectories(diretorio)
                                                             .Select(Path.GetFileName)
                                                             .ToArray();

                            // Adiciona os nomes das pastas à lista
                            PastaNomes.AddRange(nomesPastas);

                            // Caminho para o arquivo onde os nomes das pastas serão escritos
                            string caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config");
                            string caminhoArquivoCompleto = Path.Combine(caminhoArquivo, "ModversionCB.txt");

                            // Escreve os nomes das pastas no arquivo de texto
                            using (StreamWriter writer = new StreamWriter(caminhoArquivoCompleto))
                            {
                                foreach (string nomePasta in nomesPastas)
                                {
                                    writer.WriteLine(nomePasta);
                                }
                            }

                            PreencherComboBox();
                            ModVersionComboBox.SelectedIndex = 0;


                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }

        }
        catch (Exception ex)
        {
            var box = MessageBoxManager.GetMessageBoxStandard("Error", "Failed To Download Mod", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
            await box.ShowAsync();
            PopUpDownload.IsVisible = false;
            DawloadModVersionButton.IsEnabled = true;
            HomeButton.IsEnabled = true;
            ConfigButton.IsEnabled = true;
            AboutButton.IsEnabled = true;
            TemaButton.IsEnabled = true;
        }

        PopUpDownload.IsVisible = false;
        DawloadModVersionButton.IsEnabled = true;
        HomeButton.IsEnabled = true;
        ConfigButton.IsEnabled = true;
        AboutButton.IsEnabled = true;
        TemaButton.IsEnabled = true;
    }

    // Função para download e extração do mod
    private async Task DownloadAndExtractModAsync(ModApi api, string version, ModVariant variant, string destinationDirectory, ModType TipoDeMod)
    {
        var download = await api.DownloadModAsync(TipoDeMod, version, variant);

        // Constrói o caminho completo do arquivo zip usando o nome da variante
        string zipFilePath = Path.Combine(destinationDirectory, $"{variant.ToString()}.zip");

        // Usa FileStream para escrever em segundo plano
        using (var fileStream = new FileStream(zipFilePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
        {
            await download.CopyToAsync(fileStream);
        }

        // Extrai o arquivo zip para o diretório de destino
        string extractDirectory = Path.Combine(destinationDirectory, $"{variant.ToString()}");
        Directory.CreateDirectory(extractDirectory);
        ZipFile.ExtractToDirectory(zipFilePath, extractDirectory);

        // Opcional: exclui o arquivo zip após a extração
        File.Delete(zipFilePath);
    }




    public void PreencherComboBox()
    {
        // Caminho para o arquivo onde os nomes das pastas estão armazenados
        string caminhoArquivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config\\ModversionCB.txt");

        try
        {
            // Verifica se o arquivo existe
            if (File.Exists(caminhoArquivo))
            {
                // Lê os nomes das pastas do arquivo
                string[] nomesPastas = File.ReadAllLines(caminhoArquivo);

                ModVersionComboBox.Items.Clear();

                // Adiciona os nomes das pastas ao ComboBox
                foreach (string nomePasta in nomesPastas)
                {
                    ModVersionComboBox.Items.Add(nomePasta);
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }
    }



    /////////FIMMMMM////////////////// Funçoes Para Facilitar Download Do mod  //////////////////FIM////////////////



    // Install dxgi

    public async Task dxgi()
    {
        // Caminho da dll

        string CaminhoDLL = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\CommonFiles\\Dxgi\\dxgi.dll");



        // Cria o caminho completo do arquivo na pasta de destino
        string caminhoDestino = Path.Combine(Gamedirect.Text, Path.GetFileName(CaminhoDLL));

        try
        {

            if (File.Exists(CaminhoDLL))
            {
                File.Copy(CaminhoDLL, caminhoDestino, true);



            }
            else
            {
                var box = MessageBoxManager.GetMessageBoxStandard("Error", "Not Found dxgi.dll !", ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Error);
                await box.ShowAsync();
                return; // Se um arquivo não for encontrado, interrompe imediatamente
            }
        }
        catch { }


    }





    //  Desabilita funçoes de acordo com a versao do mod 

    public void DisableFuctions(object sender, SelectionChangedEventArgs e)
    {
        string ModVersion = ModVersionComboBox.SelectedItem?.ToString();

        if (ModVersionComboBox.SelectedItem?.ToString() == "0.10.0" || ModVersionComboBox.SelectedItem?.ToString() == "0.10.1h1" ||
            ModVersionComboBox.SelectedItem?.ToString() == "0.10.2h1" || ModVersionComboBox.SelectedItem?.ToString() == "0.10.3" ||
            ModVersionComboBox.SelectedItem?.ToString() == "0.10.4")

        {
            DisableFgSwitch.IsEnabled = false;
        }

        else
        {
            DisableFgSwitch.IsEnabled = true;
        }


        if (ModVersionComboBox.SelectedItem?.ToString() == "0.8.0" || ModVersionComboBox.SelectedItem?.ToString() == "0.9.0")
        {
            OpMode.IsEnabled = false;
            DisableFgSwitch.IsEnabled = true;
        }
        else
        {
            OpMode.IsEnabled = true;
        }

        if (ModVersion != "0.9.0" && ModVersion != "0.10.0" && ModVersion != "0.10.1h1" &&
            ModVersion != "0.10.2h1" && ModVersion != "0.10.3" && ModVersion != "0.10.4")
        {
            OpModeTEXT.Text = "Upscaler";

            OpMode.Items.Clear();
            OpMode.Items.Add("Fsr3");
            OpMode.Items.Add("Dlss");
            OpMode.Items.Add("Xess");
            OpMode.SelectedIndex = 0;

        }
        else
        {
            OpModeTEXT.Text = "O.P Mode";

            OpMode.Items.Clear();
            OpMode.Items.Add("Default");
            OpMode.Items.Add("Disable FG");
            OpMode.Items.Add("Replace Dlss FG");
            OpMode.Items.Add("Use Game Upscaling");
            OpMode.SelectedIndex = 0;

        }


    }



    public async Task DisableFuctions_INCIAL()

    {
        string ModVersion = ModVersionComboBox.SelectedItem?.ToString();

        if (ModVersionComboBox.SelectedItem?.ToString() == "0.10.0" || ModVersionComboBox.SelectedItem?.ToString() == "0.10.1h1" ||
            ModVersionComboBox.SelectedItem?.ToString() == "0.10.2h1" || ModVersionComboBox.SelectedItem?.ToString() == "0.10.3" ||
            ModVersionComboBox.SelectedItem?.ToString() == "0.10.4")

        {
            DisableFgSwitch.IsEnabled = false;
        }

        else
        {
            DisableFgSwitch.IsEnabled = true;
        }


        if (ModVersionComboBox.SelectedItem?.ToString() == "0.8.0" || ModVersionComboBox.SelectedItem?.ToString() == "0.9.0")
        {
            OpMode.IsEnabled = false;
            DisableFgSwitch.IsEnabled = true;
        }
        else
        {
            OpMode.IsEnabled = true;
        }

        if (ModVersion != "0.9.0" && ModVersion != "0.10.0" && ModVersion != "0.10.1h1" &&
            ModVersion != "0.10.2h1" && ModVersion != "0.10.3" && ModVersion != "0.10.4")
        {
            OpMode.Items.Clear();
            OpMode.Items.Add("fsr3");
            OpMode.Items.Add("dlss");
            OpMode.Items.Add("xess");
            OpMode.SelectedIndex = 0;
        }
        else
        {
            OpMode.Items.Clear();
            OpMode.Items.Add("Default");
            OpMode.Items.Add("Disable FG");
            OpMode.Items.Add("Replace Dlss FG");
            OpMode.Items.Add("Use Game Upscaling");
            OpMode.SelectedIndex = 0;
        }

    }


    // Lida com a opçao de temas

    private void RadioButton1_Checked(object sender, RoutedEventArgs e)
    {
        string TemaSelecionado = "1";
        this.RequestedThemeVariant = ThemeVariant.Dark;

        //background
        SolidColorBrush backgroundColor = new SolidColorBrush(Color.FromArgb(255, 22, 23, 23));
        this.Background = backgroundColor;


        //Home Section
        SelectGameDirectoryText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        Gamedirect.Foreground = new SolidColorBrush(Color.FromArgb(255, 146, 146, 146));
        Gamedirect.Background = new SolidColorBrush(Color.FromArgb(255, 35, 35, 35));
        Gamedirect.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));
        SelectGameText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        gamecombobox.Background = new SolidColorBrush(Color.FromArgb(255, 35, 35, 35));
        gamecombobox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));
        BoxButtons.Background = new SolidColorBrush(Color.FromArgb(255, 36, 36, 36));
        BoxButtons.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 36, 36, 36));
        ExpanderButton.Background = new SolidColorBrush(Color.FromArgb(255, 43, 42, 42));
        BarraLateral.Background = new SolidColorBrush(Color.FromArgb(255, 36, 36, 36));
        AppTittle.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        UninstallButton.Background = new SolidColorBrush(Color.FromArgb(255, 98, 95, 95));
        background_SubMenu.Background = new SolidColorBrush(Color.FromArgb(255, 22, 23, 23));
        BrowseButton.Background = new SolidColorBrush(Color.FromArgb(255, 99, 99, 99));
        SubmenuBackground.Background = new SolidColorBrush(Color.FromArgb(255, 36, 36, 36));

        //Config Section
        ConfigPanelBackground.Background = new SolidColorBrush(Color.FromArgb(255, 22, 23, 23));
        BoxTextDownload.Background = new SolidColorBrush(Color.FromArgb(255, 36, 36, 36));
        ApiKeyText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        KeyDoUser.Background = new SolidColorBrush(Color.FromArgb(255, 84, 82, 82));
        KeyDoUser.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 153, 149, 149));
        ModVersionText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        ComboBoxModVersionDownload.Background = new SolidColorBrush(Color.FromArgb(255, 84, 82, 82));
        ComboBoxModVersionDownload.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 153, 149, 149));
        GetApiText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));

        //About Section
        AboutBackground.Background = new SolidColorBrush(Color.FromArgb(255, 22, 23, 23));
        AboutThisText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        AboutContentText.Foreground = new SolidColorBrush(Color.FromArgb(255, 165, 165, 165));
        OpenSourceText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        BoxAboutIcons.Background = new SolidColorBrush(Color.FromArgb(255, 37, 37, 37));

        //Appearance Section
        BackgroundTemas.Background = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
        BackgroundThemes.Background = Brushes.Transparent;



        CacheTemas(TemaSelecionado);
    
    }

    private void RadioButton2_Checked(object sender, RoutedEventArgs e)
    {

        string DiretorioLocal = Directory.GetCurrentDirectory();

        //Light

        if (tema2.IsChecked == true)
        {
            string TemaSelecionado = "2";
            this.RequestedThemeVariant = ThemeVariant.Light;

            //background
            SolidColorBrush backgroundColor = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            this.Background = backgroundColor;


            //Home Section
            SelectGameDirectoryText.Foreground = new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));
            Gamedirect.Foreground = new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));
            Gamedirect.Background = new SolidColorBrush(Color.FromArgb(255, 227, 227, 227));
            Gamedirect.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 110, 110, 110));
            SelectGameText.Foreground = new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));
            gamecombobox.Background = new SolidColorBrush(Color.FromArgb(255, 227, 227, 227));
            gamecombobox.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 110, 110, 110));
            gamecombobox.Foreground = new SolidColorBrush(Color.FromArgb(255, 165, 165, 165));
            BoxButtons.Background = new SolidColorBrush(Color.FromArgb(255, 227, 227, 227));
            BoxButtons.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 110, 110, 110));
            ExpanderButton.Background = new SolidColorBrush(Color.FromArgb(255, 161, 161, 161));
            BarraLateral.Background = new SolidColorBrush(Color.FromArgb(255, 199, 199, 199));
            AppTittle.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            UninstallButton.Background = new SolidColorBrush(Color.FromArgb(255, 161, 161, 161));
            ExpanderButton.Background = new SolidColorBrush(Color.FromArgb(255, 161, 161, 161));
            background_SubMenu.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            BrowseButton.Background = new SolidColorBrush(Color.FromArgb(255, 161, 161, 161));
            SubmenuBackground.Background = new SolidColorBrush(Color.FromArgb(255, 161, 161, 161));

            //Config Section
            ConfigPanelBackground.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            BoxTextDownload.Background = new SolidColorBrush(Color.FromArgb(255, 146, 146, 146));
            ApiKeyText.Foreground  =  new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));
            KeyDoUser.Background = new SolidColorBrush(Color.FromArgb(255, 204, 203, 203));
            KeyDoUser.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 110, 110, 110));
            ModVersionText.Foreground = new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));
            ComboBoxModVersionDownload.Background = new SolidColorBrush(Color.FromArgb(255, 204, 203, 203));
            ComboBoxModVersionDownload.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 110, 110, 110));
            GetApiText.Foreground = new SolidColorBrush(Color.FromArgb(255, 82, 82, 82));

            //About Section
            AboutBackground.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            AboutThisText.Foreground = new SolidColorBrush(Color.FromArgb(255, 83, 81, 81));
            AboutContentText.Foreground = new SolidColorBrush(Color.FromArgb(255, 103, 98, 98));
            OpenSourceText.Foreground = new SolidColorBrush(Color.FromArgb(255, 83, 81, 81));
            BoxAboutIcons.Background = new SolidColorBrush(Color.FromArgb(255, 205, 205, 205));

            //Appearance Section
            BackgroundTemas.Background = new SolidColorBrush(Color.FromArgb(255, 237, 226, 226));
            BackgroundThemes.Background = Brushes.Transparent;



            CacheTemas(TemaSelecionado);
        }
    }

    


    private void CacheTemas(string TemaSelecionado)
    {
        // Caminho do arquivo
        string CaminhoCacheNaoGravado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config");

        if (!Directory.Exists(CaminhoCacheNaoGravado))
        {
            Directory.CreateDirectory(CaminhoCacheNaoGravado);



            using (StreamWriter writer = File.CreateText(Path.Combine(CaminhoCacheNaoGravado, "ThemeCache.adx")))
            {
                writer.Write(string.Empty);
                writer.Write(TemaSelecionado);
            }
        }
        else
        {
            using (StreamWriter writer = File.CreateText(Path.Combine(CaminhoCacheNaoGravado, "ThemeCache.adx")))
            {
                writer.Write(string.Empty);
                writer.Write(TemaSelecionado);
            }
        }

    }

    private void LerCacheTemas()
    {
        try
        {
            string TemaEscolhido = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Fsr3Files\\Config\\ThemeCache.adx"));


            // tema dark

            if (TemaEscolhido == "1")
            {
                tema1.IsChecked = true;
                
            }
            // tema branco
            else if (TemaEscolhido == "2")
            {
                tema2.IsChecked = true;
                
            }
            


        }
        catch { tema1.IsChecked = true; }


    }





    // Opçao de Idiomas


    private void Ingles_Checked(object sender, RoutedEventArgs e)
    {

        //Ingles

        if (IdiomaIngles.IsChecked == true)
        {
            string IdiomaSelecionado = "1";

            //Mudanças Abaixo
            SelectGameDirectoryText.Text = "Game Directory";
            SelectGameDirectoryText.Margin = new Thickness(264, -310, 0, 10);
            Gamedirect.Watermark = "Select Game Directory";
            SelectGameText.Text = "Select a Game";
            SelectGameText.Margin = new Thickness(270, -160, 0, 0);
            UninstallButton.Content = "Uninstall";
            ApplyButton.Content = "Apply";
            OptionsText.Text = "Options";
            InstallNvngx.Content = "Install";
            DisableEpicOverlay.Content = "Disable";
            EditUpscalingText.Text = "  Edit Upscaling";
            SharpnessText.Text = "Sharpness";
            FSRUltraQualityText.Text = "FSR Ultra Quality";
            FSRQualityText.Text = "FSR Quality";
            FSRBalancedText.Text = "FSR Balanced";
            ModDownloadText.Text = "Mod Download";
            ModVersionText.Text = "Mod Version";
            DawloadModVersionButton.Content = "Download";
            IdiomaText.Text = "Language";
            ThemeText.Text = "Themes";

            CacheIdioma(IdiomaSelecionado);
        }
    }

    private void Portugues_Checked(object sender, RoutedEventArgs e)
    {


        //Portugues

        if (IdiomaPortugues.IsChecked == true)
        {
            string IdiomaSelecionado = "2";

            //Mudanças abaixo
            SelectGameDirectoryText.Text = "Diretorio do Jogo   ";
            SelectGameDirectoryText.Margin = new Thickness(250, -310, 0, 10);
            Gamedirect.Watermark = "Selecione o Diretorio do Jogo";
            SelectGameText.Text = "Selecione o Jogo";
            SelectGameText.Margin = new Thickness(256, -160, 0, 0);
            UninstallButton.Content = "Desinstalar";
            ApplyButton.Content = "Aplicar";
            OptionsText.Text = "Opções";
            InstallNvngx.Content = "Instalar";
            DisableEpicOverlay.Content = "Desativar";
            EditUpscalingText.Text = "Editar Upscaling";
            SharpnessText.Text = "Nitidez";
            FSRUltraQualityText.Text = "FSR Ultra Qualidade";
            FSRQualityText.Text = "FSR Qualidade";
            FSRBalancedText.Text = "FSR Equilibrado";
            ModDownloadText.Text = "Baixar Mod";
            ModVersionText.Text = "Versão do Mod";
            DawloadModVersionButton.Content = "Baixar";
            IdiomaText.Text = "Idioma";
            ThemeText.Text = "Tema";
            CacheIdioma(IdiomaSelecionado);

        }
    }


    private void CacheIdioma(string TemaSelecionado)
    {
        // Caminho do arquivo
        string CaminhoCacheNaoGravado = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fsr3Files\\Config");

        if (!Directory.Exists(CaminhoCacheNaoGravado))
        {
            Directory.CreateDirectory(CaminhoCacheNaoGravado);



            using (StreamWriter writer = File.CreateText(Path.Combine(CaminhoCacheNaoGravado, "LanguageCache.adx")))
            {
                writer.Write(string.Empty);
                writer.Write(TemaSelecionado);
            }
        }
        else
        {
            using (StreamWriter writer = File.CreateText(Path.Combine(CaminhoCacheNaoGravado, "LanguageCache.adx")))
            {
                writer.Write(string.Empty);
                writer.Write(TemaSelecionado);
            }
        }

    }

    private void LerCacheIdioma()
    {
        try
        {
            string IdiomaEscolhido = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Fsr3Files\\Config\\LanguageCache.adx"));

            if (IdiomaEscolhido == "1")
            {
                IdiomaIngles.IsChecked = true;
            }
            else if (IdiomaEscolhido == "2")
            {
                IdiomaPortugues.IsChecked = true;
            }

        }
        catch { tema1.IsChecked = true; }


    }

    // Open Site Clicks

    private void Github_Click(object sender, RoutedEventArgs e)
    {
        string SiteEN = "https://github.com/Spexxl/FSR3-Installer-Tool";
        Process.Start(new ProcessStartInfo(SiteEN) { UseShellExecute = true });
    }

    private void Patreon_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://www.patreon.com/SpexxLorioh") { UseShellExecute = true });
    }

    private void Website_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://fsr-3-tool-website.vercel.app") { UseShellExecute = true });
    }

    private void GetApiKey_Click(object sender, RoutedEventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://mods.lukefz.xyz") { UseShellExecute = true });
    }


}







