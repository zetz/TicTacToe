// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Akka.Interfaced CodeGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using ProtoBuf;
using TrackableData;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;

#region IUserData

namespace Domain.Data
{
    [ProtoContract]
    public partial class TrackableUserData : IUserData
    {
        [IgnoreDataMember]
        public IPocoTracker<IUserData> Tracker { get; set; }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IUserData>)value;
                Tracker = t;
            }
        }

        ITracker<IUserData> ITrackable<IUserData>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (IPocoTracker<IUserData>)value;
                Tracker = t;
            }
        }

        public ITrackable GetChildTrackable(object name)
        {
            switch ((string)name)
            {
                default:
                    return null;
            }
        }

        public IEnumerable<KeyValuePair<object, ITrackable>> GetChildTrackables(bool changedOnly = false)
        {
            yield break;
        }

        public static class PropertyTable
        {
            public static readonly PropertyInfo Name = typeof(IUserData).GetProperty("Name");
            public static readonly PropertyInfo RegisterTime = typeof(IUserData).GetProperty("RegisterTime");
            public static readonly PropertyInfo LastLoginTime = typeof(IUserData).GetProperty("LastLoginTime");
            public static readonly PropertyInfo LoginCount = typeof(IUserData).GetProperty("LoginCount");
            public static readonly PropertyInfo PlayCount = typeof(IUserData).GetProperty("PlayCount");
            public static readonly PropertyInfo WinCount = typeof(IUserData).GetProperty("WinCount");
            public static readonly PropertyInfo LoseCount = typeof(IUserData).GetProperty("LoseCount");
            public static readonly PropertyInfo DrawCount = typeof(IUserData).GetProperty("DrawCount");
        }

        private string _Name;

        [ProtoMember(1)] public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (Tracker != null && Name != value)
                    Tracker.TrackSet(PropertyTable.Name, _Name, value);
                _Name = value;
            }
        }

        private DateTime _RegisterTime;

        [ProtoMember(2)] public DateTime RegisterTime
        {
            get
            {
                return _RegisterTime;
            }
            set
            {
                if (Tracker != null && RegisterTime != value)
                    Tracker.TrackSet(PropertyTable.RegisterTime, _RegisterTime, value);
                _RegisterTime = value;
            }
        }

        private DateTime _LastLoginTime;

        [ProtoMember(3)] public DateTime LastLoginTime
        {
            get
            {
                return _LastLoginTime;
            }
            set
            {
                if (Tracker != null && LastLoginTime != value)
                    Tracker.TrackSet(PropertyTable.LastLoginTime, _LastLoginTime, value);
                _LastLoginTime = value;
            }
        }

        private int _LoginCount;

        [ProtoMember(4)] public int LoginCount
        {
            get
            {
                return _LoginCount;
            }
            set
            {
                if (Tracker != null && LoginCount != value)
                    Tracker.TrackSet(PropertyTable.LoginCount, _LoginCount, value);
                _LoginCount = value;
            }
        }

        private int _PlayCount;

        [ProtoMember(5)] public int PlayCount
        {
            get
            {
                return _PlayCount;
            }
            set
            {
                if (Tracker != null && PlayCount != value)
                    Tracker.TrackSet(PropertyTable.PlayCount, _PlayCount, value);
                _PlayCount = value;
            }
        }

        private int _WinCount;

        [ProtoMember(6)] public int WinCount
        {
            get
            {
                return _WinCount;
            }
            set
            {
                if (Tracker != null && WinCount != value)
                    Tracker.TrackSet(PropertyTable.WinCount, _WinCount, value);
                _WinCount = value;
            }
        }

        private int _LoseCount;

        [ProtoMember(7)] public int LoseCount
        {
            get
            {
                return _LoseCount;
            }
            set
            {
                if (Tracker != null && LoseCount != value)
                    Tracker.TrackSet(PropertyTable.LoseCount, _LoseCount, value);
                _LoseCount = value;
            }
        }

        private int _DrawCount;

        [ProtoMember(8)] public int DrawCount
        {
            get
            {
                return _DrawCount;
            }
            set
            {
                if (Tracker != null && DrawCount != value)
                    Tracker.TrackSet(PropertyTable.DrawCount, _DrawCount, value);
                _DrawCount = value;
            }
        }
    }

    [ProtoContract]
    public class TrackableUserDataTrackerSurrogate
    {
        [ProtoMember(1)] public EnvelopedObject<string> Name;
        [ProtoMember(2)] public EnvelopedObject<DateTime> RegisterTime;
        [ProtoMember(3)] public EnvelopedObject<DateTime> LastLoginTime;
        [ProtoMember(4)] public EnvelopedObject<int> LoginCount;
        [ProtoMember(5)] public EnvelopedObject<int> PlayCount;
        [ProtoMember(6)] public EnvelopedObject<int> WinCount;
        [ProtoMember(7)] public EnvelopedObject<int> LoseCount;
        [ProtoMember(8)] public EnvelopedObject<int> DrawCount;

        public static implicit operator TrackableUserDataTrackerSurrogate(TrackablePocoTracker<IUserData> tracker)
        {
            if (tracker == null)
                return null;

            var surrogate = new TrackableUserDataTrackerSurrogate();
            foreach(var changeItem in tracker.ChangeMap)
            {
                switch (changeItem.Key.Name)
                {
                    case "Name":
                        surrogate.Name = new EnvelopedObject<string> { Value = (string)changeItem.Value.NewValue };
                        break;
                    case "RegisterTime":
                        surrogate.RegisterTime = new EnvelopedObject<DateTime> { Value = (DateTime)changeItem.Value.NewValue };
                        break;
                    case "LastLoginTime":
                        surrogate.LastLoginTime = new EnvelopedObject<DateTime> { Value = (DateTime)changeItem.Value.NewValue };
                        break;
                    case "LoginCount":
                        surrogate.LoginCount = new EnvelopedObject<int> { Value = (int)changeItem.Value.NewValue };
                        break;
                    case "PlayCount":
                        surrogate.PlayCount = new EnvelopedObject<int> { Value = (int)changeItem.Value.NewValue };
                        break;
                    case "WinCount":
                        surrogate.WinCount = new EnvelopedObject<int> { Value = (int)changeItem.Value.NewValue };
                        break;
                    case "LoseCount":
                        surrogate.LoseCount = new EnvelopedObject<int> { Value = (int)changeItem.Value.NewValue };
                        break;
                    case "DrawCount":
                        surrogate.DrawCount = new EnvelopedObject<int> { Value = (int)changeItem.Value.NewValue };
                        break;
                }
            }
            return surrogate;
        }

        public static implicit operator TrackablePocoTracker<IUserData>(TrackableUserDataTrackerSurrogate surrogate)
        {
            if (surrogate == null)
                return null;

            var tracker = new TrackablePocoTracker<IUserData>();
            if (surrogate.Name != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.Name, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.Name.Value });
            if (surrogate.RegisterTime != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.RegisterTime, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.RegisterTime.Value });
            if (surrogate.LastLoginTime != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.LastLoginTime, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.LastLoginTime.Value });
            if (surrogate.LoginCount != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.LoginCount, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.LoginCount.Value });
            if (surrogate.PlayCount != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.PlayCount, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.PlayCount.Value });
            if (surrogate.WinCount != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.WinCount, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.WinCount.Value });
            if (surrogate.LoseCount != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.LoseCount, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.LoseCount.Value });
            if (surrogate.DrawCount != null)
                tracker.ChangeMap.Add(TrackableUserData.PropertyTable.DrawCount, new TrackablePocoTracker<IUserData>.Change { NewValue = surrogate.DrawCount.Value });
            return tracker;
        }
    }
}

