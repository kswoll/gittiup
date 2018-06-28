using LiteDB;

namespace Gittiup.Library.Models
{
    public class GittiupDb : LiteDatabase
    {
        public LiteCollection<AccountModel> Accounts => GetCollection<AccountModel>("accounts");
        public LiteCollection<RepositoryModel> Repositories => GetCollection<RepositoryModel>("repositories");
        public LiteCollection<UserModel> Users => GetCollection<UserModel>("users");

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