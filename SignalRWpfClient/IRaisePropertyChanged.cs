using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SignalRWpfClient
{
    public interface IRaisePropertyChanged
    {
        void OnPropertyChanged(string propertyName);
    }

    public static class PropertyChangedExtensions
    {
        public static void SetValueIfDifferent<T>(this IRaisePropertyChanged source, ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            // ReSharper disable once ExplicitCallerInfoArgument
            source.SetValueIfDifferent(ref field, value, EqualityComparer<T>.Default, propertyName);
        }

        public static void SetValueIfDifferent<T>(this IRaisePropertyChanged source, ref T field, T value, IEqualityComparer<T> comparer, [CallerMemberName] string propertyName = null)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (comparer == null) throw new ArgumentNullException("comparer");

            if (comparer.Equals(field, value))
                return;

            field = value;
            source.OnPropertyChanged(propertyName);
        }
    }
}