using LiteDB;

namespace Gittiup.Library.Models
{
    public class GittiupDb : LiteDatabase
    {
        public LiteCollection<AccountModel> Accounts => GetCollection<AccountModel>("accounts");
        public LiteCollection<RepositoryModel> Repositories => GetCollection<RepositoryModel>("repositories");

        static GittiupDb()
        {
            var mapper = BsonMapper.Global;
            mapper.Entity<RepositoryModel>().DbRef(x => x.Account);
        }

        public GittiupDb() : base(Settings.DbFile)
        {
        }
    }
}