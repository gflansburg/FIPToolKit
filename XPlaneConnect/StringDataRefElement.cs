using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XPlaneConnect
{
    public class StringDataRefElement : DataRefElement
    {
        private static readonly object lockElement = new object();
        public int StringLength { get; set; }
        public DateTime LastUpdateTime { get; set; }
        new public string Value { get; set; }
        new public List<NotifyChangeHandler> Delegates = new List<NotifyChangeHandler>();

        new public delegate void NotifyChangeHandler(StringDataRefElement sender, string newValue);
        new public event NotifyChangeHandler OnValueChange;

        private int CharactersInitialized;

        public bool IsCompletelyInitialized
        {
            get
            {
                return CharactersInitialized >= StringLength;
            }
        }

        protected override void ValueChanged()
        {
            OnValueChange?.Invoke(this, Value);
        }

        public void Update(int index, char character)
        {
            lock (lockElement)
            {
                if( (DateTime.Now - LastUpdateTime) > MaxAge || ForceUpdate)
                {
                    // The string has changed, this is the first character received of the new string, so we invalidate the previous string
                    CharactersInitialized = 0;
                    Value = string.Empty;
                }
                LastUpdateTime = DateTime.Now;
                ForceUpdate = false;

                var fireEvent = !IsCompletelyInitialized;

                if (!IsCompletelyInitialized)
                    CharactersInitialized++;

                if (character > 0)
                {
                    if (Value.Length <= index)
                        Value = Value.PadRight(index + 1, ' ');

                    var current = Value[index];
                    if (current != character)
                    {
                        Value = Value.Remove(index, 1).Insert(index, character.ToString());
                        fireEvent = true;
                    }
                }
                
                if (IsCompletelyInitialized && fireEvent || ForceUpdate)
                {
                    ValueChanged();
                    CharactersInitialized = 0;
                }
            }
        }

        public StringDataRefElement()
        {
            CharactersInitialized = 0;
            Value = string.Empty;
        }
    }
}
