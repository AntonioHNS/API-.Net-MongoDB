using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using API.Data.Collections;

namespace API_Infectados.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class InfectadoController : ControllerBase {
        API.Data.MongoDB _mongoDB;
        IMongoCollection<Infectado> _infectadosCollection;

        public InfectadoController(API.Data.MongoDB mongoDB)
        {
            _mongoDB = mongoDB;
            _infectadosCollection = _mongoDB.DB.GetCollection<Infectado>(typeof(Infectado).Name.ToLower());
        }

        [HttpPost]
        public ActionResult SalvarInfectado([FromBody] InfectadoDto dto)
        {
            var infectado = new Infectado(dto.CPF, dto.DataNascimento, dto.Sexo, dto.Latitude, dto.Longitude);
            _infectadosCollection.InsertOne(infectado);
            return StatusCode(201, "Infectado adicionado com sucesso");
        }

        [HttpGet]
        public ActionResult ObterInfectados() {
            var infectados = _infectadosCollection.Find(Builders<Infectado>.Filter.Empty).ToList();
            return Ok(infectados);
        }

        [HttpGet("{cpf}")]
        public ActionResult ObterInfectado(string cpf)
        {
            var infectado = _infectadosCollection.Find(Builders<Infectado>.Filter.Where(_=>_.CPF==cpf)).ToList();
            return Ok(infectado);
        }

        [HttpDelete("{cpf}")]
        public ActionResult DeleteInfectado(string cpf){
            _infectadosCollection.DeleteOne(Builders<Infectado>.Filter.Where(_ => _.CPF == cpf));
            return Ok("Deletado com sucesso");
        }

        [HttpPut]
        public ActionResult AtualizaInfectado([FromBody] InfectadoDto dto){
            var infectado = new Infectado(dto.CPF, dto.DataNascimento, dto.Sexo, dto.Latitude,dto.Longitude);
            _infectadosCollection.UpdateOne(Builders<Infectado>.Filter.Where(_=>_.CPF == dto.CPF),Builders<Infectado>.Update.Set("sexo",dto.Sexo));
            return Ok("Atualizado com sucesso");
        }
    }
}
