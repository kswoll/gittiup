using System;
using System.ComponentModel;
using System.Reflection;
using Movel.Utils;

namespace Movel.Ears
{
    public class Ear<TOutput> : DisposableHost, IEar<TOutput>
    {
        public TOutput Value { get; private set; }
        public event EarValueChangedHandler<TOutput> ValueChanged;

        private readonly object target;
        private readonly PropertyInfo[] path;
        private readonly object[] cache;
        private readonly EarListener[] listeners;

        public Ear(object target, PropertyInfo[] path)
        {
            if (path == null)
                throw new ArgumentNullException(nameof(path));
            if (path.Length == 0)
                throw new ArgumentException($"'{nameof(path)}' cannot be empty");

            this.target = target ?? throw new ArgumentNullException(nameof(target));
            this.path = path;
            cache = new object[path.Length + 1];
            cache[0] = target;
            listeners = new EarListener[path.Length];

            RecalculateValue(0);
            AddListeners(0);
        }

        protected override void OnDispose()
        {
            RemoveListeners(0);
        }

        private void RecalculateValue(int startingPoint)
        {
            var current = cache[startingPoint];
            var isDone = false;

            for (var i = startingPoint; i < path.Length; i++)
            {
                var property = path[i];

                if (!isDone)
                {
                    var next = property.GetValue(current, null);
                    current = next;

                    if (current == null)
                    {
                        isDone = true;
                    }
                }
                cache[i + 1] = current;
            }

            var oldValue = Value;
            var newValue = (TOutput)(current ?? default(TOutput));
            if (!Equals(oldValue, newValue))
            {
                Value = (TOutput)current;
                ValueChanged?.Invoke(this, oldValue, newValue);
            }
        }

        private void AddListeners(int startingPoint)
        {
            var current = cache[startingPoint];
            for (var i = startingPoint; i < path.Length; i++)
            {
                var property = path[i];

                if (current is INotifyPropertyChanged target)
                {
                    var listener = EarListener.Start(this, target, i);
                    target.PropertyChanged += listener.TargetOnPropertyChanged;
                    listeners[i] = listener;
                }

                var next = property.GetValue(current, null);
                current = next;

                if (current == null)
                {
                    break;
                }
            }
        }

        private void RemoveListeners(int startingPoint)
        {
            var current = cache[startingPoint];
            for (var i = startingPoint; i < path.Length; i++)
            {
                var property = path[i];

                if (current is INotifyPropertyChanged target)
                {
                    var listener = listeners[i];
                    target.PropertyChanged -= listener.TargetOnPropertyChanged;
                }

                var next = property.GetValue(current, null);
                current = next;

                if (current == null)
                {
                    break;
                }
            }
        }

        private class EarListener
        {
            private readonly Ear<TOutput> ear;
            private readonly int index;

            private EarListener(Ear<TOutput> ear, int index)
            {
                this.ear = ear ?? throw new ArgumentNullException(nameof(ear));
                if (index < 0 || index >= ear.path.Length)
                    throw new IndexOutOfRangeException(nameof(index));
                this.index = index;
            }

            public static EarListener Start(Ear<TOutput> ear, INotifyPropertyChanged target, int index)
            {
                var listener = new EarListener(ear, index);
                target.PropertyChanged += listener.TargetOnPropertyChanged;
                return listener;
            }


            public void TargetOnPropertyChanged(object o, PropertyChangedEventArgs propertyChangedEventArgs)
            {
                var property = ear.path[index];
                if (propertyChangedEventArgs.PropertyName == property.Name)
                {
                    var oldValue = ear.cache[index + 1];
                    var newValue = property.GetValue(ear.cache[index], null);

                    if (!Equals(oldValue, newValue))
                    {
                        ear.RemoveListeners(index);
                        ear.AddListeners(index);
                        ear.RecalculateValue(index);
                    }
                }
            }
        }
    }
}