namespace SupermarketEntityData.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SupermarketModel;

    public sealed class Configuration : DbMigrationsConfiguration<SupermarketEntityData.SupermarketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(SupermarketEntityData.SupermarketContext context)
        {
            context.Update();
        }
    }
}
