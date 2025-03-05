namespace Negosud.Utils
{
    public static class StatusTranslator
    {
        private static readonly Dictionary<string, Dictionary<string, string>> ContextualTranslations = new()
        {
            {
                "Purchase", new Dictionary<string, string>
                {
                    { "Pending", "En attente" },
                    { "Done", "Reçue" },
                    { "Cancelled", "Annulée" }
                }
            },
            {
                "Inventory", new Dictionary<string, string>
                {
                    { "Done", "Terminé" },
                    { "Cancelled", "Annulé" }
                }
            },
            {
                "Sale", new Dictionary<string, string>
                {
                    { "Pending", "En attente" },
                    { "Done", "Payée" },
                    { "Cancelled", "Annulée" }
                }
            }
        };

        public static string Translate(string context, string statusName)
        {
            if (ContextualTranslations.TryGetValue(context, out var translations) &&
                translations.TryGetValue(statusName, out var translatedName))
            {
                return translatedName;
            }

            return statusName;
        }
    }
}
