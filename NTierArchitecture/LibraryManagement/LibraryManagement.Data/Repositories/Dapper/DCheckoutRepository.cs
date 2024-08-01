using Dapper;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Data.Repositories.Dapper
{
    public class DCheckoutRepository : ICheckoutRepository
    {
        private string _cnString;

        public DCheckoutRepository(string connectionString)
        {
            _cnString = connectionString;
        }

        public int Add(CheckoutLog newCheckoutLog)
        {
            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"INSERT INTO CheckoutLog (BorrowerID, MediaID, CheckoutDate, DueDate, ReturnDate)
                                    VALUES (@BorrowerID, @MediaID, @CheckoutDate, @DueDate, @ReturnDate)
                                    SELECT SCOPE_IDENTITY()";

                    var parameters = new
                    {
                        newCheckoutLog.BorrowerID,
                        newCheckoutLog.MediaID,
                        newCheckoutLog.CheckoutDate,
                        newCheckoutLog.DueDate,
                        newCheckoutLog.ReturnDate
                    };

                    return cn.ExecuteScalar<int>(command, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        public List<CheckoutLog> GetAllCheckedoutMedia()
        {
            List<CheckoutLog> list = new List<CheckoutLog>();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"
                                    SELECT cl.CheckoutLogID, cl.BorrowerID, cl.MediaID, cl.CheckoutDate, cl.DueDate, cl.ReturnDate,
                                           b.BorrowerID, b.FirstName, b.LastName, b.Email, b.Phone,
                                           m.MediaID, m.MediaTypeID, m.Title, m.IsArchived
                                    FROM CheckoutLog cl
                                    INNER JOIN Borrower b ON b.BorrowerID = cl.BorrowerID
                                    INNER JOIN Media m ON m.MediaID = cl.MediaID
                                    WHERE cl.ReturnDate IS NULL";

                    list = cn.Query<CheckoutLog, Borrower, Media, CheckoutLog>(
                        command,
                        (cl, borrower, media) =>
                        {
                            cl.Borrower = borrower;
                            cl.Media = media;
                            return cl;
                        },
                        splitOn: "BorrowerID,MediaID"
                    ).ToList();
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
            Borrower? borrower = null;

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT * 
                                    FROM Borrower
                                    WHERE Email = @Email";

                    borrower = cn.QueryFirstOrDefault<Borrower>(command, new { email });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return borrower;
        }

        public List<CheckoutLogDto> GetCheckedoutMediaByBorrowerID(int borrowerID)
        {
            List<CheckoutLogDto> list = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT cl.CheckoutLogID, m.Title
                                    FROM CheckoutLog cl
                                    INNER JOIN Media m ON m.MediaID = cl.MediaID
                                    WHERE cl.BorrowerID = @BorrowerID
                                    AND cl.ReturnDate IS NULL
                                    GROUP BY cl.CheckoutLogID, m.Title";

                    list = cn.Query<CheckoutLogDto>(command, new { borrowerID }).ToList();                 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public List<CheckoutLog> GetCheckoutLogsByBorrowerID(int borrowerID)
        {
            List<CheckoutLog> list = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT *
                                    FROM CheckoutLog
                                    WHERE BorrowerID = @BorrowerID";

                    list = cn.Query<CheckoutLog>(command, new { borrowerID }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public List<Media> GetUncheckedoutUnarchivedMedia()
        {
            List<Media> list = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT m.MediaID, m.MediaTypeID, m.Title, m.IsArchived
                                    FROM Media m
                                    LEFT JOIN (
                                        SELECT cl.MediaID, MAX(cl.CheckoutLogID) AS LatestCheckoutLogID
                                        FROM CheckoutLog cl
                                        GROUP BY cl.MediaID
                                    ) LatestLog ON m.MediaID = LatestLog.MediaID
                                    LEFT JOIN CheckoutLog cl ON LatestLog.LatestCheckoutLogID = cl.CheckoutLogID
                                    WHERE m.IsArchived = 0
                                    AND (LatestLog.MediaID IS NULL OR cl.ReturnDate IS NOT NULL)";

                    list = cn.Query<Media>(command).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public void Update(int checkoutLogID)
        {
            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"UPDATE [CheckoutLog] SET
                                            ReturnDate = @ReturnDate
                                    WHERE CheckoutLogID = @CheckoutLogID";

                    var parameters = new
                    {
                        ReturnDate = DateTime.Now,
                        checkoutLogID
                    };

                    cn.Execute(command, parameters);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
