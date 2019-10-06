using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TgaLib;

namespace TgaViewer
{
    /// <summary>
    /// メインウィンドウViewModelクラス。
    /// </summary>
    class MainWindowViewModel : ViewModelBase
    {
        #region fields

        /// <summary>
        /// TGAファイルオープンコマンド。
        /// </summary>
        private ICommand openTgaCommand_;

        /// <summary>
        /// アプリ終了コマンド。
        /// </summary>
        private ICommand exitCommand_;

        /// <summary>
        /// 強制的にアルファチャネルを使用するか。
        /// </summary>
        private bool useAlphaChannelForcibly_;

        /// <summary>
        /// 表示画像。
        /// </summary>
        private BitmapSource imageSource_;

        /// <summary>
        /// 開いた画像ファイルパス。
        /// </summary>
        private string openedFile_;

        #endregion  // fields


        #region properties

        /// <summary>
        /// TGAファイルオープンコマンドを取得する。
        /// </summary>
        public ICommand OpenTgaCommand
        {
            get
            {
                return openTgaCommand_ ?? (openTgaCommand_ = new RelayCommand(
                    () =>
                    {
                        var message = new OpenFileDialogMessage(OnOpenFileDialog)
                        {
                            Filter = "tgaファイル|*.tga|すべてのファイル|*.*",
                            Title = "tgaファイルを開く"
                        };
                        MessengerInstance.Send(message);
                    }));
            }
        }

        /// <summary>
        /// アプリ終了コマンドを取得する。
        /// </summary>
        public ICommand ExitCommand
        {
            get
            {
                return exitCommand_ ?? (exitCommand_ = new RelayCommand(
                    () =>
                    {
                        MessengerInstance.Send(new ExitMessage());
                    }));
            }
        }

        /// <summary>
        /// アルファチャネルを強制的に使用するか?を取得、設定する。
        /// </summary>
        public bool UseAlphaChannelForcibly
        {
            get { return useAlphaChannelForcibly_; }
            set { Set(nameof(UseAlphaChannelForcibly), ref useAlphaChannelForcibly_, value); }
        }

        /// <summary>
        /// 表示画像を取得、設定する。
        /// </summary>
        public BitmapSource ImageSource
        {
            get { return imageSource_; }
            set { Set(nameof(ImageSource), ref imageSource_, value); }
        }

        /// <summary>
        /// 開いた画像ファイルパスを取得、設定する。
        /// </summary>
        public string OpenedFile
        {
            get { return openedFile_; }
            set { Set(nameof(OpenedFile), ref openedFile_, value); }
        }

        #endregion  // properties


        #region private methods

        /// <summary>
        /// ファイルオープン時の処理。
        /// </summary>
        /// <param name="message">ファイルオープンメッセージ。</param>
        private void OnOpenFileDialog(OpenFileDialogMessage message)
        {
            if (!message.DialogResult.HasValue || !message.DialogResult.Value)
            {
                return;
            }

            try
            {
                using (var fs = new FileStream(message.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var reader = new BinaryReader(fs))
                {
                    var tga = new TgaImage(reader, UseAlphaChannelForcibly);
                    ImageSource = tga.GetBitmap();
                }
                OpenedFile = message.FileName;
            }
            catch (Exception ex)
            {
                MessengerInstance.Send<DialogMessage>(
                    new DialogMessage(string.Format("ファイルのオープンに失敗しました\r\n{0}", ex), (r) => { })
                    {
                        Caption = "ファイルオープンエラー"
                    });
            }
        }

        #endregion  // private methods
    }
}
