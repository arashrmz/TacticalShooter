using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Src.Scripts.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private int pickupTime = 2000;

        private CancellationTokenSource _cancellationTokenSource;
        private bool _isPlayerOverItem;

        // Called when the player enters the item's collider
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerOverItem = true;
                if (_cancellationTokenSource == null || _cancellationTokenSource.IsCancellationRequested)
                    StartCountdown();
            }
        }

        // Called when the player exits the item's collider
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _isPlayerOverItem = false;

                if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
                {
                    // Cancel the ongoing countdown.
                    _cancellationTokenSource.Cancel();

                    Debug.Log("Countdown cancelled.");

                    _cancellationTokenSource.Dispose();
                }
            }
        }

        // Method for handling the countdown asynchronously
        private async void StartCountdown()
        {
            try
            {
                _cancellationTokenSource = new CancellationTokenSource();

                await Task.Delay(pickupTime, cancellationToken: _cancellationTokenSource.Token);

                if (_isPlayerOverItem)
                {
                    Debug.Log("Received Item!");

                    // Apply logic to give item to player here...

                    gameObject.SetActive(false); // Disable or remove item from scene after receiving it.      
                }
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Countdown cancelled.");
            }

            finally
            {
                _cancellationTokenSource.Dispose();
                _isPlayerOverItem = false;
            }
        }
    }
}