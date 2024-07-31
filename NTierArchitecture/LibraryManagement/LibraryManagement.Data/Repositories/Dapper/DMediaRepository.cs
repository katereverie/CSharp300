using Dapper;
using LibraryManagement.Core.Entities;
using LibraryManagement.Core.Interfaces.Repositories;
using Microsoft.Data.SqlClient;

namespace LibraryManagement.Data.Repositories.Dapper
{
    public class DMediaRepository : IMediaRepository
    {
        private string _cnString;

        public DMediaRepository(string connectionString)
        {
            _cnString = connectionString;
        }

        public int Add(Media newMedia)
        {
            using (var cn = new SqlConnection(_cnString))
            {
                cn.Open();

                var command = @"INSERT INTO Media (MediaTypeID, Title, IsArchived)
                                VALUES (@MediaTypeID, @Title, @IsArchived);
                                SELECT CAST(SCOPE_IDENTITY() as int);";

                var parameters = new
                {
                    newMedia.MediaTypeID,
                    newMedia.Title,
                    newMedia.IsArchived
                };
                try
                {
                    return cn.ExecuteScalar<int>(command, parameters);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in Add method: {ex.Message}");
                    return -1;
                }
            }
        }

        public bool Archive(int mediaID)
        {
            using (var cn = new SqlConnection(_cnString))
            {
                var command = @"UPDATE [Media] SET
                                        IsArchived = @IsArchived
                                WHERE MediaID = @MediaID";

                var parameters = new
                {
                    IsArchived = true,
                    mediaID
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

        public List<Media> GetAll()
        {
            List<Media> list = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = "SELECT * FROM Media";
                    list = cn.Query<Media>(command).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public List<Media> GetAllArchived()
        {
            List<Media> list = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT MediaTypeID, Title
                                    FROM Media
                                    WHERE IsArchived = 1
                                    ORDER BY MediaTypeID, Title";

                    list = cn.Query<Media>(command).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public List<Media> GetAllUnarchived()
        {
            List<Media> list = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT * 
                                    FROM Media
                                    WHERE IsArchived = 0";

                    list = cn.Query<Media>(command).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public Media? GetMediaById(int mediaID)
        {
            Media? media = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT * 
                                    FROM Media 
                                    WHERE MediaID = @MediaID";

                    media = cn.QueryFirstOrDefault<Media>(command, new { MediaID = mediaID});
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return media;
        }

        public List<Media> GetMediaByType(int mediaTypeID)
        {
            List<Media> media = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT * 
                                    FROM Media 
                                    WHERE MediaTypeID = @MediaTypeID";

                    media = cn.Query<Media>(command, new { MediaTypeID = mediaTypeID }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return media;
        }

        public List<MediaCheckoutCount> GetTopThreeMostPopularMedia()
        {
            List<MediaCheckoutCount> list = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT TOP 3 m.MediaID, m.Title, mt.MediaTypeName, COUNT(cl.MediaID) AS CheckoutCount
                                    FROM Media m 
                                    INNER JOIN MediaType mt ON mt.MediaTypeID = m.MediaTypeID
                                    LEFT JOIN CheckoutLog cl ON cl.MediaID = m.MediaID
                                    GROUP BY m.MediaID, m.Title, mt.MediaTypeName
                                    ORDER BY CheckoutCount DESC";

                    list = cn.Query<MediaCheckoutCount>(command).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return list;
        }

        public List<Media> GetUnarchivedByType(int mediaTypeID)
        {
            List<Media> media = new();

            try
            {
                using (var cn = new SqlConnection(_cnString))
                {
                    var command = @"SELECT * 
                                    FROM Media 
                                    WHERE IsArchived = 0 AND MediaTypeID = @MediaTypeID";

                    media = cn.Query<Media>(command, new { MediaTypeID = mediaTypeID }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return media;
        }

        public void Update(Media request)
        {
            using (var cn = new SqlConnection(_cnString))
            {
                var command = @"UPDATE [Media] SET
                                        MediaTypeID = @MediaTypeID,
                                        Title = @Title
                                WHERE MediaID = @MediaID";
                var parameters = new
                {
                    request.MediaTypeID,
                    request.Title,
                    request.MediaID,
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
