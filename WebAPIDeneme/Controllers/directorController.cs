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
    public class directorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        
        public directorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        //get all director data
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                select *
                from director
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

        


        [HttpPost]
        public JsonResult Post(director dir)
        {
            string query = @"
                insert into director(firstName, lastName, dateOfBirth, dateOfDeath, SEX, spouse)
                values (@firstName, @lastName, @dateOfBirth, @dateOfDeath, @SEX, @spouse)
            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@firstName", dir.firstName);
                    myCommand.Parameters.AddWithValue("@lastName", dir.lastName);
                    myCommand.Parameters.AddWithValue("@dateOfBirth", dir.dateOfBirth);
                    if(dir.dateOfDeath!= DateTime.Parse("0001-01-01T00:00:00")) { myCommand.Parameters.AddWithValue("@dateOfDeath", dir.dateOfDeath); } else
                    {
                        myCommand.Parameters.AddWithValue("@dateOfDeath", DBNull.Value);
                    }
                    
                    myCommand.Parameters.AddWithValue("@SEX", dir.SEX);
                    if(dir.spouse!=null) { myCommand.Parameters.AddWithValue("@spouse", dir.spouse); } else
                    {
                        myCommand.Parameters.AddWithValue("@spouse", DBNull.Value);
                    }
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Director added succesfuly");
        }



    }
}
