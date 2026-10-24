using System;
using System.Collections.Generic;
using PLANAZO_POO_CSharp.Modelos;
using PLANAZO_POO_CSharp.Servicios;

GestorReservas gestor = new GestorReservas();

gestor.AgregarEvento(new Cine(1, "Avatar", new DateTime(2026, 11, 10), 20000m, 50));
gestor.AgregarEvento(new Cine(2, "Intensamente 2", new DateTime(2026, 11, 12), 18000m, 40));

gestor.AgregarEvento(new Concierto(3, "Concierto de Rock", new DateTime(2026, 11, 15), 50000m, 100));
gestor.AgregarEvento(new Concierto(4, "Festival de Salsa", new DateTime(2026, 11, 20), 60000m, 80));

gestor.AgregarEvento(new Taller(5, "Taller de Pintura", new DateTime(2026, 11, 18), 30000m, 25));
gestor.AgregarEvento(new Taller(6, "Taller de Fotografia", new DateTime(2026, 11, 22), 40000m, 20));

gestor.AgregarEvento(new EventoDeportivo(7, "Partido de Futbol", new DateTime(2026, 11, 25), 35000m, 120));

int opcion = -1;

do
{
    Console.WriteLine();
    Console.WriteLine("1. Ver eventos disponibles");
    Console.WriteLine("2. Consultar evento");
    Console.WriteLine("3. Reservar entradas");
    Console.WriteLine("4. Ver reservas realizadas");
    Console.WriteLine("5. Cancelar una reserva");
    Console.WriteLine("6. Ver resumen de reservas");
    Console.WriteLine("7. Consultar reservas por usuario");
    Console.WriteLine("0. Salir");
    Console.WriteLine();

    Console.Write("Seleccione una opcion: ");
    string entrada = Console.ReadLine() ?? "";

    if (!int.TryParse(entrada, out opcion))
    {
        Console.WriteLine("Debe ingresar un numero valido.");
        continue;
    }

    try
    {
        switch (opcion)
        {
            case 1:
                Console.WriteLine();
                Console.WriteLine("EVENTOS DISPONIBLES");

                foreach (Evento evento in gestor.ObtenerEventos())
                {
                    Console.WriteLine();
                    Console.WriteLine("ID: " + evento.Id);
                    Console.WriteLine("Nombre: " + evento.Nombre);
                    Console.WriteLine("Fecha: " + evento.Fecha.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Precio: $" + evento.CalcularPrecioFinal().ToString("N0"));
                    Console.WriteLine("Cupos disponibles: " + evento.CuposDisponibles);
                }
                break;

            case 2:
                Console.Write("Ingrese el ID del evento: ");
                int idEvento = int.Parse(Console.ReadLine() ?? "");

                Evento encontrado = gestor.ConsultarEvento(idEvento);

                Console.WriteLine("Nombre: " + encontrado.Nombre);
                Console.WriteLine("Fecha: " + encontrado.Fecha.ToString("dd/MM/yyyy"));
                Console.WriteLine("Precio base: $" + encontrado.PrecioBase.ToString("N0"));
                Console.WriteLine("Precio final: $" + encontrado.CalcularPrecioFinal().ToString("N0"));
                Console.WriteLine("Capacidad maxima: " + encontrado.CapacidadMaxima);
                Console.WriteLine("Cupos disponibles: " + encontrado.CuposDisponibles);
                break;

            
            case 3:
                Console.WriteLine();
                Console.WriteLine("REGISTRAR RESERVA");

                Console.Write("Ingrese su numero de identificacion: ");
                int idUsuario = int.Parse(Console.ReadLine() ?? "");

                Usuario usuario;

                try
                {
                    usuario = gestor.ConsultarUsuario(idUsuario);
                    Console.WriteLine("Bienvenido, " + usuario.Nombre);
                }
                catch
                {
                    Console.Write("Ingrese su nombre: ");
                    string nombre = Console.ReadLine() ?? "";

                    usuario = gestor.RegistrarUsuario(idUsuario, nombre);
                    Console.WriteLine("Usuario registrado correctamente.");
                }

                Console.WriteLine();
                Console.WriteLine("EVENTOS DISPONIBLES");

                foreach (Evento evento in gestor.ObtenerEventos())
                {
                    Console.WriteLine(evento.Id + ". " + evento.Nombre +
                                    " - $" + evento.CalcularPrecioFinal().ToString("N0") +
                                    " - Cupos: " + evento.CuposDisponibles);
                }

                Console.WriteLine();

                Console.Write("Ingrese el ID del evento: ");
                int eventoSeleccionado = int.Parse(Console.ReadLine() ?? "");

                Console.Write("Cantidad de entradas: ");
                int cantidad = int.Parse(Console.ReadLine() ?? "");

                int idReserva = gestor.ObtenerReservas().Count + 1;

                Reserva nuevaReserva = gestor.CrearReserva(
                    idReserva, idUsuario, eventoSeleccionado, cantidad);

                Console.WriteLine();
                Console.WriteLine("RESERVA REALIZADA CORRECTAMENTE");
                Console.WriteLine("Numero de reserva: " + nuevaReserva.Id);
                Console.WriteLine("Cliente: " + nuevaReserva.Usuario.Nombre);
                Console.WriteLine("Evento: " + nuevaReserva.Evento.Nombre);
                Console.WriteLine("Entradas: " + nuevaReserva.CantidadEntradas);
                Console.WriteLine("Total a pagar: $" + nuevaReserva.ValorTotal.ToString("N0"));
                Console.WriteLine("Estado: " + nuevaReserva.Estado);

                break;


            
            case 4:
                Console.WriteLine();
                Console.WriteLine("RESERVAS REALIZADAS");

                List<Reserva> listaReservas = gestor.ObtenerReservas();

                if (listaReservas.Count == 0)
                {
                    Console.WriteLine("No hay reservas registradas.");
                }
                else
                {
                    foreach (Reserva reserva in listaReservas)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Reserva: " + reserva.Id);
                        Console.WriteLine("Cliente: " + reserva.Usuario.Nombre);
                        Console.WriteLine("Evento: " + reserva.Evento.Nombre);
                        Console.WriteLine("Entradas: " + reserva.CantidadEntradas);
                        Console.WriteLine("Valor total: $" + reserva.ValorTotal.ToString("N0"));
                        Console.WriteLine("Estado: " + reserva.Estado);
                    }
                }

                break;


            
            case 5:
                Console.WriteLine();
                Console.WriteLine("CANCELAR RESERVA");

                Console.Write("Ingrese el numero de la reserva: ");
                int idCancelar = int.Parse(Console.ReadLine() ?? "");

                gestor.CancelarReserva(idCancelar);

                Console.WriteLine();
                Console.WriteLine("Reserva cancelada correctamente.");
                Console.WriteLine("Las entradas fueron devueltas al evento.");

                break;



            case 6:
                Console.WriteLine();
                Console.WriteLine("RESUMEN DE RESERVAS");

                int reservasActivas = gestor.ContarReservasActivas();
                int entradasReservadas = gestor.ContarEntradasReservadas();
                decimal totalReservas = gestor.CalcularTotalReservasActivas();

                Console.WriteLine("Reservas activas: " + reservasActivas);
                Console.WriteLine("Entradas reservadas: " + entradasReservadas);
                Console.WriteLine("Valor total: $" + totalReservas.ToString("N0"));

                break;


            
            case 7:
                Console.WriteLine();
                Console.WriteLine("CONSULTAR RESERVAS POR USUARIO");

                Console.Write("Ingrese la identificacion del usuario: ");
                int idBuscarUsuario = int.Parse(Console.ReadLine() ?? "");

                Usuario usuarioBuscado = gestor.ConsultarUsuario(idBuscarUsuario);

                List<Reserva> reservasUsuario = gestor.ObtenerReservasPorUsuario(idBuscarUsuario);

                Console.WriteLine();
                Console.WriteLine("Usuario: " + usuarioBuscado.Nombre);

                if (reservasUsuario.Count == 0)
                {
                    Console.WriteLine("Este usuario no tiene reservas.");
                }
                else
                {
                    foreach (Reserva reserva in reservasUsuario)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Reserva: " + reserva.Id);
                        Console.WriteLine("Evento: " + reserva.Evento.Nombre);
                        Console.WriteLine("Entradas: " + reserva.CantidadEntradas);
                        Console.WriteLine("Total: $" + reserva.ValorTotal.ToString("N0"));
                        Console.WriteLine("Estado: " + reserva.Estado);
                    }
                }

                break;


            case 0:
                Console.WriteLine("Gracias por utilizar PLANAZO.");
                break;

            default:
                Console.WriteLine("Opcion no valida.");
                break;
        }
    }
    catch (Exception error)
    {
        Console.WriteLine("Error: " + error.Message);
    }

} while (opcion != 0);
