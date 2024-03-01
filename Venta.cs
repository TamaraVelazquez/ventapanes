using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ventapanes
{
    public class Venta : INotifyPropertyChanged
    {
        public ObservableCollection<Pan> Pans { get; set; } = new();
        public Pan Pan { get; set;} = new();
        public ICommand AgregarCommand { get; set; }
        public ICommand EliminarCommand { get; set; }
        public ICommand PagarCommand { get; set; }
        public ICommand CobrarCommand { get; set; }
        public decimal TotalPagar { get; set;}


        public Venta()
        {
            Cargar();
            AgregarCommand = new RelayCommand<Pan>(Agregar);
            EliminarCommand = new RelayCommand(Eliminar);
            PagarCommand = new RelayCommand<Pan>(Pagar);
            CobrarCommand = new RelayCommand(Cobrar);
        }

        public void Agregar(Pan? pans)
        {
            if(pans != null)
            {
                Pans.Add(pans);
                Pan = new();
                PropertyChanged?.Invoke(this, new(nameof(Pan)));
                Guardar();
            }
        }

        public void Guardar()
        {
            var json = JsonSerializer.Serialize(Pans);
            File.WriteAllText("pans.json", json);
        }

        public void Eliminar()
        {
            if (Pan != null)
            {
                Pans.Remove(Pan);
                Guardar();
            }
        }

        public void Cargar()
        {
            if (File.Exists("pans.json"))
            {
                string json = File.ReadAllText("pans.json");
                var panes = JsonSerializer.Deserialize<ObservableCollection<Pan>>(json);
                if (panes != null)
                {
                    foreach (var a in panes)
                    {
                        Pans.Add(a);
                    }
                }
            }
        }

        public void Pagar(Pan? pans)
        {
            if (Pan != null)
            {
                TotalPagar = Pan.Cantidad * Pan.Costo;
            }
            else
            {
                TotalPagar = 0m;
            }
            Cargar();
            Guardar();
        }

        public void Cobrar()
        {
            TotalPagar = 0m;
            Eliminar();
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
