using System;
using Prism.Mvvm;

namespace ConfApp.ViewModels
{
    public class ModelBase : BindableBase
    {
        public ModelBase()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
    }
}