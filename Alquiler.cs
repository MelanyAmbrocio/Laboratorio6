using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio6
{
    class Alquiler
    {
        string nit;
        string placa;
        DateTime fechaAlquiler;
        DateTime fechaDevolucion;
        int kmRecorridos;

        public string Nit { get => nit; set => nit = value; }
        public string Placa { get => placa; set => placa = value; }
        public DateTime FechaAlquiler { get => fechaAlquiler; set => fechaAlquiler = value; }
        public DateTime FechaDevolucion { get => fechaDevolucion; set => fechaDevolucion = value; }
        public int KmRecorridos { get => kmRecorridos; set => kmRecorridos = value; }
    }
}
