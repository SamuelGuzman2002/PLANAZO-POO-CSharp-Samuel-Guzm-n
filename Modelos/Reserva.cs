
using System;

namespace PLANAZO_POO_CSharp.Modelos
{
    public class Reserva
    {
        public int Id { get; }
        public Usuario Usuario { get; }
        public Evento Evento { get; }
        public int CantidadEntradas { get; }
        public decimal ValorTotal { get; }
        public string Estado { get; private set; }

        public Reserva(int id, Usuario usuario, Evento evento,
                       int cantidadEntradas)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El identificador de la reserva debe ser positivo.");

            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (evento == null)
                throw new ArgumentNullException(nameof(evento));

            if (cantidadEntradas <= 0)
                throw new ArgumentException(
                    "La cantidad de entradas debe ser mayor que cero.");

            evento.ReservarCupos(cantidadEntradas);

            Id = id;
            Usuario = usuario;
            Evento = evento;
            CantidadEntradas = cantidadEntradas;

            ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;

            Estado = "Activa";
        }

        public void Cancelar()
        {
            if (Estado == "Cancelada")
                throw new InvalidOperationException(
                    "Esta reserva ya fue cancelada.");

            // Devolver las entradas al evento
            Evento.LiberarCupos(CantidadEntradas);

            Estado = "Cancelada";
        }
    }
}
