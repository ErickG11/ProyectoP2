

﻿namespace ProyectoP2.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }

        public Usuario()
        {
            Id = Guid.NewGuid();
        }

    }
}
