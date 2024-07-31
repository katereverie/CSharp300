﻿using Dapper;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Data.Repositories.Dapper
{
    public class DBorrowerRepository : IBorrowerRepository
    {
        private string _cnString;

        public DBorrowerRepository(string connectionString)
        {
            _cnString = connectionString;
        }

        public void Add(Borrower newBorrower)
        {
            using (var cn = new SqlConnection(_cnString))
            {
                var command = @"INSERT INTO Borrower (FirstName, LastName, Email, Phone)
                                VALUES (@FirstName, @LastName, @Email, @Phone)";
                var parameters = new
                {
                    newBorrower.FirstName,
                    newBorrower.LastName,
                    newBorrower.Email,
                    newBorrower.Phone
                };

                try
                {
                    cn.Execute(command, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void Delete(Borrower borrower)
        {
            using (var cn = new SqlConnection(_cnString))
            {
                cn.Open();
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var deleteCheckoutLogsCommand = "DELETE FROM CheckoutLog WHERE BorrowerID = @BorrowerID";
                        cn.Execute(deleteCheckoutLogsCommand, new { BorrowerID = borrower.BorrowerID }, transaction);

                        var deleteBorrowerCommand = "DELETE FROM Borrower WHERE BorrowerID = @BorrowerID";
                        cn.Execute(deleteBorrowerCommand, new { BorrowerID = borrower.BorrowerID }, transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        public List<Borrower> GetAll()
        {
            List<Borrower> list = new List<Borrower>();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = "SELECT * FROM Borrower";
                    list = cn.Query<Borrower>(command).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public Borrower? GetByEmail(string email)
        {
            Borrower? borrower = new Borrower();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT *
                                  FROM Borrower
                                  WHERE Email = @Email";
                    var parameter = new
                    {
                        Email = email
                    };

                    borrower = cn.QueryFirstOrDefault<Borrower>(command, parameter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return borrower;
        }

        public void Update(Borrower request)
        {

            using (var cn = new SqlConnection(_cnString))
            {
                var command = @"UPDATE [Borrower] SET
                                        FirstName = @FirstName,
                                        LastName = @LastName,
                                        Email = @Email,
                                        Phone = @Phone
                                WHERE BorrowerID = @BorrowerID";
                var parameters = new
                {
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Phone,
                    request.BorrowerID
                };

                try
                {
                    cn.Execute(command, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}