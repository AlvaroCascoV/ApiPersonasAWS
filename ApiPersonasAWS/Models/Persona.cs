using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPersonasAWS.Models
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int Edad { get; set; }
    }
}
