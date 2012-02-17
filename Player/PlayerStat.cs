using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EngineLibrary
{
    public class PlayerStat
    {
        public PlayerStat(string name, float initialValue)
        {
            this.Name = name;
            _currentValue = initialValue;
        }
        public PlayerStat(string name, float initialValue, float minValue)
        {
            this.Name = name;
            _currentValue = initialValue;
            MinValue = minValue;
        }
        public PlayerStat(string name, float initialValue, float minValue, float maxValue)
        {
            this.Name = name;
            _currentValue = initialValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }
        /// <summary>
        /// A human readable name for this statistic.
        /// </summary>
        public string Name { get; set; }
        private float _currentValue, _minValue, _maxValue;
        /// <summary>
        /// The value the player stat currently holds.
        /// </summary>
        public float CurrentValue
        {
            get { return _currentValue; }
            set
            {
                float setValue = value;
                if (UseMinValue && value < _minValue)
                    setValue = _minValue;
                if (UseMaxValue && value > _maxValue)
                    setValue = _maxValue;
                if (!StatValueBeforeChanged.Invoke(_currentValue, setValue))
                    return;
                _currentValue = setValue;
                StatValueHasChanged.Invoke(_currentValue, setValue);
            }
        }
        /// <summary>
        /// The minimum value this stat may have. Will only be heeded if the UseMinValue property is set to
        /// true. Setting a minimum value automatically enables UseMinValue. Value must be less than or equal
        /// to MaxValue, if MaxValue is set.
        /// </summary>
        public float MinValue
        {
            get { return _minValue; }
            set
            {
                if (UseMaxValue && value > _maxValue)
                    throw new ArgumentOutOfRangeException("Minimum value may not exceed maximum value.");
                _minValue = value;
                if (_currentValue < _minValue)
                    _currentValue = _minValue;
                UseMinValue = true;
            }
        }
        /// <summary>
        /// The maximum value this stat may have. Will only be heeded if the UseMaxValue property is set to 
        /// true. Setting a maximum value automatically enables UseMaxValue. Value must be greater than or equal
        /// to MinValue, if MinValue is set.
        /// </summary>
        public float MaxValue
        {
            get { return _maxValue; }
            set
            {
                if (UseMinValue && value < _minValue)
                    throw new ArgumentOutOfRangeException("Maximum value may not be less than minimum value.");
                _maxValue = value;
                if (_currentValue > _maxValue)
                    _currentValue = _maxValue;
                UseMaxValue = true;
            }
        }
        public bool UseMaxValue { get; set; }
        public bool UseMinValue { get; set; }

        public delegate void StatValueChange(float oldValue, float newValue);
        public event StatValueChange StatValueHasChanged;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldValue">The value of the player stat before the change occurs.</param>
        /// <param name="newValue">The value of the player stat after the change occurs.</param>
        /// <returns>A boolean value specifying if this change should occur. True permits the value change,
        /// false halts the change.</returns>
        public delegate bool StatValueBeforeChange(float oldValue, float newValue);
        public event StatValueBeforeChange StatValueBeforeChanged;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        /// <summary>
        /// Determines if two PlayerStats are contain equal values.
        /// </summary>
        /// <param name="obj">The other PlayerStat to compare to.</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is PlayerStat))
                return false;
            PlayerStat otherStat = (PlayerStat)obj;
            if (otherStat.CurrentValue != _currentValue)
                return false;
            if (otherStat.UseMaxValue != UseMaxValue ||
                (otherStat.UseMaxValue == UseMaxValue && otherStat.MaxValue != _maxValue))
                return false;
            if (otherStat.UseMinValue != UseMinValue ||
                (otherStat.UseMinValue == UseMinValue && otherStat.MinValue != _minValue))
                return false;
            if (!otherStat.Name.ToLower().Equals(Name.ToLower()))
                return false;
            return true;
        }
        /// <summary>
        /// Returns the current values of the PlayerStat as a human readable string.
        /// </summary>
        /// <returns>The string contents of the PlayerStat</returns>
        public override string ToString()
        {
            return "{PlayerStat: '" + Name + "': " + _currentValue.ToString() +
                (UseMinValue || UseMaxValue ? " (" : "") + (UseMinValue ? "Min: " +
                MinValue.ToString() : "") + (UseMinValue && UseMaxValue ? ", " : "") +
                (UseMaxValue ? "Max: " + MaxValue.ToString() : "") + (UseMinValue ||
                UseMaxValue ? ")" : "") + "}";
        }
    }
}
