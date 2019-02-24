using System;
using System.Windows;
using Transports.Core.Contexts;

namespace Transports.Desktop.ModalWindows
{
    public partial class SaveDataWindow
    {
        public SaveDataWindow()
        {
            InitializeComponent();
            YesBtn.Click += (sender, e) => InMemoryContext.Instance.SaveData();
            NoBtn.Click += (sender, e) => Environment.Exit(0);
        }
    }
}
