using LiteDB;

namespace Gittiup.Library.Models
{
    public class GittiupDb : LiteDatabase
    {
        public LiteCollection<AccountModel> Accounts => GetCollection<AccountModel>("accounts");
        public LiteCollection<RepositoryModel> Repositories => GetCollection<RepositoryModel>("repositories").Include(x => x.Account);
        public LiteCollection<UserModel> Users => GetCollection<UserModel>("users");

        static GittiupDb()
        {
            var mapper = BsonMapper.Global;
            mapper.Entity<RepositoryModel>().DbRef(x => x.Account, "accounts");
        }

        public GittiupDb() : base(Settings.DbFile)
        {
        }
    }
}