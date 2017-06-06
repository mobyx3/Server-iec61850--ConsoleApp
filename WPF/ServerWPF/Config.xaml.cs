﻿
using System.Collections.Generic;
using System.IO.Ports;
using System.Windows.Controls;
using Server.ModBus;

namespace ServerWPF
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Config 
    {
        public Config()
        {
            InitializeComponent();
        }

        public void InitConfig()
        {
            PortTextBox.Text = Server.Server.Server.ServerConfig.PortServer.ToString();
            HostTextBox.Text = Server.Server.Server.ServerConfig.LocalIPAddr;
            
            foreach (var item in BaudRateComboBox.Items)
            {
                if (((ComboBoxItem)item).Content.ToString() == ConfigModBus.BaudRate.ToString())
                {
                    BaudRateComboBox.SelectedIndex = BaudRateComboBox.Items.IndexOf(item);
                    break;
                }
            }

            foreach (var item in SerialPortParityComboBox.Items)
            {
                if (((ComboBoxItem)item).Content.ToString() == ConfigModBus.SerialPortParity.ToString())
                {
                    SerialPortParityComboBox.SelectedIndex = SerialPortParityComboBox.Items.IndexOf(item);
                    break;
                }
            }           

            foreach (var item in SerialPortStopBitsComboBox.Items)
            {
                if (((ComboBoxItem)item).Content.ToString() == ConfigModBus.SerialPortStopBits.ToString())
                {
                    SerialPortStopBitsComboBox.SelectedIndex = SerialPortStopBitsComboBox.Items.IndexOf(item);
                    break;
                }
            }

            List <string> portList =new List <string>();
            string[] portStrList = SerialPort.GetPortNames();
            portList.Clear();
            foreach (string port in portStrList)
            {
                portList.Add(port);
            }
            portList.Sort();

            foreach (var port in portList)
            {
                ComPortNameComboBox.Items.Add(new ComboBoxItem{Content = port});
            }

            foreach (var item in ComPortNameComboBox.Items)
            {
                if (((ComboBoxItem)item).Content.ToString() == ConfigModBus.ComPortName)
                {
                    ComPortNameComboBox.SelectedIndex = ComPortNameComboBox.Items.IndexOf(item);
                    break;
                }
            }

            DownloadScopeCheckBox.IsChecked = ConfigDownloadScope.Enable;
            RemoveAfterDownloadCheckBox.IsChecked = ConfigDownloadScope.Remove;

            foreach (var item in TypeScopeComboBox.Items)
            {
                if (((ComboBoxItem)item).Content.ToString() == ConfigDownloadScope.Type)
                {
                    TypeScopeComboBox.SelectedIndex = TypeScopeComboBox.Items.IndexOf(item);
                    break;
                }
            }

            foreach (var item in ComtradeTypeComboBox.Items)
            {
                if (((ComboBoxItem)item).Content.ToString() == ConfigDownloadScope.ComtradeType)
                {
                    ComtradeTypeComboBox.SelectedIndex = ComtradeTypeComboBox.Items.IndexOf(item);
                    break;
                }
            }

            ConfigurationAddrTextBox.Text = ConfigDownloadScope.ConfigurationAddr.ToString("X4");
            OscilCmndAddrTextBox.Text = ConfigDownloadScope.OscilCmndAddr.ToString("X4");
            PathScopeTextBox.Text = ConfigDownloadScope.PathScope?.Replace("\\", "");
            OscilNominalFrequencyTextBox.Text = ConfigDownloadScope.OscilNominalFrequency;

        }

        private void Ok_Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
