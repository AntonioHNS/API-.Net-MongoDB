using System;

namespace API_Infectados
{
    public class InfectadoDto
    {

        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; }
        public double Latitude { get; set; }

        public double Longitude { get; set; }


    }
}
