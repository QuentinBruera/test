using NegosudModel.Dto;
using NegosudModel.Request;
using Newtonsoft.Json;

namespace NegosudWeb.Services
{
    public class CartService
    {
        private readonly HttpClient _httpClient;
        private List<ArticleDetailsDto> _cartItems = new List<ArticleDetailsDto>();
        private Dictionary<int, int> quantities = new();
        private CreateSaleRequest _pendingOrder;

        public event Action OnChange;

        public CartService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void SavePendingOrder(CreateSaleRequest order)
        {
            _pendingOrder = order;
        }

        public CreateSaleRequest GetPendingOrder()
        {
            return _pendingOrder;
        }

        public void AddToCart(ArticleDetailsDto article, int quantity)
        {
            if (_cartItems.Any(a => a.Id == article.Id))
            {
                quantities[article.Id] += quantity;
            }
            else
            {
                _cartItems.Add(article);
                quantities[article.Id] = quantity;
            }

            OnChange?.Invoke();
        }

        public int GetQuantity(int articleId)
        {
            return quantities.ContainsKey(articleId) ? quantities[articleId] : 0;
        }

        public static decimal GetUnitPriceTTC(ArticleDetailsDto article)
        {
            return (decimal)(article.UnitPrice * (1 + article.TVA));
        }

        public decimal GetTotalTTC()
        {
            return _cartItems.Sum(i => GetUnitPriceTTC(i) * (quantities.ContainsKey(i.Id) ? quantities[i.Id] : 1));
        }

        public void RemoveFromCart(ArticleDetailsDto article)
        {
            if (_cartItems.Contains(article))
            {
                _cartItems.Remove(article);
                OnChange?.Invoke();
            }
        }

        public List<ArticleDetailsDto> GetCartItems()
        {
            return _cartItems;
        }

        public bool IsInCart(int articleId)
        {
            return _cartItems.Any(a => a.Id == articleId);
        }

        public void UpdateQuantity(int articleId, int newQuantity)
        {
            if (quantities.ContainsKey(articleId))
            {
                quantities[articleId] = newQuantity;
                OnChange?.Invoke();
            }
        }

        public async Task<int?> CreateSaleAsync(CreateSaleRequest request)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            var response = await _httpClient.PostAsJsonAsync("api/sales", request, cts.Token);

            if (!response.IsSuccessStatusCode)
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erreur HTTP {response.StatusCode}: {errorMessage}");
            }

            var resultat = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<int>(resultat);
        }
    }
}
