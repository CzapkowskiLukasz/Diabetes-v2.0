using Diabetes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Diabetes.DAL
{
    public class DBMethods : InterfaceDB
    {
        string constring;
        public DBMethods(IConfiguration _configuration)
        {
            constring = _configuration.GetValue<string>("ConnectionString:FoodDB");
        }
        public List<Food> List()
        {
            List<Food> list = new List<Food>();
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("getfood", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Food food = new Food();
                food.id = reader.GetInt32(0);
                food.name = reader.GetString(1);
                food.calories = reader.GetInt32(2);
                food.carbohydrates = reader.GetDouble(3);
                food.glycemicIndex = reader.GetInt32(4);
                int maincatint = reader.GetInt32(5);
                SqlConnection con2 = new SqlConnection(constring);
                SqlCommand cmd2 = new SqlCommand("getcategory", con2);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlParameter categoryID = new SqlParameter("@catId", SqlDbType.Int);
                categoryID.Value = maincatint;
                cmd2.Parameters.Add(categoryID);
                con2.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    food.category1.name=reader2.GetString(0);
                }
                reader2.Close();
                con2.Close();
                list.Add(food);
            }
            reader.Close();
            con.Close();
            return list;

        }


        public Food GetByName(string name)
        {
            Food food = new Food();
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("search", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter nameqsl = new SqlParameter("@name", SqlDbType.NChar, 100);
            nameqsl.Value = name.ToLower();
            cmd.Parameters.Add(nameqsl);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                food.name = reader.GetString(1);
                food.calories = reader.GetInt32(2);
                food.carbohydrates = reader.GetDouble(3);
                food.proteins = reader.GetDouble(4);
                food.fats = reader.GetDouble(5);
                food.glycemicIndex = reader.GetInt32(6);
                food.glycemicLoad = reader.GetInt32(7);
            }
            reader.Close();
            con.Close();
            return food;
        }
        public Food GetById(int id)
        {
            Food food = new Food();
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("searchById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter foodID = new SqlParameter("@foodId", SqlDbType.Int);
            foodID.Value = id;
            cmd.Parameters.Add(foodID);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                food.name = reader.GetString(1);
                food.calories = reader.GetInt32(2);
                food.carbohydrates = reader.GetDouble(3);
                food.proteins = reader.GetDouble(4);
                food.fats = reader.GetDouble(5);
                food.glycemicIndex = reader.GetInt32(6);
                food.glycemicLoad = reader.GetInt32(7);
                int maincatint = reader.GetInt32(8);
                SqlConnection con2 = new SqlConnection(constring);
                SqlCommand cmd2 = new SqlCommand("getcategory", con2);
                cmd2.CommandType = CommandType.StoredProcedure;
                SqlParameter categoryID = new SqlParameter("@catId", SqlDbType.Int);
                categoryID.Value = maincatint;
                cmd2.Parameters.Add(categoryID);
                con2.Open();
                SqlDataReader reader2 = cmd2.ExecuteReader();
                while (reader2.Read())
                {
                    food.category1.name = reader2.GetString(0);
                }
                reader2.Close();
                con2.Close();
                if (reader.IsDBNull(9) == false)
                {
                    int seccat = reader.GetInt32(9);
                    categoryID.Value = seccat;
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.Add(categoryID);
                    con2.Open();
                    reader2 = cmd2.ExecuteReader();
                    while (reader2.Read())
                    {
                        food.category2.name = reader2.GetString(0);
                    }
                    reader2.Close();
                    con2.Close();
                }  
            }
            reader.Close();
            con.Close();
            return food;
        }
        public void Delete(int id)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("foodDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter foodid_SqlParam = new SqlParameter("@foodId", SqlDbType.Int);
            foodid_SqlParam.Value = id;
            cmd.Parameters.Add(foodid_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Edit(Food food)
        {
            //editFood
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand();
            if (food.category2.id == -1)
            {
                cmd.CommandText = "editFoodonecat";
                cmd.Connection = con;
            }
            else
            {
                cmd.CommandText = "editFood";
                cmd.Connection = con;
            }
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter id_SqlParam = new SqlParameter("@foodId", SqlDbType.Int);
            id_SqlParam.Value = food.id;
            cmd.Parameters.Add(id_SqlParam);

            SqlParameter name_SqlParam = new SqlParameter("@name", SqlDbType.NChar, 100);
            name_SqlParam.Value = food.name;
            cmd.Parameters.Add(name_SqlParam);

            SqlParameter calories_SqlParam = new SqlParameter("@calories", SqlDbType.Int);
            calories_SqlParam.Value = food.calories;
            cmd.Parameters.Add(calories_SqlParam);

            SqlParameter carbohydrates_SqlParam = new SqlParameter("@carbohydrates", SqlDbType.Float);
            carbohydrates_SqlParam.Value = food.carbohydrates;
            cmd.Parameters.Add(carbohydrates_SqlParam);

            SqlParameter proteins_SqlParam = new SqlParameter("@proteins", SqlDbType.Float);
            proteins_SqlParam.Value = food.proteins;
            cmd.Parameters.Add(proteins_SqlParam);

            SqlParameter fats_SqlParam = new SqlParameter("@fats", SqlDbType.Float);
            fats_SqlParam.Value = food.fats;
            cmd.Parameters.Add(fats_SqlParam);

            SqlParameter GI_SqlParam = new SqlParameter("@GI", SqlDbType.Int);
            GI_SqlParam.Value = food.glycemicIndex;
            cmd.Parameters.Add(GI_SqlParam);

            SqlParameter GL_SqlParam = new SqlParameter("@GL", SqlDbType.Int);
            GL_SqlParam.Value = food.glycemicLoad;
            cmd.Parameters.Add(GL_SqlParam);

            SqlParameter main_SqlParam = new SqlParameter("@main", SqlDbType.Int);
            main_SqlParam.Value = food.category1.id;
            cmd.Parameters.Add(main_SqlParam);

            if (food.category2.id != -1)
            {
                SqlParameter sec_SqlParam = new SqlParameter("@sec", SqlDbType.Int);
                sec_SqlParam.Value = food.category2.id;
                cmd.Parameters.Add(sec_SqlParam);
            }

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
        }
        public void Add(Food food)
        {
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand();
            if (food.category2.id == -1)
            {
                cmd.CommandText = "createfoodonecat";
                cmd.Connection = con;
            }
            else
            {
                cmd.CommandText = "createfood";
                cmd.Connection = con;
            }
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter name_SqlParam = new SqlParameter("@name", SqlDbType.NChar, 100);
            name_SqlParam.Value = food.name;
            cmd.Parameters.Add(name_SqlParam);

            SqlParameter calories_SqlParam = new SqlParameter("@calories", SqlDbType.Int);
            calories_SqlParam.Value = food.calories;
            cmd.Parameters.Add(calories_SqlParam);

            SqlParameter carbohydrates_SqlParam = new SqlParameter("@carbohydrates", SqlDbType.Float);
            carbohydrates_SqlParam.Value = food.carbohydrates;
            cmd.Parameters.Add(carbohydrates_SqlParam);

            SqlParameter proteins_SqlParam = new SqlParameter("@proteins", SqlDbType.Float);
            proteins_SqlParam.Value = food.proteins;
            cmd.Parameters.Add(proteins_SqlParam);

            SqlParameter fats_SqlParam = new SqlParameter("@fats", SqlDbType.Float);
            fats_SqlParam.Value = food.fats;
            cmd.Parameters.Add(fats_SqlParam);

            SqlParameter GI_SqlParam = new SqlParameter("@GI", SqlDbType.Int);
            GI_SqlParam.Value = food.glycemicIndex;
            cmd.Parameters.Add(GI_SqlParam);

            SqlParameter GL_SqlParam = new SqlParameter("@GL", SqlDbType.Int);
            GL_SqlParam.Value = food.glycemicLoad;
            cmd.Parameters.Add(GL_SqlParam);

            SqlParameter main_SqlParam = new SqlParameter("@main", SqlDbType.Int);
            main_SqlParam.Value = food.category1.id;
            cmd.Parameters.Add(main_SqlParam);

            if(food.category2.id != -1)
            {
                SqlParameter sec_SqlParam = new SqlParameter("@sec", SqlDbType.Int);
                sec_SqlParam.Value = food.category2.id;
                cmd.Parameters.Add(sec_SqlParam);
            }
            SqlParameter foodID_SqlParam = new SqlParameter("@foodID", SqlDbType.Int);
            foodID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(foodID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            int foodID = (int)cmd.Parameters["@foodID"].Value;
        }
        public List<Category> Categories()
        {
            List<Category> categories = new List<Category>();
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("getallcategory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Category category = new Category();
                category.id = reader.GetInt32(0);
                category.name = reader.GetString(1);
                categories.Add(category);
            }
            reader.Close();
            con.Close();
            return categories;
        }
    }
}
