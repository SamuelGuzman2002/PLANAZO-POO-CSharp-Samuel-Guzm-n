
using System;

namespace PLANAZO_POO_CSharp.Modelos
{
    public class Usuario
    {
        public int Id { get; }
        public string Nombre { get; }

        public Usuario(int id, string nombre)
        {
            if (id <= 0)
                throw new ArgumentException(
                    "El identificador del usuario debe ser positivo.");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException(
                    "El nombre del usuario no puede estar vacío.");

            Id = id;
            Nombre = nombre.Trim();
        }
    }
}
