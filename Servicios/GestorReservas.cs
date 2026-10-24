
using System;
using System.Collections.Generic;
using PLANAZO_POO_CSharp.Modelos;

namespace PLANAZO_POO_CSharp.Servicios
{
    public class GestorReservas
    {
        private List<Evento> eventos = new List<Evento>();
        private List<Usuario> usuarios = new List<Usuario>();
        private List<Reserva> reservas = new List<Reserva>();

        public void AgregarEvento(Evento evento)
        {
            foreach (Evento e in eventos)
            {
                if (e.Id == evento.Id)
                {
                    throw new Exception("Este evento ya existe");
                }
            }

            eventos.Add(evento);
        }

        public List<Evento> ObtenerEventos()
        {
            return eventos;
        }

        public Evento ConsultarEvento(int id)
        {
            foreach (Evento evento in eventos)
            {
                if (evento.Id == id)
                {
                    return evento;
                }
            }

            throw new Exception("No se encontro el evento");
        }

        public Usuario RegistrarUsuario(int id, string nombre)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Id == id)
                {
                    throw new Exception("El usuario ya existe");
                }
            }

            Usuario nuevoUsuario = new Usuario(id, nombre);
            usuarios.Add(nuevoUsuario);

            return nuevoUsuario;
        }

        public Usuario ConsultarUsuario(int id)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (usuario.Id == id)
                {
                    return usuario;
                }
            }

            throw new Exception("Usuario no encontrado");
        }

        public Reserva CrearReserva(int idReserva, int idUsuario,
                                    int idEvento, int cantidad)
        {
            foreach (Reserva r in reservas)
            {
                if (r.Id == idReserva)
                {
                    throw new Exception("Esta reserva ya existe");
                }
            }

            Usuario usuario = ConsultarUsuario(idUsuario);
            Evento evento = ConsultarEvento(idEvento);

            Reserva reserva = new Reserva(idReserva, usuario, evento, cantidad);

            reservas.Add(reserva);

            return reserva;
        }

        public List<Reserva> ObtenerReservas()
        {
            return reservas;
        }

        public List<Reserva> ObtenerReservasPorUsuario(int idUsuario)
        {
            ConsultarUsuario(idUsuario);

            List<Reserva> resultado = new List<Reserva>();

            foreach (Reserva reserva in reservas)
            {
                if (reserva.Usuario.Id == idUsuario)
                {
                    resultado.Add(reserva);
                }
            }

            return resultado;
        }

        public void CancelarReserva(int idReserva)
        {
            foreach (Reserva reserva in reservas)
            {
                if (reserva.Id == idReserva)
                {
                    reserva.Cancelar();
                    return;
                }
            }

            throw new Exception("No se encontro la reserva");
        }

        public int ContarReservasActivas()
        {
            int total = 0;

            foreach (Reserva reserva in reservas)
            {
                if (reserva.Estado == "Activa")
                {
                    total++;
                }
            }

            return total;
        }

        public int ContarEntradasReservadas()
        {
            int total = 0;

            foreach (Reserva reserva in reservas)
            {
                if (reserva.Estado == "Activa")
                {
                    total = total + reserva.CantidadEntradas;
                }
            }

            return total;
        }

        public decimal CalcularTotalReservasActivas()
        {
            decimal total = 0;

            foreach (Reserva reserva in reservas)
            {
                if (reserva.Estado == "Activa")
                {
                    total = total + reserva.ValorTotal;
                }
            }

            return total;
        }
    }
}
