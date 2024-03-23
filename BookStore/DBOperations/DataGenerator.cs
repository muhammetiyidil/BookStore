using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(new Book { Id = 1, Title = "İnsan Neyle Yaşar", GenreId = 1, PageCount = 120, PublishDate = new DateTime(2004, 02, 28) },
                                       new Book { Id = 2, Title = "Lean Personal", GenreId = 1, PageCount = 200, PublishDate = new DateTime(2004, 04, 30) },
                                       new Book { Id = 3, Title = "Denizler Altında Fersah", GenreId = 2, PageCount = 440, PublishDate = new DateTime(2004, 08, 29) });
                context.SaveChanges();
            }

        }
    }
}
