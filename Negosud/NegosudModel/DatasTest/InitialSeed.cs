using NegosudModel.Context;
using NegosudModel.Entities;

namespace NegosudModel.DatasTest
{
    public static class InitialSeed
    {
        public static void SeedDatas()
        {
            //using (var context = new NegosudContext())
            //{
            //    AddSupplier(context);
            //    AddCustomer(context);
            //    AddFamily(context);
            //    AddArticle(context);
            //    AddReason(context);
            //    AddStatus(context);
            //}
        }

        public static void AddSupplier(NegosudContext context)
        {
            if (context.Suppliers.Any()) return;

            context.Suppliers.Add(new Supplier
            {
                Name = "Fortant",
                Address = "9, Quai Paul Riquet",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "04 67 80 90 90",
                CellPhoneNumber = "06 88 12 35 48"
            });

            context.Suppliers.Add(new Supplier
            {
                Name = "Joy",
                Address = "Lieu dit joy",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 62 09 03 20",
                CellPhoneNumber = "06 66 23 59 87"
            });

            context.Suppliers.Add(new Supplier
            {
                Name = "Jurançon",
                Address = "4, rue de la mosaïque",
                City = "Jurançon",
                ZipCode = "64110",
                LandlineNumber = "05 59 62 87 76",
                CellPhoneNumber = "06 25 36 98 74"
            });

            context.Suppliers.Add(new Supplier
            {
                Name = "Pellehaut",
                Address = "le petit Pellehaut",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 62 29 48 79",
                CellPhoneNumber = "06 55 24 17 89"
            });


            context.Suppliers.Add(new Supplier
            {
                Name = "Tariquet",
                Address = "château du tariquet",
                City = "Eauze",
                ZipCode = "32800",
                LandlineNumber = "05 62 09 87 82",
                CellPhoneNumber = "07 24 58 96 32"
            });

            context.Suppliers.Add(new Supplier
            {
                Name = "Uby",
                Address = "uby",
                City = "Cazaubon",
                ZipCode = "32150",
                LandlineNumber = "05 62 09 51 93",
                CellPhoneNumber = "07 88 96 32 54"
            });

            context.SaveChanges();
        }

        public static void AddCustomer(NegosudContext context)
        {
            if (context.Customers.Any()) return;

            context.Customers.Add(new Customer
            {
                Name = "Bernard",
                FirstName = "Sophie",
                DateOfBirth = new DateOnly(1982, 1, 1),
                Address = "12, rue des oliviers",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 34 56 78 90",
                CellPhoneNumber = "06 12 34 56 78"
            });

            context.Customers.Add(new Customer
            {
                Name = "Dubois",
                FirstName = "Alexandre",
                DateOfBirth = new DateOnly(1984, 10, 14),
                Address = "58, boulevard des vignes",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 40 67 89 01",
                CellPhoneNumber = "06 98 76 54 32"
            });

            context.Customers.Add(new Customer
            {
                Name = "Dupont",
                FirstName = "Lucas",
                DateOfBirth = new DateOnly(1988, 12, 20),
                Address = "25, avenue du Lac Bleu",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 59 23 45 67",
                CellPhoneNumber = "06 12 36 59 87"
            });

            context.Customers.Add(new Customer
            {
                Name = "Lefevre",
                FirstName = "Mathieu",
                DateOfBirth = new DateOnly(1975, 2, 17),
                Address = "7, impasse des chênes",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 58 14 25 36",
                CellPhoneNumber = "06 56 39 82 33"
            });


            context.Customers.Add(new Customer
            {
                Name = "Martin",
                FirstName = "Emilie",
                DateOfBirth = new DateOnly(1966, 6, 6),
                Address = "89, chemin des étables",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 53 12 34 56",
                CellPhoneNumber = "06 22 54 66 99"
            });

            context.Customers.Add(new Customer
            {
                Name = "Moreau",
                FirstName = "Claire",
                DateOfBirth = new DateOnly(1994, 3, 30),
                Address = "41, place du marché",
                City = "Pau",
                ZipCode = "64000",
                LandlineNumber = "05 55 67 89 00",
                CellPhoneNumber = "07 29 58 66 32"
            });

            context.SaveChanges();
        }

