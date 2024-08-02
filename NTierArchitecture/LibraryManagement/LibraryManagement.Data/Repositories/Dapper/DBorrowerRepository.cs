using Dapper;
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

        public int Add(Borrower newBorrower)
        {
            using (var cn = new SqlConnection(_cnString))
            {
                var command = @"INSERT INTO Borrower (FirstName, LastName, Email, Phone)
                                VALUES (@FirstName, @LastName, @Email, @Phone)
                                SELECT SCOPE_IDENTITY()";
                var parameters = new
                {
                    newBorrower.FirstName,
                    newBorrower.LastName,
                    newBorrower.Email,
                    newBorrower.Phone
                };

                try
                {
                    return cn.ExecuteScalar<int>(command, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return -1;
                }
            }
        }

        public bool Delete(Borrower borrower)
        {
            using (var cn = new SqlConnection(_cnString))
            {
                cn.Open();
                using (var transaction = cn.BeginTransaction())
                {
                    try
                    {
                        var deleteCheckoutLogsCommand = "DELETE FROM CheckoutLog WHERE BorrowerID = @BorrowerID";
                        cn.Execute(deleteCheckoutLogsCommand, new { borrower.BorrowerID }, transaction);

                        var deleteBorrowerCommand = "DELETE FROM Borrower WHERE BorrowerID = @BorrowerID";
                        cn.Execute(deleteBorrowerCommand, new { borrower.BorrowerID }, transaction);

                        transaction.Commit();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex.Message);
                        return false;
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
            Borrower? borrower = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT *
                                  FROM Borrower
                                  WHERE Email = @Email";

                    borrower = cn.QueryFirstOrDefault<Borrower>(command, new { Email = email });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return borrower;
        }

        public List<CheckoutLog>? GetCheckoutLogs(Borrower borrower)
        {
            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT cl.CheckoutLogID, cl.BorrowerID, cl.MediaID, cl.CheckoutDate, cl.DueDate, cl.ReturnDate,
                                           m.MediaID, m.MediaTypeID, m.Title, m.IsArchived
                                    FROM CheckoutLog cl
                                    INNER JOIN Media m ON m.MediaID = cl.MediaID
                                    WHERE cl.BorrowerID = @BorrowerID";

                    List<CheckoutLog> list = cn.Query<CheckoutLog, Media, CheckoutLog>(
                        command,
                        (cl, m) =>
                        {
                            cl.Media = m;
                            return cl;
                        },
                        new { borrower.BorrowerID },
                        splitOn: "MediaID"
                    ).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }


        public bool Update(Borrower request)
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
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
