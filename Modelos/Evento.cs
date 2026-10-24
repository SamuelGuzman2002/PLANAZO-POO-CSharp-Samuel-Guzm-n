
using System;

namespace PLANAZO_POO_CSharp.Modelos
{
    public abstract class Evento
    {
        public int Id { get; }
        public string Nombre { get; }
        public DateTime Fecha { get; }
        public decimal PrecioBase { get; }
        public int CapacidadMaxima { get; }
        public int CuposDisponibles { get; private set; }

        protected Evento(int id, string nombre, DateTime fecha,
                          decimal precioBase, int capacidadMaxima)
        {
            if (id <= 0)
                throw new ArgumentException("El ID debe ser positivo.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre no puede estar vacío.");

            if (precioBase < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            if (capacidadMaxima <= 0)
                throw new ArgumentException("La capacidad debe ser mayor que cero.");

            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            PrecioBase = precioBase;
            CapacidadMaxima = capacidadMaxima;
            CuposDisponibles = capacidadMaxima;
        }

        public abstract decimal CalcularPrecioFinal();

        public void ReservarCupos(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de entradas debe ser mayor que cero.");

            if (cantidad > CuposDisponibles)
                throw new InvalidOperationException(
                    "No hay suficientes cupos disponibles.");

            CuposDisponibles -= cantidad;
        }

        public void LiberarCupos(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad debe ser mayor que cero.");

            if (cantidad > CapacidadMaxima - CuposDisponibles)
                throw new InvalidOperationException(
                    "No se pueden devolver más cupos de los reservados.");

            CuposDisponibles += cantidad;
        }
    }
}
