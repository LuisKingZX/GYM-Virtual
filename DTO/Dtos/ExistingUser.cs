﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Dtos
{
    public class ExistingUser
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingresa tu nombre")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "Ingresa tu apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Ingresa tu fecha de nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }  // Usamos DateTime para representar una fecha
        [Required(ErrorMessage = "Ingresa tu direccion")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Ingresa tu numero de identificacion")]
        public string NumeroDeIdentificacion { get; set; }

        [Required(ErrorMessage = "Ingresa tu correo")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Correo { get; set; }
        [Required(ErrorMessage = "Ingresa tu telefono")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Ingresa tu contraseña")]
        public string Contrasena { get; set; }

        public ExistingUser(
        int id,
        string nombreCompleto,
        DateTime fechaDeNacimiento,
        string direccion,
        string numeroDeIdentificacion,
        string correo,
        string telefono,
        string contrasena)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            FechaDeNacimiento = fechaDeNacimiento;
            Direccion = direccion;
            NumeroDeIdentificacion = numeroDeIdentificacion;
            Correo = correo;
            Telefono = telefono;
            Contrasena = contrasena;
        }
        public ExistingUser(
        string nombreCompleto,
        DateTime fechaDeNacimiento,
        string direccion,
        string numeroDeIdentificacion,
        string correo,
        string telefono,
        string contrasena)
        {
            NombreCompleto = nombreCompleto;
            FechaDeNacimiento = fechaDeNacimiento;
            Direccion = direccion;
            NumeroDeIdentificacion = numeroDeIdentificacion;
            Correo = correo;
            Telefono = telefono;
            Contrasena = contrasena;
        }
        public ExistingUser() { }

    }
}
