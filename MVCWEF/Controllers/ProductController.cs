using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// conection database
using System.Data;
using System.Data.SqlClient;
using MVCWEF.Models;

namespace MVCWEF.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source = (local)\sqlexpress; Initial Catalog = crud; Integrated Security = true";

        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtProduct = new DataTable();

            // conection database
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlData = new SqlDataAdapter("select * from Products", sqlCon);
                sqlData.Fill(dtProduct);
            }
            // return datatable
            return View(dtProduct);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {

            //Enviar a la vista un producto es opcional, pero es mejor enviarlo por si acaso
            return View(new Product());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "insert into products values(@ProductName, @Price, @Count)";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                //productname es en realidad name pero se cambio el nombre atravez de una anotacion[]
                sqlCommand.Parameters.AddWithValue("@ProductName", product.name);
                sqlCommand.Parameters.AddWithValue("@Price", product.price);
                sqlCommand.Parameters.AddWithValue("@Count", product.count);
                sqlCommand.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
            
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = new Product();
            DataTable dataTable = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                string query = "select * from products where id = @id";
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connectionString);
                sqlDataAdapter.SelectCommand.Parameters.AddWithValue("@id", id);
                sqlDataAdapter.Fill(dataTable);
                if (dataTable.Rows.Count == 1)
                {
                    product.id = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                    product.name = dataTable.Rows[0][1].ToString();
                    product.price = Convert.ToDecimal(dataTable.Rows[0][2].ToString());
                    product.count = Convert.ToInt32(dataTable.Rows[0][3].ToString());
                    return View(product);
                }
                else
                 return RedirectToAction("Index");
                
            }
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            //misma estrructura que el metodo create
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                //productname es en realidad name pero se cambio el nombre atravez de una anotacion[]
                string query = "update products set name = @ProductName, price = @price, count = @count where id = @id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.Parameters.AddWithValue("@id", product.id);
                sqlCommand.Parameters.AddWithValue("@productName", product.name);
                sqlCommand.Parameters.AddWithValue("@price", product.price);
                sqlCommand.Parameters.AddWithValue("@count", product.count);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
            
        }

        // GET: Product/Delete/5 /////// SI LLEGO A COPIAR ESTO EN LA URL REALMENTE ME VA A BORRAR ESE ELEMENTO CON ID X, CUIDADO 
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                //productname es en realidad name pero se cambio el nombre atravez de una anotacion[]
                string query = "delete from products where id = @id";
                SqlCommand sqlCommand = new SqlCommand(query, sqlCon);
                sqlCommand.Parameters.AddWithValue("@id", id);
                sqlCommand.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5 // en este ejemplo no vamos a usar este metodo porque no usamos un formulario post, 
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