#endregion

#region IUserContext

namespace Domain.Data
{
    [ProtoContract]
    public partial class TrackableUserContext : IUserContext
    {
        [IgnoreDataMember]
        private TrackableUserContextTracker _tracker;

        [IgnoreDataMember]
        public TrackableUserContextTracker Tracker
        {
            get
            {
                return _tracker;
            }
            set
            {
                _tracker = value;
                Data.Tracker = value?.DataTracker;
                Achivements.Tracker = value?.AchivementsTracker;
            }
        }

        public bool Changed { get { return Tracker != null && Tracker.HasChange; } }

        ITracker ITrackable.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableUserContextTracker)value;
                Tracker = t;
            }
        }

        ITracker<IUserContext> ITrackable<IUserContext>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableUserContextTracker)value;
                Tracker = t;
            }
        }

        IContainerTracker<IUserContext> ITrackableContainer<IUserContext>.Tracker
        {
            get
            {
                return Tracker;
            }
            set
            {
                var t = (TrackableUserContextTracker)value;
                Tracker = t;
            }
        }

        public ITrackable GetChildTrackable(object name)
        {
            switch ((string)name)
            {
                case "Data":
                    return Data as ITrackable;
                case "Achivements":
                    return Achivements as ITrackable;
                default:
                    return null;
            }
        }

        public IEnumerable<KeyValuePair<object, ITrackable>> GetChildTrackables(bool changedOnly = false)
        {
            var trackableData = Data as ITrackable;
            if (trackableData != null && (changedOnly == false || trackableData.Changed))
                yield return new KeyValuePair<object, ITrackable>("Data", trackableData);
            var trackableAchivements = Achivements as ITrackable;
            if (trackableAchivements != null && (changedOnly == false || trackableAchivements.Changed))
                yield return new KeyValuePair<object, ITrackable>("Achivements", trackableAchivements);
        }

        private TrackableUserData _Data;

        [ProtoMember(1)] public TrackableUserData Data
        {
            get
            {
                return _Data;
            }
            set
            {
                if (_Data != null)
                    _Data.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.DataTracker;
                _Data = value;
            }
        }

        TrackableUserData IUserContext.Data
        {
            get { return _Data; }
            set { _Data = (TrackableUserData)value; }
        }

        private TrackableDictionary<int, UserAchievement> _Achivements;

        [ProtoMember(2)] public TrackableDictionary<int, UserAchievement> Achivements
        {
            get
            {
                return _Achivements;
            }
            set
            {
                if (_Achivements != null)
                    _Achivements.Tracker = null;
                if (value != null)
                    value.Tracker = Tracker?.AchivementsTracker;
                _Achivements = value;
            }
        }

        TrackableDictionary<int, UserAchievement> IUserContext.Achivements
        {
            get { return _Achivements; }
            set { _Achivements = (TrackableDictionary<int, UserAchievement>)value; }
        }
    }

    [ProtoContract]
    public class TrackableUserContextTracker : IContainerTracker<IUserContext>
    {
        [ProtoMember(1)] public TrackablePocoTracker<IUserData> DataTracker { get; set; } = new TrackablePocoTracker<IUserData>();
        [ProtoMember(2)] public TrackableDictionaryTracker<int, UserAchievement> AchivementsTracker { get; set; } = new TrackableDictionaryTracker<int, UserAchievement>();

        public bool HasChange
        {
            get
            {
                return
                    DataTracker.HasChange ||
                    AchivementsTracker.HasChange ||
                    false;
            }
        }

        public void Clear()
        {
            DataTracker.Clear();
            AchivementsTracker.Clear();
        }

        public void ApplyTo(object trackable)
        {
            ApplyTo((IUserContext)trackable);
        }

        public void ApplyTo(IUserContext trackable)
        {
            DataTracker.ApplyTo(trackable.Data);
            AchivementsTracker.ApplyTo(trackable.Achivements);
        }

        public void ApplyTo(ITracker tracker)
        {
            ApplyTo((TrackableUserContextTracker)tracker);
        }

        public void ApplyTo(ITracker<IUserContext> tracker)
        {
            ApplyTo((TrackableUserContextTracker)tracker);
        }

        public void ApplyTo(TrackableUserContextTracker tracker)
        {
            DataTracker.ApplyTo(tracker.DataTracker);
            AchivementsTracker.ApplyTo(tracker.AchivementsTracker);
        }

        public void RollbackTo(object trackable)
        {
            RollbackTo((IUserContext)trackable);
        }

        public void RollbackTo(IUserContext trackable)
        {
            DataTracker.RollbackTo(trackable.Data);
            AchivementsTracker.RollbackTo(trackable.Achivements);
        }

        public void RollbackTo(ITracker tracker)
        {
            RollbackTo((TrackableUserContextTracker)tracker);
        }

        public void RollbackTo(ITracker<IUserContext> tracker)
        {
            RollbackTo((TrackableUserContextTracker)tracker);
        }

        public void RollbackTo(TrackableUserContextTracker tracker)
        {
            DataTracker.RollbackTo(tracker.DataTracker);
            AchivementsTracker.RollbackTo(tracker.AchivementsTracker);
        }
    }
}

#endregion
