﻿using System;
using MongoDB.Driver.GeoJsonObjectModel;

namespace API.Data.Collections
{
    public class Infectado
    {
        public Infectado(string cpf, DateTime dataNascimento, string sexo, double latitude, double longitude)
        {
            this.CPF = cpf;
            this.DataNascimento = dataNascimento;
            this.Sexo = sexo;
            this.Localizacao = new GeoJson2DGeographicCoordinates(longitude, latitude);

        }

        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; }
        public GeoJson2DGeographicCoordinates Localizacao { get; set; }
    }
}