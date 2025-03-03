using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Client.Services
{
    public class NavigationService
    {
        // Instance singleton
        private static NavigationService? _instance;
        private static readonly object _lock = new();

        // Contrôle de contenu qui affichera les pages
        private ContentControl? _contentControl;

        // Pile de navigation pour gérer l'historique
        private Stack<UserControl> _navigationStack = new();

        // Page actuelle
        private UserControl? _currentPage;

        // Action à exécuter lors d'un changement de page
        public event Action<UserControl>? CurrentPageChanged;

        private NavigationService() { }

        public static NavigationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new NavigationService();
                    }
                }
                return _instance;
            }
        }

        // Initialiser le service de navigation avec le ContentControl qui affichera les pages
        public void Initialize(ContentControl contentControl)
        {
            _contentControl = contentControl;
        }

        // Naviguer vers une nouvelle page
        public void NavigateTo(UserControl page)
        {
            if (_contentControl == null)
            {
                throw new InvalidOperationException("Le service de navigation n'a pas été initialisé.");
            }

            // Sauvegarder la page actuelle dans la pile de navigation (si elle existe)
            if (_currentPage != null)
            {
                _navigationStack.Push(_currentPage);
            }

            // Mettre à jour la page actuelle et l'afficher
            _currentPage = page;
            _contentControl.Content = page;

            // Déclencher l'événement de changement de page
            CurrentPageChanged?.Invoke(page);
        }

        // Naviguer vers la page précédente
        public bool GoBack()
        {
            if (_navigationStack.Count == 0)
            {
                return false; // Impossible de revenir en arrière
            }

            // Récupérer la page précédente
            _currentPage = _navigationStack.Pop();

            // Afficher la page précédente
            if (_contentControl != null)
            {
                _contentControl.Content = _currentPage;

                // Déclencher l'événement de changement de page
                CurrentPageChanged?.Invoke(_currentPage);
                return true;
            }

            return false;
        }

        // Vider l'historique de navigation
        public void ClearHistory()
        {
            _navigationStack.Clear();
        }

        // Vérifier si la navigation arrière est possible
        public bool CanGoBack()
        {
            return _navigationStack.Count > 0;
        }
    }
}