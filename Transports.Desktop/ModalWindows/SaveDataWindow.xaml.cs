using System;
using Transports.Core.Contexts;

namespace Transports.Desktop.ModalWindows
{
    public partial class SaveDataWindow
    {
        public SaveDataWindow()
        {
            InitializeComponent();
            YesBtn.Click += (sender, e) =>
            {
                InMemoryContext.Instance.SaveData();
                Environment.Exit(0);
            };
            NoBtn.Click += (sender, e) => Environment.Exit(0);
        }
    }
}
