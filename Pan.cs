using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ventapanes
{
    public enum Panes { Concha, Dona, Cuerno, Bolillo}
    public class Pan
    {
        public Panes PanComprado { get; set; }
        public byte Cantidad { get; set; }
        public decimal Costo
        {
            get
            {
                if (PanComprado == Panes.Concha) 
                {
                    return 8m;
                }
                if (PanComprado == Panes.Dona) 
                {
                    return 10m;
                }
                if(PanComprado == Panes.Bolillo)
                {
                    return 5m;
                }
                else
                {
                    return 8m;
                }
            }
        }
    }
}