        public static void AddFamily(NegosudContext context)
        {
            if (context.Families.Any()) return;

            context.Families.Add(new Family
            {
                Name = "Rouge"
            });

            context.Families.Add(new Family
            {
                Name = "Blanc"
            });

            context.Families.Add(new Family
            {
                Name = "Rosé"
            });

            context.SaveChanges();
        }

        public static void AddArticle(NegosudContext context)
        {
            if (context.Articles.Any()) return;

            context.Articles.Add(new Article
            {
                Name = "Merlot",
                TVA = 0.20,
                Description = "Fruits rouges mûrs et épices",
                UnitPrice = 5.80,
                Quantity = 0,
                MinimumQuantity = 10,
                IsActive = true,
                SupplierId = 1,
                FamilyId = 1
            });

            context.Articles.Add(new Article
            {
                Name = "Chardonnay",
                TVA = 0.20,
                Description = "Fruits tropicaux et agrumes",
                UnitPrice = 6.10,
                Quantity = 0,
                MinimumQuantity = 20,
                IsActive = true,
                SupplierId = 1,
                FamilyId = 2
            });

            context.Articles.Add(new Article
            {
                Name = "Grenache Rosé",
                TVA = 0.20,
                Description = "Fraise et framboise",
                UnitPrice = 4.90,
                Quantity = 0,
                MinimumQuantity = 30,
                IsActive = true,
                SupplierId = 1,
                FamilyId = 3
            });

            context.Articles.Add(new Article
            {
                Name = "L'insolent",
                TVA = 0.20,
                Description = "Fruités",
                UnitPrice = 5.80,
                Quantity = 0,
                MinimumQuantity = 10,
                IsActive = true,
                SupplierId = 2,
                FamilyId = 1
            });

            context.Articles.Add(new Article
            {
                Name = "Ode à la Joie",
                TVA = 0.20,
                Description = "Touche boisée",
                UnitPrice = 6.10,
                Quantity = 0,
                MinimumQuantity = 20,
                IsActive = true,
                SupplierId = 2,
                FamilyId = 2
            });

            context.Articles.Add(new Article
            {
                Name = "Saint André",
                TVA = 0.20,
                Description = "Frais et sucré",
                UnitPrice = 4.90,
                Quantity = 0,
                MinimumQuantity = 30,
                IsActive = true,
                SupplierId = 2,
                FamilyId = 3
            });

            context.Articles.Add(new Article
            {
                Name = "Ballet d’Octobre",
                TVA = 0.20,
                Description = "Fruits exotiques, miel et épices",
                UnitPrice = 5.80,
                Quantity = 0,
                MinimumQuantity = 10,
                IsActive = true,
                SupplierId = 3,
                FamilyId = 1
            });

            context.Articles.Add(new Article
            {
                Name = "Cuvée Thibault",
                TVA = 0.20,
                Description = "Agrumes et fruits à chair blanche",
                UnitPrice = 6.10,
                Quantity = 0,
                MinimumQuantity = 20,
                IsActive = true,
                SupplierId = 3,
                FamilyId = 2
            });

            context.Articles.Add(new Article
            {
                Name = "Cuvée Jean",
                TVA = 0.20,
                Description = "Fruits confits, miel et fleurs blanches",
                UnitPrice = 4.90,
                Quantity = 0,
                MinimumQuantity = 30,
                IsActive = true,
                SupplierId = 3,
                FamilyId = 3
            });

            context.Articles.Add(new Article
            {
                Name = "Harmonie Rouge",
                TVA = 0.20,
                Description = "Fruité intense",
                UnitPrice = 5.80,
                Quantity = 0,
                MinimumQuantity = 10,
                IsActive = true,
                SupplierId = 4,
                FamilyId = 1
            });

            context.Articles.Add(new Article
            {
                Name = "Harmonie Blanc",
                TVA = 0.20,
                Description = "Pêche, ananas et fruit de la passion",
                UnitPrice = 6.10,
                Quantity = 0,
                MinimumQuantity = 20,
                IsActive = true,
                SupplierId = 4,
                FamilyId = 2
            });

            context.Articles.Add(new Article
            {
                Name = "L’été Gascon",
                TVA = 0.20,
                Description = "Poire, pêche et agrumes",
                UnitPrice = 4.90,
                Quantity = 0,
                MinimumQuantity = 30,
                IsActive = true,
                SupplierId = 4,
                FamilyId = 3
            });

            context.Articles.Add(new Article
            {
                Name = "Premières Grives",
                TVA = 0.20,
                Description = "Fruits exotiques et à chair blanche",
                UnitPrice = 5.80,
                Quantity = 0,
                MinimumQuantity = 10,
                IsActive = true,
                SupplierId = 5,
                FamilyId = 1
            });

            context.Articles.Add(new Article
            {
                Name = "Classic",
                TVA = 0.20,
                Description = "Agrumes et fleurs blanches",
                UnitPrice = 6.10,
                Quantity = 0,
                MinimumQuantity = 20,
                IsActive = true,
                SupplierId = 5,
                FamilyId = 2
            });

            context.Articles.Add(new Article
            {
                Name = "Réserve",
                TVA = 0.20,
                Description = "Fruits mûrs et vanille",
                UnitPrice = 4.90,
                Quantity = 0,
                MinimumQuantity = 30,
                IsActive = true,
                SupplierId = 5,
                FamilyId = 3
            });

            context.Articles.Add(new Article
            {
                Name = "UBY N°1",
                TVA = 0.20,
                Description = "Fruit de la passion, mangue et pamplemousse",
                UnitPrice = 5.80,
                Quantity = 0,
                MinimumQuantity = 10,
                IsActive = true,
                SupplierId = 6,
                FamilyId = 1
            });

            context.Articles.Add(new Article
            {
                Name = "UBY N°2",
                TVA = 0.20,
                Description = "Fruits tropicaux et agrumes",
                UnitPrice = 6.10,
                Quantity = 0,
                MinimumQuantity = 20,
                IsActive = true,
                SupplierId = 6,
                FamilyId = 2
            });

            context.Articles.Add(new Article
            {
                Name = "UBY N°3",
                TVA = 0.20,
                Description = "Framboise et fraise",
                UnitPrice = 4.90,
                Quantity = 0,
                MinimumQuantity = 30,
                IsActive = true,
                SupplierId = 6,
                FamilyId = 3
            });

            context.SaveChanges();
        }

