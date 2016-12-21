using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TgaViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region constructors

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Messenger.Default.Register<ExitMessage>(this, (m) => Close());
            Messenger.Default.Register<OpenFileDialogMessage>(this, (m) => m.ShowDialog());
        }

        #endregion  // constructors
    }
}
