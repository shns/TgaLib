using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TgaViewer
{
    /// <summary>
    /// ファイルオープンメッセージクラス。
    /// </summary>
    class OpenFileDialogMessage : MessageBase
    {
        #region fields

        /// <summary>
        /// ファイルオープン後のコールバック。
        /// </summary>
        private Action<OpenFileDialogMessage> callback_;

        #endregion  // fields


        #region properties

        /// <summary>
        /// ファイルオープンの結果を取得、設定する。
        /// </summary>
        public bool? DialogResult { get; set; }

        /// <summary>
        /// 拡張子が省略されたときに、ファイル名の拡張子を付加するかを取得、設定する。
        /// </summary>
        public bool AddExtension { get; set; }

        /// <summary>
        /// 指定されたファイルが存在しないときに警告を表示するかを取得、設定する。
        /// </summary>
        public bool CheckFileExists { get; set; }

        /// <summary>
        /// 無効なパスやファイル名を入力したときに警告を表示するかを取得、設定する。
        /// </summary>
        public bool CheckPathExists { get; set; }

        /// <summary>
        /// ファイルダイアログのカスタムプレースのリストを取得、設定する。
        /// </summary>
        public IList<FileDialogCustomPlace> CustomPlaces { get; set; }

        /// <summary>
        /// デフォルトで表示するファイルの拡張子を取得、設定する。
        /// </summary>
        public string DefaultExt { get; set; }

        /// <summary>
        /// ショートカットが指定されたときに、ショートカットが指す先を使用するか
        /// ショートカット自身(.lnk)を使用するかを取得、設定する。
        /// </summary>
        public bool DereferenceLinks { get; set; }

        /// <summary>
        /// 選択されたファイルパスを取得する。
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// 選択されたファイルパスの配列を取得する。
        /// </summary>
        public string[] FileNames { get; private set; }

        /// <summary>
        /// ダイアログに表示するファイル種別のフィルタを取得、設定する。
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// ダイアログで選択されているフィルタのインデックスを取得、設定する。
        /// </summary>
        public int FilterIndex { get; set; }

        /// <summary>
        /// ダイアログで表示するディレクトリの初期位置を取得、設定する。
        /// </summary>
        public string InitialDirectory { get; set; }

        /// <summary>
        /// 複数選択するかを取得、設定する。
        /// </summary>
        public bool Multiselect { get; set; }

        /// <summary>
        /// ダイアログに表示された読み込み専用のチェックボックスがチェックされているかを取得、設定する。
        /// </summary>
        public bool ReadOnlyChecked { get; set; }

        /// <summary>
        /// 選択されたファイル名を取得する。
        /// </summary>
        public string SafeFileName { get; private set; }

        /// <summary>
        /// 選択されたファイル名の配列を取得する。
        /// </summary>
        public string[] SafeFileNames { get; private set; }

        /// <summary>
        /// ダイアログに読み込み専用のチェックボックスを表示するかを取得、設定する。
        /// </summary>
        public bool ShowReadOnly { get; set; }

        /// <summary>
        /// ダイアログに付加するオブジェクトを取得、設定する。
        /// </summary>
        public object Tag { get; set; }

        /// <summary>
        /// ダイアログのタイトルを取得、設定する。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// ダイアログが有効なWin32ファイル名のみ受け付けるかを取得、設定する。
        /// </summary>
        public bool ValidateNames { get; set; }

        #endregion  // properties


        #region constructors

        /// <summary>
        /// コンストラクタ。
        /// </summary>
        /// <param name="callback">ファイルオープン後のコールバック。</param>
        public OpenFileDialogMessage(Action<OpenFileDialogMessage> callback)
        {
            callback_ = callback;

            AddExtension = true;
            CheckFileExists = true;
            CheckPathExists = true;
            CustomPlaces = new List<FileDialogCustomPlace>();
            DefaultExt = string.Empty;
            DereferenceLinks = true;
            FileName = string.Empty;
            FileNames = new string[0];
            Filter = string.Empty;
            FilterIndex = 1;
            InitialDirectory = string.Empty;
            Multiselect = false;
            ReadOnlyChecked = false;
            SafeFileName = string.Empty;
            SafeFileNames = new string[0];
            ShowReadOnly = false;
            Tag = null;
            Title = string.Empty;
            ValidateNames = true;
        }

        #endregion  // constructors


        #region public methods

        /// <summary>
        /// ファイルオープンのダイアログを表示する。
        /// </summary>
        public void ShowDialog()
        {
            var dialog = new OpenFileDialog();
            SetToDialog(dialog);
            DialogResult = dialog.ShowDialog();
            SetFromDialog(dialog);
            callback_(this);
        }

        #endregion  // public methods


        #region private methods

        /// <summary>
        /// ダイアログにパラメータを設定する。
        /// </summary>
        /// <param name="dialog">ファイルオープンのダイアログ。</param>
        private void SetToDialog(OpenFileDialog dialog)
        {
            dialog.AddExtension = AddExtension;
            dialog.CheckFileExists = CheckFileExists;
            dialog.CheckPathExists = CheckPathExists;
            dialog.CustomPlaces = CustomPlaces;
            dialog.DefaultExt = DefaultExt;
            dialog.DereferenceLinks = DereferenceLinks;
            dialog.Filter = Filter;
            dialog.FilterIndex = FilterIndex;
            dialog.InitialDirectory = InitialDirectory;
            dialog.Multiselect = dialog.Multiselect;
            dialog.ReadOnlyChecked = ReadOnlyChecked;
            dialog.ShowReadOnly = ShowReadOnly;
            dialog.Tag = Tag;
            dialog.Title = Title;
            dialog.ValidateNames = ValidateNames;
        }

        /// <summary>
        /// ダイアログからパラメータを設定する。
        /// </summary>
        /// <param name="dialog">ファイルオープンのダイアログ。</param>
        private void SetFromDialog(OpenFileDialog dialog)
        {
            if (DialogResult.HasValue && DialogResult.Value)
            {
                FileName = dialog.FileName;
                FileNames = dialog.FileNames;
                SafeFileName = dialog.SafeFileName;
                SafeFileNames = dialog.SafeFileNames;
            }
            else
            {
                FileName = string.Empty;
                FileNames = new string[0];
                SafeFileName = string.Empty;
                SafeFileNames = new string[0];
            }
        }

        #endregion  // private methods
    }
}