        public static void AddReason(NegosudContext context)
        {
            if (context.Reasons.Any()) return;

            context.Reasons.Add(new Reason
            {
                Name = "Purchase",
                Color = "#4CAF50"
            });

            context.Reasons.Add(new Reason
            {
                Name = "Sale",
                Color = "#7E57C2"
            });

            context.Reasons.Add(new Reason
            {
                Name = "Broken",
                Color = "#F44336"
            });

            context.Reasons.Add(new Reason
            {
                Name = "Lost",
                Color = "#FF9800"
            });

            context.Reasons.Add(new Reason
            {
                Name = "Tasting",
                Color = "#00BCD4"
            });

            context.Reasons.Add(new Reason
            {
                Name = "Inventory",
                Color = "#009688"
            });

            context.Reasons.Add(new Reason
            {
                Name = "Correction",
                Color = "#2196F3"
            });

            context.SaveChanges();
        }

        public static void AddStatus(NegosudContext context)
        {
            if (context.Statuses.Any()) return;

            context.Statuses.Add(new Status
            {
                Name = "Pending",
                Color = "#FF9800"
            });

            context.Statuses.Add(new Status
            {
                Name = "Done",
                Color = "#7E57C2"
            });

            context.Statuses.Add(new Status
            {
                Name = "Cancelled",
                Color = "#F44336"
            });

            context.SaveChanges();
        }
    }
}
