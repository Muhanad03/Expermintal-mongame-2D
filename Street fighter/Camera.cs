using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace rpg
{
    public class Camera
    {
        private Vector2 _position;
        private Vector2 _offset;
        private float _lerpFactor;

        private float _shakeDuration;
        private float _shakeMagnitude;
        private float _shakeTimer;
        private Vector2 _shakeOffset;
        private Random _random;

        private float _zoom; // Add a zoom factor
        private float _maxZoom = 2.0f; // Set the maximum zoom level
        private float _minZoom = 0.5f; // Set the minimum zoom level

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public Vector2 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = MathHelper.Clamp(value, _minZoom, _maxZoom); }
        }

        public Camera(Vector2 offset, float lerpFactor)
        {
            _position = Vector2.Zero;
            _offset = offset;
            _lerpFactor = lerpFactor;

            _shakeDuration = 0f;
            _shakeMagnitude = 0f;
            _shakeTimer = 0f;
            _shakeOffset = Vector2.Zero;
            _random = new Random();
            _zoom = 1.0f; // Initial zoom level
        }

        public void Update(GameTime gameTime, Vector2 targetPosition)
        {
            // Calculate the position to keep the player centered
            Vector2 newPosition = targetPosition + _offset + _shakeOffset;

            // Calculate the difference between the old and new positions
            Vector2 positionDifference = newPosition - _position;

            // Apply the zoom factor to the position difference
            positionDifference /= _zoom;

            // Update the camera position using lerping
            _position += positionDifference * _lerpFactor;

            if (_shakeTimer > 0f)
            {
                _shakeTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

                float magnitude = _shakeMagnitude * (_shakeTimer / _shakeDuration);
                _shakeOffset = new Vector2((float)_random.NextDouble() - 0.5f, (float)_random.NextDouble() - 0.5f) * magnitude;
            }
            else
            {
                _shakeOffset = Vector2.Zero;
            }
        }


        public void Shake(float duration, float magnitude)
        {
            _shakeDuration = duration;
            _shakeMagnitude = magnitude;
            _shakeTimer = duration;
        }

        public Matrix GetViewMatrix()
        {
            return Matrix.CreateTranslation(new Vector3(-_position, 0f)) *
                   Matrix.CreateScale(new Vector3(_zoom, _zoom, 1f));
        }
    }
}
