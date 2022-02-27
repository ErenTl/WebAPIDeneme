using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebAPIDeneme.Models;

namespace WebAPIDeneme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class movieController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public movieController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //get all movie data
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select *
                from movie
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }




        //create new movie
        [HttpPost]
        public JsonResult Post(movie mov)
        {
            string query = @"
                insert into movie(movieTitle, releaseDate, imdbRank) values (@movieTitle,@releaseDate,@imdbRank)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@movieTitle", mov.movieTitle);

                    if (mov.releaseDate != DateTime.Parse("0001-01-01T00:00:00")) { myCommand.Parameters.AddWithValue("@releaseDate", mov.releaseDate); }
                    else { myCommand.Parameters.AddWithValue("@releaseDate", DBNull.Value); }

                    if(mov.imdbRank!=0.0m) { myCommand.Parameters.AddWithValue("@imdbRank", mov.imdbRank); } else 
                    { myCommand.Parameters.AddWithValue("@imdbRank", DBNull.Value); }

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Movie added succesfuly");
        }





        //set movie data from id
        [HttpPut]
        public JsonResult Put(movie mov)
        {
            string query = @"
                update movie set movieTitle=@movieTitle,
                    releaseDate=@releaseDate,
                    imdbRank=@imdbRank
                where id=@id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", mov.id);
                    myCommand.Parameters.AddWithValue("@movieTitle", mov.movieTitle);
                    myCommand.Parameters.AddWithValue("@releaseDate", mov.releaseDate);
                    myCommand.Parameters.AddWithValue("@imdbRank", mov.imdbRank);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated movieData succesfuly");
        }



        //set individual datas in table

        [Route("setMovieTitle")]
        [HttpPut]
        public JsonResult setMovieTitle(movie mov)
        {
            string query = @"
                update movie set movieTitle=@movieTitle where id=@id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", mov.id);
                    myCommand.Parameters.AddWithValue("@movieTitle", mov.movieTitle);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated movieTitle succesfuly");
        }


        [Route("setReleaseDate")]
        [HttpPut]
        public JsonResult setReleaseDate(movie mov)
        {
            string query = @"
                update movie set releaseDate=@releaseDate where id=@id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", mov.id);
                    myCommand.Parameters.AddWithValue("@releaseDate", mov.releaseDate);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated releaseDate succesfuly");
        }

               
        [Route("setImdbRank")]
        [HttpPut]
        public JsonResult setImdbRank(movie mov)
        {
            string query = @"
                update movie set imdbRank=@imdbRank where id=@id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", mov.id);
                    myCommand.Parameters.AddWithValue("@imdbRank", mov.imdbRank);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updated imdbRank succesfuly");
        }
        //_________________________________________


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                delete from movie where id=@id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deleted succesfuly");
        }


        [HttpGet("single/{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                select * from movie where id=@id
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@id", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }



    }
}
