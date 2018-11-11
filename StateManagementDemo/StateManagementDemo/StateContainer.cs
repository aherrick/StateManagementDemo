﻿using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace StateManagementDemo
{
    [ContentProperty("Content")]
    [Preserve(AllMembers = true)]
    public class StateCondition : View
    {
        public object Is { get; set; }
        public object IsNot { get; set; }
        public View Content { get; set; }
    }

    [ContentProperty("Conditions")]
    [Preserve(AllMembers = true)]
    public class StateContainer : ContentView
    {
        public List<StateCondition> Conditions { get; set; } = new List<StateCondition>();

        // https://forums.xamarin.com/discussion/102024/change-bindableproperty-create-to-bindableproperty-create
        //public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(object), typeof(StateContainer), propertyChanged: StateChanged);

        public static readonly BindableProperty StateProperty = BindableProperty.Create<StateContainer, object>(x => x.State, null, propertyChanged: StateChanged);

        public static void Init()
        {
        }

        private static void StateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var parent = bindable as StateContainer;
            parent?.ChooseStateProperty(newValue);
        }

        public object State
        {
            get { return GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        private void ChooseStateProperty(object newValue)
        {
            foreach (StateCondition stateCondition in Conditions)
            {
                if (stateCondition.Is != null)
                {
                    if (stateCondition.Is.ToString().Equals(newValue.ToString()))
                    {
                        Content = stateCondition.Content;
                    }
                }
                else if (stateCondition.IsNot != null)
                {
                    if (!stateCondition.IsNot.ToString().Equals(newValue.ToString()))
                    {
                        Content = stateCondition.Content;
                    }
                }
            }
        }
    }
}